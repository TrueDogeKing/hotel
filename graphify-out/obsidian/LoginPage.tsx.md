---
source_file: "frontend/src/pages/LoginPage.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# LoginPage.tsx

## Context

_Source: `frontend/src/pages/LoginPage.tsx` (defined near L1; showing L1–L46 of 128)._

```tsx
import { useEffect, useState, type FormEvent } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { isAxiosError } from "axios";
import { useTranslation } from "react-i18next";
import { useAuth } from "../auth/AuthContext";
import LanguageSwitcher from "../components/LanguageSwitcher";

// Mirrors the server policy (RateLimiting:Auth:PermitLimit). Used only to warn the user
// before the lockout; the server's 429 + Retry-After response is the authoritative source.
const MAX_ATTEMPTS = 5;
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

```

## Connections
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[LanguageSwitcher()]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[LoginPage()]] - `contains` [EXTRACTED]
- [[useAuth()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n