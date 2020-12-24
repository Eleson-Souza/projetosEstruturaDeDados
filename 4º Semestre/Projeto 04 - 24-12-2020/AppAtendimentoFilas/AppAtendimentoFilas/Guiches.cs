using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAtendimentoFilas
{
    class Guiches
    {
        private List<Guiche> listaGuiches;

        // construtor
        public Guiches()
        {
            this.listaGuiches = new List<Guiche>();
        }

        // getter e setter
        internal List<Guiche> ListaGuiches { get => listaGuiches; set => listaGuiches = value; }

        // outro método
        public void Adicionar(Guiche guiche)
        {
            this.listaGuiches.Add(guiche);
        }
    }
}
