using ColorTransform.Models;

namespace ColorTransform.Transforms;

public interface IColorTransform
{
    RgbColor Apply(RgbColor color);
}