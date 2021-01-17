using System;
using System.Collections.Generic;

namespace AppControleAcesso2.Models
{
    public partial class Ambientes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? UsuariosId { get; set; }
    }
}
