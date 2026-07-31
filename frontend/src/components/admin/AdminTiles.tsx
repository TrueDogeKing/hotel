import type { ElementType } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import {
  IconBed,
  IconCalendar,
  IconCheckSquare,
  IconClipboard,
  IconGrid,
  IconLock,
  IconSparkles,
  IconUsers,
  IconUtensils,
} from "../icons";

interface Tile {
  /** i18n key under admin.tiles — supplies both the name and the description. */
  key: string;
  to: string;
  Icon: ElementType;
  /** Bento spans, as in the reference dashboard: the sections used daily get the
   *  bigger tiles, so the grid reads as a priority order rather than a list.
   *
   *  Three wide (2 cells each) + six single is 12 cells, which fills the
   *  six-column grid in exactly two rows and the four-column one in three — no
   *  ragged last row at either width. Adding a tile means re-doing that sum; a
   *  ninth tile is what cost the 2×2 the schedule used to have. */
  span?: "wide" | "tall" | "big";
}

const TILES: Tile[] = [
  { key: "schedule", to: "/admin/harmonogram", Icon: IconCalendar, span: "wide" },
  { key: "housekeeping", to: "/admin/sprzatanie", Icon: IconSparkles, span: "wide" },
  { key: "bookings", to: "/admin/rezerwacje", Icon: IconClipboard, span: "wide" },
  { key: "occupancy", to: "/admin/oblozenie", Icon: IconGrid },
  { key: "rooms", to: "/admin/pokoje", Icon: IconBed },
  { key: "tasks", to: "/admin/zadania", Icon: IconCheckSquare },
  { key: "closures", to: "/admin/blokady", Icon: IconLock },
  { key: "mealTimes", to: "/admin/posilki", Icon: IconUtensils },
  { key: "users", to: "/admin/uzytkownicy", Icon: IconUsers },
];

/**
 * The way into every admin section: a grid of tiles on the dashboard rather than a
 * row of links in the header. Each tile carries an icon and a name, and reveals what
 * the section is for on hover or focus — a description there is room for here and
 * never was in a navbar.
 */
export default function AdminTiles() {
  const { t } = useTranslation();

  return (
    <nav className="tile-grid" aria-label={t("admin.tiles.label")}>
      {TILES.map(({ key, to, Icon, span }) => (
        <Link key={key} to={to} className={`tile${span ? ` tile-${span}` : ""}`}>
          <span className="tile-icon">
            <Icon />
          </span>
          <span className="tile-name">{t(`admin.tiles.${key}.name`)}</span>
          {/* Always rendered, revealed on hover/focus: sliding in text that is only
              built on hover would leave a screen reader with nothing to read. */}
          <span className="tile-description">{t(`admin.tiles.${key}.description`)}</span>
        </Link>
      ))}
    </nav>
  );
}
