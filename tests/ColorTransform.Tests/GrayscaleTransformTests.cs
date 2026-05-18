using ColorTransform.Models;
using ColorTransform.Transforms;
using Xunit;

namespace ColorTransform.Tests;

public class GrayscaleTransformTests
{
    /*
    AVERAGE FORMULA TESTS:
    */
    [Fact]
    public void grayscale_average_transform_works_with_all_channels_equal()
    {
        var transform = new GrayscaleTransform(GrayscaleFormula.Average);
        var color = new RgbColor(100, 100, 100);

        var result = transform.Apply(color);

        Assert.Equal(100, result.Red);
        Assert.Equal(100, result.Green);
        Assert.Equal(100, result.Blue);
    }

    [Fact]
    public void grayscale_average_transform_works_with_different_channel_values()
    {
        var transform = new GrayscaleTransform(GrayscaleFormula.Average);
        var color = new RgbColor(100, 150, 200);

        var result = transform.Apply(color);

        Assert.Equal(150, result.Red);
        Assert.Equal(150, result.Green);
        Assert.Equal(150, result.Blue);
    }

    [Fact]
    public void grayscale_average_transform_works_when_scaled_result_is_not_an_integer()
    {
        var transform = new GrayscaleTransform(GrayscaleFormula.Average);
        var color = new RgbColor(0, 0, 1);

        var result = transform.Apply(color);

        Assert.Equal(0, result.Red);
        Assert.Equal(0, result.Green);
        Assert.Equal(0, result.Blue);
    }

     /*
    LUMINANCE FORMULA TESTS:
    */
    [Fact]
    public void grayscale_luminance_transform_works_with_all_channels_equal()
    {
        var transform = new GrayscaleTransform(GrayscaleFormula.Luminance);
        var color = new RgbColor(100, 100, 100);

        var result = transform.Apply(color);

        Assert.Equal(100, result.Red);
        Assert.Equal(100, result.Green);
        Assert.Equal(100, result.Blue);
    }

    [Fact]
    public void grayscale_luminance_transform_works_with_different_channel_values()
    {
        var transform = new GrayscaleTransform(GrayscaleFormula.Luminance);
        var color = new RgbColor(100, 150, 200);

        var result = transform.Apply(color);

        Assert.Equal(143, result.Red);
        Assert.Equal(143, result.Green);
        Assert.Equal(143, result.Blue);
    }

    [Fact]
    public void grayscale_luminance_transform_works_when_scaled_result_is_not_an_integer()
    {
        var transform = new GrayscaleTransform(GrayscaleFormula.Luminance);
        var color = new RgbColor(0, 0, 1);

        var result = transform.Apply(color);

        Assert.Equal(0, result.Red);
        Assert.Equal(0, result.Green);
        Assert.Equal(0, result.Blue);
    }
}