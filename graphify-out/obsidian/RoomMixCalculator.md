---
source_file: "src/CampCenter.Application/Services/RoomMixCalculator.cs"
type: "code"
community: "Room Mix Calculator Tests"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Mix_Calculator_Tests
---

# RoomMixCalculator

## Context

_Source: `src/CampCenter.Application/Services/RoomMixCalculator.cs` (defined near L6; showing L4–L51 of 126)._

```csharp
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
```

## Connections
- [[.DistributePeople()]] - `method` [EXTRACTED]
- [[.SuggestMix()]] - `method` [EXTRACTED]
- [[.TotalCapacity()]] - `method` [EXTRACTED]
- [[.ValidateMix()]] - `method` [EXTRACTED]
- [[RoomMixCalculator.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Mix_Calculator_Tests