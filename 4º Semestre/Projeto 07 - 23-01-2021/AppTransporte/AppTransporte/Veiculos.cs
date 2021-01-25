using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTransporte
{
    class Veiculos
    {
        private List<Veiculo> veiculos;

        public List<Veiculo> ListaVeiculos { get => veiculos; }

        public Veiculos()
        {
            this.veiculos = new List<Veiculo>
            {
                new Veiculo(1, "AAA-0000", 10),
                new Veiculo(2, "BBB-1110", 15),
                new Veiculo(3, "CCC-2222", 20),
                new Veiculo(4, "DDD-3333", 25),
                new Veiculo(5, "EEE-4444", 30),
                new Veiculo(6, "FFF-5555", 35),
                new Veiculo(7, "GGG-6666", 40),
                new Veiculo(8, "HHH-7777", 45),
            };
        }

        public void incluir(Veiculo veiculo)
        {
            if (ListaVeiculos.Where(x => x.Equals(veiculo)).Count() > 0)
            {
                Console.WriteLine("Veiculo já cadastrado");
                return;
            }

            this.veiculos.Add(veiculo);
        }

        internal Veiculo pesquisar(int idVeiculo)
        {

            return veiculos.Where(x => x.Id == idVeiculo).SingleOrDefault();
        }
    }
}