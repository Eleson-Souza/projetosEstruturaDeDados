using System;
using System.Collections.Generic;
using System.Text;

namespace AppBooks
{
    class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        // construtores
        public Exemplar(int tombo, List<Emprestimo> emprestimos)
        {
            this.tombo = tombo;
            this.emprestimos = emprestimos;
        }

        public Exemplar() : this(1, new List<Emprestimo>())
        { }

        // getters e setters
        public int Tombo { get => tombo; set => tombo = value; }
        public List<Emprestimo> Emprestimos { get => emprestimos; set => emprestimos = value; }

        // outros métodos
        public bool Emprestar()
        {
            if(this.Disponivel())
            {
                this.emprestimos.RemoveAt(emprestimos.Count - 1);
                this.emprestimos.Add(new Emprestimo(DateTime.Now));
                return true;
            }

            return false;            
        }

        public bool Devolver()
        {
            bool naoDevolvido = (emprestimos.Count > 0) && this.emprestimos[emprestimos.Count - 1].DtEmprestimo != default(DateTime);

            if(naoDevolvido)
            {
                this.emprestimos[emprestimos.Count - 1].DtDevolucao = DateTime.Now;
                this.emprestimos[emprestimos.Count - 1].DtEmprestimo = default(DateTime);
                return true;
            }

            return false;
        }

        public bool Disponivel()
        {
            return (emprestimos.Count > 0) ? this.emprestimos[emprestimos.Count - 1].DtEmprestimo == default(DateTime) : false;
        }

        public int QtdeEmprestimos()
        {
            return this.emprestimos.Count;
        }
    }
}
