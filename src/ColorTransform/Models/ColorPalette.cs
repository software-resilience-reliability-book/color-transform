namespace ColorTransform.Models;

public class ColorPalette
{
    public IReadOnlyList<RgbColor> Colors { get; }
    public string Name { 
        get; 
        set {
            ArgumentException.ThrowIfNullOrEmpty(value);
            field = value;
        }
    }

    public ColorPalette(IEnumerable<RgbColor> colors, string name)
    {
        Colors = colors.ToList().AsReadOnly();
        Name = name;
    }
}