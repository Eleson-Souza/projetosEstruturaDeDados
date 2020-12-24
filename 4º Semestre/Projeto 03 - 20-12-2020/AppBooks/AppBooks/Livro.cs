using System;
using System.Collections.Generic;
using System.Text;

namespace AppBooks
{
    class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares;

        // construtores
        public Livro(int isbn, string titulo, string autor, string editora, List<Exemplar> exemplares)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            this.exemplares = exemplares;
        }

        public Livro() : this(0, "", "", "", new List<Exemplar> { })
        { }

        // getters e setters
        public int Isbn { get => isbn; set => isbn = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Editora { get => editora; set => editora = value; }
        internal List<Exemplar> Exemplares { get => exemplares; set => exemplares = value; }

        // outros métodos
        public void AdicionarExemplar(Exemplar exemplar)
        {
            this.exemplares.Add(exemplar);
        }

        public int QtdeExemplares()
        {
            return this.exemplares.Count;
        }

        public int QtdeDisponiveis()
        {
            int quantDisponiveis = 0;
            foreach(Exemplar exemplar in this.exemplares)
            {
                if (exemplar.Disponivel())
                    quantDisponiveis++;
            }

            return quantDisponiveis;
        }

        public int QtdeEmprestimos()
        {
            int quantTotalEmprestimos = 0;
            foreach(Exemplar exemplar in this.exemplares)
            {
                if (!exemplar.Disponivel())
                    quantTotalEmprestimos++;
            }

            return quantTotalEmprestimos;
        }

        public double PercDisponibilidade()
        {
            if (QtdeExemplares() == 0)
                return 0;

            return QtdeDisponiveis() / QtdeExemplares() * 100;
        }

        public override bool Equals(object obj)
        {
            return this.Isbn.Equals(((Livro)obj).Isbn);
        }
    }
}
