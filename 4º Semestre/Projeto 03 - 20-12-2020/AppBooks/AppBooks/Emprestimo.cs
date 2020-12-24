using System;
using System.Collections.Generic;
using System.Text;

namespace AppBooks
{
    class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime dtDevolucao;

        // construtores
        public Emprestimo(DateTime dtEmprestimo)
        {
            this.dtEmprestimo = dtEmprestimo;
            this.dtDevolucao = default(DateTime);
        }
        public Emprestimo()
        {
            this.dtEmprestimo = default(DateTime);
            this.dtDevolucao = default(DateTime);
        }

        // getters e setters
        public DateTime DtEmprestimo { get => dtEmprestimo; set => dtEmprestimo = value; }
        public DateTime DtDevolucao { get => dtDevolucao; set => dtDevolucao = value; }
    }
}
