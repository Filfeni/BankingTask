using System.ComponentModel.DataAnnotations;
namespace BankingTask.API.Data.Enums
{
    public enum TipoCuentaEnum
    {
        [Display(Name = "Ahorro")]
        Ahorro = 1,
        [Display(Name = "Corriente")]
        Corriente = 2
    }
}
