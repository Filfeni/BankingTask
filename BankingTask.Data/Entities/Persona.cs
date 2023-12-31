﻿using System;
using System.Collections.Generic;

namespace BankingTask.Data.Entities
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
