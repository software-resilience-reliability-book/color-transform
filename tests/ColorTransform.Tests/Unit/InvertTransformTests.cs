using ColorTransform.Models;
using ColorTransform.Transforms;

namespace ColorTransform.Tests;

public class InvertTransformTests
{
    [Fact]
    public void invert_transform_works_without_clamping()
    {
        var transform = new InvertTransform();
        var color = new RgbColor(100, 150, 200);

        var result = transform.Apply(color);

        Assert.Equal(155, result.Red);
        Assert.Equal(105, result.Green);
        Assert.Equal(55, result.Blue);
    }

    [Fact]
    public void invert_transform_clamps_values_to_0_255()
    {
        var transform = new InvertTransform();
        var color = new RgbColor(255, 0, 0);

        var result = transform.Apply(color);

        Assert.Equal(0, result.Red);
        Assert.Equal(255, result.Green);
        Assert.Equal(255, result.Blue);
    }
}