using System.ComponentModel.DataAnnotations;

namespace BankingTask.Data.Enums
{
    public enum TipoCuentaEnum
    {
        [Display(Name = "Ahorro")]
        Ahorro = 1,
        [Display(Name = "Corriente")]
        Corriente = 2
    }
}
