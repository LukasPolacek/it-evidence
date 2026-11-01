using ItEvidence.Domain.Assets;

namespace ItEvidence.Domain.Tests;

public class InventoryNumbersTests
{
    [Theory]
    [InlineData(2026, 1, "IT-2026-0001")]
    [InlineData(2026, 42, "IT-2026-0042")]
    [InlineData(2027, 9999, "IT-2027-9999")]
    public void Format_pads_sequence_to_four_digits(int year, int sequence, string expected)
    {
        Assert.Equal(expected, InventoryNumbers.Format(year, sequence));
    }

    [Theory]
    [InlineData(1999, 1)]
    [InlineData(2026, 0)]
    [InlineData(2026, 10000)]
    public void Format_rejects_out_of_range_values(int year, int sequence)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => InventoryNumbers.Format(year, sequence));
    }

    [Theory]
    [InlineData("IT-2026-0001", true)]
    [InlineData("IT-2026-0000", false)]
    [InlineData("IT-2026-001", false)]
    [InlineData("it-2026-0001", false)]
    [InlineData("IT-26-0001", false)]
    [InlineData(" IT-2026-0001", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValid_checks_exact_format(string? value, bool expected)
    {
        Assert.Equal(expected, InventoryNumbers.IsValid(value));
    }

    [Fact]
    public void Next_starts_at_one_for_new_year()
    {
        Assert.Equal("IT-2027-0001", InventoryNumbers.Next(2027, ["IT-2026-0001", "IT-2026-0002"]));
    }

    [Fact]
    public void Next_continues_after_highest_number_of_the_year_and_ignores_gaps()
    {
        var existing = new[] { "IT-2026-0001", "IT-2026-0007", "IT-2026-0003", "IT-2025-0050", "nesmysl" };

        Assert.Equal("IT-2026-0008", InventoryNumbers.Next(2026, existing));
    }
}
