using System;
using AppControleAcesso2.Models;

namespace AppControleAcesso2
{
    class Program
    {
        static void Main(string[] args)
        {
            controle_acessos_ediiContext context = new controle_acessos_ediiContext();

            var usuario = new Usuarios()
            {
                Id = 1,
                Nome = "Eleson"
            };

            try
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                Console.WriteLine("Usuario inserido com sucesso!");
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao inserir usuário!");
            }
        }
    }
}
