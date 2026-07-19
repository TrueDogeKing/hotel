# Multi-stage build for the frontend (Vite/React) served by nginx, using Bun.
FROM oven/bun:1.3 AS build
WORKDIR /app

COPY package.json bun.lock ./
RUN bun install --frozen-lockfile

COPY . .
RUN bun run build

FROM nginx:alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 8080

# busybox wget (bundled in nginx:alpine) hits the /health location served by nginx.conf.
# Use 127.0.0.1 (not localhost): nginx binds IPv4 only, while busybox resolves localhost to ::1.
HEALTHCHECK --interval=5s --timeout=3s --start-period=10s --retries=5 \
    CMD wget -q -O /dev/null http://127.0.0.1:8080/health || exit 1

CMD ["nginx", "-g", "daemon off;"]
