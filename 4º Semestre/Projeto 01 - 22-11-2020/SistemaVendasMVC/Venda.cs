using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendasMVC
{
    class Venda
    {
        private int qtde;
        private double valor;

        // construtores
        public Venda(int qtde, double valor)
        {
            this.qtde = qtde;
            this.valor = valor;
        }

        public Venda() : this(0, 0.00)
        { }

        // setters e getters
        public int Qtde 
        { 
            get => qtde; 
            set => qtde = value; 
        }
        public double Valor 
        { 
            get => valor; 
            set => valor = value; 
        }

        public double ValorMedio()
        {
            return valor / qtde;
        }
    }
}
