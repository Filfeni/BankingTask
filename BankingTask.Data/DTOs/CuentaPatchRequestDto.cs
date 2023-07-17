using BankingTask.Data.Enums;

namespace BankingTask.Data.DTOs
{
    public class CuentaPatchRequestDto
    {
        public TipoCuentaEnum TipoCuenta { get; set; }
        public bool Estado { get; set; }
    }
}
