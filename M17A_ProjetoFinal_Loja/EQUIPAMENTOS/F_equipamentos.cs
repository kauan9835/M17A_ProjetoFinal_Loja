using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace M17A_ProjetoFinal_Loja.EQUIPAMENTOS
{
    public partial class F_equipamentos : Form
    {
        private BaseDados bd;
        private byte[] imagemEquipamento = null;
        private int equipamentoIdAtual = -1;

        public F_equipamentos(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            dtpDataEntrada.Value = DateTime.Now;
            CarregarEquipamentos();
            CarregarCategorias();
            CarregarCompatibilidades();
        }

        private void CarregarEquipamentos()
        {
            string sql = "SELECT Id, Nome, CodigoProduto, Categoria, Marca, Preco, DataEntrada FROM Equipamentos ORDER BY DataEntrada DESC";
            DataTable dt = bd.DevolveSQL(sql);
            dataGridViewEquipamentos.DataSource = dt;
        }

        private void CarregarCategorias()
        {
            cmbCategoria.Items.AddRange(new string[] {
                "Processador",
                "Placa-mãe",
                "Memória RAM",
                "Armazenamento",
                "Placa de Vídeo",
                "Fonte",
                "Gabinete",
                "Cooler",
                "Periféricos",
                "Rede",
                "Outros"
            });
        }

        private void CarregarCompatibilidades()
        {
            // Compatibilidades pré-definidas para hardware
            cmbCompatibilidade.Items.AddRange(new string[] {
                "LGA 1700",
                "LGA 1200",
                "AM4",
                "AM5",
                "DDR4",
                "DDR5",
                "PCIe 4.0",
                "PCIe 5.0",
                "SATA III",
                "M.2 NVMe",
                "USB 3.2",
                "USB-C",
                "HDMI 2.1",
                "DisplayPort 1.4",
                "ATX",
                "Micro-ATX",
                "Mini-ITX",
                "Outra"
            });
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                InserirEquipamento();
                LimparCampos();
                CarregarEquipamentos();
                MessageBox.Show("Equipamento inserido com sucesso!");
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preencha o nome do equipamento!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Preencha o código do produto!");
                return false;
            }

            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma categoria!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPreco.Text) || !decimal.TryParse(txtPreco.Text, out _))
            {
                MessageBox.Show("Preço inválido!");
                return false;
            }

            return true;
        }

        private void InserirEquipamento()
        {
            string sql = @"INSERT INTO Equipamentos 
                          (Nome, CodigoProduto, Categoria, Marca, Compatibilidade, Garantia, Preco, Descricao, Imagem, DataEntrada) 
                          VALUES (@Nome, @Codigo, @Categoria, @Marca, @Compatibilidade, @Garantia, @Preco, @Descricao, @Imagem, @DataEntrada)";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Nome", txtNome.Text),
                new SqlParameter("@Codigo", txtCodigo.Text),
                new SqlParameter("@Categoria", cmbCategoria.Text),
                new SqlParameter("@Marca", txtMarca.Text),
                new SqlParameter("@Compatibilidade", cmbCompatibilidade.Text),
                new SqlParameter("@Garantia", txtGarantia.Text),
                new SqlParameter("@Preco", decimal.Parse(txtPreco.Text)),
                new SqlParameter("@Descricao", txtDescricao.Text),
                new SqlParameter("@Imagem", imagemEquipamento ?? (object)DBNull.Value),
                new SqlParameter("@DataEntrada", dtpDataEntrada.Value)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (equipamentoIdAtual > 0)
            {
                EditarEquipamento();
            }
            else
            {
                MessageBox.Show("Selecione um equipamento para editar!");
            }
        }

        private void EditarEquipamento()
        {
            if (equipamentoIdAtual > 0)
            {
                string sql = @"UPDATE Equipamentos SET 
                              Nome = @Nome, 
                              CodigoProduto = @Codigo, 
                              Categoria = @Categoria, 
                              Marca = @Marca, 
                              Compatibilidade = @Compatibilidade, 
                              Garantia = @Garantia, 
                              Preco = @Preco, 
                              Descricao = @Descricao, 
                              Imagem = @Imagem,
                              DataEntrada = @DataEntrada
                              WHERE Id = @Id";

                var parametros = new List<SqlParameter>
                {
                    new SqlParameter("@Id", equipamentoIdAtual),
                    new SqlParameter("@Nome", txtNome.Text),
                    new SqlParameter("@Codigo", txtCodigo.Text),
                    new SqlParameter("@Categoria", cmbCategoria.Text),
                    new SqlParameter("@Marca", txtMarca.Text),
                    new SqlParameter("@Compatibilidade", cmbCompatibilidade.Text),
                    new SqlParameter("@Garantia", txtGarantia.Text),
                    new SqlParameter("@Preco", decimal.Parse(txtPreco.Text)),
                    new SqlParameter("@Descricao", txtDescricao.Text),
                    new SqlParameter("@Imagem", imagemEquipamento ?? (object)DBNull.Value),
                    new SqlParameter("@DataEntrada", dtpDataEntrada.Value)
                };

                bd.ExecutarSQL(sql, parametros);
                MessageBox.Show("Equipamento editado com sucesso!");
                CarregarEquipamentos();
                LimparCampos();
            }
        }

        private void btnInserirImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagens (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
                openFileDialog.Title = "Selecionar imagem do equipamento";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        imagemEquipamento = File.ReadAllBytes(filePath);
                        picImagemEquipamento.Image = Image.FromFile(filePath);
                      ;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao carregar imagem: {ex.Message}");
                    }
                }
            }
        }

        private void LimparCampos()
        {
            equipamentoIdAtual = -1;
            txtId.Clear();
            txtNome.Clear();
            txtCodigo.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtMarca.Clear();
            cmbCompatibilidade.SelectedIndex = -1;
            txtGarantia.Clear();
            txtPreco.Clear();
            txtDescricao.Clear();
            picImagemEquipamento.Image = null;
            imagemEquipamento = null;
            lblImagem.Text = "Imagem do equipamento";
            dtpDataEntrada.Value = DateTime.Now;
        }

        private void dataGridViewEquipamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEquipamentos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewEquipamentos.SelectedRows[0];

                // ID do equipamento
                equipamentoIdAtual = Convert.ToInt32(row.Cells["Id"].Value);
                txtId.Text = equipamentoIdAtual.ToString();

                // Preencher os campos com os dados do equipamento selecionado
                txtNome.Text = row.Cells["Nome"].Value?.ToString() ?? "";
                txtCodigo.Text = row.Cells["CodigoProduto"].Value?.ToString() ?? "";
                cmbCategoria.Text = row.Cells["Categoria"].Value?.ToString() ?? "";
                txtMarca.Text = row.Cells["Marca"].Value?.ToString() ?? "";

                // Carregar dados completos para os campos que não estão no DataGridView
                CarregarDadosCompletos(equipamentoIdAtual);
            }
        }

        private void CarregarDadosCompletos(int id)
        {
            string sql = "SELECT * FROM Equipamentos WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            DataTable dt = bd.DevolveSQL(sql, parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                cmbCompatibilidade.Text = row["Compatibilidade"]?.ToString() ?? "";
                txtGarantia.Text = row["Garantia"]?.ToString() ?? "";
                txtPreco.Text = row["Preco"]?.ToString() ?? "";
                txtDescricao.Text = row["Descricao"]?.ToString() ?? "";

                // Data de entrada
                if (row["DataEntrada"] != DBNull.Value)
                {
                    dtpDataEntrada.Value = Convert.ToDateTime(row["DataEntrada"]);
                }

                // Imagem
                if (row["Imagem"] != DBNull.Value)
                {
                    imagemEquipamento = (byte[])row["Imagem"];
                    using (MemoryStream ms = new MemoryStream(imagemEquipamento))
                    {
                        picImagemEquipamento.Image = Image.FromStream(ms);
                    }
                    lblImagem.Text = "Imagem carregada!";
                }
                else
                {
                    picImagemEquipamento.Image = null;
                    imagemEquipamento = null;
                    lblImagem.Text = "Imagem do equipamento";
                }
            }
        }

        private void btnRemoverImagem_Click(object sender, EventArgs e)
        {
            picImagemEquipamento.Image = null;
            imagemEquipamento = null;
            lblImagem.Text = "Imagem do equipamento";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (equipamentoIdAtual > 0)
            {
                var result = MessageBox.Show("Tem a certeza que deseja eliminar este equipamento?",
                                           "Confirmar Eliminação",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string sql = "DELETE FROM Equipamentos WHERE Id = @Id";
                    var parametros = new List<SqlParameter>
                    {
                        new SqlParameter("@Id", equipamentoIdAtual)
                    };

                    bd.ExecutarSQL(sql, parametros);
                    MessageBox.Show("Equipamento eliminado com sucesso!");
                    LimparCampos();
                    CarregarEquipamentos();
                }
            }
            else
            {
                MessageBox.Show("Selecione um equipamento para eliminar!");
            }
        }
    }
}
