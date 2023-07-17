using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.BusinessLogic.Services
{
    public class MovimientoService : IMovimientoService
    {
        public readonly BankingDBContext _context;

        public MovimientoService(BankingDBContext context)
        {
            _context = context;
        }

        public async Task<Movimiento?> GetMovimiento(int movimientoId)
        {
            return await _context.Movimientos.Include(x => x.Cuenta).Where(x => x.Id == movimientoId).FirstOrDefaultAsync();
        }
        public async Task<Movimiento?> GetMovimientoByCuenta(int cuentaId)
        {
            return await _context.Movimientos.Where(x => x.CuentaId == cuentaId).OrderByDescending(x => x.Fecha).FirstOrDefaultAsync();
        }
        public IQueryable<Movimiento> GetMovimientoByClienteIdAndFecha(ReporteRequestDto dto)
        {
            return _context.Movimientos
                .Include(x => x.Cuenta)
                .Include(x => x.Cuenta.Cliente)
                .Where(x => x.Cuenta.ClienteId == dto.clienteId && x.Fecha >= dto.Desde && x.Fecha <= dto.Hasta);
        }
        public async Task<Movimiento> CreateMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return movimiento;
        }
    }
}
