using ColorTransform.Models;
using ColorTransform.Utilities;

namespace ColorTransform.Transforms;

public class GrayscaleTransform : IColorTransform
{
    public RgbColor Apply(RgbColor color) {
        int grayscale = (color.Red + color.Green + color.Blue) / 3;
        return new RgbColor(grayscale, grayscale, grayscale);
    }
}