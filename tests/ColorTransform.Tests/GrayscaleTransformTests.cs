using ColorTransform.Models;
using ColorTransform.Transforms;
using Xunit;

namespace ColorTransform.Tests;

public class GrayscaleTransformTests
{
    [Fact]
    public void grayscale_transform_works_with_all_channels_equal()
    {
        var transform = new GrayscaleTransform();
        var color = new RgbColor(100, 100, 100);

        var result = transform.Apply(color);

        Assert.Equal(100, result.Red);
        Assert.Equal(100, result.Green);
        Assert.Equal(100, result.Blue);
    }

    [Fact]
    public void grayscale_transform_works_with_different_channel_values()
    {
        var transform = new GrayscaleTransform();
        var color = new RgbColor(100, 150, 200);

        var result = transform.Apply(color);

        Assert.Equal(150, result.Red);
        Assert.Equal(150, result.Green);
        Assert.Equal(150, result.Blue);
    }

    [Fact]
    public void grayscale_transform_works_when_scaled_result_is_not_an_integer()
    {
        var transform = new GrayscaleTransform();
        var color = new RgbColor(0, 0, 1);

        var result = transform.Apply(color);

        Assert.Equal(0, result.Red);
        Assert.Equal(0, result.Green);
        Assert.Equal(0, result.Blue);
    }
}