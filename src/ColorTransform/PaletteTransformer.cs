using ColorTransform.Models;
using ColorTransform.Transforms;

namespace ColorTransform;

/*
This is the main public API for the ColorTransform library. This class accepts
an IColorTransform via constructor injection. Its methods accept a ColorPalette
and return a new ColorPalette with the transform applied to each color. It
should work the same in the case of zero, one, or many colors.
*/

public class PaletteTransformer
{
    private readonly IColorTransform _transform;

    public PaletteTransformer(IColorTransform transform)
    {
        _transform = transform;
    }

    public ColorPalette Transform(ColorPalette palette)
    {
        return new ColorPalette(palette.Colors.Select(_transform.Apply), palette.Name);
    }
}