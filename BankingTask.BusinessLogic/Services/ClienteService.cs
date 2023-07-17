using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.BusinessLogic.Services
{
    public class ClienteService : IClienteService
    {
        private readonly BankingDBContext _context;
        public ClienteService(BankingDBContext context)
        {
            _context = context;
        }
        public async Task<Cliente?> GetCliente(int id)
        {
            return await _context.Clientes.Include(x => x.Persona).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Cliente?> GetClienteByIdentificacion(string identificacion)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Persona.Identificacion == identificacion);
        }
        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            
            return cliente;
        }
        public async Task<Cliente> UpdateCliente(Cliente cliente, ClientePutRequestDto dto)
        {
            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;
            cliente.Persona.Direccion = dto.Direccion;
            cliente.Persona.Edad = dto.Edad;
            cliente.Persona.Genero = dto.Genero;
            cliente.Persona.Identificacion = dto.Identificacion;
            cliente.Persona.Nombre = dto.Nombre;
            cliente.Persona.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();

            return cliente;
        }
        public async Task<Cliente> DeleteCliente(Cliente cliente)
        {
            cliente.Estado = false;
            await _context.SaveChangesAsync();

            return cliente;
        }
    }
}
