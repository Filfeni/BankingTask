﻿using System;
using System.Collections.Generic;

namespace BankingTask.Data.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int Id { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }
        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
