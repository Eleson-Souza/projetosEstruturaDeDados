using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppAtendimentoFilas
{
    public partial class Form1 : Form
    {
        // global
        Senhas senhas = new Senhas();
        Guiches guiches = new Guiches();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            lbxFilaSenhas.Items.Clear();

            senhas.Gerar();

            foreach(Senha senha in senhas.FilaSenhas)
            {
                lbxFilaSenhas.Items.Add(senha.DadosParciais());
            }
        }

        private void btnListarSenhas_Click(object sender, EventArgs e)
        {
            lbxFilaSenhas.Items.Clear();

            foreach (Senha senha in senhas.FilaSenhas)
            {
                lbxFilaSenhas.Items.Add(senha.DadosParciais());
            }
        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            int idGuicheCampo = int.Parse(txtGuiches.Text);
            Guiche guicheEncontrado = guiches.ListaGuiches.Find(item => item.Id == idGuicheCampo);

            if(guicheEncontrado != null)
            {
                bool existeFilaSenhas = guicheEncontrado.Chamar(senhas.FilaSenhas);

                if(existeFilaSenhas)
                {
                    MessageBox.Show($"A Senha {guicheEncontrado.Atendimentos.Last().Id} foi chamada pelo Guichê {guicheEncontrado.Id}!");
                } 
                else
                {
                    MessageBox.Show("Não existe mais nenhuma fila de senhas!");
                }
            }
            else
            {
                MessageBox.Show("O guichê informado não existe!");
            }
        }

        private void btnListarAtendimentos_Click(object sender, EventArgs e)
        {
            lbxFilaAtendimentos.Items.Clear();

            Guiche guicheEncontrado = guiches.ListaGuiches.Find(item => item.Id == int.Parse(txtGuiches.Text));

            if(guicheEncontrado != null)
            {
                foreach (Senha senha in guicheEncontrado.Atendimentos)
                {
                    lbxFilaAtendimentos.Items.Add(senha.DadosCompletos());
                }
            }
            else
            {
                MessageBox.Show("O guichê informado não existe!");
            }
            
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            guiches.Adicionar(new Guiche(guiches.ListaGuiches.Count+1));

            lblQtdeGuiches.Text = guiches.ListaGuiches.Count().ToString();
        }
    }
}
