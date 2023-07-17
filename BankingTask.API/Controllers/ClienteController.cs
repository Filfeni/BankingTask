using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using BankingTask.BusinessLogic.Services;

namespace BankingTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;

        public ClienteController( IMapper mapper, IClienteService clienteService)
        {
            _mapper = mapper;
            _clienteService = clienteService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> GetCliente([FromRoute]int id)
        {
            var cliente = await _clienteService.GetCliente(id);
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

            await _clienteService.CreateCliente(clienteEntity);

            return Created("GetCliente", dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> UpdateCliente([FromRoute] int id, [FromBody] ClientePutRequestDto dto)
        {
            var cliente = await _clienteService.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteService.UpdateCliente(cliente, dto);

            var response = _mapper.Map<ClienteResponseDto>(cliente);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteService.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteService.DeleteCliente(cliente);

            return NoContent();
        }
    }
}
