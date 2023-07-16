using AutoMapper;
using BankingTask.API.Data.DTOs;
using BankingTask.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly BankingDBContext _context;
        private readonly IMapper _mapper;

        public MovimientoController(BankingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoResponseDto>> GetMovimiento([FromRoute] int id)
        {
            var movimiento = _context.Movimientos.Include(x => x.Cuenta).Where(x => x.Id == id).FirstOrDefault();
            if (movimiento == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<MovimientoResponseDto>(movimiento);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Movimiento>> CreateMovimiento(MovimientoRequestDto dto)
        {
            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(x => x.NumeroCuenta == dto.NumeroCuenta);

            if (cuenta == null)
            {
                return NotFound();
            }
            
            var ultimoMovimiento = await _context.Movimientos.Where(x => x.CuentaId == cuenta.Id).OrderByDescending(x => x.Fecha).FirstOrDefaultAsync();
            decimal ultimoSaldo = ultimoMovimiento == null ? cuenta.SaldoInicial : ultimoMovimiento.SaldoDisponible;
            
            if (dto.TipoMovimiento == false && dto.Valor > ultimoSaldo)
            {
                return BadRequest(new { Message = "El valor del movimiento excede el saldo disponible"});
            }

            var movimiento = _mapper.Map<Movimiento>(dto);
            movimiento.CuentaId = cuenta.Id;
            movimiento.Fecha = DateTime.Now;
            movimiento.SaldoDisponible = dto.TipoMovimiento == true ? ultimoSaldo + movimiento.Valor : ultimoSaldo - movimiento.Valor;
            _context.Add(movimiento);

            await _context.SaveChangesAsync();

            return Created("GetMovimiento", dto);
        }
    }
}
