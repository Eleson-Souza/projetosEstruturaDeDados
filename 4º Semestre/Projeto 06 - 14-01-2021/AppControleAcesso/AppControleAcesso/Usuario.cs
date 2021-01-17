using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleAcesso
{
    class Usuario
    {
        private int id;
        private string nome;
        private List<Ambiente> ambientes;

        public Usuario(int id, string nome, List<Ambiente> ambientes)
        {
            this.id = id;
            this.nome = nome;
            this.ambientes = ambientes;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }

        public bool ConcederPermissao(Ambiente ambiente)
        {
            bool possuiAmbiente = this.ambientes.Find(amb => amb.Id == ambiente.Id) != null;

            // se já possui o ambiente na lista retorna false, caso não possua será adicionado e retorna true.
            if (possuiAmbiente)
                return false;

            this.ambientes.Add(ambiente);
            return true;
        }

        public bool RevogarPermissao(Ambiente ambiente)
        {
            bool possuiAmbiente = this.ambientes.Find(amb => amb.Id == ambiente.Id) != null;

            // se possui o ambiente na lista, será revogado (removido) e retorna true. Se não tiver retorna false.
            if(possuiAmbiente)
            {
                this.ambientes.Remove(ambiente);
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return this.id.Equals(((Usuario)obj).Id);
        }
    }
}
