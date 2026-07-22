---
source_file: "frontend/src/pages/admin/SessionsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# SessionsPage.tsx

## Context

_Source: `frontend/src/pages/admin/SessionsPage.tsx` (defined near L1; showing L1–L46 of 235)._

```tsx
import { useEffect, useState, type FormEvent } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import {
  archiveSession,
  createSession,
  deleteSession,
  formatZl,
  getSessions,
  groszeToZl,
  publishSession,
  updateSession,
  zlToGrosze,
  type CampSession,
} from "../../api/admin";

interface SessionFormState {
  name: string;
  startDate: string;
  endDate: string;
  priceZl: string;
  depositZl: string;
}

const emptyForm: SessionFormState = {
  name: "",
  startDate: "",
  endDate: "",
  priceZl: "",
  depositZl: "",
};

export default function SessionsPage() {
  const { t, i18n } = useTranslation();
  const [sessions, setSessions] = useState<CampSession[]>([]);
  const [form, setForm] = useState<SessionFormState>(emptyForm);
  const [editing, setEditing] = useState<CampSession | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function reload() {
    setSessions(await getSessions());
  }

  useEffect(() => {
```

## Connections
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[CampSession]] - `imports` [EXTRACTED]
- [[SessionFormState]] - `contains` [EXTRACTED]
- [[SessionsPage()]] - `contains` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[archiveSession()]] - `imports` [EXTRACTED]
- [[createSession()]] - `imports` [EXTRACTED]
- [[deleteSession()]] - `imports` [EXTRACTED]
- [[emptyForm_1]] - `contains` [EXTRACTED]
- [[formatZl()]] - `imports` [EXTRACTED]
- [[getSessions()]] - `imports` [EXTRACTED]
- [[groszeToZl()]] - `imports` [EXTRACTED]
- [[publishSession()]] - `imports` [EXTRACTED]
- [[updateSession()]] - `imports` [EXTRACTED]
- [[zlToGrosze()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages