using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingTask.API.Data.Entities;
using AutoMapper;
using BankingTask.API.Data.DTOs;

namespace BankingTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly BankingDBContext _context;
        private readonly IMapper _mapper;

        public ClienteController(BankingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente(CrearClienteDto dto)
        {
            var personaEntity = _mapper.Map<Persona>(dto);
            var clienteEntity = _mapper.Map<Cliente>(dto);
            clienteEntity.Persona = personaEntity;

            _context.Add(clienteEntity);

            await _context.SaveChangesAsync();

            return Created("GetCliente", dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.Include(x => x.Persona).FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            bool personaExiste = PersonaExists(cliente.PersonaId);

            _context.Clientes.Remove(cliente);
            _context.Personas.Remove(cliente.Persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool PersonaExists(int id)
        {
            return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
