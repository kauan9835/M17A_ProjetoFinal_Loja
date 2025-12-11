using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M17A_ProjetoFinal_Loja
{
    public partial class F_compras : Form
    {
        private BaseDados bd;
        private Dictionary<int, string> clientesDict = new Dictionary<int, string>();

        public F_compras(BaseDados bd)
        {

            InitializeComponent();
            this.bd = bd;
            CarregarEquipamentos();
            CarregarCategorias();
            CarregarCompatibilidades();
            CarregarClientesNoComboBox();

        }

        private void CarregarEquipamentos()
        {
            string sql = "SELECT * FROM Equipamentos ORDER BY Nome";
            DataTable dt = bd.DevolveSQL(sql);
            dataGridViewEquipamentos.DataSource = dt;
        }

        private void CarregarCategorias()
        {
            string sql = "SELECT DISTINCT Categoria FROM Equipamentos WHERE Categoria IS NOT NULL ORDER BY Categoria";
            DataTable dt = bd.DevolveSQL(sql);

            cmbCategoria.Items.Clear();
            cmbCategoria.Items.Add(""); // Item vazio para limpar filtro

            foreach (DataRow row in dt.Rows)
            {
                cmbCategoria.Items.Add(row["Categoria"].ToString());
            }
        }

        private void CarregarCompatibilidades()
        {
            string sql = "SELECT DISTINCT Compatibilidade FROM Equipamentos WHERE Compatibilidade IS NOT NULL ORDER BY Compatibilidade";
            DataTable dt = bd.DevolveSQL(sql);

            cmbCompatibilidade.Items.Clear();
            cmbCompatibilidade.Items.Add(""); // Item vazio para limpar filtro

            foreach (DataRow row in dt.Rows)
            {
                cmbCompatibilidade.Items.Add(row["Compatibilidade"].ToString());
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtModelo.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbCompatibilidade.SelectedIndex = -1;
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando seleciona uma categoria
            btnFiltrar_Click_1(sender, e);
        }

        private void cmbCompatibilidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando seleciona uma compatibilidade
            btnFiltrar_Click_1(sender, e);
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando digita no nome
            btnFiltrar_Click_1(sender, e);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando digita no modelo
            btnFiltrar_Click_1(sender, e);
        }



        private void btnFiltrar_Click_1(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string categoria = cmbCategoria.Text;
            string compatibilidade = cmbCompatibilidade.Text;
            string modelo = txtModelo.Text;

            string sql = "SELECT * FROM Equipamentos WHERE 1=1";
            var parametros = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                sql += " AND Nome LIKE @Nome";
                parametros.Add(new SqlParameter("@Nome", $"%{nome}%"));
            }

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                sql += " AND Categoria = @Categoria";
                parametros.Add(new SqlParameter("@Categoria", categoria));
            }

            if (!string.IsNullOrWhiteSpace(compatibilidade))
            {
                sql += " AND Compatibilidade = @Compatibilidade";
                parametros.Add(new SqlParameter("@Compatibilidade", compatibilidade));
            }

            if (!string.IsNullOrWhiteSpace(modelo))
            {
                sql += " AND Marca LIKE @Modelo";
                parametros.Add(new SqlParameter("@Modelo", $"%{modelo}%"));
            }

            sql += " ORDER BY Nome";

            DataTable dt = bd.DevolveSQL(sql, parametros);
            dataGridViewEquipamentos.DataSource = dt;
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipamentos.SelectedRows.Count > 0)
            {
                // Verificar se um cliente foi selecionado
                if (cmbClientes.SelectedIndex <= 0) // LINHA MODIFICADA
                {
                    MessageBox.Show("Por favor, selecione um cliente primeiro!", "Selecionar Cliente",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbClientes.Focus();
                    return;
                }

                // Obter ID do cliente selecionado
                int clienteId = Convert.ToInt32(clientesDict[cmbClientes.SelectedIndex]); // LINHA NOVA
                string nomeCliente = cmbClientes.Text.Split('(')[0].Trim(); // LINHA NOVA

                DataGridViewRow row = dataGridViewEquipamentos.SelectedRows[0];

                try
                {
                    int equipamentoId = Convert.ToInt32(row.Cells["Id"].Value);
                    string nomeEquipamento = row.Cells["Nome"].Value.ToString();
                    decimal preco = Convert.ToDecimal(row.Cells["Preco"].Value);

                    string numeroFatura = $"FAT-{DateTime.Now:yyyyMMdd-HHmmss}";
                    DateTime dataCompra = DateTime.Now;

                    // Registrar a compra com cliente - MODIFICADO
                    RegistrarCompra(equipamentoId, nomeEquipamento, preco, numeroFatura,
                                   dataCompra, clienteId);

                    // MOSTRAR TALÃO NA TELA - LINHA NOVA
                    MostrarTalaoTela(numeroFatura, nomeEquipamento, preco, dataCompra,
                                   nomeCliente, cmbClientes.Text);

                    LimparCampos();
                    CarregarEquipamentos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro na compra: {ex.Message}", "Erro",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecione um equipamento para comprar!", "Aviso",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CarregarClientesNoComboBox()
        {
            try
            {
                string sql = "SELECT Id, Nome, NIF FROM Clientes ORDER BY Nome";
                DataTable dt = bd.DevolveSQL(sql);

                cmbClientes.Items.Clear();
                clientesDict.Clear();

                // Adicionar item vazio no início
                cmbClientes.Items.Add("-- Selecione um cliente --");

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["Id"]);
                    string nome = row["Nome"].ToString();
                    string nif = row["NIF"].ToString();

                    string displayText = $"{nome} (NIF: {nif})";
                    cmbClientes.Items.Add(displayText);
                    clientesDict.Add(cmbClientes.Items.Count - 1, id.ToString());
                }

                cmbClientes.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes: {ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarTalaoTela(string numeroFatura, string nomeEquipamento, decimal preco,
                                     DateTime dataCompra, string nomeCliente, string infoCliente)
        {
            try
            {
                // Limpar panel se já tiver conteúdo
                panelTalao.Controls.Clear();
                panelTalao.Visible = true;
                panelTalao.BorderStyle = BorderStyle.FixedSingle;
                panelTalao.BackColor = Color.White;

                int yPos = 20;
                int margem = 20;

                // CABEÇALHO
                Label lblCabecalho = new Label();
                lblCabecalho.Text = "LOJA DE EQUIPAMENTOS";
                lblCabecalho.Font = new Font("Arial", 16, FontStyle.Bold);
                lblCabecalho.Location = new Point(margem, yPos);
                lblCabecalho.Size = new Size(400, 30);
                lblCabecalho.TextAlign = ContentAlignment.MiddleCenter;
                panelTalao.Controls.Add(lblCabecalho);
                yPos += 40;

                // ... (resto do código do método MostrarTalaoTela que forneci anteriormente)
                // CONTINUAR COM TODO O CÓDIGO DO MÉTODO MostrarTalaoTela AQUI
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar talão: {ex.Message}", "Erro",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirTalao(string numeroFatura, string nomeEquipamento, decimal preco,
                                  DateTime dataCompra, string nomeCliente, string infoCliente)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (sender, e) =>
                {
                    Graphics g = e.Graphics;

                    // Configurar fontes
                    Font fontTitulo = new Font("Arial", 18, FontStyle.Bold);
                    Font fontNormal = new Font("Arial", 12);
                    Font fontNegrito = new Font("Arial", 12, FontStyle.Bold);
                    Font fontPequeno = new Font("Arial", 10);

                    // ... (código de impressão completo)
                    // COLAR AQUI TODO O CÓDIGO DO EVENTO PrintPage
                };

                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = pd;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    pd.Print();
                    MessageBox.Show("Talão enviado para impressão!", "Sucesso",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                pd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao imprimir: {ex.Message}", "Erro",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
