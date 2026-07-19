import { useTranslation } from "react-i18next";
import { useAuth } from "../../auth/AuthContext";
import AdminLayout from "../../components/admin/AdminLayout";

// Dashboard aggregates (occupancy, pending deposits, tasks) arrive in phase 4.
export default function AdminDashboardPage() {
  const { t } = useTranslation();
  const { userLogin } = useAuth();

  return (
    <AdminLayout>
      <p>{t("admin.welcome", { login: userLogin ?? "" })}</p>
    </AdminLayout>
  );
}
