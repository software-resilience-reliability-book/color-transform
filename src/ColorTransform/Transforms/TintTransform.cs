using ColorTransform.Models;
using ColorTransform.Utilities;

namespace ColorTransform.Transforms;

public class TintTransform(RgbColor tint, float factor) : IColorTransform
{
    public RgbColor Apply(RgbColor color)
    {
        if (factor < 0 || factor > 1)
            throw new ArgumentOutOfRangeException(nameof(factor));

        int red = ColorMath.Clamp((int)(color.Red + (tint.Red - color.Red) * factor));
        int green = ColorMath.Clamp((int)(color.Green + (tint.Green - color.Green) * factor));
        int blue = ColorMath.Clamp((int)(color.Blue + (tint.Blue - color.Blue) * factor));
        return new RgbColor(red, green, blue);
    }
}