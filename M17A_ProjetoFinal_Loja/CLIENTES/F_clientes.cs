using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M17A_ProjetoFinal_Loja
{
    public partial class F_clientes : Form
    {
        private BaseDados bd;

        public F_clientes(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            CarregarClientes();
        }

        // Carrega clientes para o DataGridView
        private void CarregarClientes()
        {
            string sql = "SELECT * FROM Clientes";
            DataTable dt = bd.DevolveSQL(sql);
            dataGridView1.DataSource = dt;
        }

        // Botão GUARDAR (INSERIR/EDITAR)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preencha o nome!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNIF.Text))
            {
                MessageBox.Show("Preencha o NIF!");
                return;
            }

            // Se tiver um ID selecionado, é EDITAR
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                // EDITAR cliente
                string sql = $@"UPDATE Clientes SET 
                              Nome = '{txtNome.Text}',
                              DataNascimento = '{dtpData.Value:yyyy-MM-dd}',
                              Morada = '{txtMorada.Text}',
                              CodigoPostal = '{txtCodigoPostal.Text}',
                              NIF = '{txtNIF.Text}',
                              Telemovel = '{txtTelemovel.Text}',
                              Email = '{txtEmail.Text}'
                              WHERE Id = {txtId.Text}";

                bd.ExecutarSQL(sql);
                MessageBox.Show("Cliente editado com sucesso!");
            }
            else
            {
                // INSERIR novo cliente
                string sql = $@"INSERT INTO Clientes 
                              (Nome, DataNascimento, Morada, CodigoPostal, NIF, Telemovel, Email) 
                              VALUES 
                              ('{txtNome.Text}', 
                               '{dtpData.Value:yyyy-MM-dd}', 
                               '{txtMorada.Text}', 
                               '{txtCodigoPostal.Text}', 
                               '{txtNIF.Text}', 
                               '{txtTelemovel.Text}', 
                               '{txtEmail.Text}')";

                bd.ExecutarSQL(sql);
                MessageBox.Show("Cliente inserido com sucesso!");
            }

            LimparCampos();
            CarregarClientes();
        }

        // Botão CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // Botão EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                txtId.Text = row.Cells["Id"].Value.ToString();
                txtNome.Text = row.Cells["Nome"].Value.ToString();

                // Data de nascimento
                if (row.Cells["DataNascimento"].Value != DBNull.Value)
                {
                    dtpData.Value = Convert.ToDateTime(row.Cells["DataNascimento"].Value);
                }

                txtMorada.Text = row.Cells["Morada"].Value.ToString();
                txtCodigoPostal.Text = row.Cells["CodigoPostal"].Value.ToString();
                txtNIF.Text = row.Cells["NIF"].Value.ToString();
                txtTelemovel.Text = row.Cells["Telemovel"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para editar!");
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            dtpData.Value = DateTime.Now;
            txtMorada.Clear();
            txtCodigoPostal.Clear();
            txtNIF.Clear();
            txtTelemovel.Clear();
            txtEmail.Clear();
        }

        // Quando seleciona uma linha no DataGridView
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Opcional: preencher automaticamente ao selecionar
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtNIF.Text = row.Cells["NIF"].Value.ToString();
            }
        }

        private void F_clientes_Load(object sender, EventArgs e)
        {

        }
    }
}