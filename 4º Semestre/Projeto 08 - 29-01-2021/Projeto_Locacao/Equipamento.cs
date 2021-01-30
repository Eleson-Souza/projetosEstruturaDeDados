using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Locacao
{
    class Equipamento
    {
        private int id;
        private string tipo;
        private double valorLocacaoDiaria;
        private Queue<Item> itens;

        public int Id { get => id; }
        public string Tipo { get => tipo; }
        public Queue<Item> Itens { get => itens; }

        public Equipamento(int id, double valorLocacaoDiaria, string tipo, Queue<Item> itens)
        {
            this.id = id;
            this.tipo = tipo;
            this.valorLocacaoDiaria = valorLocacaoDiaria;
            this.itens = itens;
        }

        public void CadastrarItem(Item item)
        {
            foreach(var i in itens)
            {
                if (i.Equals(item))
                    throw new Exception($"Um item já foi cadastrado com o patrimônio {item.Patrimonio}. Insira um patrimônio diferente");
            }

            itens.Enqueue(item);

            Console.Clear();
            Console.WriteLine("\n Item Cadastrado com sucesso");
        }

        public Item LiberarItens()
        {
            return itens.Dequeue();
        }

        public void ResgatarItens(Item item)
        {
            itens.Enqueue(item);
        }

        public void ListarItens()
        {
            if (itens.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($" Itens do equipamento {tipo}");

                foreach (var item in itens)
                {
                    Console.WriteLine($"\n Patrimônio: {item.Patrimonio} | Avaria: {(item.Avaria ? "Sim" : "Não")}");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\n Não há itens cadastrados para este equipamento");
            }
        }

        public override bool Equals(object obj)
        {
            var equipamento = obj as Equipamento;

            if (equipamento.id == this.id)
            {
                return true;
            }

            return false;
        }
    }
}
