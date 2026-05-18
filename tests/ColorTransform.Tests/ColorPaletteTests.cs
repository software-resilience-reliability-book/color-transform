using ColorTransform.Models;
using Xunit;

namespace ColorTransform.Tests;

public class ColorPaletteTests
{
    [Fact]
    public void palette_with_valid_colors_creates()
    {
        var palette = new ColorPalette(new List<RgbColor> { new RgbColor(0, 0, 0) }, "Test");
        Assert.Single(palette.Colors);
        Assert.Equal("Test", palette.Name);
    }

    [Fact]
    public void palette_cannot_have_null_name()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new ColorPalette(new List<RgbColor> { new RgbColor(0, 0, 0) }, null!));
    }

    [Fact]
    public void palette_cannot_have_empty_name()
    {
        Assert.Throws<ArgumentException>(() => 
            new ColorPalette(new List<RgbColor> { new RgbColor(0, 0, 0) }, ""));
    }
}