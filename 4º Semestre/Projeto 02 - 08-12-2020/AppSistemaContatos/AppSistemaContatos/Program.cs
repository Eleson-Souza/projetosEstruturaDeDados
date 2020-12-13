using System;
using System.Threading;

namespace AppSistemaContatos
{
    class Program
    {
        static void Main(string[] args)
        {
            string opcao, nome, email, telefone;
            int dia, mes, ano;
            Contatos contatos = new Contatos();

            do
            {
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("| 0. Sair                            |");
                Console.WriteLine("| 1. Adicionar Contato               |");
                Console.WriteLine("| 2. Pesquisar Contato               |");
                Console.WriteLine("| 3. Alterar Contato                 |");
                Console.WriteLine("| 4. Remover Contato                 |");
                Console.WriteLine("| 5. Listar Contatos                 |");
                Console.WriteLine("--------------------------------------");

                Console.Write("\nInforme a opção escolhida: ");
                opcao = Console.ReadLine();

                Console.WriteLine("");
                switch (opcao)
                {
                    case "0":
                        break;
                    case "1":
                        Console.WriteLine("ADICIONANDO CONTATO");

                        Console.Write("Nome.....: ");
                        nome = Console.ReadLine();
                        Console.Write("Email....: ");
                        email = Console.ReadLine();
                        Console.Write("Telefone.: ");
                        telefone = Console.ReadLine();
                        Console.WriteLine("Data Nasc: ");
                        Console.Write("........Dia: ");
                        dia = int.Parse(Console.ReadLine());
                        Console.Write("........Mês: ");
                        mes = int.Parse(Console.ReadLine());
                        Console.Write("........Ano: ");
                        ano = int.Parse(Console.ReadLine());

                        contatos.Adicionar(new Contato(email, nome, telefone, new Data(dia, mes, ano)));

                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("PESQUISANDO CONTATO (Busca por email ou nome)");

                        Console.Write("Nome.....: ");
                        nome = Console.ReadLine();
                        Console.Write("Email....: ");
                        email = Console.ReadLine();

                        Contato contatoEncontrado = contatos.Pesquisar(new Contato(email, nome, "", new Data()));

                        Console.Clear();

                        if (contatoEncontrado != null)
                        {
                            Console.WriteLine("\nContato Encontrado:");
                            Console.WriteLine(contatoEncontrado.ToString());

                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("\nContato não encontrado!");
                            Thread.Sleep(500);
                        }

                        break;
                    case "3":
                        Console.WriteLine("ALTERANDO CONTATO");

                        Console.Write("Nome.....: ");
                        nome = Console.ReadLine();
                        Console.Write("Email....: ");
                        email = Console.ReadLine();
                        Console.Write("Telefone.: ");
                        telefone = Console.ReadLine();
                        Console.WriteLine("Data Nasc: ");
                        Console.Write(".............Dia: ");
                        dia = int.Parse(Console.ReadLine());
                        Console.Write(".............Mês: ");
                        mes = int.Parse(Console.ReadLine());
                        Console.Write(".............Ano: ");
                        ano = int.Parse(Console.ReadLine());

                        bool isUpdated = contatos.Alterar(new Contato(email, nome, telefone, new Data(dia, mes, ano)));

                        Console.Clear();
                        Console.WriteLine("\n{0}", isUpdated ? "Atualizado com sucesso!" : "Contato não encontrado e NÃO atualizado!");

                        Thread.Sleep(1000);
                        break;
                    case "4":
                        Console.WriteLine("REMOVENDO CONTATO (Por email)");

                        Console.Write("Email....: ");
                        email = Console.ReadLine();

                        // exlusão com todos os dados
                        bool isDelected = contatos.Remover(new Contato(email, "", "", new Data()));

                        Console.Clear();

                        if (isDelected)
                            Console.WriteLine("\nContato excluído com sucesso!");
                        else
                            Console.WriteLine("\nContato não encontrado e NÂO excluído!");

                        Thread.Sleep(1000);
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("LISTANDO CONTATOS");

                        if (contatos.Agenda.Count > 0)
                            contatos.ListarContatos();
                        else
                            Console.WriteLine("\nNão há contatos para serem exibidos!");

                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("OPÇÃO INVÁLIDA, TENTE NOVAMENTE!");
                        break;

                }

            } while (opcao != "0");
        }
    }
}
