import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "./LanguageSwitcher";
import ThemeToggle from "./ThemeToggle";
import { IconSunSea } from "./icons";

interface Props {
  /**
   * On the landing page the section links are same-page anchors and the "book"
   * button is the point of the header. On every other public page they have to
   * carry you back to the landing page, and the button would offer what you are
   * already doing — so it is dropped.
   */
  variant?: "home" | "sub";
}

const SECTIONS = ["offer", "how", "contact"] as const;

/**
 * The public site's masthead: brand, section links, theme and language, and the
 * booking call to action.
 *
 * Shared rather than copied because the booking flow used to carry a different
 * header entirely — another brand mark, no theme toggle — which made the step
 * from the landing page into the booking feel like a step into another site.
 */
export default function PublicHeader({ variant = "home" }: Props) {
  const { t } = useTranslation();
  // Anchors on the landing page itself; a real navigation away from it
  // elsewhere, which is what makes the browser scroll to the section on arrival.
  const href = (id: string) => (variant === "home" ? `#${id}` : `/#${id}`);

  return (
    <header className="home-header">
      {variant === "home" ? (
        <a className="home-brand" href="#top">
          <IconSunSea />
          {t("common.appName")}
        </a>
      ) : (
        <Link className="home-brand" to="/">
          <IconSunSea />
          {t("common.appName")}
        </Link>
      )}

      <nav className="home-nav">
        {SECTIONS.map((id) => (
          <a key={id} href={href(id)}>
            {t(`home.nav${id.charAt(0).toUpperCase()}${id.slice(1)}`)}
          </a>
        ))}
      </nav>

      <div className="home-header-actions">
        <ThemeToggle />
        <LanguageSwitcher />
        {variant === "home" && (
          <Link className="home-book-btn" to="/rezerwacja">
            {t("home.bookNow")}
          </Link>
        )}
      </div>
    </header>
  );
}
