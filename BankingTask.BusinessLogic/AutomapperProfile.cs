using AutoMapper;
using BankingTask.BusinessLogic.Extensions;
using BankingTask.Data.DTOs;
using BankingTask.Data.Entities;

namespace BankingTask.BusinessLogic
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ClientePostRequestDto, Cliente>();
            CreateMap<ClientePostRequestDto, Persona>();

            CreateMap<Cliente, ClienteResponseDto>()
                .ForMember(dest => dest.Nombre, act => act.MapFrom(src => src.Persona.Nombre))
                .ForMember(dest => dest.Genero, act => act.MapFrom(src => src.Persona.Genero))
                .ForMember(dest => dest.Edad, act => act.MapFrom(src => src.Persona.Edad))
                .ForMember(dest => dest.Identificacion, act => act.MapFrom(src => src.Persona.Identificacion))
                .ForMember(dest => dest.Direccion, act => act.MapFrom(src => src.Persona.Direccion))
                .ForMember(dest => dest.Telefono, act => act.MapFrom(src => src.Persona.Telefono));

            CreateMap<Cuenta, CuentaResponseDto>()
                .ForMember(dest => dest.Cliente, act => act.MapFrom(src => src.Cliente.Persona.Nombre))
                .ForMember(dest => dest.TipoCuenta, act => act.MapFrom(src => src.TipoCuenta.GetDisplayName()));
            CreateMap<CuentaPostRequestDto, Cuenta>();

            CreateMap<MovimientoRequestDto, Movimiento>();
            CreateMap<Movimiento, MovimientoResponseDto>()
                .ForMember(dest => dest.NumeroCuenta, act => act.MapFrom(src => src.Cuenta.NumeroCuenta))
                .ForMember(dest => dest.TipoMovimiento, act => act.MapFrom(src => TipoMovimientoDesc(src.TipoMovimiento)));

            CreateMap<Movimiento, ReporteResponseDto>()
                .ForMember(dest => dest.NumeroCuenta, act => act.MapFrom(src => src.Cuenta.NumeroCuenta))
                .ForMember(dest => dest.TipoMovimiento, act => act.MapFrom(src => TipoMovimientoDesc(src.TipoMovimiento)))
                .ForMember(dest => dest.SaldoInicial, act => act.MapFrom(src => src.SaldoDisponible - src.Valor));
        }
        public static string TipoMovimientoDesc(bool tipo) => tipo switch
        {
            true => "Credito",
            false => "Debito"
        };
    }
}
