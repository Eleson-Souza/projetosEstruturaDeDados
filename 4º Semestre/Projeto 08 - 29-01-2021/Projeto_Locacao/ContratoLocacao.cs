using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Locacao
{
    class ContratoLocacao
    {
        private int id;
        private int idCliente;
        private DateTime dataSaida;
        private DateTime dataEntrada;
        private List<Relacao> relacoes;
        private Stack<Item> itensDisponibilizados;

        public int Id { get => id; }
        public int IdCliente { get => idCliente; }
        public DateTime DataSaida { get => dataSaida; }
        public DateTime DataEntrada { get => dataEntrada; }

        public ContratoLocacao(int id, int idCliente, DateTime dataSaida, DateTime dataEntrada, List<Relacao> relacoes, Stack<Item> itensDisponibilizados)
        {
            this.idCliente = idCliente;
            this.dataSaida = dataSaida;
            this.dataEntrada = dataEntrada;
            this.relacoes = relacoes;
            this.itensDisponibilizados = itensDisponibilizados;
        }

        public bool Liberado()
        {
            return (itensDisponibilizados.Count > 0 ? true : false);
        }

        public void LiberacaoItens(List<Item> itens)
        {
            foreach (var item in itens)
            {
                this.itensDisponibilizados.Push(item);
            }
        }

        public void DadosContrato()
        {
            Console.WriteLine($"\n Id cliente: {idCliente} | Data Entrada: {dataEntrada.ToString()} | Data Saída: {dataSaida.ToString()}");

            foreach (var relacao in relacoes)
            {
                relacao.DadosRelacao();
            }

            //Stack<Item> itensDisponibilizadosAux = itensDisponibilizados;

            foreach (var itens in itensDisponibilizados)
            {
                itens.DadosItens();
            }
        }

        public override bool Equals(object obj)
        {
            var contrato = obj as ContratoLocacao;

            if (contrato.id == this.id)
            {
                return true;
            }

            return false;
        }

        internal void DevolverItens(Equipamentos equipamentos)
        {

            while (itensDisponibilizados.Count > 0)
            {
                Item item = itensDisponibilizados.Pop();
                Equipamento equipamento = equipamentos.Buscar(item.Equipamento);
                equipamento.ResgatarItens(item);
            }
        }

        public void DisponibilizarItens(Equipamentos equipamentos)
        {
            foreach (var relacao in relacoes)
            {
                Equipamento equipamento = equipamentos.Buscar(relacao.Equipamento);

                for (int i = 0; i < relacao.Quantidade; i++)
                {
                    itensDisponibilizados.Push(equipamento.LiberarItens());
                }
                
            }
        }

    }
}
