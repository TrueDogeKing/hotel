---
source_file: "frontend/src/pages/LoginPage.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# LoginPage()

## Context

_Source: `frontend/src/pages/LoginPage.tsx` (defined near L13; showing L11–L58 of 128)._

```tsx
const DEFAULT_RETRY_SECONDS = 30;

export default function LoginPage() {
  const { t } = useTranslation();
  const { login: signIn } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const from =
    (location.state as { from?: { pathname?: string } } | null)?.from?.pathname ?? "/admin";
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const [attemptsLeft, setAttemptsLeft] = useState(MAX_ATTEMPTS);
  // Timestamp (ms) until which login is blocked after hitting the rate limit (null = not blocked).
  const [lockedUntil, setLockedUntil] = useState<number | null>(null);
  const [now, setNow] = useState(() => Date.now());

  // While blocked, tick `now` to re-render the countdown; the interval clears the lock once it passes.
  useEffect(() => {
    if (lockedUntil === null) return;
    const timer = setInterval(() => {
      if (Date.now() >= lockedUntil) {
        setLockedUntil(null);
        setAttemptsLeft(MAX_ATTEMPTS);
        setError(null);
      } else {
        setNow(Date.now());
      }
    }, 500);
    return () => clearInterval(timer);
  }, [lockedUntil]);

  const remainingSeconds = lockedUntil ? Math.max(0, Math.ceil((lockedUntil - now) / 1000)) : 0;
  const blocked = lockedUntil !== null && remainingSeconds > 0;

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    if (blocked) return;
    setError(null);
    setSubmitting(true);
    try {
      await signIn({ login, password });
      setAttemptsLeft(MAX_ATTEMPTS);
      navigate(from, { replace: true });
    } catch (err) {
      if (isAxiosError(err) && err.response?.status === 429) {
        // Rate limit exceeded: start the retry countdown from the Retry-After header.
```

## Connections
- [[LoginPage.tsx]] - `contains` [EXTRACTED]
- [[useAuth()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n