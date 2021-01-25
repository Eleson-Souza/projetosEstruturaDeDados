using System;
using System.Collections.Generic;
using System.Threading;

namespace ProjetoTransporte
{
    class Program
    {
        static void Main(string[] args)
        {
            string opcao, nome, idInserido;
            int id, idAux;

            Garagens garagens = new Garagens();
            Veiculos veiculos = new Veiculos();
            Viagens viagens = new Viagens();

            do
            {
                Console.WriteLine();
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar Veiculo");
                Console.WriteLine("2. Cadastrar Garagem");
                Console.WriteLine("3. Iniciar Jornada");
                Console.WriteLine("4. Encerrar Jornada");
                Console.WriteLine("5. Liberar viagem de uma determinada origem para um determinado destino");
                Console.WriteLine("6. Listar veículos em determinada garagem");
                Console.WriteLine("7. Informar qtde de viagens efetuadas de uma determinada origem para um determinado destino");
                Console.WriteLine("8. Listar viagens efetuadas de uma determinada origem para um determinado destino");
                Console.WriteLine("9. Informar qtde de passageiros transportados de uma determinada origem para um determinado destino");
                Console.WriteLine();
                Console.Write("Selecione uma opção: ");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("1. Cadastrar Veiculo\n");

                            if (!garagens.JornadaAtiva)
                            {
                                Console.Write("ID do veículo...........: ");
                                int idVeiculo = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Placa do veículo........: ");
                                string placa = Console.ReadLine();

                                Console.Write("Lotação máxima do veículo: ");
                                int lotacao = Convert.ToInt32(Console.ReadLine());

                                veiculos.incluir(new Veiculo(idVeiculo, placa, lotacao));

                                Console.WriteLine("\nCadastro realizado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("\nCadastro não pode ser realizado enquanto a jornada está ativa!");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();
                            Console.WriteLine();

                            if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                            {
                                Console.WriteLine($"{ex.Message}".ToUpper());
                            }
                            else
                            {
                                Console.WriteLine("\nO valor ID e Lotação Máxima aceitam apenas números.".ToUpper());
                            }

                        }

                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case "2":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("2. Cadastrar Garagem\n");

                            if (!garagens.JornadaAtiva)
                            {
                                Console.Write("ID da garagem: ");
                                int idGaragem = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Local da garagem: ");
                                string local = Console.ReadLine();

                                garagens.incluir(new Garagem(idGaragem, local, new Stack<Veiculo>()));

                                Console.WriteLine("\nGaragem adicionada com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("\nCadastro não pode ser realizado enquanto a jornada está ativa!");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                            {
                                Console.WriteLine($"\n{ex.Message}".ToUpper());
                            }
                            else
                            {
                                Console.WriteLine("\nO valor ID aceita apenas números.".ToUpper());
                            }

                            Console.WriteLine();

                        }

                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case "3":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("3. Iniciar Jornada\n");

                            if (!garagens.JornadaAtiva)
                            {
                                DistribuirVeiculos(garagens, veiculos);

                                garagens.iniciarJornada();

                                Console.WriteLine("\nJornada iniciada com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("\nA jornada já se encontra iniciada!");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine($"{ex.Message}".ToUpper());
                            Console.WriteLine();

                        }

                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case "4":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("4. Encerrar Jornada\n");

                            if (garagens.JornadaAtiva)
                            {
                                foreach (var transporte in garagens.encerrarJornada())
                                {
                                    Veiculo veiculo = transporte.Veiculo;

                                    Console.WriteLine($"Placa: {veiculo.Placa} | passageiros transportados: {veiculo.Lotacao}");
                                }

                                Console.WriteLine("\nJornada encerrada com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("\nA jornada já se encontra encerrada!");
                            }


                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("\nHouve um erro: " + ex.Message);
                        }

                        Thread.Sleep(2000);
                        break;
                    case "5":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("5. Liberar viagem de uma determinada origem para um determinado destino\n");

                            if (garagens.JornadaAtiva)
                            {
                                if (garagens.ListaGaragens.Count > 1)
                                {
                                    Console.Write("ID da garagem de origem: ");
                                    int idOrigem = Convert.ToInt32(Console.ReadLine());
                                    Garagem garagemOrigem = garagens.pesquisar(idOrigem);

                                    Console.Write("ID da garagem de destino: ");
                                    int idDestino = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("ID do veículo da viagem: ");
                                    int idVeiculo = Convert.ToInt32(Console.ReadLine());


                                    Garagem garagemDestino = garagens.pesquisar(idDestino);
                                    Veiculo veiculoViagem = veiculos.pesquisar(idVeiculo);

                                    viagens.incluir(new Viagem(garagemOrigem, garagemDestino, veiculoViagem));

                                    Console.WriteLine("\nOperação realizada com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("\nNúmero de garagens insuficientes para iniciar uma viagem!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nA liberação desta viagem não é possível, pois a jornada não está em andamento.");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nHouve um erro: " + ex.Message);
                        }

                        Thread.Sleep(2500);
                        Console.Clear();
                        break;
                    case "6":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("6. Listar veículos em determinada garagem\n");

                            Console.Write("ID da garagem: ");
                            int idGaragem = Convert.ToInt32(Console.ReadLine());

                            Garagem garagemConsultada = garagens.pesquisar(idGaragem);

                            if (garagemConsultada != null)
                            {
                                Console.Clear();
                                Console.WriteLine($"\nQuantidade de veículos: {garagemConsultada.qtdeDeVeiculos().ToString()}\nPotencial de transporte: {garagemConsultada.potencialDeTransporte().ToString()}");
                            }
                            else
                            {
                                Console.WriteLine("\nGaragem não encontrada");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine("\nO valor ID aceita apenas números. == ".ToUpper());
                            Console.WriteLine();

                        }

                        Thread.Sleep(2000);
                        break;
                    case "7":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("7. Informar qtde de viagens efetuadas de uma determinada origem para um determinado destino\n");

                            Console.WriteLine(viagens.listarQuantidadeViagens());
                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine("\nO valor ID aceita apenas números.".ToUpper());
                            Console.WriteLine();

                        }

                        Thread.Sleep(2000);
                        break;
                    case "8":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("8. Listar viagens efetuadas de uma determinada origem para um determinado destino\n");

                            if (viagens.FilaViagens.Count > 0)
                            {
                                Console.WriteLine(viagens.listarViagensEfetuadas());
                            }
                            else
                            {
                                Console.WriteLine("\nNão houveram viagens.");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine("\nO valor ID aceita apenas números.".ToUpper());
                            Console.WriteLine();

                        }

                        Thread.Sleep(2000);
                        break;
                    case "9":

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("9. Informar qtde de passageiros transportados de uma determinada origem para um determinado destino\n");

                            if (viagens.FilaViagens.Count > 0)
                            {
                                Console.WriteLine(viagens.listarQuantidadeTransportada());
                            }
                            else
                            {
                                Console.WriteLine("\nNão houveram viagens.");
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.Clear();

                            Console.WriteLine("\nO valor ID aceita apenas números.".ToUpper());
                            Console.WriteLine();

                        }

                        Thread.Sleep(2000);
                        break;

                }

            } while (!opcao.Equals("0"));
        }

        private static void DistribuirVeiculos(Garagens garagens, Veiculos veiculos)
        {
            int indice = 0;
            int qtdGaragens = garagens.ListaGaragens.Count;

            foreach (var veiculo in veiculos.ListaVeiculos)
            {
                if (indice >= qtdGaragens)
                    indice = 0;

                garagens.ListaGaragens[indice].Veiculos.Push(veiculo);

                indice++;
            }
        }
    }
}