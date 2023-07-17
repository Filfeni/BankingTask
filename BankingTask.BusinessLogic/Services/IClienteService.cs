using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;

namespace BankingTask.BusinessLogic.Services
{
    public interface IClienteService
    {
        public Task<Cliente?> GetCliente(int id);
        public Task<Cliente?> GetClienteByIdentificacion(string identificacion);
        public Task<Cliente> CreateCliente(Cliente cliente);
        public Task<Cliente> UpdateCliente(Cliente cliente, ClientePutRequestDto dto);
        public Task<Cliente> DeleteCliente(Cliente cliente);
    }
}
