using AutoMapper;
using BankingTask.API.Data.DTOs;
using BankingTask.API.Data.Entities;
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

        public ReporteController(BankingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteResponseDto>>> GetMovimiento([FromQuery] ReporteRequestDto dto)
        {
            var movimientosList = _context.Movimientos
                .Include(x => x.Cuenta)
                .Include(x => x.Cuenta.Cliente)
                .Where(x => x.Cuenta.ClienteId == dto.clienteId && x.Fecha >= dto.Desde && x.Fecha <= dto.Hasta);

            if (!movimientosList.Any())
            {
                return NotFound();
            }

            var response = movimientosList.Select(x => _mapper.Map<ReporteResponseDto>(x)).ToList();

            return Ok(response);
        }
    }
}
