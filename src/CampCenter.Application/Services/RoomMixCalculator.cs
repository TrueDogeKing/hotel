namespace CampCenter.Application.Services;

/// Pure room-mix math: suggesting a mix of room types for a headcount and
/// validating a booker-adjusted mix against availability. Counts are keyed by
/// room capacity (e.g. {4: 40, 2: 1}).
public static class RoomMixCalculator
{
    public static long TotalCapacity(IReadOnlyDictionary<int, int> counts) =>
        counts.Sum(kv => (long)kv.Key * kv.Value);

    /// A mix split in two: the kadra get their own rooms, the campers the rest.
    /// Combined is what the room assignment actually draws from, so that no room
    /// can be handed to both halves.
    public record SplitMix(
        Dictionary<int, int> SupervisorMix,
        Dictionary<int, int> CamperMix,
        Dictionary<int, int> Combined
    );

    /// Houses the supervisors first — in the smallest rooms that will hold them —
    /// and then the campers in what is left, with the ordinary largest-first fill.
    /// Returns null when the free rooms cannot cover both halves separately.
    ///
    /// With no supervisors this is exactly <see cref="SuggestMix"/>, which is what
    /// keeps every existing booking and the whole public path behaving as before.
    public static SplitMix? SuggestSplitMix(
        int camperCount,
        int supervisorCount,
        IReadOnlyDictionary<int, int> freeByCapacity
    )
    {
        if (supervisorCount <= 0)
        {
            var only = SuggestMix(camperCount, freeByCapacity);
            return only is null ? null : new SplitMix([], only, only);
        }

        // Smallest-first for the kadra is not only the preference, it is also the
        // best the campers can hope for: the tighter the staff fit, the more beds
        // are left behind. So there is no second attempt to make — if the campers
        // don't fit after this, no other separation would have helped, and mixing
        // the two cohorts into one room is never on the table. Refusing lets the
        // admin place people by hand, which is the honest outcome.
        var supervisorMix = SuggestMixSmallestFirst(supervisorCount, freeByCapacity);
        if (supervisorMix is null)
        {
            return null;
        }

        var camperMix =
            camperCount > 0 ? SuggestMix(camperCount, Subtract(freeByCapacity, supervisorMix)) : [];

        return camperMix is null
            ? null
            : new SplitMix(supervisorMix, camperMix, Merge(supervisorMix, camperMix));
    }

    /// The mirror of <see cref="SuggestMix"/>: fill from the smallest room type up,
    /// so a handful of supervisors take a double rather than a dormitory. The same
    /// shrink pass then trims the last room, which is capacity-agnostic and works
    /// unchanged in either direction.
    private static Dictionary<int, int>? SuggestMixSmallestFirst(
        int headcount,
        IReadOnlyDictionary<int, int> freeByCapacity
    )
    {
        var free = freeByCapacity
            .Where(kv => kv.Value > 0)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        if (TotalCapacity(free) < headcount)
        {
            return null;
        }

        var mix = new Dictionary<int, int>();
        var remaining = headcount;
        var lastCapacity = 0;
        while (remaining > 0)
        {
            var capacity = free.Where(kv => kv.Value > 0).Min(kv => kv.Key);
            mix[capacity] = mix.GetValueOrDefault(capacity) + 1;
            free[capacity]--;
            remaining -= capacity;
            lastCapacity = capacity;
        }

        var lastRoomLoad = remaining + lastCapacity; // remaining is ≤ 0 here
        var shrinkCandidates = free.Where(kv => kv.Value > 0 && kv.Key >= lastRoomLoad)
            .Select(kv => kv.Key)
            .ToList();
        if (shrinkCandidates.Count > 0)
        {
            var smallest = shrinkCandidates.Min();
            if (smallest < lastCapacity)
            {
                mix[lastCapacity]--;
                if (mix[lastCapacity] == 0)
                {
                    mix.Remove(lastCapacity);
                }

                mix[smallest] = mix.GetValueOrDefault(smallest) + 1;
            }
        }

        return mix;
    }

    /// Validates a split mix the way <see cref="ValidateMix"/> validates a single
    /// one: the two halves together must be available, and each half must cover its
    /// own cohort without a redundant room.
    ///
    /// Judging redundancy per half is the whole point — a two-bed room for the
    /// kadra looks redundant against the group's combined headcount and is not.
    public static string? ValidateSplitMix(
        int camperCount,
        int supervisorCount,
        IReadOnlyDictionary<int, int> camperCounts,
        IReadOnlyDictionary<int, int> supervisorCounts,
        IReadOnlyDictionary<int, int> freeByCapacity
    )
    {
        // Both halves draw on the same free rooms, so they are checked against
        // availability together before either is judged on its own.
        var union = Merge(camperCounts, supervisorCounts);
        foreach (var (capacity, count) in union.Where(kv => kv.Value > 0))
        {
            if (count > freeByCapacity.GetValueOrDefault(capacity))
            {
                return "mix-unavailable";
            }
        }

        if (supervisorCount > 0)
        {
            var supervisorError = ValidateMix(supervisorCount, supervisorCounts, union);
            if (supervisorError is not null)
            {
                return supervisorError;
            }
        }
        else if (supervisorCounts.Any(kv => kv.Value > 0))
        {
            return "mix-redundant-room";
        }

        return ValidateMix(camperCount, camperCounts, union);
    }

    private static Dictionary<int, int> Merge(
        IReadOnlyDictionary<int, int> a,
        IReadOnlyDictionary<int, int> b
    )
    {
        var merged = a.Where(kv => kv.Value > 0).ToDictionary(kv => kv.Key, kv => kv.Value);
        foreach (var (capacity, count) in b.Where(kv => kv.Value > 0))
        {
            merged[capacity] = merged.GetValueOrDefault(capacity) + count;
        }

        return merged;
    }

    private static Dictionary<int, int> Subtract(
        IReadOnlyDictionary<int, int> from,
        IReadOnlyDictionary<int, int> taken
    )
    {
        var left = from.ToDictionary(kv => kv.Key, kv => kv.Value);
        foreach (var (capacity, count) in taken)
        {
            left[capacity] = left.GetValueOrDefault(capacity) - count;
        }

        return left.Where(kv => kv.Value > 0).ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    /// Greedy largest-first fill, then a shrink pass so the last room is the
    /// smallest free type that still covers its load (301 people with free 4s and
    /// 2s → 75×4 + 1×2, not 76×4). Returns null when the free rooms cannot cover
    /// the headcount.
    public static Dictionary<int, int>? SuggestMix(
        int headcount,
        IReadOnlyDictionary<int, int> freeByCapacity
    )
    {
        var free = freeByCapacity
            .Where(kv => kv.Value > 0)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        if (TotalCapacity(free) < headcount)
        {
            return null;
        }

        var mix = new Dictionary<int, int>();
        var remaining = headcount;
        int lastCapacity = 0;
        while (remaining > 0)
        {
            var capacity = free.Where(kv => kv.Value > 0).Max(kv => kv.Key);
            mix[capacity] = mix.GetValueOrDefault(capacity) + 1;
            free[capacity]--;
            remaining -= capacity;
            lastCapacity = capacity;
        }

        // Shrink pass: the last room may be oversized for its actual load.
        var lastRoomLoad = remaining + lastCapacity; // remaining is ≤ 0 here
        var shrinkCandidates = free.Where(kv => kv.Value > 0 && kv.Key >= lastRoomLoad)
            .Select(kv => kv.Key)
            .ToList();
        if (shrinkCandidates.Count > 0)
        {
            var smallest = shrinkCandidates.Min();
            if (smallest < lastCapacity)
            {
                mix[lastCapacity]--;
                if (mix[lastCapacity] == 0)
                {
                    mix.Remove(lastCapacity);
                }

                mix[smallest] = mix.GetValueOrDefault(smallest) + 1;
            }
        }

        return mix;
    }

    /// Validates a booker-adjusted mix: available counts, full coverage, and no
    /// redundant room (removing any single selected room must break coverage —
    /// nobody reserves the whole building for a handful of people).
    public static string? ValidateMix(
        int headcount,
        IReadOnlyDictionary<int, int> counts,
        IReadOnlyDictionary<int, int> freeByCapacity
    )
    {
        if (counts.Count == 0 || counts.Any(kv => kv.Value < 0) || counts.All(kv => kv.Value == 0))
        {
            return "mix-empty";
        }

        foreach (var (capacity, count) in counts.Where(kv => kv.Value > 0))
        {
            if (count > freeByCapacity.GetValueOrDefault(capacity))
            {
                return "mix-unavailable";
            }
        }

        var total = TotalCapacity(counts);
        if (total < headcount)
        {
            return "mix-too-small";
        }

        foreach (var capacity in counts.Where(kv => kv.Value > 0).Select(kv => kv.Key))
        {
            if (total - capacity >= headcount)
            {
                return "mix-redundant-room";
            }
        }

        return null;
    }

    /// Distributes people across the selected rooms: every room at capacity except
    /// the last, which takes the remainder.
    public static List<(int Capacity, int PeopleCount)> DistributePeople(
        int headcount,
        IReadOnlyDictionary<int, int> counts
    )
    {
        var rooms = counts
            .Where(kv => kv.Value > 0)
            .OrderByDescending(kv => kv.Key)
            .SelectMany(kv => Enumerable.Repeat(kv.Key, kv.Value))
            .ToList();

        var result = new List<(int, int)>();
        var remaining = headcount;
        foreach (var capacity in rooms)
        {
            var take = Math.Min(capacity, remaining);
            result.Add((capacity, take));
            remaining -= take;
        }

        return result;
    }
}
