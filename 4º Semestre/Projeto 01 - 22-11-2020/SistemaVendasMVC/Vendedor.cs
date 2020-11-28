using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SistemaVendasMVC
{
    class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas = new Venda[31];

        // construtores
        public Vendedor(int id, string nome, double percComissao, Venda[] asVendas)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;
            this.asVendas = asVendas;
        }
        
        public Vendedor()
        {
            this.id = 0;
            this.nome = "";
            this.percComissao = 0.00;

            Venda[] vendas = new Venda[31];
            for(int i=0; i<=31; i++)
            {
                vendas[i].Qtde = 0;
                vendas[i].Valor = 0.00;
            }
        }

        // getters e setters
        public int Id { 
            get => id; 
            set => id = value; 
        }
        public string Nome 
        { 
            get => nome; 
            set => nome = value; 
        }
        public double PercComissao 
        { 
            get => percComissao; 
            set => percComissao = value; 
        }
        internal Venda[] AsVendas 
        { 
            get => asVendas; 
            set => asVendas = value; 
        }

        // outros métodos
        public void registrarVenda(int dia, Venda venda)
        {
            asVendas[dia - 1] = venda;
        }

        public double valorVendas()
        {
            double valorTotal = 0;
            if(asVendas.Length > 0)
            {
                foreach(Venda v in asVendas)
                {
                    if(v != null)
                        valorTotal += v.Valor;
                }
            }

            return Math.Round(valorTotal, 2);
        }

        public double valorComissao()
        {
            double valorVendas = this.valorVendas();
            double valorComissao = Math.Round((valorVendas * (percComissao / 100)), 2);
            return valorComissao;
        }

        public double ValorMedioDiario()
        {
            // verifica a quantidade de vendas foram realizadas.
            int quantVendas = this.asVendas.Where(venda => (venda != null)).Count();

            double valorMedio = (this.valorVendas()) / quantVendas;
            return Math.Round(valorMedio, 2);
        }
    }
}
