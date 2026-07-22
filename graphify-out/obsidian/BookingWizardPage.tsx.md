---
source_file: "frontend/src/pages/BookingWizardPage.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# BookingWizardPage.tsx

## Context

_Source: `frontend/src/pages/BookingWizardPage.tsx` (defined near L1; showing L1–L46 of 343)._

```tsx
import { useMemo, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import LanguageSwitcher from "../components/LanguageSwitcher";
import { formatZl } from "../api/admin";
import { createBooking, getPublicSessions, validateMix, type PublicSession } from "../api/public";

type Step = "headcount" | "session" | "rooms" | "contact" | "summary";

interface ContactForm {
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  notes: string;
}

const emptyContact: ContactForm = {
  organizationName: "",
  contactName: "",
  email: "",
  phone: "",
  notes: "",
};

export default function BookingWizardPage() {
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const [step, setStep] = useState<Step>("headcount");
  const [headcountInput, setHeadcountInput] = useState("");
  const [headcount, setHeadcount] = useState(0);
  const [sessions, setSessions] = useState<PublicSession[]>([]);
  const [session, setSession] = useState<PublicSession | null>(null);
  const [counts, setCounts] = useState<Record<string, number>>({});
  const [contact, setContact] = useState<ContactForm>(emptyContact);
  const [error, setError] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const dateFormatter = useMemo(
    () =>
      new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
        dateStyle: "medium",
      }),
    [i18n.language],
```

## Connections
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[BookingWizardPage()]] - `contains` [EXTRACTED]
- [[ContactForm]] - `contains` [EXTRACTED]
- [[LanguageSwitcher()]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[PublicSession]] - `imports` [EXTRACTED]
- [[Step]] - `contains` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[createBooking()]] - `imports` [EXTRACTED]
- [[emptyContact]] - `contains` [EXTRACTED]
- [[formatZl()]] - `imports` [EXTRACTED]
- [[getPublicSessions()]] - `imports` [EXTRACTED]
- [[public.ts]] - `imports_from` [EXTRACTED]
- [[validateMix()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend