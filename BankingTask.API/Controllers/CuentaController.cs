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
    public class CuentaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICuentaService _cuentaService;
        private readonly IClienteService _clienteService;

        public CuentaController(IMapper mapper, ICuentaService cuentaService, IClienteService clienteService)
        {
            _mapper = mapper;
            _cuentaService = cuentaService;
            _clienteService = clienteService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaResponseDto>> GetCuenta([FromRoute] int id)
        {
            var cuenta = await _cuentaService.GetCuenta(id);
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
            var cliente = await _clienteService.GetClienteByIdentificacion(dto.IdentificacionCliente);

            if (cliente == null)
            {
                return NotFound();
            }

            var cuenta = _mapper.Map<Cuenta>(dto);
            cuenta.ClienteId = cliente.Id;
            await _cuentaService.CreateCuenta(cuenta);

            return Created("GetCuenta", dto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CuentaResponseDto>> UpdateCuenta([FromRoute] int id, [FromBody] CuentaPatchRequestDto dto)
        {
            var cuenta = await _cuentaService.GetCuenta(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            await _cuentaService.UpdateCuenta(cuenta, dto);

            var response = _mapper.Map<CuentaResponseDto>(cuenta);

            return Ok(response);
        }
    }
}
