namespace ColorTransform.Web.ErrorLogging;

public interface IErrorLog
{
    void Record(string message, Exception exception);
}
