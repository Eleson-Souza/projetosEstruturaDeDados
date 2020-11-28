using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendasMVC
{
    class Vendedores
    {
        private Vendedor[] osVendedores = new Vendedor[10];
        private int max;
        private int qtde;

        public Vendedores(Vendedor[] osVendedores, int max, int qtde)
        {
            this.osVendedores = osVendedores;
            this.max = max;
            this.qtde = qtde;
        }

        public Vendedores() : this(new Vendedor[10], 10, 0)
        { }

        // getters e setters
        public int Qtde { get => qtde; set => qtde = value; }
        public Vendedor[] OsVendedores { get => osVendedores; set => osVendedores = value; }
        public int Max { get => max; set => max = value; }

        public bool addVendedor(Vendedor v)
        {
            bool podeAdicionar = (this.qtde <= this.max);
            if(podeAdicionar)
            {
                this.osVendedores[this.qtde] = v;
                this.qtde++;
            }

            return podeAdicionar;
        }

        public bool delVendedor(Vendedor v)
        {
            int contVendas = 0;
            bool podeDeletar = (this.qtde > 0);
            if(podeDeletar)
            {
                foreach(Vendedor ven in this.osVendedores)
                {
                    if (ven != null && ven.Id == v.Id)
                    {
                        // antes de excluir, verifica se o array de vendas do vendedor encontrado está nulo/vazio.
                        for (int i = 0; i < ven.AsVendas.Length; i++)
                        {
                            // se existirem conteudos no array...
                            if (ven.AsVendas[i] != null)
                                contVendas++;
                        }
                        // se não existirem vendas...
                        if (contVendas == 0)
                        {
                            ven.Id = -1;
                            ven.Nome = "";
                            ven.PercComissao = 0.00;
                        }
                        else
                            podeDeletar = false;
                    }
                    else
                        podeDeletar = false;
                }
            }

            return podeDeletar;
        }

        public Vendedor searchVendedor(Vendedor v)
        {
            Vendedor vendedorEncontrado = new Vendedor(-1, "", 0.00, new Venda[31]);

            foreach(Vendedor ve in this.osVendedores)
            {
                if (ve != null)
                    if(ve.Id == v.Id)
                        vendedorEncontrado = ve;
            }

            return vendedorEncontrado;
        }

        public double valorVendas()
        {
            double valorTotalVendas = 0;
            foreach (Vendedor vendedor in this.osVendedores)
            {
                // se o elemento do array de vendedores obtiver dados... percorrerá suas vendas mensais.
                if(vendedor != null)
                {
                    foreach (Venda venda in vendedor.AsVendas)
                    {
                        // se o elemento do array de vendas obtiver dados...  fará a totalização de suas vendas mensais.
                        if (venda != null)
                            valorTotalVendas += venda.Valor;
                    }
                }
            }

            return Math.Round(valorTotalVendas, 2);
        }

        public double valorComissao()
        {
            double valorTotalComissao = 0;
            foreach(Vendedor vendedor in osVendedores)
            {
                if(vendedor != null)
                {
                    foreach (Venda venda in vendedor.AsVendas)
                    {
                        if (venda != null)
                            valorTotalComissao += (venda.Valor * (vendedor.PercComissao / 100));
                    }
                }
            }

            return Math.Round(valorTotalComissao, 2);
        }

        public Vendedor[] listarVendedores()
        {
            return this.osVendedores;
        }
    }
}
