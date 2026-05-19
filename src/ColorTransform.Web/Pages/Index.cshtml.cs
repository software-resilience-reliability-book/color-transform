using ColorTransform;
using ColorTransform.Models;
using ColorTransform.Transforms;
using ColorTransform.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ColorTransform.Web.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string TransformType { get; set; } = "invert";

    [BindProperty]
    public string InputColor { get; set; } = "#336699";

    public string? OutputColor { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        try
        {
            var converter = new HexConverter();
            var input = converter.FromHexString(InputColor);
            var palette = new ColorPalette([input], "demo");
            var result = new PaletteTransformer(CreateTransform(TransformType)).Transform(palette);
            OutputColor = converter.ToHexString(result.Colors[0]);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private static IColorTransform CreateTransform(string transformType) =>
        transformType switch
        {
            "invert" => new InvertTransform(),
            "grayscale-average" => new GrayscaleTransform(GrayscaleFormula.Average),
            "tint" => new TintTransform(new RgbColor(255, 255, 255), 0.5f),
            _ => new InvertTransform(),
        };
}
