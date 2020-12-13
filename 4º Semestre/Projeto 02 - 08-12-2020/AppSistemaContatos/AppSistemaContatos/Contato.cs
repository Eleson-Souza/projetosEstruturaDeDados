using System;
using System.Collections.Generic;
using System.Text;

namespace AppSistemaContatos
{
    class Contato
    {
        private string email;
        private string nome;
        private string telefone;
        private Data dtNasc;

        // construtores
        public Contato(string email, string nome, string telefone, Data dtNasc)
        {
            this.email = email;
            this.nome = nome;
            this.telefone = telefone;
            this.dtNasc = dtNasc;
        }

        public Contato() : this("", "", "", new Data())
        { }

        // getters e setters
        public string Email { get => email; set => email = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public Data DtNasc { get => dtNasc; set => dtNasc = value; }

        // outros métodos
        public int getIdade()
        {
            int diaAtual = DateTime.Now.Day;
            int mesAtual = DateTime.Now.Month;
            int anoAtual = DateTime.Now.Year;

            int idade = anoAtual - this.dtNasc.Ano;

            // verificando se o contato ainda não fez aniversário no ano atual.
            if (this.dtNasc.Mes > mesAtual ||
                this.dtNasc.Mes == mesAtual && this.dtNasc.Dia > diaAtual)
            {
                idade = (anoAtual - 1) - this.dtNasc.Ano;
            }

            return idade;
        }

        // retornando uma string com todos os dados do contato
        public override string ToString()
        {
            return $"Nome...........: {this.nome}\n" +
                   $"Email..........: {this.email}\n" +
                   $"Telefone.......: {this.telefone}\n" +
                   $"Data Nascimento: {this.dtNasc.ToString()}" +
                   $"Idade: {this.getIdade()}";
        }

        public override bool Equals(object obj)
        {
            return this.Email.Equals(((Contato)obj).Email);
        }
    }
}
