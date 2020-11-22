using System;

namespace SistemaVendasMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            int idAuto = 1, idVendedor, quantVendas;
            string nome;
            double percComissao, valorVendas;

            Console.WriteLine("SISTEMA DE VENDEDORES\n");

            Vendedores vendedores = new Vendedores();

            int opcao = 1;
            while (opcao != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");

                Console.Write("\nInforme a opção escolhida: ");
                opcao = int.Parse(Console.ReadLine());

                Console.WriteLine("");
                switch (opcao)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("CADASTRO DE VENDEDOR");

                        Console.Write("Nome......................: ");
                        nome = Console.ReadLine();
                        Console.Write("Percentual de comissão (%): ");
                        percComissao = double.Parse(Console.ReadLine());

                        bool foiCadastrado = vendedores.addVendedor(new Vendedor(idAuto, nome, percComissao, new Venda[31]));
                        idAuto++; // incrementando o id.

                        if(foiCadastrado)
                        {
                            Console.WriteLine("\nCadastro realizado com sucesso!");
                        } else
                        {
                            Console.WriteLine("\nErro ao cadastrar, número máximo de vendedores excedido!");
                        }
                        break;
                    case 2:
                        Console.WriteLine("CONSULTAR VENDEDOR");

                        Console.Write("ID do vendedor......................: ");
                        idVendedor = int.Parse(Console.ReadLine());

                        Vendedor vendedorEncontrado = vendedores.searchVendedor(new Vendedor(idVendedor, "", 0.00, new Venda[31]));

                        if(vendedorEncontrado.Id != -1)
                        {
                            Console.WriteLine("\nDados do Vendedor encontrado (mensal):");
                            Console.WriteLine("ID: {0}", vendedorEncontrado.Id);
                            Console.WriteLine("Nome: {0}", vendedorEncontrado.Nome);
                            Console.WriteLine("Valor total vendas: R$ {0}", vendedorEncontrado.valorVendas());
                            Console.WriteLine("Valor comissão: R$ {0}", vendedorEncontrado.valorComissao());
                            Console.WriteLine("Valor médio das vendas diárias: {0}");
                        } else
                        {
                            Console.WriteLine("\nVendedor não encontrado!");
                        }
                        break;
                    case 3:
                        Console.WriteLine("EXCLUIR VENDEDOR");

                        Console.Write("ID do vendedor......................: ");
                        idVendedor = int.Parse(Console.ReadLine());

                        bool vendedorExcluido = vendedores.delVendedor(new Vendedor(idVendedor, "", 0.00, new Venda[31]));
                        if(vendedorExcluido)
                        {
                            Console.WriteLine("\nVendedor excluído com sucesso!");
                        } else
                        {
                            Console.WriteLine("\nVendedor não excluído, ou não existe esse vendedor ou o mesmo possui vendas em seu nome!");
                        }
                        break;
                    case 4:
                        Console.WriteLine("REGISTRAR VENDA (Diária)");

                        Console.Write("ID do vendedor......................: ");
                        idVendedor = int.Parse(Console.ReadLine());
                        Console.Write("Informe a quantidade de vendas realizadas: ");
                        quantVendas = int.Parse(Console.ReadLine());
                        Console.Write("Informe o valor total das vendas.........: R$ ");
                        valorVendas = double.Parse(Console.ReadLine());

                        vendedorEncontrado = vendedores.searchVendedor(new Vendedor(idVendedor, "", 0.00, new Venda[31]));

                        if (vendedorEncontrado.Id != -1)
                        {
                            int dia = 0;
                            for (int i = 0; i <= 31; i++)
                            {
                                if (vendedores.OsVendedores[vendedorEncontrado.Id - 1].AsVendas[i] == null)
                                {
                                    dia = i + 1;
                                    break;
                                }
                            }

                            vendedores.OsVendedores[vendedorEncontrado.Id - 1].registrarVenda(dia, new Venda(quantVendas, valorVendas));

                            Console.WriteLine("\nVenda registrada com sucesso!");
                        }
                        else
                            Console.WriteLine("\nVendedor informado não existe no sistema!");

                        break;
                    case 5:
                        Console.WriteLine("LISTAGEM DE VENDEDORES\n");

                        Vendedor[] todosVendedores = vendedores.listarVendedores();

                        foreach(Vendedor v in todosVendedores)
                        {
                            if(v != null)
                            {
                                Console.WriteLine("ID: {0}", v.Id);
                                Console.WriteLine("Nome: {0}", v.Nome);
                                Console.WriteLine();
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("COMANDO INVÁLIDO, TENTE NOVAMENTE!");
                        break;
                } 
            }           
        }
    }
}
