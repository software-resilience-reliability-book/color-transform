using ColorTransform.Models;
using ColorTransform.Utilities;
using Xunit;

namespace ColorTransform.Tests;

public class HexConverterTests
{
    [Fact]
    public void hex_string_creates_rgb_color_when_valid()
    {
        var converter = new HexConverter();
        var color = converter.FromHexString("#000000");
        Assert.Equal(0, color.Red);
        Assert.Equal(0, color.Green);
        Assert.Equal(0, color.Blue);
    }

    [Fact]
    public void hex_string_fails_when_not_six_characters()
    {
        var converter = new HexConverter();
        Assert.Throws<ArgumentException>(() => converter.FromHexString("#00000"));
    }

    [Fact]
    public void hex_string_fails_when_contains_invalid_characters()
    {
        var converter = new HexConverter();
        Assert.Throws<ArgumentException>(() => converter.FromHexString("#00000G"));
    }

    [Fact]
    public void created_hex_string_includes_hash_when_requested()
    {
        var converter = new HexConverter();
        var hex = converter.ToHexString(new RgbColor(10, 100, 255), true);
        Assert.Equal("#0A64FF", hex);
    }

    [Fact]
    public void created_hex_string_omits_hash_when_requested()
    {
        var converter = new HexConverter();
        var hex = converter.ToHexString(new RgbColor(10, 100, 255), false);
        Assert.Equal("0A64FF", hex);
    }
}