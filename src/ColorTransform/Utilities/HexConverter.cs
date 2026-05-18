using ColorTransform.Models;

namespace ColorTransform.Utilities;

/*
The ColorTransform library only works with RgbColor objects. 
This class provides a way to convert between RgbColor objects and hex strings.
*/
public class HexConverter
{
    public RgbColor FromHexString(string hex)
    {
        hex = hex.TrimStart('#');

        if (hex.Length != 6)
            throw new ArgumentException("Hex color must be 6 characters.", nameof(hex));

        if (!System.Text.RegularExpressions.Regex.IsMatch(hex, "^[0-9A-Fa-f]{6}$"))
            throw new ArgumentException("Hex color must contain only hex characters.", nameof(hex));

        return new RgbColor(
            Convert.ToInt32(hex[0..2], 16),
            Convert.ToInt32(hex[2..4], 16),
            Convert.ToInt32(hex[4..6], 16)
        );
    }

    public string ToHexString(RgbColor color, bool includeHash = true)
    {
        var hex = $"{color.Red:X2}{color.Green:X2}{color.Blue:X2}";
        if (includeHash)
            hex = $"#{hex}";
        return hex;
    }
}