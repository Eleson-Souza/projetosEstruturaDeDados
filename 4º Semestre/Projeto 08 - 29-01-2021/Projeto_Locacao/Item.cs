using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Locacao
{
    class Item
    {
        private Equipamento equipamento;
        private int patrimonio;
        private bool avaria;

        public int Patrimonio { get => patrimonio; }
        public bool Avaria { get => avaria; }
        public Equipamento Equipamento { get => equipamento; }

        public Item(Equipamento equipamento, int patrimonio, bool avaria)
        {
            this.equipamento = equipamento;
            this.patrimonio = patrimonio;
            this.avaria = avaria;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Item;

            if (item.patrimonio.Equals(this.patrimonio))
            {
                return true;
            }

            return false;
        }

        internal void DadosItens()
        {
            Console.WriteLine($" Tipo: {equipamento.Tipo} | Patrimônio: {patrimonio} | Avariado: {(avaria ? "sim" : "nao")}");
        }
    }
}
