using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;

namespace BankingTask.BusinessLogic.Services
{
    public interface IMovimientoService
    {
        public Task<Movimiento?> GetMovimiento(int movimientoId);
        public Task<Movimiento?> GetMovimientoByCuenta(int cuentaId);
        public Task<Movimiento> CreateMovimiento(Movimiento movimiento);
        public IQueryable<Movimiento> GetMovimientoByClienteIdAndFecha(ReporteRequestDto dto);
    }
}
