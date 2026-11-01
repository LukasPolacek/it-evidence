using ItEvidence.Domain.Assets;
using static ItEvidence.Domain.Tests.TestData;

namespace ItEvidence.Domain.Tests;

public class WarrantyTests
{
    [Theory]
    [InlineData(0, true)]    // konci dnes
    [InlineData(30, true)]   // konci posledni den lhuty
    [InlineData(31, false)]  // az za 31 dni
    [InlineData(-1, false)]  // uz skoncila
    public void Warranty_expiring_soon_within_30_days(int daysUntilEnd, bool expected)
    {
        var asset = NewLaptop(warrantyUntil: Today.AddDays(daysUntilEnd));

        Assert.Equal(expected, asset.IsWarrantyExpiringSoon(Today));
    }

    [Fact]
    public void Number_of_days_can_be_changed()
    {
        var asset = NewLaptop(warrantyUntil: Today.AddDays(60));

        Assert.False(asset.IsWarrantyExpiringSoon(Today));
        Assert.True(asset.IsWarrantyExpiringSoon(Today, days: 90));
    }

    [Fact]
    public void Expired_warranty()
    {
        Assert.True(NewLaptop(warrantyUntil: Today.AddDays(-1)).IsWarrantyExpired(Today));
        Assert.False(NewLaptop(warrantyUntil: Today).IsWarrantyExpired(Today));
    }

    [Fact]
    public void Asset_without_warranty_is_neither_expiring_nor_expired()
    {
        var asset = new Asset(Guid.NewGuid(), "IT-2026-0001", AssetType.Other, "Logitech", "MX Keys");

        Assert.False(asset.IsWarrantyExpiringSoon(Today));
        Assert.False(asset.IsWarrantyExpired(Today));
    }
}
