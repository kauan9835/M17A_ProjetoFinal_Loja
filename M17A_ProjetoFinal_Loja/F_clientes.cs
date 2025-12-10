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
        BaseDados bd;
        int clienteId = 0;

        public F_clientes(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        // Guardar o cliente na base de dados
        

        // Atualizar a lista dos clientes no DataGridView
        private void ListarClientes()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            Cliente c = new Cliente(bd);
            dataGridView1.DataSource = c.Listar();
        }

        // Limpar as TextBox do formulário
        private void LimparForm()
        {
            clienteId = 0;
            txtNome.Text = "";
            dtpData.Value = DateTime.Now;
            txtMorada.Text = "";
            txtCodigoPostal.Text = "";
            txtNIF.Text = "";
            txtTelemovel.Text = "";
            txtEmail.Text = "";

            // Mostrar botão guardar, esconder editar/eliminar
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = true;
        }

        private void F_clientes_Load(object sender, EventArgs e)
        {
            ListarClientes();
            // Esconder botões editar/eliminar inicialmente
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
        }

        private void dgv_clientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dataGridView1.CurrentCell.RowIndex;
            if (linha == -1)
                return;

            clienteId = int.Parse(dataGridView1.Rows[linha].Cells[0].Value.ToString());

            // Esconder o botão de adicionar novo
            btnGuardar.Visible = false;

            // Preencher o formulário com os dados do cliente selecionado
            Cliente cliente = new Cliente(bd);
            cliente.Id = clienteId;
            cliente.Procurar();

            txtNome.Text = cliente.Nome;
            dtpData.Value = cliente.DataNascimento;
            txtMorada.Text = cliente.Morada;
            txtCodigoPostal.Text = cliente.CodigoPostal;
            txtNIF.Text = cliente.NIF;
            txtTelemovel.Text = cliente.Telemovel;
            txtEmail.Text = cliente.Email;

            // Mostrar os botões editar/eliminar
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
        }


        // Botão para atualizar o registo selecionado
        private void bt_editar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Cliente
            Cliente novo = new Cliente(bd);
            novo.Id = clienteId;

            // Preencher os dados do cliente
            novo.Nome = txtNome.Text;
            novo.DataNascimento = dtpData.Value;
            novo.Morada = txtMorada.Text;
            novo.CodigoPostal = txtCodigoPostal.Text;
            novo.NIF = txtNIF.Text;
            novo.Telemovel = txtTelemovel.Text;
            novo.Email = txtEmail.Text;

            // Validar os dados
            List<string> erros = novo.Validar();

            // Se tiver erros nos dados
            if (erros.Count > 0)
            {
                // Mostrar os erros
                string mensagem = "";
                foreach (string erro in erros)
                    mensagem += erro + "; ";
                lb_feedback.Text = mensagem;
                lb_feedback.ForeColor = Color.Red;
                return;
            }

            // Guardar na base de dados
            novo.Atualizar();

            // Limpar o formulário
            LimparForm();

            // Atualizar a lista dos clientes no DataGrid
            ListarClientes();

            // Feedback ao utilizador
            lb_feedback.Text = "Cliente atualizado com sucesso.";
            lb_feedback.ForeColor = Color.Black;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dataGridView1.CurrentCell.RowIndex;
            if (linha == -1)
                return;

            clienteId = int.Parse(dataGridView1.Rows[linha].Cells[0].Value.ToString());

            // Esconder o botão de adicionar novo
            btnGuardar.Visible = false;

            // Preencher o formulário com os dados do cliente selecionado
            Cliente cliente = new Cliente(bd);
            cliente.Id = clienteId;
            cliente.Procurar();

            txtNome.Text = cliente.Nome;
            dtpData.Value = cliente.DataNascimento;
            txtMorada.Text = cliente.Morada;
            txtCodigoPostal.Text = cliente.CodigoPostal;
            txtNIF.Text = cliente.NIF;
            txtTelemovel.Text = cliente.Telemovel;
            txtEmail.Text = cliente.Email;

            // Mostrar os botões editar/eliminar
            btnEditar.Visible = true;
            btnEliminar.Visible = true;


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada na DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtém a linha selecionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Remove a linha da DataGridView
                dataGridView1.Rows.Remove(selectedRow);

                // (Opcional) Remove também da fonte de dados (ex: banco de dados)
                // EliminarDoBancoDeDados(selectedRow.Cells["ID"].Value);

                MessageBox.Show("Cliente eliminado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Cliente
            Cliente novo = new Cliente(bd);
            novo.Id = clienteId;

            // Preencher os dados do cliente
            novo.Nome = txtNome.Text;
            novo.DataNascimento = dtpData.Value;
            novo.Morada = txtMorada.Text;
            novo.CodigoPostal = txtCodigoPostal.Text;
            novo.NIF = txtNIF.Text;
            novo.Telemovel = txtTelemovel.Text;
            novo.Email = txtEmail.Text;

            // Validar os dados
            List<string> erros = novo.Validar();

            // Se tiver erros nos dados
            if (erros.Count > 0)
            {
                // Mostrar os erros
                string mensagem = "";
                foreach (string erro in erros)
                    mensagem += erro + "; ";
                lb_feedback.Text = mensagem;
                lb_feedback.ForeColor = Color.Red;
                return;
            }

            // Guardar na base de dados
            novo.Atualizar();

            // Limpar o formulário
            LimparForm();

            // Atualizar a lista dos clientes no DataGrid
            ListarClientes();

            // Feedback ao utilizador
            lb_feedback.Text = "Cliente atualizado com sucesso.";
            lb_feedback.ForeColor = Color.Black;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Cliente
            Cliente novo = new Cliente(bd);

            // Preencher os dados do cliente
            novo.Nome = txtNome.Text;
            novo.DataNascimento = dtpData.Value;
            novo.Morada = txtMorada.Text;
            novo.CodigoPostal = txtCodigoPostal.Text;
            novo.NIF = txtNIF.Text;
            novo.Telemovel = txtTelemovel.Text;
            novo.Email = txtEmail.Text;

            // Validar os dados
            List<string> erros = novo.Validar();

            // Se tiver erros nos dados
            if (erros.Count > 0)
            {
                // Mostrar os erros
                string mensagem = "";
                foreach (string erro in erros)
                    mensagem += erro + "; ";
                lb_feedback.Text = mensagem;
                lb_feedback.ForeColor = Color.Red;
                return;
            }

            // Guardar na base de dados
            novo.Adicionar();

            // Limpar o formulário
            LimparForm();

            // Atualizar a lista dos clientes no DataGrid
            ListarClientes();

            // Feedback ao utilizador
            lb_feedback.Text = "Cliente adicionado com sucesso.";
            lb_feedback.ForeColor = Color.Black;
        }
    }
}