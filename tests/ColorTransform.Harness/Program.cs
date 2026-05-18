using ColorTransform.Models;
using ColorTransform.Utilities;


// Sandbox development test: use this to step into "FromHexString"
var converter = new HexConverter();
RgbColor color = converter.FromHexString("#FF5733");
Console.WriteLine($"Red: {color.Red}, Green: {color.Green}, Blue: {color.Blue}");

// Sandbox development test: use this to step into "ToHexString"
RgbColor color2 = new RgbColor(255, 87, 51);
string hex = converter.ToHexString(color2);
Console.WriteLine($"Hex: {hex}");