using BankingTask.Data.DTOs;
using FluentValidation;

namespace BankingTask.BusinessLogic.Validators
{
    public class ReporteRequestValidator : AbstractValidator<ReporteRequestDto>
    {
        public ReporteRequestValidator()
        {
            RuleFor(dto => dto.Desde).NotNull();
            RuleFor(dto => dto.Hasta).NotNull();
            RuleFor(dto => dto.clienteId).GreaterThan(0).NotNull();
        }
    }
}
