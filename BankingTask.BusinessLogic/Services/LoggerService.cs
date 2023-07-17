using Microsoft.Extensions.Logging;

namespace BankingTask.BusinessLogic.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

        public void Log(Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, \n {ex.StackTrace}");
        }
    }
}
