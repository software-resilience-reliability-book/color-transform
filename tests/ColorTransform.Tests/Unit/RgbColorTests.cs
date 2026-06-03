using ColorTransform.Models;

namespace ColorTransform.Tests.Unit;

public class RgbColorTests
{
    [Theory]
    [InlineData(-1, 0, 0)]
    [InlineData(0, -1, 0)]
    [InlineData(0, 0, -1)]
    [InlineData(256, 0, 0)]
    [InlineData(0, 256, 0)]
    [InlineData(0, 0, 256)]
    public void color_fails_when_component_values_are_out_of_range(int r, int g, int b)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new RgbColor(r, g, b));
    }
}
