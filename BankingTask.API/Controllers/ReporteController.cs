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
    public class ReporteController : ControllerBase
    {
        private readonly BankingDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMovimientoService _movimientoService;

        public ReporteController(BankingDBContext context, IMapper mapper, IMovimientoService movimientoService)
        {
            _context = context;
            _mapper = mapper;
            _movimientoService = movimientoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteResponseDto>>> GetMovimiento([FromQuery] ReporteRequestDto dto)
        {
            var movimientosList = _movimientoService.GetMovimientoByClienteIdAndFecha(dto);

            if (!movimientosList.Any())
            {
                return NotFound();
            }

            var response = movimientosList.Select(x => _mapper.Map<ReporteResponseDto>(x)).ToList();

            return Ok(response);
        }
    }
}
