using BankingTask.API.Data.Enums;

namespace BankingTask.API.Data.DTOs
{
    public class CuentaResponseDto
    {
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
    }
}
