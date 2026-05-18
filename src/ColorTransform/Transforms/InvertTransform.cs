using ColorTransform.Models;

namespace ColorTransform.Transforms;

public class InvertTransform : IColorTransform
{
    public RgbColor Apply(RgbColor color) {
        int red = InvertComponent(color.Red);
        int green = InvertComponent(color.Green);
        int blue = InvertComponent(color.Blue);
        return new RgbColor(red, green, blue);
    }

    // TODO: If this is used elsewhere extract it to a static utility class.
    private static int InvertComponent(int component)
    {
        if (component < 0) component = 0;
        if (component > 255) component = 255;
        return 255 - component;
    }
}