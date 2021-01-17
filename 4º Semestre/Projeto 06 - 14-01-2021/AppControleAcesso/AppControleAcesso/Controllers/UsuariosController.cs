using AppControleAcesso.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleAcesso.Controllers
{
    class UsuariosController
    {
        private DbContext context = new DbContext();

        public void InsertUsuario(Usuarios usuario)
        {
            try
            {
                context.Usuarios.Add(usuario);
                Console.WriteLine("\nInsert realizado!\n");
            }
            catch(Exception)
            {
                Console.WriteLine("\nErro ao inserir usuário!\n");
            }
        }
    }
}
