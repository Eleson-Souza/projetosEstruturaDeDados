using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Locacao
{
    class ContratosLocacao
    {
        private List<ContratoLocacao> contratos;

        public ContratosLocacao(List<ContratoLocacao> contratos)
        {
            this.contratos = contratos;
        }

        public void Cadastrar(ContratoLocacao contrato)
        {
            if (IdJaCadastrado(contrato))
                throw new Exception("Um equipamento com este Id já foi cadastrado");

            contratos.Add(contrato);

        }

        private bool IdJaCadastrado(ContratoLocacao contrato)
        {
            if (contratos.Where(x => x.Equals(contrato)).Count() > 0)
                return true;

            return false;
        }

        public void ListarContratos()
        {
            foreach (var contrato in contratos)
            {
                contrato.DadosContrato();
            }
        }

        internal List<ContratoLocacao> ListarContratosLiberados()
        {
            return contratos.Where(x => x.Liberado()).ToList();
        }

        internal List<ContratoLocacao> PesquisarComFiltros(ContratoLocacao contrato)
        {
            List<ContratoLocacao> contratosPesquisados = contratos;

            if (contrato.IdCliente != 0)
            {
                contratosPesquisados = contratosPesquisados.Where(x => x.IdCliente.Equals(contrato.IdCliente)).ToList();
            }

            if (contrato.DataEntrada != DateTime.MinValue)
            {
                contratosPesquisados = contratosPesquisados.Where(x => x.DataEntrada.Equals(contrato.DataEntrada)).ToList();
            }

            if (contrato.DataSaida != DateTime.MinValue)
            {
                contratosPesquisados = contratosPesquisados.Where(x => x.DataEntrada.Equals(contrato.DataEntrada)).ToList();
            }

            return contratosPesquisados;
        }

        internal ContratoLocacao Buscar(ContratoLocacao contratoLocacao)
        {
            return contratos.Where(x => x.Equals(contratoLocacao)).FirstOrDefault();
        }
    }
}
