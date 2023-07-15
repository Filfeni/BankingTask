using BankingTask.API.Data.Enums;
using System;
using System.Collections.Generic;

namespace BankingTask.API.Data.Entities
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public TipoCuentaEnum TipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
