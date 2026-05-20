using ColorTransform.Models;
using ColorTransform.Transforms;

namespace ColorTransform.Tests;

public class PaletteTransformerTests
{
    /*
    This mock is here to decouple the actual transform from the test rather than
    tie it to any one implementation. For example: we could have used "InvertTransform"
    instead of "ColorTransformMock", but then these tests all count on the invert transform
    working correctly, and tests are not isolated to the system under test (PaletteTransformer).
    */
    class ColorTransformMock : IColorTransform
    {
        public RgbColor Apply(RgbColor color)
        {
            return new RgbColor(color.Red + 1, color.Green + 1, color.Blue + 1);
        }
    }
    
    [Fact]
    public void palette_transformer_applies_transform_to_each_color()
    {
        var transformer = new PaletteTransformer(new ColorTransformMock());
        var palette = new ColorPalette(
            [
                new RgbColor(0, 0, 0),
                new RgbColor(100, 101, 102),
            ],
            "My Palette");

        var result = transformer.Transform(palette);

        Assert.Equal(2, result.Colors.Count);

        Assert.Equal(1, result.Colors[0].Red);
        Assert.Equal(1, result.Colors[0].Green);
        Assert.Equal(1, result.Colors[0].Blue);

        Assert.Equal(101, result.Colors[1].Red);
        Assert.Equal(102, result.Colors[1].Green);
        Assert.Equal(103, result.Colors[1].Blue);
    }

    [Fact]
    public void palette_transformer_preserves_palette_name()
    {
        var transformer = new PaletteTransformer(new ColorTransformMock());
        var palette = new ColorPalette([new RgbColor(0, 0, 0)], "Original");

        var result = transformer.Transform(palette);

        Assert.Equal("Original", result.Name);
    }

    [Fact]
    public void palette_transformer_returns_empty_palette_when_given_empty_palette()
    {
        var transformer = new PaletteTransformer(new ColorTransformMock());
        var palette = new ColorPalette([], "Empty");

        var result = transformer.Transform(palette);

        Assert.Empty(result.Colors);
        Assert.Equal("Empty", result.Name);
    }
}
