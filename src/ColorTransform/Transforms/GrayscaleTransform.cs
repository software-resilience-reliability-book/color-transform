using ColorTransform.Models;

namespace ColorTransform.Transforms;

public class GrayscaleTransform(GrayscaleFormula formula = GrayscaleFormula.Average) : IColorTransform
{
    public RgbColor Apply(RgbColor color)
    {
        int grayscale = formula switch
        {
            GrayscaleFormula.Average => ToAverage(color),
            GrayscaleFormula.Luminance => ToLuminance(color),
            _ => throw new ArgumentOutOfRangeException(nameof(formula)),
        };
        return new RgbColor(grayscale, grayscale, grayscale);
    }

    private static int ToAverage(RgbColor color) =>
        (color.Red + color.Green + color.Blue) / 3;

    private static int ToLuminance(RgbColor color) =>
        // This is the int "fast luminance" algorithm.
        (2 * color.Red + 5 * color.Green + 1 * color.Blue) / 8;
}
