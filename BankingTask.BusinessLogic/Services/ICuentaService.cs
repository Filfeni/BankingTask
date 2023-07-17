using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;

namespace BankingTask.BusinessLogic.Services
{
    public interface ICuentaService
    {
        public Task<Cuenta?> GetCuenta(int cuentaId);
        public Task<Cuenta?> GetCuentaByNumeroCuenta(string numeroCuenta);
        public Task<Cuenta> CreateCuenta(Cuenta cuenta);
        public Task<Cuenta> UpdateCuenta(Cuenta cuenta, CuentaPatchRequestDto dto);
    }
}
