using System;
using System.Collections.Generic;
using System.Text;

namespace AppMedicamento
{
    class Medicamentos
    {
        private List<Medicamento> listaMedicamentos;

        // constructor
        public Medicamentos()
        {
            this.listaMedicamentos = new List<Medicamento>();
        }

        // getter e setter
        internal List<Medicamento> ListaMedicamentos { get => listaMedicamentos; set => listaMedicamentos = value; }

        // outros metodos
        public void Adicionar(Medicamento medicamento)
        {
            this.listaMedicamentos.Add(medicamento);
        }

        public bool Deletar(Medicamento medicamento)
        {
            if (medicamento.QtdeDisponivel() > 0)
                return false;

            this.listaMedicamentos.Remove(medicamento);
            return true;
        }

        public Medicamento Pesquisar(Medicamento medicamento)
        {
            return this.listaMedicamentos.Find(med => med.Id == medicamento.Id);
        }
    }
}
