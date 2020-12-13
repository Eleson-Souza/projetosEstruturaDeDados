using System;
using System.Collections.Generic;
using System.Text;

namespace AppSistemaContatos
{
    class Data
    {
        private int dia;
        private int mes;
        private int ano;

        // construtores
        public Data(int dia, int mes, int ano)
        {
            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public Data() : this(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year)
        { }

        // getters e setters
        public int Dia { get => dia; set => dia = value; }
        public int Mes { get => mes; set => mes = value; }
        public int Ano { get => ano; set => ano = value; }

        // Outros métodos
        public void SetData(int dia, int mes, int ano)
        {
            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public override string ToString()
        {
            return $"{this.dia}/{this.mes}/{this.ano}";
        }
    }
}
