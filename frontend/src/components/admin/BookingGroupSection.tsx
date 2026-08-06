import { useCallback, useEffect, useRef, useState } from "react";
import { useTranslation } from "react-i18next";
import {
  bookingStatuses,
  getBookingGroupPage,
  type BookingGroupCategory,
  type BookingStatus,
  type DashboardBooking,
} from "../../api/admin";
import { formatDate as formatIsoDate } from "../../utils/dates";
import { useAuth } from "../../auth/AuthContext";

/** Rows per request. Enough to fill the fold on a laptop without a second round
 *  trip, small enough that opening a fold is cheap on a long history. */
const PAGE_SIZE = 20;

interface Props {
  category: BookingGroupCategory;
  /** Open on first render — the current groups, which are what the page is for. */
  defaultOpen?: boolean;
  selectedBookingId: string | null;
  onSelect: (bookingId: string | null) => void;
  onStatusChange: (bookingId: string, status: BookingStatus) => Promise<void>;
  /** Bumped by the parent after a mutation, to re-fetch what is already loaded. */
  refreshToken: number;
}

/**
 * One foldable list of groups, loaded a page at a time.
 *
 * Nothing is fetched until the fold is opened, and further pages arrive only as
 * the end of the list is scrolled into view. That matters most for the inactive
 * list, which grows with every stay the centre has ever hosted and would
 * otherwise be loaded in full on every visit to the dashboard.
 */
export default function BookingGroupSection({
  category,
  defaultOpen = false,
  selectedBookingId,
  onSelect,
  onStatusChange,
  refreshToken,
}: Props) {
  const { t, i18n } = useTranslation();
  const { canEdit } = useAuth();
  const [open, setOpen] = useState(defaultOpen);
  const [rows, setRows] = useState<DashboardBooking[]>([]);
  const [total, setTotal] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const sentinelRef = useRef<HTMLDivElement>(null);
  // Guards against a second request for the same page: the sentinel can come into
  // view again while the first one is still in flight.
  const loadingRef = useRef(false);

  const loadPage = useCallback(
    async (skip: number) => {
      if (loadingRef.current) return;
      loadingRef.current = true;
      setLoading(true);
      try {
        const page = await getBookingGroupPage(category, skip, PAGE_SIZE);
        // Replace on the first page, append after it — so a refresh cannot
        // duplicate rows that are already on screen.
        setRows((current) => (skip === 0 ? page.items : [...current, ...page.items]));
        setTotal(page.total);
        setError(null);
      } catch {
        setError(t("dashboard.groups.loadError"));
      } finally {
        loadingRef.current = false;
        setLoading(false);
      }
    },
    [category, t],
  );

  /** The refresh token whose data this fold currently holds. A mutation elsewhere
   *  on the page bumps the token; the fold notices the next time it is open and
   *  re-reads its first page. A closed fold does nothing until it is opened —
   *  which is the whole point of loading these on demand. */
  const loadedToken = useRef(refreshToken);

  // The first page lands when the fold is opened, not before, and again after a
  // mutation has invalidated what it holds. loadPage(0) replaces the rows
  // outright, so a group that a status change moved to another category cannot
  // linger here.
  useEffect(() => {
    if (!open) return;
    const stale = loadedToken.current !== refreshToken;
    if (!stale && (rows.length > 0 || total !== null)) return;
    loadedToken.current = refreshToken;
    void loadPage(0);
  }, [open, refreshToken, rows.length, total, loadPage]);

  const hasMore = total !== null && rows.length < total;

  // Pages arrive as the end of the list is reached. An observer rather than a
  // scroll handler: it fires once per crossing instead of on every frame.
  useEffect(() => {
    const sentinel = sentinelRef.current;
    if (!open || !hasMore || !sentinel) return;

    const observer = new IntersectionObserver(
      (entries) => {
        if (entries.some((entry) => entry.isIntersecting)) void loadPage(rows.length);
      },
      // Start the fetch just before the sentinel is actually visible, so the rows
      // are usually there by the time the reader gets to them.
      { rootMargin: "200px" },
    );
    observer.observe(sentinel);
    return () => observer.disconnect();
  }, [open, hasMore, rows.length, loadPage]);

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);
  const heading = t(`dashboard.groups.${category.toLowerCase()}`);

  return (
    <section className={`group-section${open ? " open" : ""}`}>
      <h2>
        <button
          type="button"
          className="group-section-toggle"
          aria-expanded={open}
          onClick={() => setOpen((current) => !current)}
        >
          <span className="group-section-chevron" aria-hidden="true">
            {open ? "▾" : "▸"}
          </span>
          {heading}
          {/* The count comes from the server with the first page, so a fold that
              has never been opened shows no number rather than a wrong one. */}
          {total !== null && <span className="group-section-count">{total}</span>}
        </button>
      </h2>

      {open && (
        <div className="group-section-body">
          {error && <p role="alert">{error}</p>}

          {rows.length === 0 && !loading && !error && (
            <p className="group-section-empty">{t("dashboard.groups.empty")}</p>
          )}

          {rows.length > 0 && (
            <table className="admin-table">
              <tbody>
                {rows.map((booking) => (
                  <tr
                    key={booking.id}
                    className={booking.id === selectedBookingId ? "selected-row" : ""}
                    onClick={() => onSelect(selectedBookingId === booking.id ? null : booking.id)}
                  >
                    <td>{booking.organizationName}</td>
                    <td>
                      {formatDate(booking.startDate)} – {formatDate(booking.endDate)}
                    </td>
                    <td>{t("dashboard.beds", { count: booking.occupiedBeds })}</td>
                    <td>
                      {/* Stop propagation so picking a status doesn't also toggle
                          the row's programme panel. A worker reads the status
                          instead of choosing it. */}
                      {canEdit ? (
                        <select
                          value={booking.status}
                          aria-label={t("dashboard.status")}
                          onClick={(e) => e.stopPropagation()}
                          onChange={(e) => {
                            e.stopPropagation();
                            void onStatusChange(booking.id, e.target.value as BookingStatus);
                          }}
                        >
                          {bookingStatuses.map((status) => (
                            <option key={status} value={status}>
                              {t(`adminBookings.statuses.${status}`)}
                            </option>
                          ))}
                        </select>
                      ) : (
                        t(`adminBookings.statuses.${booking.status}`)
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}

          {loading && <p className="group-section-loading">{t("common.loading")}</p>}

          {/* Watched, not clicked — but it is a button as well, so the list can be
              extended without a scroll (keyboard, or a short fold that never
              crosses the observer). */}
          {hasMore && (
            <div ref={sentinelRef} className="group-section-more">
              <button type="button" disabled={loading} onClick={() => void loadPage(rows.length)}>
                {t("dashboard.groups.loadMore", {
                  shown: rows.length,
                  total: total ?? rows.length,
                })}
              </button>
            </div>
          )}
        </div>
      )}
    </section>
  );
}
