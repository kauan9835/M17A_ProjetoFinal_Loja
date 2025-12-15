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
            DataTable dt = Compra.ObterEquipamentosDisponiveis(bd);

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
            DataTable dt = Compra.ObterEquipamentosDisponiveis(bd, nome, categoria, compatibilidade, modelo);

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

                    // USAR A CLASSE COMPRA (sem SQL no F_compras)
                    var compra = new Compra(bd)
                    {
                        ClienteId = clienteId,
                        EquipamentoId = equipamentoId,
                        Quantidade = 1,
                        PrecoUnitario = preco,
                        NumeroFatura = numeroFatura,
                        DataCompra = dataCompra
                    };

                    // Validar os dados
                    var erros = compra.Validar();
                    if (erros.Count > 0)
                    {
                        MessageBox.Show($"Erros na compra:\n{string.Join("\n", erros)}",
                                       "Erro de Validação",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                        return;
                    }

                    // Adicionar a compra (SQL está na classe Compra)
                    compra.Adicionar();

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
           {
    try
    {
        // Limpar panel
        panelTalao.Controls.Clear();
        panelTalao.Visible = true;
        panelTalao.BackColor = Color.White;
        panelTalao.BorderStyle = BorderStyle.FixedSingle;

        int yPos = 20;
        int margem = 20;

        // 1. CABEÇALHO (já tem)
        Label lblCabecalho = new Label();
        lblCabecalho.Text = "LOJA DE EQUIPAMENTOS";
        lblCabecalho.Font = new Font("Arial", 16, FontStyle.Bold);
        lblCabecalho.Location = new Point(margem, yPos);
        lblCabecalho.Size = new Size(400, 30);
        lblCabecalho.TextAlign = ContentAlignment.MiddleCenter;
        panelTalao.Controls.Add(lblCabecalho);
        yPos += 40;

        // 2. Fatura e Data
        Label lblFatura = new Label();
        lblFatura.Text = $"Fatura: {numeroFatura}";
        lblFatura.Font = new Font("Arial", 10);
        lblFatura.Location = new Point(margem, yPos);
        lblFatura.Size = new Size(400, 20);
        panelTalao.Controls.Add(lblFatura);
        yPos += 25;

        Label lblData = new Label();
        lblData.Text = $"Data: {dataCompra:dd/MM/yyyy HH:mm}";
        lblData.Font = new Font("Arial", 10);
        lblData.Location = new Point(margem, yPos);
        lblData.Size = new Size(400, 20);
        panelTalao.Controls.Add(lblData);
        yPos += 30;

        // 3. Cliente
        Label lblCliente = new Label();
        lblCliente.Text = $"Cliente: {nomeCliente}";
        lblCliente.Font = new Font("Arial", 10, FontStyle.Bold);
        lblCliente.Location = new Point(margem, yPos);
        lblCliente.Size = new Size(400, 20);
        panelTalao.Controls.Add(lblCliente);
        yPos += 25;

        // 4. Produto
        Label lblProduto = new Label();
        lblProduto.Text = $"Produto: {nomeEquipamento}";
        lblProduto.Font = new Font("Arial", 10);
        lblProduto.Location = new Point(margem, yPos);
        lblProduto.Size = new Size(400, 20);
        panelTalao.Controls.Add(lblProduto);
        yPos += 25;

        // 5. Preço
        Label lblPreco = new Label();
        lblPreco.Text = $"Preço: {preco:C2}";
        lblPreco.Font = new Font("Arial", 10);
        lblPreco.Location = new Point(margem, yPos);
        lblPreco.Size = new Size(400, 20);
        panelTalao.Controls.Add(lblPreco);
        yPos += 40;

        // 6. Botão Fechar
        Button btnFechar = new Button();
        btnFechar.Text = "Fechar";
        btnFechar.Location = new Point(150, yPos);
        btnFechar.Size = new Size(100, 30);
        btnFechar.Click += (s, e) => panelTalao.Visible = false;
        panelTalao.Controls.Add(btnFechar);

        // Ajustar tamanho do panel
        panelTalao.Size = new Size(440, yPos + 60);
        panelTalao.Location = new Point(
            (this.ClientSize.Width - panelTalao.Width) / 2,
            (this.ClientSize.Height - panelTalao.Height) / 2
        );
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erro ao mostrar talão: {ex.Message}");
    }
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
