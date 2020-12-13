using System;
using System.Collections.Generic;
using System.Text;

namespace AppSistemaContatos
{
    class Contatos
    {
        private List<Contato> agenda;

        // construtores
        public Contatos(List<Contato> agenda)
        {
            this.agenda = agenda;
        }

        public Contatos()
        {
            this.agenda = new List<Contato>();
        }

        // getter (readonly)
        public List<Contato> Agenda { get => agenda; }

        // outros métodos
        public bool Adicionar(Contato c)
        {
            try
            {
                this.agenda.Add(c);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Contato Pesquisar(Contato c)
        {
            // buscando pelo email ou pela combinação de nome e data de nascimento.
            return this.agenda.Find(agenda => agenda.Email == c.Email || agenda.Nome == c.Nome);
        }

        public bool Alterar(Contato c)
        {
            int index = this.agenda.FindIndex(cont => cont.Email == c.Email || cont.Nome == c.Nome);

            if (index == -1)
                return false;
            
            this.agenda[index] = new Contato(c.Email, c.Nome, c.Telefone, c.DtNasc);
            return true;
        }

        public bool Remover(Contato c)
        {
            return this.agenda.Remove(c);
        }

        public void ListarContatos()
        {
            foreach(Contato c in this.agenda)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine(c.ToString());
            }
        }

        public List<Contato> ListarContatosGrid()
        {
            return this.agenda;
        }

        // implementar override do método Equals na classe Contato.
    }
}
