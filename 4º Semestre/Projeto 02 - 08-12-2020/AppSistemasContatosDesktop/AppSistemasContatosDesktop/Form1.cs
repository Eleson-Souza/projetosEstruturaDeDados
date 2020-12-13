using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSistemasContatosDesktop
{
    public partial class Agenda : Form
    {
        // objeto global
        Contatos contatos = new Contatos();
        public Agenda()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtFone.Clear();
            txtDia.Clear();
            txtMes.Clear();
            txtAno.Clear();
            txtEmail.Focus();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            // verificando se campos estão preenchidos
            bool isValidCampos = 
                validarCampos(new List<TextBox> { txtEmail, txtNome, txtFone, txtDia, txtMes, txtAno });

            if(isValidCampos)
            {
                string email = txtEmail.Text;
                string nome = txtNome.Text;
                string telefone = txtFone.Text;
                int dia = int.Parse(txtDia.Text);
                int mes = int.Parse(txtMes.Text);
                int ano = int.Parse(txtAno.Text);

                // verificando se contato já existe, pelo email.
                Contato contatoEncontrado = contatos.Pesquisar(new Contato(email, "", "", new Data()));

                // se o contato não existe, será adicionado, senão apenas alterado.
                if(contatoEncontrado == null)
                {
                    contatos.Adicionar(new Contato(email, nome, telefone, new Data(dia, mes, ano)));
                    MessageBox.Show("Contato adicionado com sucesso!");
                }
                else
                {
                    contatos.Alterar(new Contato(email, nome, telefone, new Data(dia, mes, ano)));
                    MessageBox.Show("Contato alterado com sucesso!");
                }

                gerarGrid(dataGridContatos, contatos.ListarContatosGrid());
            }
            else
            {
                MessageBox.Show("Informe todos os campos!");
            }
        }

        private void Agenda_Load(object sender, EventArgs e)
        {
            dataGridContatos.ColumnCount = 4;
            dataGridContatos.Columns[0].Name = "Nome";
            dataGridContatos.Columns[1].Name = "Email";
            dataGridContatos.Columns[2].Name = "Telefone";
            dataGridContatos.Columns[3].Name = "Data Nasc.";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // verificando se campos estão preenchidos
            bool isValidCampo = validarCampos(new List<TextBox> { txtEmail });

            if (isValidCampo)
            {
                string email = txtEmail.Text;

                bool isRemoved = contatos.Remover(new Contato(email, "", "", new Data()));

                gerarGrid(dataGridContatos, contatos.ListarContatosGrid());
                MessageBox.Show(isRemoved ? "Contato removido com sucesso!" : "Contato não encontrado!"); 
            }
            else
            {
                MessageBox.Show("Informe o campo email!");
            }
        }

        private static void gerarGrid(DataGridView dt, List<Contato> contatos)
        {
            // limpando grid
            dt.Rows.Clear();

            var rows = new List<string[]>();
            foreach (Contato contato in contatos)
            {
                string[] row1 = new string[] { contato.Nome, contato.Email, contato.Telefone, contato.DtNasc.ToString() };
                rows.Add(row1);
            }

            foreach (string[] rowArray in rows)
            {
                dt.Rows.Add(rowArray);
            }
        }

        private static bool validarCampos(List<TextBox> campos)
        {
            foreach(TextBox campo in campos)
            {
                if (campo.Text == "")
                    return false;
            }

            return true;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            // verificando se campo email está preenchido
            bool isValidCampo = validarCampos(new List<TextBox> { txtEmail });

            if (isValidCampo)
            {
                string email = txtEmail.Text;

                Contato contatoEncontrado = contatos.Pesquisar(new Contato(email, "", "", new Data()));

                if(contatoEncontrado != null)
                {
                    gerarGrid(dataGridContatos, new List<Contato> { contatoEncontrado });
                }
                else
                {
                    MessageBox.Show("Contato não encontrado!");
                }

            }
            else
            {
                MessageBox.Show("Informe o campo email!");
            }
        }

        private void dataGridContatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNome.Text = dataGridContatos.CurrentRow.Cells[0].Value.ToString();
            txtEmail.Text = dataGridContatos.CurrentRow.Cells[1].Value.ToString();
            txtFone.Text = dataGridContatos.CurrentRow.Cells[2].Value.ToString();

            string[] dataNasc = dataGridContatos.CurrentRow.Cells[3].Value.ToString().Split('/');
            txtDia.Text = dataNasc[0];
            txtMes.Text = dataNasc[1];
            txtAno.Text = dataNasc[2];
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            gerarGrid(dataGridContatos, contatos.ListarContatosGrid());
        }
    }
}
