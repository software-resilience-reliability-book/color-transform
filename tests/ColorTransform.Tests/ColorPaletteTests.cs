using ColorTransform.Models;

namespace ColorTransform.Tests;

public class ColorPaletteTests
{
    [Fact]
    public void palette_creates_when_valid()
    {
        var palette = new ColorPalette(new List<RgbColor> { new RgbColor(0, 0, 0) }, "Test");
        Assert.Single(palette.Colors);
        Assert.Equal("Test", palette.Name);
    }

    [Fact]
    public void palette_fails_when_name_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new ColorPalette(new List<RgbColor> { new RgbColor(0, 0, 0) }, null!));
    }

    [Fact]
    public void palette_fails_when_name_is_empty()
    {
        Assert.Throws<ArgumentException>(() => 
            new ColorPalette(new List<RgbColor> { new RgbColor(0, 0, 0) }, ""));
    }
}