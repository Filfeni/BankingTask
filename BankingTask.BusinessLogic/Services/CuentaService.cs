using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.BusinessLogic.Services
{
    public class CuentaService : ICuentaService
    {
        public readonly BankingDBContext _context;
        public CuentaService(BankingDBContext context) 
        {
            _context = context;
        }
        
        public async Task<Cuenta?> GetCuenta(int cuentaId)
        {
            return await _context.Cuentas.Include(x => x.Cliente).Include(x => x.Cliente.Persona).FirstOrDefaultAsync(x => x.Id == cuentaId);
        }
        public async Task<Cuenta?> GetCuentaByNumeroCuenta(string numeroCuenta)
        {
            return await _context.Cuentas.FirstOrDefaultAsync(x => x.NumeroCuenta == numeroCuenta);
        }

        public async Task<Cuenta> CreateCuenta(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return cuenta;
        }

        public async Task<Cuenta> UpdateCuenta(Cuenta cuenta, CuentaPatchRequestDto dto)
        {
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return cuenta;
        }
    }
}
