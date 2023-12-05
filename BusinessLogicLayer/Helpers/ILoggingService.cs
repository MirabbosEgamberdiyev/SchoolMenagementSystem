

namespace BusinessLogicLayer.Helpers;
public interface ILoggingService
{
    void LogError(string message);
    void LogInfo(string message);
    void LogWarning(string message);
}
