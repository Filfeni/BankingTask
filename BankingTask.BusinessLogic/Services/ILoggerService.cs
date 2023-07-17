namespace BankingTask.BusinessLogic.Services
{
    public interface ILoggerService
    {
        public void Log(string message);
        public void Log(Exception message);
    }
}
