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
    public partial class F_equipamentos : Form
    {
        private BaseDados bd;

        public F_equipamentos(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            CarregarEquipamentos();
        }

        // Carrega equipamentos para o DataGridView
        private void CarregarEquipamentos()
        {
            string sql = "SELECT * FROM Equipamentos";
            DataTable dt = bd.DevolveSQL(sql);
            dataGridView1.DataSource = dt;
        }

        // Botão INSERIR
        private void btnInserir_Click(object sender, EventArgs e)
        {
            string sql = $@"INSERT INTO Equipamentos 
                          (Nome, CodigoProduto, Categoria, Marca, Preco) 
                          VALUES 
                          ('{txtNome.Text}', '{txtCodigo.Text}', '{cmbCategoria.Text}', '{txtMarca.Text}', {txtPreco.Text})";

            bd.ExecutarSQL(sql);
            CarregarEquipamentos();
            MessageBox.Show("Equipamento inserido!");
        }

        // Botão CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtCodigo.Clear();
            txtMarca.Clear();
            txtPreco.Clear();
            txtDescricao.Clear();
        }

        // Botão EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["Id"].Value);

                string sql = $@"UPDATE Equipamentos SET 
                              Nome = '{txtNome.Text}', 
                              CodigoProduto = '{txtCodigo.Text}', 
                              Categoria = '{cmbCategoria.Text}', 
                              Marca = '{txtMarca.Text}', 
                              Preco = {txtPreco.Text}
                              WHERE Id = {id}";

                bd.ExecutarSQL(sql);
                CarregarEquipamentos();
                MessageBox.Show("Equipamento editado!");
            }
        }

        // Botão para carregar imagem
        private void btnCarregarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imagens|*.jpg;*.png;*.bmp";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName);
            }
        }

        // Quando seleciona uma linha no DataGridView
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtCodigo.Text = row.Cells["CodigoProduto"].Value.ToString();
                txtMarca.Text = row.Cells["Marca"].Value.ToString();
                txtPreco.Text = row.Cells["Preco"].Value.ToString();
            }
        }
    }
}
