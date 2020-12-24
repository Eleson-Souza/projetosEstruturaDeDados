using System;
using System.Collections.Generic;
using System.Threading;

namespace AppBooks
{
    class Program
    {
        static void Main(string[] args)
        {
            string opcao, titulo, autor, editora;
            int isbn, tombo;

            Livros livros = new Livros();

            do
            {
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("| 0.Sair                             |");
                Console.WriteLine("| 1.Adicionar livro                  |");
                Console.WriteLine("| 2.Pesquisar livro(sintético)       |");
                Console.WriteLine("| 3.Pesquisar livro(analítico)       |");
                Console.WriteLine("| 4.Adicionar exemplar               |");
                Console.WriteLine("| 5.Registrar empréstimo             |");
                Console.WriteLine("| 6.Registrar devolução              |");
                Console.WriteLine("--------------------------------------");

                Console.Write("\nEscolha uma das opções: ");
                opcao = Console.ReadLine();

                Console.WriteLine("\n");
                switch(opcao)
                {
                    case "0":
                        break;
                    case "1":
                        Console.WriteLine("ADICIONAR LIVRO");

                        Console.Write("\nISBN...: ");
                        isbn = int.Parse(Console.ReadLine());
                        Console.Write("Título.: ");
                        titulo = Console.ReadLine();
                        Console.Write("Autor..: ");
                        autor = Console.ReadLine();
                        Console.Write("Editora: ");
                        editora = Console.ReadLine();

                        livros.Adicionar(new Livro(isbn, titulo, autor, editora, new List<Exemplar> { }));

                        Console.WriteLine("Cadastro realizado com sucesso!");

                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("PESQUISAR LIVRO (SINTÉTICO)");

                        ListarLivros(livros);

                        Console.Write("\nISBN do livro...: ");
                        isbn = int.Parse(Console.ReadLine());

                        Livro livroEncontrado = livros.Pesquisar(new Livro(isbn, "", "", "", new List<Exemplar> { }));

                        if(livroEncontrado != null)
                        {
                            Console.WriteLine($"\nResumo dos exemplares do livro '{livroEncontrado.Titulo.ToUpper()}'\n");
                            Console.WriteLine("");
                            Console.WriteLine($"Quantidade...................: {livroEncontrado.QtdeExemplares()}");
                            Console.WriteLine($"Disponíveis..................: {livroEncontrado.QtdeDisponiveis()}");
                            Console.WriteLine($"Emprestados..................: {livroEncontrado.QtdeEmprestimos()}");
                            //Console.WriteLine($"Percentual de disponibilidade: {livroEncontrado.PercDisponibilidade()}%");
                        }
                        else
                        {
                            Console.WriteLine("\nLivro não encontrado!");
                        }

                        Thread.Sleep(1500);
                        break;
                    case "3":
                        Console.WriteLine("PESQUISAR LIVRO (ANALÍTICO)");

                        ListarLivros(livros);

                        Console.Write("\nISBN do livro...: ");
                        isbn = int.Parse(Console.ReadLine());

                        livroEncontrado = livros.Pesquisar(new Livro(isbn, "", "", "", new List<Exemplar> { }));

                        if (livroEncontrado != null)
                        {
                            Console.WriteLine($"\nResumo dos exemplares do livro '{livroEncontrado.Titulo.ToUpper()}'\n");
                            Console.WriteLine("");
                            Console.WriteLine($"Quantidade...................: {livroEncontrado.QtdeExemplares()}");
                            Console.WriteLine($"Disponíveis..................: {livroEncontrado.QtdeDisponiveis()}");
                            Console.WriteLine($"Emprestados..................: {livroEncontrado.QtdeEmprestimos()}");
                            //Console.WriteLine($"Percentual de disponibilidade: {livroEncontrado.PercDisponibilidade()}%");

                            if(livroEncontrado.QtdeExemplares() > 0)
                            {
                                Console.WriteLine("\nDetalhes dos Exemplares\n");
                                foreach (Exemplar ex in livroEncontrado.Exemplares)
                                {
                                    Console.WriteLine($"\nTombo: {ex.Tombo}");
                                    Console.WriteLine("Empréstimos:");
                                    foreach (Emprestimo emp in ex.Emprestimos)
                                    {
                                        Console.WriteLine($"Data Empréstimo: {emp.DtEmprestimo}");
                                        Console.WriteLine($"Data Devolução: {emp.DtDevolucao}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNão existem exemplares paras serem exibidos!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nLivro não encontrado!");
                        }

                        Thread.Sleep(1500);
                        break;
                    case "4":
                        Console.WriteLine("ADICIONAR EXEMPLAR");

                        ListarLivros(livros);

                        Console.Write("\nISBN do livro....: ");
                        isbn = int.Parse(Console.ReadLine());
                        Console.Write("Tombo do exemplar: ");
                        tombo = int.Parse(Console.ReadLine());

                        livroEncontrado = livros.Pesquisar(new Livro(isbn, "", "", "", new List<Exemplar> { }));

                        if(livroEncontrado != null)
                        {
                            livroEncontrado.AdicionarExemplar(new Exemplar(tombo, new List<Emprestimo> { new Emprestimo() }));
                            Console.WriteLine("\nExemplar adicionado com sucesso!");
                        } 
                        else
                        {
                            Console.WriteLine("\nLivro não encontrado!");
                        }

                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("REGISTRAR EMPRÉSTIMO");

                        ListarLivros(livros);

                        Console.Write("\nISBN do livro escolhido....: ");
                        isbn = int.Parse(Console.ReadLine());

                        livroEncontrado = livros.Acervo.Find(item => item.Isbn == isbn);

                        if(livroEncontrado != null)
                        {
                            int quantDisponiveis = livroEncontrado.QtdeDisponiveis();
                            if(quantDisponiveis > 0)
                            {
                                foreach(Exemplar ex in livroEncontrado.Exemplares)
                                {
                                    if(ex.Emprestar())
                                    {
                                        Console.WriteLine($"\nEmpréstimo do livro '{livroEncontrado.Titulo}' realizado com sucesso!");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNão temos exemplares disponíveis para esse livro!");
                            }

                            break;
                        }

                        Console.WriteLine("Livro não encontrado!");

                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "6":
                        Console.WriteLine("REGISTRAR DEVOLUÇÃO");

                        ListarLivros(livros);

                        Console.Write("\nISBN do livro escolhido....: ");
                        isbn = int.Parse(Console.ReadLine());

                        livroEncontrado = livros.Acervo.Find(item => item.Isbn == isbn);

                        if(livroEncontrado != null)
                        {
                            for(int i=0; i<livroEncontrado.Exemplares.Count; i++)
                            {
                                if (livroEncontrado.Exemplares[i].Devolver())
                                {
                                    Console.WriteLine($"\nDevolução do livro '{livroEncontrado.Titulo}' realizada com sucesso!");
                                    break;
                                }
                                else if(i == livroEncontrado.Exemplares.Count - 1)
                                {
                                    Console.WriteLine($"\nNão é possível realizar devolução deste livro, pois o mesmo já se encontra devolvido!");
                                }
                            }

                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                        }

                        Console.WriteLine("Livro não encontrado!");

                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente!");

                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            } while (opcao != "0");
        }

        public static void ListarLivros(Livros livros)
        {
            Console.WriteLine($"\nLivros disponíveis:\n");
            foreach (Livro livro in livros.Acervo)
            {
                Console.WriteLine($"ISBN: {livro.Isbn}");
                Console.WriteLine($"Nome: {livro.Titulo}");
                Console.WriteLine($"Autor: {livro.Autor}");
                Console.WriteLine();
            }
        }
    }
}
