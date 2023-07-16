using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingTask.API.Data.Entities;
using AutoMapper;
using BankingTask.API.Data.DTOs;
using Humanizer;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> GetCliente([FromRoute]int id)
        {
            var cliente = await _context.Clientes.Include(x => x.Persona).FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ClienteResponseDto>(cliente);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(ClientePostRequestDto dto)
        {
            var personaEntity = _mapper.Map<Persona>(dto);
            var clienteEntity = _mapper.Map<Cliente>(dto);
            clienteEntity.Persona = personaEntity;

            _context.Add(clienteEntity);

            await _context.SaveChangesAsync();

            return Created("GetCliente", dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> UpdateCliente([FromRoute] int id, [FromBody] ClientePutRequestDto dto)
        {
            var cliente = await _context.Clientes.Include(x => x.Persona).FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;
            cliente.Persona.Direccion = dto.Direccion;
            cliente.Persona.Edad = dto.Edad;
            cliente.Persona.Genero = dto.Genero;
            cliente.Persona.Identificacion = dto.Identificacion;
            cliente.Persona.Nombre = dto.Nombre;
            cliente.Persona.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();

            var response = _mapper.Map<ClienteResponseDto>(cliente);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Estado = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
