namespace ColorTransform.Models;

public record ColorPalette
{
    public IReadOnlyList<RgbColor> Colors { get; }
    public string Name { get; }

    public ColorPalette(IEnumerable<RgbColor> colors, string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Colors = colors.ToList().AsReadOnly();
        Name = name;
    }
}