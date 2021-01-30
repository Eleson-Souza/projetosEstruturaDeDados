using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Locacao
{
    class Equipamentos
    {
        private List<Equipamento> equipamentos;

        public Equipamentos(List<Equipamento> equipamentos)
        {
            this.equipamentos = equipamentos;
        }

        public void Cadastrar(Equipamento equipamento)
        {
            if (IdJaCadastrado(equipamento))
                throw new Exception("Um equipamento com este Id já foi cadastrado");

            equipamentos.Add(equipamento);

        }

        public Equipamento Buscar(Equipamento equipamentoProcurado)
        {
            return equipamentos.Where(x => x.Equals(equipamentoProcurado)).SingleOrDefault();
        }

        public void ListarEquipamentos() {
            foreach(var equipamento in equipamentos)
            {
                Console.WriteLine($" ID: {equipamento.Id} | Equipamento: {equipamento.Tipo}");
            }
        }

        private bool IdJaCadastrado(Equipamento equipamento)
        {
            if (equipamentos.Where(x => x.Equals(equipamento)).Count() > 0)
                return true;

            return false;
        }
    }
}
