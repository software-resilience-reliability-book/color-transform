using ColorTransform.Models;
using ColorTransform.Utilities;

namespace ColorTransform.Tests;

public class HexConverterTests
{
    [Fact]
    public void hex_string_fails_when_not_six_characters()
    {
        var converter = new HexConverter();
        Assert.Throws<ArgumentException>(() => converter.FromHexString("#00000"));
    }

    [Fact]
    public void hex_string_fails_when_contains_invalid_hex_characters()
    {
        var converter = new HexConverter();
        Assert.Throws<ArgumentException>(() => converter.FromHexString("#00000G"));
    }

    [Fact]
    public void hex_string_with_leading_hash_creates_correct_rbg_component_values()
    {
        var converter = new HexConverter();

        var color = converter.FromHexString("#FF5511");

        Assert.Equal(255, color.Red);
        Assert.Equal(85, color.Green);
        Assert.Equal(17, color.Blue);
    }

    [Fact]
    public void hex_string_without_leading_hash_creates_correct_rbg_component_values()
    {
        var converter = new HexConverter();

        var color = converter.FromHexString("FF5511");

        Assert.Equal(255, color.Red);
        Assert.Equal(85, color.Green);
        Assert.Equal(17, color.Blue);
    }

    [Fact]
    public void rgb_color_creates_correct_hex_string_with_requested_hash()
    {
        var converter = new HexConverter();
        var hex = converter.ToHexString(new RgbColor(10, 100, 255), true);
        Assert.Equal("#0A64FF", hex);
    }

    [Fact]
    public void rgb_color_creates_correct_hex_string_without_requested_hash()
    {
        var converter = new HexConverter();
        var hex = converter.ToHexString(new RgbColor(10, 100, 255), false);
        Assert.Equal("0A64FF", hex);
    }
}