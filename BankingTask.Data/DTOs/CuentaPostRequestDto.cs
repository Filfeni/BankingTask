using BankingTask.Data.Enums;

namespace BankingTask.Data.DTOs
{
    public class CuentaPostRequestDto
    {
        public string NumeroCuenta { get; set; }
        public TipoCuentaEnum TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string IdentificacionCliente { get; set; }
    }
}
