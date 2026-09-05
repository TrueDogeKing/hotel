# CampCenter on Kubernetes

Manifests for running the same four containers the Docker Compose stack runs —
PostgreSQL, the .NET API, the nginx/SPA frontend and (in dev) Mailpit — on a
Kubernetes cluster.

Compose is not going away: `bun run dev` and `bun run prod:up` still work and
are still the shortest path for a single machine. This is the alternative for a
cluster.

## What Kubernetes actually is

Docker Compose is a *runner*: you tell it "start these containers", it starts
them, and if one dies at 3am it stays dead (`restart: unless-stopped` retries
the container, but nothing checks whether the process inside is still doing its
job).

Kubernetes is a *reconciler*. You never tell it to start anything. You hand it a
description of the desired state — "one API pod, healthy according to this HTTP
check, reachable under this name, with these environment variables" — and a
control loop keeps comparing that description to reality and fixing the
difference, forever. Kill a pod and a new one appears. Change the image tag and
it replaces pods one at a time, waiting for each replacement to pass its
readiness check before removing the next.

The vocabulary used in these files:

| Object | What it is |
| --- | --- |
| **Pod** | One or more containers scheduled together, sharing a network namespace. The smallest thing Kubernetes runs. Pods are cattle — they get replaced, never repaired. |
| **Deployment** | "Keep N identical pods of this template alive, and roll them safely when the template changes." Used for the API and the SPA. |
| **StatefulSet** | Like a Deployment, but each pod keeps a stable identity and its own disk. Used for PostgreSQL. |
| **Service** | A stable in-cluster DNS name in front of a changing set of pods. `postgres`, `api` and `frontend` resolve exactly as the compose service names did. |
| **Ingress** | HTTP routing from outside the cluster in — the role `docker/Caddyfile` plays today. |
| **ConfigMap / Secret** | Non-secret and secret key/value data, injected as environment variables. This is where `.env` went. |
| **PersistentVolumeClaim** | "I need 5 GiB of disk that outlives my pod." The named docker volume, generalised. |
| **CronJob** | A scheduled Job. Replaces the poll loop in `scripts/backup-db.sh`. |
| **Namespace** | A folder for all of the above. `campcenter` (prod) and `campcenter-dev` can coexist in one cluster. |

## Why bother, for this project

Honestly: a camp centre booking app on one server does not *need* Kubernetes,
and the compose stack is less machinery to understand. What the extra complexity
buys:

* **Self-healing.** Liveness probes restart a wedged API; readiness probes pull
  a starting pod out of rotation until its EF Core migrations finish, so a
  deploy never serves a half-booted app.
* **Zero-downtime deploys.** A new frontend pod must pass `/health` before the
  old one is removed. Compose's `up -d --build` stops the old container first.
* **Config and secrets separated from the workload.** Editing `secrets.env`
  changes the Secret's content hash, which changes its generated name, which
  changes the pod template, which rolls the pods — no "did I remember to restart
  after editing .env?".
* **Scheduling built in.** The backup CronJob replaces a hand-written daemon
  loop, a healthcheck that watches the loop, and a catch-up branch for "the
  server was off overnight" — Kubernetes already has all three.
* **Portability.** The same manifests run on Docker Desktop, k3s on a VPS, or a
  managed cluster. Moving hosts stops being a rewrite of the deployment.

What it does *not* buy: fewer moving parts, a simpler mental model, or high
availability from a single node. If the box dies, one-node Kubernetes is just as
down as one-node compose.

## Layout

```
k8s/
├── base/                    environment-independent shape of the stack
│   ├── namespace.yaml
│   ├── postgres.yaml        StatefulSet + headless Service + volume claim
│   ├── api.yaml             Deployment + Service + three probes
│   ├── frontend.yaml        Deployment + Service
│   ├── ingress.yaml         /api → api, / → frontend
│   └── kustomization.yaml   + ConfigMap with the non-secret settings
└── overlays/
    ├── dev/                 local images, Mailpit, throwaway secrets
    └── prod/                real SMTP, gitignored secrets, backup CronJob,
                             2 SPA replicas + PDB, NetworkPolicy
```

Kustomize (built into `kubectl`, nothing extra to install) composes the two: an
overlay pulls in `base` and patches it. `kubectl kustomize k8s/overlays/dev`
prints the final YAML without touching a cluster — read that output whenever you
are unsure what a change actually does.

### How compose maps onto this

| docker-compose.prod.yml | Kubernetes |
| --- | --- |
| `services.api` | Deployment `api` + Service `api` |
| `services.postgres` + named volume | StatefulSet `postgres` + `volumeClaimTemplates` |
| `services.caddy` + `Caddyfile` | Ingress `campcenter` (TLS terminates upstream, as today) |
| `services.db-backup` + poll loop | CronJob `db-backup` |
| `environment:` block | ConfigMap `campcenter-config` |
| `.env` secrets | Secret `campcenter-secrets` (from `secrets.env`) |
| `healthcheck:` | `readinessProbe` + `livenessProbe` + `startupProbe`, over two different endpoints — see "Health endpoints" below |
| `depends_on: service_healthy` | for *traffic*, readiness probes — a Service has no endpoints until a pod is ready. For *startup ordering*, the `wait-for-postgres` init container in `base/api.yaml`: the API migrates the database before Kestrel listens, and a headless Service with no ready endpoints answers DNS with NXDOMAIN, so without it the first start reliably crashes. |

### Health endpoints

The API exposes three, and the probes deliberately do not all use the same one:

| Endpoint | Checks | Used by |
| --- | --- | --- |
| `/health` | all of them, database included | the Docker `HEALTHCHECK` and compose's `depends_on: service_healthy` |
| `/health/live` | none — "the process is up and serving" | Kubernetes **liveness** |
| `/health/ready` | the `ready`-tagged checks, i.e. `AddDbContextCheck<AppDbContext>()` | Kubernetes **readiness** and **startup** |

The split matters because failing the two probes has very different
consequences. Failing readiness only takes the pod out of the Service's
endpoints; failing liveness *restarts the container*. If liveness also checked
the database, a brief database outage would restart every API pod at once —
turning a recoverable blip into a thundering herd of cold starts, all of them
re-running migrations against the database that was already struggling. So
liveness asks the narrow question and readiness asks the useful one.

All three have rate limiting disabled (`DisableRateLimiting()`): the global
limiter partitions by client IP, and a probe that got a `429` would count as a
failed probe.

## Getting a cluster

`kubectl` is already here (Docker Desktop ships it). It needs a cluster to point
at. On this machine the cheapest option is Docker Desktop's built-in one:

**Docker Desktop → Settings → Kubernetes → Enable Kubernetes → Apply & restart.**
The first start downloads the control-plane images and takes a few minutes.

```bash
kubectl config use-context docker-desktop
kubectl get nodes
```

Expect one node with `STATUS Ready`. Alternatives that work the same way:
`kind create cluster`, `minikube start`, or k3s on a VPS.

For the Ingress to do anything, the cluster needs an ingress controller — Docker
Desktop does not ship one:

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.15.1/deploy/static/provider/cloud/deploy.yaml
```

```bash
kubectl wait -n ingress-nginx --for=condition=available deploy/ingress-nginx-controller --timeout=240s
```

Pick a controller release that supports your cluster's version — v1.15.1 is what
was verified here against Kubernetes 1.34. On Docker Desktop the controller's
LoadBalancer Service takes ports 80/443 on the host and reports
`EXTERNAL-IP: localhost`.

Skipping the controller is fine — every check below except the Ingress one works
through `kubectl port-forward` instead.

## Deploy (dev)

```bash
bun run k8s:images
```

```bash
bun run k8s:up
```

The overlay tags the images `campcenter/api:dev` and `campcenter/frontend:dev`
with `imagePullPolicy: IfNotPresent`, so nothing is ever pulled from a registry —
the cluster is expected to find them in its own image store. On Docker Desktop
with the containerd image store enabled (Settings → General), a local
`docker build` lands where the cluster can see it. If the pods come up
`ImagePullBackOff` anyway, the two stores are separate on your setup; import the
image explicitly:

```bash
docker image save campcenter/api:dev | docker exec -i desktop-control-plane ctr -n k8s.io images import -
```

On kind that is `kind load docker-image campcenter/api:dev`; on any real cluster,
push to a registry and set `newName` in the overlay's `images:` block.

Tear down (this deletes the namespace, and with it the database volume):

```bash
bun run k8s:down
```

## How to check it works

Work down this list; each step tells you something the previous one could not.

### 1. The manifests are valid — no cluster needed

```bash
bun run k8s:render
```

```bash
kubectl apply -k k8s/overlays/dev --dry-run=client
```

Catches typos, bad indentation and unresolved references before anything runs.

### 2. Everything scheduled and became ready

```bash
kubectl get pods -n campcenter-dev -w
```

Expect, within a minute or two:

```
NAME                        READY   STATUS    RESTARTS   AGE
api-6c9f7d5b8c-2xk4n        1/1     Running   0          70s
frontend-7d4b9c6f5d-lm8qp   1/1     Running   0          70s
mailpit-5b8d7c9f4-vr2ts     1/1     Running   0          70s
postgres-0                  1/1     Running   0          70s
```

`READY 1/1` is the column that matters — it means the readiness probe passes,
not merely that the process started. A climbing `RESTARTS` means the liveness
probe is failing; see Troubleshooting.

```bash
kubectl rollout status deploy/api -n campcenter-dev
```

```bash
bun run k8s:status
```

### 3. The API is alive and migrated the database

```bash
bun run k8s:logs
```

Expect the EF Core migration lines, then `Now listening on: http://[::]:8080`.

```bash
kubectl exec -n campcenter-dev postgres-0 -- psql -U campcenter -d campcenter -c "\dt"
```

Expect the CampCenter tables — `Bookings`, `Rooms`, `Closures`,
`BookingRoomAssignments`, … If they are there, the API found the Service
`postgres`, authenticated with the Secret's credentials, ran its migrations and
seeded the admin account.

### 4. The app answers over HTTP

`/health` is deliberately not routed through the Ingress, so reach it directly:

```bash
kubectl port-forward -n campcenter-dev svc/api 5080:8080
```

```bash
curl -w " %{http_code}\n" http://localhost:5080/health/live
```

```bash
curl -w " %{http_code}\n" http://localhost:5080/health/ready
```

Both `Healthy 200`. To prove `/health/ready` is not lying, scale the database to
zero and watch only readiness go red while the container keeps running:

```bash
kubectl scale statefulset/postgres -n campcenter-dev --replicas=0
```

```bash
kubectl get pods -n campcenter-dev -l app.kubernetes.io/name=api
```

The API goes `READY 0/1` with `RESTARTS 0` — out of rotation, not restarted,
which is exactly the intended split. `kubectl scale … --replicas=1` brings it
back.

The whole app, through the SPA container (whose nginx proxies `/api` itself):

```bash
bun run k8s:forward
```

Then open <http://localhost:8080> and log in at
<http://localhost:8080/admin/logowanie> with `admin` / `Admin123!`. A successful
login exercises the full chain: browser → nginx → Service `api` → API → Service
`postgres` → database.

### 5. The Ingress routes (needs the controller installed above)

```bash
curl -H "Host: campcenter.localhost" http://127.0.0.1/
```

```bash
curl -H "Host: campcenter.localhost" -X POST http://127.0.0.1/api/auth/login \
  -H "Content-Type: application/json" -d '{"login":"admin","password":"Admin123!"}'
```

The first returns the SPA's `index.html`, the second a JWT — proving the Ingress
splits the two paths to two different Services.

Use `127.0.0.1`, not `localhost`: Docker Desktop's port binding
(`com.docker.backend.exe`) listens on `0.0.0.0` only, while `localhost` resolves
to `::1` first here, so `curl http://localhost/` fails to connect even though
`netstat` shows something on port 80. Same IPv4/IPv6 trap the frontend
Dockerfile's healthcheck comment calls out. In a browser,
<http://campcenter.localhost> works — browsers resolve `*.localhost` to
127.0.0.1 with no hosts-file entry.

### 6. E-mail is captured

```bash
kubectl port-forward -n campcenter-dev svc/mailpit 8025:8025
```

Make a booking in the SPA, then look for the confirmation mail at
<http://localhost:8025>.

### 7. Self-healing — the part compose does not do

```bash
kubectl delete pod -n campcenter-dev -l app.kubernetes.io/name=api
```

```bash
kubectl get pods -n campcenter-dev -w
```

The Deployment notices it is one pod short of the desired state and creates a
replacement within seconds. Nothing you ran told it to; that is the whole point
of a reconciler.

### 8. Data survives the pod

```bash
kubectl exec -n campcenter-dev postgres-0 -- psql -U campcenter -d campcenter -c "select count(*) from \"Rooms\";"
```

```bash
kubectl delete pod -n campcenter-dev postgres-0
```

```bash
kubectl wait -n campcenter-dev --for=condition=ready pod/postgres-0 --timeout=180s
```

Re-run the count — same number. The PersistentVolumeClaim outlived the pod:

```bash
kubectl get pvc -n campcenter-dev
```

`data-postgres-0` should be `Bound`.

### 9. A rolling update keeps serving

```bash
kubectl rollout restart deploy/frontend -n campcenter-dev
```

```bash
kubectl rollout status deploy/frontend -n campcenter-dev
```

With the port-forward from step 4 open and the page reloading, requests keep
succeeding: the replacement pod has to pass `/health` before the old one is
removed. `kubectl rollout undo deploy/frontend -n campcenter-dev` goes back.

Note that `kubectl port-forward svc/…` picks **one** pod and tunnels to it, so
the forward itself dies the moment that pod is replaced — connection-refused
after a rollout finishes means the tunnel is gone, not the app. Restart the
port-forward and try again. Only the Ingress path (step 5) load-balances the way
real traffic would.

### 10. The backup job (prod overlay)

```bash
kubectl create job --from=cronjob/db-backup manual-backup -n campcenter
```

```bash
kubectl logs -n campcenter job/manual-backup
```

Expect a line naming the dump it wrote under `/backups`.

## Troubleshooting

| Symptom | Where to look |
| --- | --- |
| `ImagePullBackOff` | The image is not in the cluster's image store. Run `bun run k8s:images`, then see the import command under "Deploy (dev)" — the Docker daemon's store and the cluster's are not always the same one. |
| `CrashLoopBackOff` | `kubectl logs -n campcenter-dev <pod> --previous` — the logs of the run that died. |
| `Pending` forever | `kubectl describe pod -n campcenter-dev <pod>` — usually no node has the requested CPU/memory, or the PVC found no storage class to bind to. |
| `READY 0/1`, no restarts | The readiness probe is failing. `kubectl describe pod` lists the probe failures; the container runs but gets no traffic. |
| 503 from the Ingress | The Service has no ready endpoints: `kubectl get endpoints -n campcenter-dev api`. |
| A config change had no effect | Kustomize hashes ConfigMap/Secret names, so pods roll only when the content actually changes. `kubectl describe pod` shows which generation a pod is on. |
| `kubectl` reaches nothing | `kubectl config current-context` — no cluster is selected. |

`kubectl describe pod` and
`kubectl get events -n campcenter-dev --sort-by=.lastTimestamp` answer most
"why isn't this running" questions.

## Production notes

Before `bun run k8s:prod:up`:

1. `cp k8s/overlays/prod/secrets.env.example k8s/overlays/prod/secrets.env` and
   fill it in. That file is gitignored. Kubernetes Secrets are only
   base64-encoded in etcd — on a shared cluster use Sealed Secrets, SOPS or an
   external secret store rather than a file on disk.
2. Replace `osrodek.example.com` in `overlays/prod/ingress-patch.yaml` **and** in
   the `Cors__AllowedOrigins__0` / `Booking__PublicBaseUrl` literals in
   `overlays/prod/kustomization.yaml`.
3. Set the real SMTP host in the same `configMapGenerator`.
4. On a multi-node cluster, add `newName: <registry>/campcenter-api` to the
   `images:` block and push the images there.

**TLS.** As with `docker/Caddyfile`, nothing here terminates TLS: the site sits
behind a Cloudflare Tunnel that already does. On a host with a real public IP,
install cert-manager, add a `ClusterIssuer`, and give the Ingress a `tls:` block
plus the `cert-manager.io/cluster-issuer` annotation.

**Scaling.** `frontend` scales freely. `api` is pinned to one replica because it
applies EF Core migrations on startup (`Database__MigrateAutomatically`), and
several replicas would mean several migrators racing on a fresh deploy. To scale
it, move the migration out of the app's startup path into a Job that runs before
the rollout (a Helm `pre-upgrade` hook, an Argo CD `PreSync` hook, or a plain Job
applied by the deploy script) and set `Database__MigrateAutomatically=false`.

**Backups.** The CronJob dumps to a PersistentVolumeClaim that lives in the same
cluster as the database it protects — the same objection the compose setup
answers with `BACKUP_DIR` on another disk. Point it at off-cluster storage (an
NFS/SMB volume, or add a step that uploads to object storage) before relying on
it.
