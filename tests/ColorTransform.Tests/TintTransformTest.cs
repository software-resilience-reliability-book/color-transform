
using ColorTransform.Models;
using ColorTransform.Transforms;

namespace ColorTransform.Tests;

public class TintTransformTests
{
    // This helper class saves us from writing out the same data for each test case.
    // We can instead make a list of TintCases, setting inputs and expected outputs for each.
    public record TintCase(string Name, RgbColor Input, RgbColor Tint, float Factor, RgbColor Expected);

    public static TheoryData<TintCase> InterpolationCases => new()
    {
        new TintCase(
            "factor 0; unchanged",
            new RgbColor(100, 150, 200),
            new RgbColor(255, 255, 255),
            0f,
            new RgbColor(100, 150, 200)
        ),
        new TintCase(
            "factor 1; full tint",
            new RgbColor(100, 150, 200),
            new RgbColor(255, 255, 255),
            1f,
            new RgbColor(255, 255, 255)
        ),
        new TintCase(
            "factor 0.5; partial tint",
            new RgbColor(100, 150, 200),
            new RgbColor(255, 255, 255),
            0.5f,
            new RgbColor(177, 202, 227)
        ),
    };

    [Theory]
    [MemberData(nameof(InterpolationCases))]
    public void tint_transform_interpolates_per_channel(TintCase c)
    {
        RgbColor result = new TintTransform(c.Tint, c.Factor).Apply(c.Input);
        Assert.Equal(c.Expected, result);
    }

    [Fact]
    public void tint_transform_rejects_invalid_factor_low()
    {
        var transform = new TintTransform(new RgbColor(200, 0, 0), -0.1f);
        Assert.Throws<ArgumentOutOfRangeException>(() => transform.Apply(new RgbColor(100, 100, 200)));
    }

    [Fact]
    public void tint_transform_rejects_invalid_factor_high()
    {
        var transform = new TintTransform(new RgbColor(200, 0, 0), 1.1f);
        Assert.Throws<ArgumentOutOfRangeException>(() => transform.Apply(new RgbColor(100, 100, 200)));
    }
}