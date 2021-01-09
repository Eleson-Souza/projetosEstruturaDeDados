using System;
using System.Collections.Generic;
using System.Text;

namespace AppMedicamento
{
    class Medicamento
    {
        private int id;
        private string nome;
        private string laboratorio;
        private Queue<Lote> lotes;

        // construtores
        public Medicamento(int id, string nome, string laboratorio)
        {
            this.id = id;
            this.nome = nome;
            this.laboratorio = laboratorio;
            this.lotes = new Queue<Lote>();
        }

        public Medicamento() : this(999, "", "")
        {
            this.lotes = new Queue<Lote>();
        }

        // getters e setters
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Laboratorio { get => laboratorio; set => laboratorio = value; }
        internal Queue<Lote> Lotes { get => lotes; set => lotes = value; }

        // outros métodos
        public int QtdeDisponivel()
        {
            int quantMedicamentos = 0;

            foreach(Lote lt in this.lotes)
            {
                quantMedicamentos += lt.Qtde;
            }

            return quantMedicamentos;
        }

        public void Comprar(Lote lote)
        {
            this.lotes.Enqueue(lote);
        }

        public bool Vender(int qtde)
        {
            if (QtdeDisponivel() < qtde)
                return false;

            foreach (Lote lts in this.lotes)
            {
                lts.Qtde -= qtde;

                if(lts.Qtde < 0)
                {
                    qtde = lts.Qtde * (-1);
                    lts.Qtde = 0;
                }
                else if(lts.Qtde >= 0)
                {
                    break;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return $"{this.id} - {this.nome} - {this.laboratorio} - {this.QtdeDisponivel()}";
        }

        public override bool Equals(Object obj)
        {
            return this.id.Equals(((Medicamento)obj).Id);
        }
    }
}
