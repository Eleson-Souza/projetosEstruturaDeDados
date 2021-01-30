using System;
using System.Collections.Generic;

namespace Projeto_Locacao
{
    class Program
    {
        static void Main(string[] args)
        {
            Equipamentos equipamentos = new Equipamentos(new List<Equipamento>());
            ContratosLocacao contratos = new ContratosLocacao(new List<ContratoLocacao>());

            string opcao;

            do
            {
                Console.WriteLine();
                Console.WriteLine(" 0. Finalizar processo");
                Console.WriteLine(" 1. Cadastrar tipo de equipamento ");
                Console.WriteLine(" 2. Consultar tipo de equipamento");
                Console.WriteLine(" 3. Cadastrar equipamento");
                Console.WriteLine(" 4. Registrar Contrato de Locação");
                Console.WriteLine(" 5. Consultar Contratos de Locação");
                Console.WriteLine(" 6. Liberar de Contrato de Locação");
                Console.WriteLine(" 7. Consultar Contratos de Locação liberados");
                Console.WriteLine(" 8. Devolver equipamentos de Contrato de Locação liberado");
                Console.Write("\n Selecione uma opção: ");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        try
                        {
                            Console.Clear();

                            Console.Write("\n Insira um ID para o equipamento: ");
                            int idEquipamento = Convert.ToInt32(Console.ReadLine());

                            Console.Write("\n Insira o tipo de equipamento: ");
                            string tipoEquipamento = Console.ReadLine();

                            Console.Write("\n Insira o valor da locação diária: ");
                            double valorLocacaoDiaria = Convert.ToDouble(Console.ReadLine());

                            equipamentos.Cadastrar(new Equipamento(idEquipamento, valorLocacaoDiaria, tipoEquipamento, new Queue<Item>()));

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();
                            }
                        }
                        break;
                    case "2":
                        try
                        {
                            Console.Clear();

                            Console.WriteLine();
                            equipamentos.ListarEquipamentos();
                            Console.WriteLine();

                            Console.Write(" Insira um ID referente ao tipo de equipamento a ser consultado: ");
                            int idEquipamento = Convert.ToInt32(Console.ReadLine());

                            Equipamento equipamento = equipamentos.Buscar(new Equipamento(idEquipamento, 0, String.Empty, new Queue<Item>()));

                            if (equipamento != null)
                            {
                                equipamento.ListarItens();
                            }
                            else
                            {
                                Console.WriteLine(" Tipo de equipamento selecionado inexistente. Assegure-se de escolher um ID válido para o tipo de equipamento \n Item não cadastrado.");
                            }


                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();
                            }
                        }
                        break;

                    case "3":
                        try
                        {
                            Console.Clear();

                            Console.WriteLine();
                            equipamentos.ListarEquipamentos();

                            Console.Write("\n Selecione o tipo de equipamento através de um dos IDs disponibilizados acima: ");
                            int idTipo = Convert.ToInt32(Console.ReadLine());

                            Console.Write("\n Insira a ideintificação do item (Patrimônio): ");
                            int patrimonio = Convert.ToInt32(Console.ReadLine());

                            Equipamento equipamento = equipamentos.Buscar(new Equipamento(idTipo, 0, String.Empty, new Queue<Item>()));

                            if (equipamento != null)
                            {
                                equipamento.CadastrarItem(new Item(equipamento, patrimonio, false));
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(" Tipo de equipamento selecionado inexistente. Assegure-se de escolher um ID válido para o tipo de equipamento \n Item não cadastrado.");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();
                            }

                        }

                        break;
                    case "4":

                        try
                        {
                            Console.Clear();

                            List<Relacao> relacoes = new List<Relacao>();

                            Console.Write("\n Insira o ID do contrato: ");
                            int idContrato = Convert.ToInt32(Console.ReadLine());

                            Console.Write("\n Insira o código de identificação do cliente: ");
                            int idCliente = Convert.ToInt32(Console.ReadLine());

                            Console.Write("\n Insira a data de saída do item (no formato 01/01/2020): ");
                            string dataSaida = Console.ReadLine();

                            Console.Write("\n Insira a data de entrada do item (no formato 01/01/2020): ");
                            string dataEntrada = Console.ReadLine();

                            string[] dataAux = dataSaida.Split('/');
                            DateTime dataSaidaData = new DateTime(Convert.ToInt32(dataAux[2]), Convert.ToInt32(dataAux[1]), Convert.ToInt32(dataAux[0]), 0, 0, 0);

                            dataAux = dataEntrada.Split('/');
                            DateTime dataEntradaData = new DateTime(Convert.ToInt32(dataAux[2]), Convert.ToInt32(dataAux[1]), Convert.ToInt32(dataAux[0]), 0, 0, 0);

                            int opcaoEquipamento;
                            int quantidadeEquipamento;

                            do
                            {
                                Console.Clear();

                                equipamentos.ListarEquipamentos();

                                Console.Write("\n Escolha um tipo de equipamento para adicionar ao contrato ou 0 para sair da inclusão de equipamentos: ");
                                opcaoEquipamento = Convert.ToInt32(Console.ReadLine());

                                if (opcaoEquipamento != 0)
                                {
                                    Console.Write("\n Insira a quantidade necessária: ");
                                    quantidadeEquipamento = Convert.ToInt32(Console.ReadLine());

                                    Equipamento equipamentoBuscado = equipamentos.Buscar(new Equipamento(opcaoEquipamento, 0, String.Empty, new Queue<Item>()));

                                    if (quantidadeEquipamento > equipamentoBuscado.Itens.Count)
                                    {
                                        throw new Exception($"Atualmente o estoque conta com {equipamentoBuscado.Itens.Count} {equipamentoBuscado.Tipo}. Refaça o contrato com a quantidade adequada".ToUpper());
                                    }

                                    if (equipamentoBuscado != null)
                                    {
                                        relacoes.Add(new Relacao(equipamentoBuscado, quantidadeEquipamento));
                                    }
                                    else
                                    {

                                    }

                                }

                            } while (opcaoEquipamento != 0);

                            contratos.Cadastrar(new ContratoLocacao(idContrato, idCliente, dataSaidaData, dataEntradaData, relacoes, new Stack<Item>()));
                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else if (ex.Message.Contains("Atualmente".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();
                            }

                        }


                        break;
                    case "5":

                        try
                        {
                            Console.Clear();

                            DateTime dataEntradaAux = DateTime.MinValue;
                            DateTime dataSaidaAux = DateTime.MinValue;
                            string[] dataAux;

                            Console.WriteLine(" == insira apenas os valores pelos quais deseja pesquisar == ");
                            Console.WriteLine(" == caso não queira utilizar algum dos filtros, deixe em branco e pressione enter == ");
                            Console.WriteLine(" == caso não preencha nenhum filtro, todos medicamentos serão retornados == ");
                            Console.WriteLine();

                            Console.Write("Insira o ID do contrato a ser buscado: ");
                            string idContratoAux = Console.ReadLine();
                            int idContrato = String.IsNullOrEmpty(idContratoAux) ? 0 : Convert.ToInt32(idContratoAux);

                            Console.Write("Insira o ID cliente vinculado ao contrato a ser buscado: ");
                            string idClienteAux = Console.ReadLine();
                            int idCliente = String.IsNullOrEmpty(idClienteAux) ? 0 : Convert.ToInt32(idClienteAux);

                            Console.Write("Insira a data de saída dos itens: ");
                            string dataSaida = Console.ReadLine();

                            Console.Write("Insira a data de entrada dos itens: ");
                            string dataEntrada = Console.ReadLine();

                            if (!String.IsNullOrEmpty(dataSaida))
                            {
                                dataAux = dataEntrada.Split('/');
                                dataEntradaAux = new DateTime(Convert.ToInt32(dataAux[2]), Convert.ToInt32(dataAux[1]), Convert.ToInt32(dataAux[0]));
                            }

                            if (!String.IsNullOrEmpty(dataSaida))
                            {
                                dataAux = dataEntrada.Split('/');
                                dataSaidaAux = new DateTime(Convert.ToInt32(dataAux[2]), Convert.ToInt32(dataAux[1]), Convert.ToInt32(dataAux[0]));
                            }

                            List<ContratoLocacao> contratosPesquisados = contratos.PesquisarComFiltros(new ContratoLocacao(idContrato, idCliente, dataSaidaAux, dataEntradaAux, new List<Relacao>(), new Stack<Item>()));

                            if (contratosPesquisados.Count <= 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Não foram encontrados resultados para a pesquisa");
                            }
                            else
                            {
                                Console.Clear();

                                foreach (var r in contratosPesquisados)
                                {
                                    r.DadosContrato();
                                }

                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("01/01/2001".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else if (ex.Message.Contains(" Não há um medicamento registrado com o ID".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();
                            }
                        }


                        break;
                    case "6":

                        try
                        {
                            Console.Write("Insira o ID do contrato a ser liberado: ");
                            int idContrato = Convert.ToInt32(Console.ReadLine());

                            ContratoLocacao contratoEncontrado = contratos.Buscar(new ContratoLocacao(idContrato, 0, DateTime.MinValue, DateTime.MinValue, new List<Relacao>(), new Stack<Item>()));

                            if (contratoEncontrado != null)
                            {
                                contratoEncontrado.DisponibilizarItens(equipamentos);
                                Console.Clear();
                                Console.WriteLine("Contrato liberado");
                            }
                            else
                            {
                                Console.WriteLine("Contrato não encontrado");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                            Console.WriteLine();

                        }

                        break;
                    case "7":

                        try
                        {
                            Console.Clear();

                            //Console.WriteLine(" == insira apenas os valores pelos quais deseja pesquisar == ");
                            //Console.WriteLine(" == caso não queira utilizar algum dos filtros, deixe em branco e pressione enter == ");
                            //Console.WriteLine(" == caso não preencha nenhum filtro, todos medicamentos serão retornados == ");
                            //Console.WriteLine();

                            //Console.Write("Insira o ID do contrato a ser buscado: ");
                            //int idContrato = Convert.ToInt32(Console.ReadLine());

                            //Console.Write("Insira o ID cliente vinculado ao contrato a ser buscado: ");
                            //int idCliente = Convert.ToInt32(Console.ReadLine());

                            //Console.Write("Insira a data de saída dos itens: ");
                            //string dataSaida = Console.ReadLine();

                            //Console.Write("Insira a data de entrada dos itens: ");
                            //string dataEntrada = Console.ReadLine();

                            //string[] dataAux = dataEntrada.Split('/');
                            //DateTime dataEntradaAux = new DateTime(Convert.ToInt32(dataAux[2]), Convert.ToInt32(dataAux[1]), Convert.ToInt32(dataAux[0]));

                            //dataAux = dataEntrada.Split('/');
                            //DateTime dataSaidaAux = new DateTime(Convert.ToInt32(dataAux[2]), Convert.ToInt32(dataAux[1]), Convert.ToInt32(dataAux[0]));

                            List<ContratoLocacao> contratosPesquisados = contratos.ListarContratosLiberados();

                            if (contratosPesquisados.Count <= 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Não foram encontrados contratos liberados");
                            }
                            else
                            {
                                Console.Clear();

                                foreach (var r in contratosPesquisados)
                                {
                                    r.DadosContrato();
                                }

                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("01/01/2001".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else if (ex.Message.Contains(" Não há um medicamento registrado com o ID".ToUpper()))
                            {
                                Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();
                            }
                        }


                        break;
                    case "8":

                        try
                        {
                            //Console.Clear();

                            Console.Write("Insira o ID do contrato para a devolução dos itens: ");
                            int idContrato = Convert.ToInt32(Console.ReadLine());

                            ContratoLocacao contratoEncontrado = contratos.Buscar(new ContratoLocacao(idContrato, 0, DateTime.MinValue, DateTime.MinValue, new List<Relacao>(), new Stack<Item>()));

                            if (contratoEncontrado != null)
                            {
                                contratoEncontrado.DevolverItens(equipamentos);
                                Console.WriteLine("Itens devolvidos");
                            }
                            else
                            {
                                Console.WriteLine("Contrato não encontrado");
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                            Console.WriteLine();

                        }

                        break;
                }

            } while (!opcao.Equals("0"));
        }
    }
}
