namespace BankingTask.Data.DTOs
{
    public class ReporteResponseDto
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal SaldoInicial { get; set; }
    }
}
