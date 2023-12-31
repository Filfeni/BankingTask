﻿using System;
using System.Collections.Generic;

namespace BankingTask.Data.Entities
{
    public partial class Movimiento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoDisponible { get; set; }
        public int CuentaId { get; set; }

        public virtual Cuenta Cuenta { get; set; } = null!;
    }
}
