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
        // We don't need to catch this separately because it's a subclass of ArgumentException
        // catch (ArgumentOutOfRangeException ex)
        catch (ArgumentException ex)
        {
            // TODO: dev mode should show full details and possibly a stack trace
            // Prod should only show the generic message.
            // What's the "right" way to control message detail per environment?
            // ErrorMessage = ex.Message;
            ErrorMessage = "We couldn’t apply that transform."
        }
    }

    private static IColorTransform CreateTransform(string transformType) =>
        transformType switch
        {
            "invert" => new InvertTransform(),
            "grayscale-average" => new GrayscaleTransform(GrayscaleFormula.Average),
            "tint" => new TintTransform(new RgbColor(255, 255, 255), 0.5f),
            _ => throw new ArgumentException($"Invalid transform type: {transformType}"),
        };
}
