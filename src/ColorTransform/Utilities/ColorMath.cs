internal static class ColorMath
{
    internal static int Clamp(int value) => 
        Math.Clamp(value, 0, 255);
}