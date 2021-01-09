using System;
using System.Collections.Generic;
using System.Text;

namespace AppMedicamento
{
    class Lote
    {
        private int id;
        private int qtde;
        private DateTime venc;

        // construtores
        public Lote(int id, int qtde, DateTime venc)
        {
            this.id = id;
            this.qtde = qtde;
            this.venc = venc;
        }

        public Lote() : this(1, 1, DateTime.Now)
        { }

        // getters e setters
        public int Id { get => id; set => id = value; }
        public int Qtde { get => qtde; set => qtde = value; }
        public DateTime Venc { get => venc; set => venc = value; }

        // outros métodos
        public string ToString()
        {
            return $"{this.id} - {this.qtde} - {this.venc}";
        }
    }
}
