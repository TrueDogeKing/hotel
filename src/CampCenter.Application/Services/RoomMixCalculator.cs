namespace CampCenter.Application.Services;

/// Pure room-mix math: suggesting a mix of room types for a headcount and
/// validating a booker-adjusted mix against availability. Counts are keyed by
/// room capacity (e.g. {4: 40, 2: 1}).
public static class RoomMixCalculator
{
    public static long TotalCapacity(IReadOnlyDictionary<int, int> counts) =>
        counts.Sum(kv => (long)kv.Key * kv.Value);

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
