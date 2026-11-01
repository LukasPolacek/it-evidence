using ItEvidence.Domain.Assets;
using ItEvidence.Domain.Common;

namespace ItEvidence.Domain.Tests;

public class AssetCreationTests
{
    [Fact]
    public void New_asset_is_in_stock_without_holder_and_history()
    {
        var asset = new Asset(Guid.NewGuid(), "IT-2026-0001", AssetType.Monitor, " Dell ", " P2425H ");

        Assert.Equal(AssetStatus.InStock, asset.Status);
        Assert.Null(asset.CurrentHolderId);
        Assert.Empty(asset.Assignments);
        Assert.Equal("Dell", asset.Manufacturer);
        Assert.Equal("P2425H", asset.Model);
        Assert.Null(asset.Location);
    }

    [Fact]
    public void Invalid_inventory_number_is_rejected()
    {
        Assert.Throws<DomainException>(() =>
            new Asset(Guid.NewGuid(), "PC-15", AssetType.Desktop, "HP", "EliteDesk"));
    }

    [Theory]
    [InlineData("", "EliteDesk")]
    [InlineData("HP", " ")]
    public void Manufacturer_and_model_are_required(string manufacturer, string model)
    {
        Assert.Throws<DomainException>(() =>
            new Asset(Guid.NewGuid(), "IT-2026-0001", AssetType.Desktop, manufacturer, model));
    }

    [Fact]
    public void Negative_price_is_rejected()
    {
        Assert.Throws<DomainException>(() =>
            new Asset(Guid.NewGuid(), "IT-2026-0001", AssetType.Phone, "Apple", "iPhone 16", price: -1m));
    }

    [Fact]
    public void Warranty_cannot_end_before_purchase()
    {
        Assert.Throws<DomainException>(() =>
            new Asset(Guid.NewGuid(), "IT-2026-0001", AssetType.Phone, "Apple", "iPhone 16",
                purchaseDate: new DateOnly(2026, 5, 1), warrantyUntil: new DateOnly(2026, 4, 30)));
    }
}
