using BankingTask.Data.DTOs;
using FluentValidation;

namespace BankingTask.BusinessLogic.Validators
{
    public class CuentaPatchRequestValidator : AbstractValidator<CuentaPatchRequestDto>
    {
        public CuentaPatchRequestValidator()
        {
            RuleFor(cuenta => cuenta.Estado).NotNull();
            RuleFor(cuenta => cuenta.TipoCuenta).IsInEnum().NotNull();
        }
    }
}
