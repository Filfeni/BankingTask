using AutoMapper;
using BankingTask.BusinessLogic.Services;
using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICuentaService _cuentaService;
        private readonly IMovimientoService _movimientoService;

        public MovimientoController(IMapper mapper, ICuentaService cuentaService, IMovimientoService movimientoService)
        {
            _mapper = mapper;
            _cuentaService = cuentaService;
            _movimientoService = movimientoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoResponseDto>> GetMovimiento([FromRoute] int id)
        {
            var movimiento = await _movimientoService.GetMovimiento(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<MovimientoResponseDto>(movimiento);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<MovimientoResponseDto>> CreateMovimiento(MovimientoRequestDto dto)
        {
            var cuenta = await _cuentaService.GetCuentaByNumeroCuenta(dto.NumeroCuenta);

            if (cuenta == null)
            {
                return NotFound();
            }

            var ultimoMovimiento = await _movimientoService.GetMovimientoByCuenta(cuenta.Id);
            decimal ultimoSaldo = ultimoMovimiento == null ? cuenta.SaldoInicial : ultimoMovimiento.SaldoDisponible;
            
            if (dto.TipoMovimiento == false && dto.Valor > ultimoSaldo)
            {
                return BadRequest(new { Message = "El valor del movimiento excede el saldo disponible"});
            }

            var movimiento = _mapper.Map<Movimiento>(dto);
            movimiento.CuentaId = cuenta.Id;
            movimiento.Fecha = DateTime.Now;
            movimiento.SaldoDisponible = dto.TipoMovimiento == true ? ultimoSaldo + movimiento.Valor : ultimoSaldo - movimiento.Valor;
            await _movimientoService.CreateMovimiento(movimiento);
            var result = _mapper.Map<MovimientoResponseDto>(movimiento);
            return Created("GetMovimiento", result);
        }
    }
}
