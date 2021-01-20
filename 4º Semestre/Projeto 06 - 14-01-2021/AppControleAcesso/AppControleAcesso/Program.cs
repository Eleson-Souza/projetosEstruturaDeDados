using AppControleAcesso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AppControleAcesso
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                DbContext context = new DbContext();

                string opcao, nome;
                int id;

                Cadastro cadastros = new Cadastro();


                Console.WriteLine("\nRealizando download dos dados...\n");
                Thread.Sleep(2000);

                var users = context.Usuarios.ToList();
                var ambientes = context.Ambientes.ToList();
                var logs = context.Logs.ToList();
                var usersAmbientes = context.UsuariosAmbientes.ToList();

                foreach (var user in users)
                {
                    List<Ambiente> ambsUser = new List<Ambiente>();
                    foreach (var amb in ambientes)
                    {
                        Queue<Log> logsAmbUser = new Queue<Log>();
                        foreach (var log in logs)
                        {
                            if (log.UsuarioId == user.Id)
                            {
                                logsAmbUser.Enqueue(new Log(log.DataAcesso, new Usuario(user.Id, user.Nome, new List<Ambiente> { }), log.TipoAcesso == 1 ? true : false));
                            }
                        }

                        var ambs = usersAmbientes.Where(x => x.UsuarioId == user.Id && x.AmbienteId == amb.Id);

                        if (ambs.Count() > 0)
                        {
                            ambsUser.Add(new Ambiente(amb.Id, amb.Nome, logsAmbUser));
                        }

                        cadastros.Ambientes.Add(new Ambiente(amb.Id, amb.Nome, logsAmbUser));
                    }

                    cadastros.Usuarios.Add(new Usuario(user.Id, user.Nome, ambsUser));
                }



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

                            if (cadastros.Ambientes.Find(amb => amb.Id == id) != null)
                            {
                                Console.WriteLine("\nO Id informado já existe! Tente outro!\n");
                                Thread.Sleep(1500);
                                break;
                            }

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

                            if (cadastros.Usuarios.Find(user => user.Id == id) != null)
                            {
                                Console.WriteLine("\nO Id informado já existe! Tente outro!\n");
                                Thread.Sleep(1500);
                                break;
                            }

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

                Console.WriteLine("\nRealizando backup dos dados...\n");

                // limpa os campos do banco antes de inserir os valores
                context.Usuarios.RemoveRange(context.Usuarios);
                context.Ambientes.RemoveRange(context.Ambientes);
                context.Logs.RemoveRange(context.Logs);
                context.UsuariosAmbientes.RemoveRange(context.UsuariosAmbientes);
                context.SaveChanges();

                // salva os dados das listas no banco
                foreach (var user in cadastros.Usuarios)
                {
                    var usuario = new Usuarios()
                    {
                        Id = user.Id,
                        Nome = user.Nome
                    };

                    context.Usuarios.Add(usuario);
                    context.SaveChanges();

                    foreach (var amb in user.Ambientes)
                    {
                        var ambienteUsuario = new UsuariosAmbientes()
                        {
                            AmbienteId = amb.Id,
                            UsuarioId = user.Id
                        };

                        context.UsuariosAmbientes.Add(ambienteUsuario);
                        context.SaveChanges();
                    }
                }
                foreach (var amb in cadastros.Ambientes)
                {
                    var ambiente = new Ambientes()
                    {
                        Id = amb.Id,
                        Nome = amb.Nome
                    };

                    context.Ambientes.Add(ambiente);
                    context.SaveChanges();

                    foreach (var log in amb.Logs)
                    {
                        var logg = new Logs()
                        {
                            DataAcesso = log.DtAcesso,
                            TipoAcesso = log.TipoAcesso ? 1 : 0,
                            AmbientesId = amb.Id,
                            UsuarioId = log.Usuario.Id
                        };

                        context.Logs.Add(logg);
                        context.SaveChanges();
                    }
                }

                Console.WriteLine("\nBackup realizado com sucesso!\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\nHouve um erro na aplicação, tente novamente!\n");
            }
        }
    }
}
