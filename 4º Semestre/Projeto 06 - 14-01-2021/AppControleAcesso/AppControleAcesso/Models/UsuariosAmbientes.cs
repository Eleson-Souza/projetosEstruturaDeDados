using System;
using System.Collections.Generic;

namespace AppControleAcesso.Models
{
    public partial class UsuariosAmbientes
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int AmbienteId { get; set; }
    }
}
