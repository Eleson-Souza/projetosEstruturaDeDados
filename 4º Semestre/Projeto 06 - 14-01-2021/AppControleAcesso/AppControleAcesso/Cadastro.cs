using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleAcesso
{
    class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;

        public Cadastro(List<Usuario> usuarios, List<Ambiente> ambientes)
        {
            this.usuarios = usuarios;
            this.ambientes = ambientes;
        }

        public Cadastro() : this(new List<Usuario>(), new List<Ambiente>())
        { }

        internal List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }
        public object Thread { get; internal set; }

        public void AdicionarUsuario(Usuario usuario)
        {
            this.usuarios.Add(usuario);
        }

        public bool RemoverUsuario(Usuario usuario)
        {
            var UsuarioEnc = this.usuarios.Find(user => user.Id == usuario.Id);

            if (UsuarioEnc != null)
            {
                if(UsuarioEnc.Ambientes.Count == 0)
                {
                    this.usuarios.Remove(usuario);
                    return true;
                }
            }

            return false;
        }

        public Usuario PesquisarUsuario(Usuario usuario)
        {
            return this.usuarios.Find(user => user.Id == usuario.Id);
        }

        public void AdcionarAmbiente(Ambiente ambiente)
        {
            this.ambientes.Add(ambiente);
        }

        public bool RemoverAmbiente(Ambiente ambiente)
        {
            var existeAmbiente = this.ambientes.Find(amb => amb.Id == ambiente.Id) != null;

            if (existeAmbiente)
            {
                this.ambientes.Remove(ambiente);
                return true;
            }

            return false;
        }

        public Ambiente PesquisarAmbiente(Ambiente ambiente)
        {
            return this.ambientes.Find(amb => amb.Id == ambiente.Id);
        }

        public void Upload()
        {

        }

        public void Download()
        {

        }
    }
}
