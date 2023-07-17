using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;
using FluentValidation;

namespace BankingTask.BusinessLogic.Validators
{
    public class ClientePostRequestValidator : AbstractValidator<ClientePostRequestDto>
    {
        public ClientePostRequestValidator(BankingDBContext context)
        {
            RuleFor(cliente => cliente.Contrasena).NotNull().MaximumLength(120);
            RuleFor(cliente => cliente.Direccion).NotNull().MaximumLength(255);
            RuleFor(cliente => cliente.Edad).NotNull().GreaterThan(0);
            RuleFor(cliente => cliente.Genero).NotNull().MaximumLength(50);
            RuleFor(cliente => cliente.Identificacion).NotNull().MaximumLength(10).Must(identificacion => !context.Clientes.Any(cliente => cliente.Persona.Identificacion == identificacion));
            RuleFor(cliente => cliente.Nombre).NotNull().MaximumLength(255);
            RuleFor(cliente => cliente.Telefono).NotNull().MaximumLength(15);
        }
    }
}
