using System;
using System.Collections.Generic;

namespace AppControleAcesso.Models
{
    public partial class Logs
    {
        public int Id { get; set; }
        public DateTime DataAcesso { get; set; }
        public byte TipoAcesso { get; set; }
        public int? AmbientesId { get; set; }
    }
}
