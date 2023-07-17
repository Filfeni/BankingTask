namespace BankingTask.Data.DTOs
{
    public class ReporteRequestDto
    {
        public int clienteId { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}
