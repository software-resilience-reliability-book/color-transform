using ColorTransform.Models;
using ColorTransform.Transforms;

namespace ColorTransform.Tests.Unit;

public class PaletteTransformerTests
{
    /*
        This class is here to decouple the actual transform from the test rather than
        tie it to any one implementation. For example: we could have used "InvertTransform"
        instead of "ColorTransformFake", but then these tests all count on the invert transform
        working correctly, and tests are not isolated to the system under test (PaletteTransformer).
    */
    class ColorTransformFake : IColorTransform
    {
        public RgbColor Apply(RgbColor color)
        {
            return new RgbColor(color.Red + 1, color.Green + 1, color.Blue + 1);
        }
    }

    [Fact]
    public void palette_transformer_applies_transform_to_each_color()
    {
        ColorPalette palette = new ColorPalette(
        [
            new RgbColor(0, 0, 0),
            new RgbColor(100, 101, 102),
        ],
        "My Palette");
        IColorTransform transform = new ColorTransformFake();
        PaletteTransformer transformer = new PaletteTransformer(transform);

        ColorPalette result = transformer.Transform(palette);

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
        ColorPalette palette = new ColorPalette([new RgbColor(0, 0, 0)], "Original");
        IColorTransform transform = new ColorTransformFake();
        PaletteTransformer transformer = new PaletteTransformer(transform);

        ColorPalette result = transformer.Transform(palette);

        Assert.Equal("Original", result.Name);
    }

    [Fact]
    public void palette_transformer_returns_empty_palette_when_given_empty_palette()
    {
        ColorPalette palette = new ColorPalette([], "Empty");
        IColorTransform transform = new ColorTransformFake();
        PaletteTransformer transformer = new PaletteTransformer(transform);

        ColorPalette result = transformer.Transform(palette);

        Assert.Empty(result.Colors);
        Assert.Equal("Empty", result.Name);
    }
}
