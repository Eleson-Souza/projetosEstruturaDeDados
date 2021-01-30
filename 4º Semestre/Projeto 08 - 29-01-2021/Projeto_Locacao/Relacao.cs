using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Locacao
{
    class Relacao
    {
        private Equipamento equipamento;
        private int quantidade;

        public Equipamento Equipamento { get => equipamento; }
        public int Quantidade { get => quantidade; }

        public Relacao(Equipamento tipo, int quantidade)
        {
            this.equipamento = tipo;
            this.quantidade = quantidade;
        }

        public void DadosRelacao()
        {
            Console.WriteLine($" Tipo: {equipamento.Tipo} | Quantidade: {quantidade}");
        }
    }
}
