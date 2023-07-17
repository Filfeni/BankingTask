using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using FluentValidation;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BankingTask.BusinessLogic.Validators
{
    public class MovimientoRequestValidator : AbstractValidator<MovimientoRequestDto>
    {
        public MovimientoRequestValidator(BankingDBContext context)
        {
            RuleFor(movimiento => movimiento.NumeroCuenta).NotNull().Must(numeroCuenta => context.Cuentas.Any(cuenta => cuenta.NumeroCuenta == numeroCuenta));
            RuleFor(movimiento => movimiento.TipoMovimiento).NotNull();
            RuleFor(movimiento => movimiento.Valor).NotNull().GreaterThan(0);
        }
    }
}
