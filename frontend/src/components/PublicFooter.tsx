import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

/**
 * The public site's footer. Shared for the same reason as the header: the
 * booking pages simply ended, with no way back to the site and no sign they were
 * still part of it.
 */
export default function PublicFooter() {
  const { t } = useTranslation();

  return (
    <footer className="home-footer">
      <span className="home-footer-brand">{t("common.appName")}</span>
      <Link to="/admin">{t("home.adminLink")}</Link>
      <span className="home-footer-rights">{t("home.rights")}</span>
    </footer>
  );
}
