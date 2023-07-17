using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using FluentValidation;

namespace BankingTask.BusinessLogic.Validators
{
    public class CuentaPostRequestValidator : AbstractValidator<CuentaPostRequestDto>
    {
        public CuentaPostRequestValidator(BankingDBContext context)
        {
            RuleFor(cuenta => cuenta.Estado).NotNull();
            RuleFor(cuenta => cuenta.TipoCuenta).IsInEnum().NotNull();
            RuleFor(cuenta => cuenta.SaldoInicial).GreaterThanOrEqualTo(0).NotNull();
            RuleFor(cuenta => cuenta.NumeroCuenta).Length(6).NotNull();
            RuleFor(cuenta => cuenta.IdentificacionCliente).NotNull().Must(identificacion => context.Clientes.Any(cliente => cliente.Persona.Identificacion == identificacion));
        }
    }
}
