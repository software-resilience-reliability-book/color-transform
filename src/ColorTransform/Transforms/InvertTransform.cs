using ColorTransform.Models;

namespace ColorTransform.Transforms;

public class InvertTransform : IColorTransform
{
    public RgbColor Apply(RgbColor color) {
        int red = ColorMath.Clamp(255 - color.Red);
        int green = ColorMath.Clamp(255 - color.Green);
        int blue = ColorMath.Clamp(255 - color.Blue);
        return new RgbColor(red, green, blue);
    }
}