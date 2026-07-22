---
source_file: "frontend/src/pages/admin/AdminBookingsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# AdminBookingsPage.tsx

## Context

_Source: `frontend/src/pages/admin/AdminBookingsPage.tsx` (defined near L1; showing L1–L46 of 196)._

```tsx
import { useCallback, useEffect, useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import ConfirmDialog from "../../components/ConfirmDialog";
import {
  cancelAdminBooking,
  formatZl,
  getAdminBookings,
  getSessions,
  type AdminBooking,
  type CampSession,
} from "../../api/admin";

export default function AdminBookingsPage() {
  const { t, i18n } = useTranslation();
  const [bookings, setBookings] = useState<AdminBooking[]>([]);
  const [sessions, setSessions] = useState<CampSession[]>([]);
  const [sessionFilter, setSessionFilter] = useState("");
  const [statusFilter, setStatusFilter] = useState("");
  const [expanded, setExpanded] = useState<string | null>(null);
  const [cancelTarget, setCancelTarget] = useState<AdminBooking | null>(null);
  const [error, setError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    setBookings(
      await getAdminBookings({
        sessionId: sessionFilter || undefined,
        status: statusFilter || undefined,
      }),
    );
  }, [sessionFilter, statusFilter]);

  useEffect(() => {
    let cancelled = false;
    void getSessions().then((data) => {
      if (!cancelled) setSessions(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  useEffect(() => {
    let cancelled = false;
    void getAdminBookings({
      sessionId: sessionFilter || undefined,
```

## Connections
- [[AdminBooking]] - `imports` [EXTRACTED]
- [[AdminBookingsPage()]] - `contains` [EXTRACTED]
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[CampSession]] - `imports` [EXTRACTED]
- [[ConfirmDialog()]] - `imports` [EXTRACTED]
- [[ConfirmDialog.tsx]] - `imports_from` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[cancelAdminBooking()]] - `imports` [EXTRACTED]
- [[formatZl()]] - `imports` [EXTRACTED]
- [[getAdminBookings()]] - `imports` [EXTRACTED]
- [[getSessions()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages