using System.Security.Cryptography;
using System.Text;
using CampCenter.Application.Interfaces;
using CampCenter.Infrastructure.Payments;

namespace CampCenter.UnitTests.Services;

public class P24SignCalculatorTests
{
    private static string Sha384(string s) =>
        Convert.ToHexStringLower(SHA384.HashData(Encoding.UTF8.GetBytes(s)));

    [Fact]
    public void RegisterSign_MatchesDocumentedJsonShape()
    {
        // The P24 contract hashes the exact compact JSON with this field order.
        var expected = Sha384(
            "{\"sessionId\":\"s-1\",\"merchantId\":12345,\"amount\":300000,\"currency\":\"PLN\",\"crc\":\"secretcrc\"}"
        );

        Assert.Equal(
            expected,
            P24SignCalculator.RegisterSign("s-1", 12345, 300000, "PLN", "secretcrc")
        );
    }

    [Fact]
    public void VerifySign_MatchesDocumentedJsonShape()
    {
        var expected = Sha384(
            "{\"sessionId\":\"s-1\",\"orderId\":777,\"amount\":300000,\"currency\":\"PLN\",\"crc\":\"secretcrc\"}"
        );

        Assert.Equal(
            expected,
            P24SignCalculator.VerifySign("s-1", 777, 300000, "PLN", "secretcrc")
        );
    }

    [Fact]
    public void NotificationSign_RoundTrips()
    {
        var notification = new GatewayNotification(
            12345,
            67890,
            "s-1",
            300000,
            300000,
            "PLN",
            777,
            154,
            "statement",
            Sign: ""
        );

        var sign = P24SignCalculator.NotificationSign(notification, "secretcrc");

        var expected = Sha384(
            "{\"merchantId\":12345,\"posId\":67890,\"sessionId\":\"s-1\",\"amount\":300000,"
                + "\"originAmount\":300000,\"currency\":\"PLN\",\"orderId\":777,\"methodId\":154,"
                + "\"statement\":\"statement\",\"crc\":\"secretcrc\"}"
        );
        Assert.Equal(expected, sign);
    }
}
