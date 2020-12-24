using System;
using System.Collections.Generic;
using System.Text;

namespace AppBooks
{
    class Livros
    {
        private List<Livro> acervo;

        //construtores
        public Livros(List<Livro> acervo)
        {
            this.acervo = acervo;
        }

        public Livros() : this(new List<Livro> { })
        { }

        // getter e setter
        internal List<Livro> Acervo { get => acervo; set => acervo = value; }

        // outros métodos
        public void Adicionar(Livro livro)
        {
            this.acervo.Add(livro);
        }

        public Livro Pesquisar(Livro livro)
        {
            return this.acervo.Find(item => item.Isbn == livro.Isbn);
        }
    }
}
