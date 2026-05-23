internal static class ColorMath
{
    public static int Clamp(int value) => 
        Math.Clamp(value, 0, 255);
}