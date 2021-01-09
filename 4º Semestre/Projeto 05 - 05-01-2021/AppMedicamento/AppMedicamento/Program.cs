using System;
using System.Threading;

namespace AppMedicamento
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string opcao, nome, laboratorio;
                int id, quant, dia, mes, ano;

                Medicamentos medicamentos = new Medicamentos();

                do
                {
                    Console.WriteLine("0. Finalizar processo");
                    Console.WriteLine("1. Cadastrar medicamento");
                    Console.WriteLine("2. Consultar medicamento (apenas dados)");
                    Console.WriteLine("3. Consultar medicamento (dados + lotes)");
                    Console.WriteLine("4. Comprar medicamento (Cadastrar lote)");
                    Console.WriteLine("5. Vender medicamento");
                    Console.WriteLine("6. Listar medicamentos");

                    Console.Write("\n\nOpcao escolhida: ");
                    opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "0":
                            break;
                        case "1":
                            Console.Clear();
                            Console.WriteLine("CADASTRAR MEDIMENTO");

                            Console.Write("\nID.........: ");
                            id = int.Parse(Console.ReadLine());

                            // verificando se o ID do medicamento já existe.
                            var medEncontrado = medicamentos.Pesquisar(new Medicamento(id, "", ""));
                            if (medEncontrado != null)
                            {
                                Console.WriteLine($"\nO medicamento com esse ID já existe: {medEncontrado.Nome}!\nTente outro!\n");

                                Thread.Sleep(1500);
                                break;
                            }

                            Console.Write("Nome.......: ");
                            nome = Console.ReadLine();
                            Console.Write("Laboratório: ");
                            laboratorio = Console.ReadLine();

                            medicamentos.Adicionar(new Medicamento(id, nome, laboratorio));

                            Console.WriteLine("\n\nO medicamento '{0}' for adicionado com sucesso!", nome);

                            Thread.Sleep(1500);
                            Console.Clear();

                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("CONSULTAR MEDICAMENTO (APENAS DADOS)\n");

                            Console.Write("ID do medicamento: ");
                            id = int.Parse(Console.ReadLine());

                            medEncontrado = medicamentos.Pesquisar(new Medicamento(id, "", ""));

                            if (medEncontrado != null)
                            {
                                Console.WriteLine("\nDados");
                                Console.WriteLine($"   ID.........: {medEncontrado.Id}");
                                Console.WriteLine($"   Nome.......: {medEncontrado.Nome}");
                                Console.WriteLine($"   Laboratório: {medEncontrado.Laboratorio}\n");
                            }
                            else
                            {
                                Console.WriteLine("\nNenhum medicamento encontrado com esse ID!\n");
                            }

                            Thread.Sleep(2000);

                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("CONSULTAR MEDICAMENTO (DADOS + LOTES)\n");

                            Console.Write("ID do medicamento: ");
                            id = int.Parse(Console.ReadLine());

                            medEncontrado = medicamentos.Pesquisar(new Medicamento(id, "", ""));

                            if (medEncontrado != null)
                            {
                                Console.WriteLine("\nInformações do medicamento");
                                Console.WriteLine($"   ID.........: {medEncontrado.Id}");
                                Console.WriteLine($"   Nome.......: {medEncontrado.Nome}");
                                Console.WriteLine($"   Laboratório: {medEncontrado.Laboratorio}");
                                Console.WriteLine("\nInformações dos lotes");

                                if (medEncontrado.Lotes.Count == 0)
                                {
                                    Console.WriteLine("   Este medicamento ainda não tem nenhum lote!\n");
                                }
                                else
                                {
                                    foreach (Lote lote in medEncontrado.Lotes)
                                    {
                                        Console.WriteLine("-------------------------------------");
                                        Console.WriteLine($"   ID........: {lote.Id}");
                                        Console.WriteLine($"   Quantidade: {lote.Qtde}");
                                        Console.WriteLine($"   Vencimento: {lote.Venc}\n");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNenhum medicamento encontrado com esse ID!\n");
                            }

                            Thread.Sleep(2000);

                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("COMPRAR MEDICAMENTO (CADASTRAR LOTE)");

                            Console.Write("\nId Medicamento.....: ");
                            int idMed = int.Parse(Console.ReadLine());
                            Console.Write("Id Lote............: ");
                            int idLot = int.Parse(Console.ReadLine());
                            Console.Write("Quantidade.........: ");
                            quant = int.Parse(Console.ReadLine());
                            Console.WriteLine("Data Vencimento: ");
                            Console.Write("...............Dia > ");
                            dia = int.Parse(Console.ReadLine());
                            Console.Write("...............Mês > ");
                            mes = int.Parse(Console.ReadLine());
                            Console.Write("...............Ano > ");
                            ano = int.Parse(Console.ReadLine());

                            medEncontrado = medicamentos.Pesquisar(new Medicamento(idMed, "", ""));

                            if (medEncontrado != null)
                            {
                                // verificando se o lote já existe
                                bool loteExiste = false;

                                foreach (var lote in medEncontrado.Lotes)
                                {
                                    if (lote.Id == idLot)
                                    {
                                        loteExiste = true;
                                    }
                                }

                                if(!loteExiste)
                                {
                                    medEncontrado.Comprar(new Lote(idLot, quant, new DateTime(ano, mes, dia)));

                                    Console.WriteLine("\nLote adicionado na medicação '{0}' com sucesso!\n", medEncontrado.Nome);
                                }
                                else
                                {
                                    Console.WriteLine($"\nO lote com esse ID já existe!\nTente outro!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nO medicamento informado não foi encontrado!\n");
                            }

                            Thread.Sleep(2000);

                            break;
                        case "5":
                            Console.Clear();
                            Console.WriteLine("VENDER MEDICAMENTOS\n");

                            Console.Write("Id Medicamento: ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Qtd da Venda..: ");
                            quant = int.Parse(Console.ReadLine());

                            medEncontrado = medicamentos.Pesquisar(new Medicamento(id, "", ""));

                            if (medEncontrado != null)
                            {
                                bool vendido = medEncontrado.Vender(quant);

                                if (vendido)
                                {
                                    Console.WriteLine("\nMedicamento vendido com sucesso!");

                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("\nAtenção, não existe estoque suficiente dessa medicação para a quantidade solicitada!\n");

                                    Thread.Sleep(2000);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n\nO medicamento informado não foi encontrado!");

                                Thread.Sleep(2000);
                                Console.Clear();
                            }

                            break;
                        case "6":
                            Console.Clear();
                            Console.WriteLine("LISTAR MEDICAMENTOS\n");

                            if (medicamentos.ListaMedicamentos.Count == 0)
                            {
                                Console.WriteLine("Não há medicamentos para serem exibidos!");
                            }
                            else
                            {
                                foreach (Medicamento med in medicamentos.ListaMedicamentos)
                                {
                                    Console.WriteLine("-------------------------------------");
                                    Console.WriteLine("ID.........: " + med.Id);
                                    Console.WriteLine("Nome.......: " + med.Nome);
                                    Console.WriteLine("Laboratório: " + med.Laboratorio);
                                }
                            }

                            Console.WriteLine();
                            Thread.Sleep(2000);

                            break;
                        default:

                            break;
                    }
                } while (opcao != "0");
            }
            catch (Exception)
            {
                Console.WriteLine("\n\nHouve erro ao executar a aplicação, provavelmente você deve ter informado algum dado inconsistente!");
            }
        }
    }
}
