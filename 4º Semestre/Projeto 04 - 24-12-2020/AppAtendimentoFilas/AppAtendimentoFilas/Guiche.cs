using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAtendimentoFilas
{
    class Guiche
    {
        private int id;
        private Queue<Senha> atendimentos;

        //construtores
        public Guiche(int id)
        {
            this.id = id;
            this.atendimentos = new Queue<Senha>();
        }

        public Guiche() : this(0)
        { }

        // getters e setters
        public int Id { get => id; set => id = value; }
        internal Queue<Senha> Atendimentos { get => atendimentos; set => atendimentos = value; }

        // outros métodos
        public bool Chamar(Queue<Senha> filaSenhas)
        {
            if(filaSenhas.Count > 0)
            {
                this.atendimentos.Enqueue(filaSenhas.Dequeue());
                this.atendimentos.Last().DataAtend = DateTime.Now.ToString("dd/MM/yyyy");
                this.atendimentos.Last().HoraAtend = DateTime.Now.ToString("HH:mm:ss");
                return true;
            }

            return false;
        }
    }
}
