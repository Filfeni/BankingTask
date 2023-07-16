using BankingTask.API.Data.Enums;

namespace BankingTask.API.Data.DTOs
{
    public class CuentaPatchRequestDto
    {
        public TipoCuentaEnum TipoCuenta { get; set; }
        public bool Estado { get; set; }
    }
}
