using AutoMapper;
using BankingTask.API.Data.DTOs;
using BankingTask.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly BankingDBContext _context;
        private readonly IMapper _mapper;

        public CuentaController(BankingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaResponseDto>> GetCuenta([FromRoute] int id)
        {
            var cuenta = await _context.Cuentas.Include(x => x.Cliente).Include(x => x.Cliente.Persona).FirstOrDefaultAsync(x => x.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CuentaResponseDto>(cuenta);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Cuenta>> CreateCuenta(CuentaPostRequestDto dto)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Persona.Identificacion == dto.IdentificacionCliente);

            if (cliente == null)
            {
                return NotFound();
            }

            var cuenta = _mapper.Map<Cuenta>(dto);
            cuenta.ClienteId = cliente.Id;
            _context.Add(cuenta);

            await _context.SaveChangesAsync();

            return Created("GetCuenta", dto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> UpdateCuenta([FromRoute] int id, [FromBody] CuentaPatchRequestDto dto)
        {
            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(x => x.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            var response = _mapper.Map<ClienteResponseDto>(cuenta);

            return Ok(response);
        }
    }
}
