namespace ColorTransform.Models;

public record RgbColor
{
    public int Red { get; }
    public int Green { get; }
    public int Blue { get; }

    public RgbColor(int red, int green, int blue)
    {
        ValidateRange(red, nameof(red));
        ValidateRange(green, nameof(green));
        ValidateRange(blue, nameof(blue));

        Red = red;
        Green = green;
        Blue = blue;
    }

    private void ValidateRange(int value, string name)
    {
        if (value < 0 || value > 255) throw new ArgumentOutOfRangeException(name);
    }
}