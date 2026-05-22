namespace ColorTransform.Web.ErrorLogging;

public sealed class FileErrorLog(string filePath) : IErrorLog
{
    public void Record(string message, Exception exception)
    {
        var line = $"{DateTime.UtcNow:O} {message}{Environment.NewLine}{exception}";
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        File.AppendAllText(filePath, line + Environment.NewLine);
    }
}
