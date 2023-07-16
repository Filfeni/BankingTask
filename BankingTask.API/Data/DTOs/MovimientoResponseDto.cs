namespace BankingTask.API.Data.DTOs
{
    public class MovimientoResponseDto
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string NumeroCuenta { get; set; }
    }
}
