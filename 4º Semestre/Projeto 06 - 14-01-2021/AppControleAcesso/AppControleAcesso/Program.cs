using System;
using System.Collections.Generic;
using System.Threading;
using AppControleAcesso.Controllers;
using AppControleAcesso.Models;

namespace AppControleAcesso
{
    class Program
    {
        private DbContext _context;

        public Program(DbContext context)
        {
            _context = context;
        }

        static void Main(string[] args)
        {
            using (var context2 = new DbContext())
            {
                var usuario = new Usuarios()
                {
                    Id = 1,
                    Nome = "Eleson"
                };

                context2.Usuarios.Add(usuario);
            }

            try
            {
                DbContext context = new DbContext();
                UsuariosController usuariosController = new UsuariosController();

                string opcao, nome;
                int id;

                Cadastro cadastros = new Cadastro();

                do
                {
                    Console.WriteLine(" 0. Sair");
                    Console.WriteLine(" 1. Cadastrar ambiente");
                    Console.WriteLine(" 2. Consultar ambiente");
                    Console.WriteLine(" 3. Excluir ambiente");
                    Console.WriteLine(" 4. Cadastrar usuário");
                    Console.WriteLine(" 5. Consultar usuário");
                    Console.WriteLine(" 6. Excluir usuário");
                    Console.WriteLine(" 7. Conceder permissão de acesso ao usuário");
                    Console.WriteLine(" 8. Revogar permissão de acesso ao usuário");
                    Console.WriteLine(" 9. Registrar acesso");
                    Console.WriteLine("10. Consultar logs de acesso");

                    Console.Write("\n\nOpcao escolhida: ");
                    opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "0":
                            break;
                        case "1":
                            Console.Clear();
                            Console.WriteLine("CADASTRAR AMBIENTE");

                            Console.Write("\nID..: ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Nome: ");
                            nome = Console.ReadLine();

                            cadastros.AdcionarAmbiente(new Ambiente(id, nome, new Queue<Log>()));

                            Console.WriteLine($"\nAmbiente {nome} adicionado com sucesso!");

                            Thread.Sleep(1500);
                            Console.Clear();
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("CONSULTAR AMBIENTE");

                            Console.Write("\nID do ambiente: ");
                            id = int.Parse(Console.ReadLine());

                            var ambienteEncontrado = cadastros.PesquisarAmbiente(new Ambiente(id, "", new Queue<Log>()));

                            if (ambienteEncontrado != null)
                            {
                                Console.WriteLine("\nDados do ambiente");
                                Console.WriteLine($"- ID..: {ambienteEncontrado.Id}");
                                Console.WriteLine($"- Nome: {ambienteEncontrado.Nome}\n");
                            }
                            else
                            {
                                Console.WriteLine("\nNenhum ambiente encontrado!\n");
                            }

                            Thread.Sleep(2000);
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("EXCLUIR AMBIENTE");

                            Console.Write("\nID do ambiente: ");
                            id = int.Parse(Console.ReadLine());

                            bool ambRemovido = cadastros.RemoverAmbiente(new Ambiente(id, "", new Queue<Log>()));

                            if (ambRemovido)
                                Console.WriteLine("\nAmbiente removido com sucesso!\n");
                            else
                                Console.WriteLine("\nO ambiente informado não foi encontrado!\n");

                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("CADASTRAR USUÁRIO");

                            Console.Write("\nID..: ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Nome: ");
                            nome = Console.ReadLine();

                            cadastros.AdicionarUsuario(new Usuario(id, nome, new List<Ambiente>()));

                            Console.WriteLine($"\nUsuário adicionado com sucesso!");

                            Thread.Sleep(1500);
                            Console.Clear();
                            break;
                        case "5":
                            Console.Clear();
                            Console.WriteLine("CONSULTAR USUÁRIO");

                            Console.Write("\nID do usuário: ");
                            id = int.Parse(Console.ReadLine());

                            var usuarioEncontrado = cadastros.PesquisarUsuario(new Usuario(id, "", new List<Ambiente>()));

                            if (usuarioEncontrado != null)
                            {
                                Console.WriteLine("\nDados do usuário");
                                Console.WriteLine($"- ID..: {usuarioEncontrado.Id}");
                                Console.WriteLine($"- Nome: {usuarioEncontrado.Nome}\n");

                                Console.WriteLine($"\nSeus ambientes");
                                if (usuarioEncontrado.Ambientes.Count > 0)
                                {
                                    foreach (var amb in usuarioEncontrado.Ambientes)
                                    {
                                        Console.WriteLine("-------------------------");
                                        Console.WriteLine($"- ID..: {amb.Id}");
                                        Console.WriteLine($"- Nome: {amb.Nome}");
                                    }
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("\nSem ambientes cadastrados!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNenhum usuário encontrado!\n");
                            }

                            Thread.Sleep(2000);

                            break;
                        case "6":
                            Console.Clear();
                            Console.WriteLine("EXCLUIR USUÁRIO");

                            Console.Write("\nID do usuario: ");
                            id = int.Parse(Console.ReadLine());

                            bool userRemovido = cadastros.RemoverUsuario(new Usuario(id, "", new List<Ambiente>()));

                            if (userRemovido)
                                Console.WriteLine("\nUsuário removido com sucesso!\n");
                            else
                                Console.WriteLine("\nO usuário informado não foi encontrado!\n");

                            Thread.Sleep(1500);
                            Console.Clear();
                            break;
                        case "7":
                            Console.Clear();
                            Console.WriteLine("CONCEDER PERMISSÃO DE ACESSO AO USUÁRIO");

                            Console.Write("\nID do usuário: ");
                            int idUser = int.Parse(Console.ReadLine());

                            // busca pelo usuario
                            usuarioEncontrado = cadastros.PesquisarUsuario(new Usuario(idUser, "", new List<Ambiente>()));

                            // se o usuario foi encontrado, prossegue. Se não, mensagem de não encontrado
                            if (usuarioEncontrado != null)
                            {
                                Console.Write("ID do ambiente: ");
                                int idAmb = int.Parse(Console.ReadLine());

                                ambienteEncontrado = cadastros.PesquisarAmbiente(new Ambiente(idAmb, "", new Queue<Log>()));

                                // se o ambiente existir, prossegue. Se não, mensagem de não encontrado
                                if (ambienteEncontrado != null)
                                {
                                    // tentando conceder acesso ao usuario encontrado ao ambiente encontrado
                                    bool acessoAprovado = usuarioEncontrado.ConcederPermissao(ambienteEncontrado);

                                    if (acessoAprovado)
                                    {
                                        Console.WriteLine($"\nAcesso ao ambiente {ambienteEncontrado.Nome} aprovado para o usuário {usuarioEncontrado.Nome}!\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nAcesso negado, pois este usuário já possui permissão nesse ambiente!\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nO ambiente informado não foi encontrado!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nO usuário informado não foi encontrado!\n");
                            }

                            Thread.Sleep(2000);
                            break;
                        case "8":
                            Console.Clear();
                            Console.WriteLine("REVOGAR ACESSO AO USUÁRIO");

                            Console.Write("\nID do usuário: ");
                            idUser = int.Parse(Console.ReadLine());

                            // busca pelo usuario
                            usuarioEncontrado = cadastros.PesquisarUsuario(new Usuario(idUser, "", new List<Ambiente>()));

                            // se o usuario foi encontrado, prossegue. Se não, mensagem de não encontrado
                            if (usuarioEncontrado != null)
                            {
                                Console.Write("ID do ambiente: ");
                                int idAmb = int.Parse(Console.ReadLine());

                                // busca pelo ambiente
                                ambienteEncontrado = cadastros.PesquisarAmbiente(new Ambiente(idAmb, "", new Queue<Log>()));

                                // se o ambiente existir, prossegue. Se não, mensagem de não encontrado
                                if (ambienteEncontrado != null)
                                {
                                    // tentando revogar acesso ao usuario encontrado ao ambiente encontrado
                                    bool acessoRevogado = usuarioEncontrado.RevogarPermissao(ambienteEncontrado);

                                    if (acessoRevogado)
                                    {
                                        Console.WriteLine($"\nAcesso ao ambiente {ambienteEncontrado.Nome} foi revogado para o usuário {usuarioEncontrado.Nome}!\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nAcesso não revogado, pois esse usuário já não possui acesso a esse ambiente!\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nO ambiente informado não foi encontrado!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nO usuário informado não foi encontrado!\n");
                            }

                            Thread.Sleep(2000);
                            break;
                        case "9":
                            Console.Clear();
                            Console.WriteLine("REGISTRAR ACESSO");

                            Console.Write("\nID do usuário: ");
                            idUser = int.Parse(Console.ReadLine());

                            // busca pelo usuario
                            usuarioEncontrado = cadastros.PesquisarUsuario(new Usuario(idUser, "", new List<Ambiente>()));

                            // se o usuario foi encontrado, prossegue. Se não, mensagem de não encontrado
                            if (usuarioEncontrado != null)
                            {
                                Console.Write("ID do ambiente: ");
                                int idAmb = int.Parse(Console.ReadLine());

                                // busca pelo ambiente na lista de ambientes do usuário
                                ambienteEncontrado = cadastros.PesquisarAmbiente(new Ambiente(idAmb, "", new Queue<Log>()));

                                // se o ambiente existir, prossegue. Se não, mensagem de não encontrado
                                if (ambienteEncontrado != null)
                                {
                                    var ambienteUser = usuarioEncontrado.Ambientes.Find(amb => amb.Id == ambienteEncontrado.Id);

                                    if (ambienteUser != null)
                                    {
                                        ambienteEncontrado.Logs.Enqueue(new Log(DateTime.Now, usuarioEncontrado, true));
                                        Console.WriteLine("\nAcesso registrado com sucesso!\n");
                                    }
                                    else
                                    {
                                        ambienteEncontrado.Logs.Enqueue(new Log(DateTime.Now, usuarioEncontrado, false));
                                        Console.WriteLine("\nEste usuário não tem acesso a esse ambiente ou ele não existe!\n");
                                    }                                    
                                }
                                else
                                {
                                    Console.WriteLine("\nO ambiente informado não foi encontrado!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nO usuário informado não foi encontrado!\n");
                            }

                            Thread.Sleep(2000);
                            break;
                        case "10":
                            Console.Clear();
                            Console.WriteLine("CONSULTAR LOGS DE ACESSO");

                            Console.Write("\nID do ambiente: ");
                            id = int.Parse(Console.ReadLine());

                            ambienteEncontrado = cadastros.PesquisarAmbiente(new Ambiente(id, "", new Queue<Log>()));

                            if (ambienteEncontrado != null)
                            {
                                Console.Write("\nFiltro [1-autorizados, 2-negados, 3-todos]: ");
                                int filtro = int.Parse(Console.ReadLine());

                                Console.WriteLine($"\nAmbiente {ambienteEncontrado.Nome}");
                                foreach (Log log in ambienteEncontrado.Logs)
                                {
                                    switch (filtro)
                                    {
                                        case 1:
                                            if (log.TipoAcesso)
                                            {
                                                Console.WriteLine("---------------------------");
                                                Console.WriteLine($"Data acesso.: {log.DtAcesso}");
                                                Console.WriteLine($"Nome usuário: {log.Usuario.Nome}");
                                                Console.WriteLine($"Tipo acesso.: {((log.TipoAcesso) ? "Autorizado" : "Não autorizado")}");
                                            }
                                            break;
                                        case 2:
                                            if (!log.TipoAcesso)
                                            {
                                                Console.WriteLine("---------------------------");
                                                Console.WriteLine($"Data acesso.: {log.DtAcesso}");
                                                Console.WriteLine($"Nome usuário: {log.Usuario.Nome}");
                                                Console.WriteLine($"Tipo acesso.: {((log.TipoAcesso) ? "Autorizado" : "Não autorizado")}");
                                            }
                                            break;
                                        case 3:
                                            Console.WriteLine("---------------------------");
                                            Console.WriteLine($"Data acesso.: {log.DtAcesso}");
                                            Console.WriteLine($"Nome usuário: {log.Usuario.Nome}");
                                            Console.WriteLine($"Tipo acesso.: {((log.TipoAcesso) ? "Autorizado" : "Não autorizado")}");
                                            break;
                                        default:
                                            Console.WriteLine("\nOpção de filtro inválida!\n");
                                            break;
                                    }
                                }
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("\nO ambiente informado não foi encontrado!\n");
                            }

                            break;
                        default:
                            Console.WriteLine("Opção inválida! Tente novamente outra opção.");
                            break;
                    }

                } while (opcao != "0");

                //using (var context2 = new DbContext())
                //{
                //    /*foreach (var user in cadastros.Usuarios)
                //    {*/
                //        var usuario = new Usuarios()
                //        {
                //            Id = 1,
                //            Nome = "Eleson"
                //        };
                //        context2.Usuarios.Add(usuario);
                //        //usuariosController.InsertUsuario(usuario);
                //    //}
                //}
            }
            catch (Exception)
            {
                Console.WriteLine("\nHouve um erro na aplicação, provavelmente por dados inconsistentes!\n");
            }
        }
    }
}
