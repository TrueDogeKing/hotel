using System.Text.RegularExpressions;

namespace CampCenter.Domain.Entities;

/// Natural-order comparer for Room.Number ("1" < "2" < "11" < "A-3"), so purely
/// numeric room numbers sort as numbers instead of lexicographically.
public static partial class RoomNumberComparer
{
    public static readonly IComparer<string?> Instance = Comparer<string?>.Create(Compare);

    public static int Compare(string? a, string? b)
    {
        var partsA = SplitRegex().Matches(a ?? string.Empty);
        var partsB = SplitRegex().Matches(b ?? string.Empty);

        for (var i = 0; i < Math.Min(partsA.Count, partsB.Count); i++)
        {
            var partA = partsA[i].Value;
            var partB = partsB[i].Value;

            if (
                char.IsDigit(partA[0])
                && char.IsDigit(partB[0])
                && long.TryParse(partA, out var numA)
                && long.TryParse(partB, out var numB)
            )
            {
                var numCompare = numA.CompareTo(numB);
                if (numCompare != 0)
                    return numCompare;
            }
            else
            {
                var strCompare = string.CompareOrdinal(partA, partB);
                if (strCompare != 0)
                    return strCompare;
            }
        }

        return partsA.Count.CompareTo(partsB.Count);
    }

    [GeneratedRegex(@"\d+|\D+")]
    private static partial Regex SplitRegex();
}
