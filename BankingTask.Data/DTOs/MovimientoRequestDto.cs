namespace BankingTask.Data.DTOs
{
    public class MovimientoRequestDto
    {
        public bool TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public string NumeroCuenta { get; set; }
    }
}
