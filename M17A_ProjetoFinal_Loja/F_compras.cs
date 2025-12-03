using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace M17A_ProjetoFinal_Loja
{
    public partial class F_compras : Form
    {
        private BaseDados bd;

        public F_compras(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            CarregarEquipamentos();
            CarregarCategorias();
            CarregarCompatibilidades();
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

        private void btnFiltrar_Click(object sender, EventArgs e)
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
                DataGridViewRow row = dataGridViewEquipamentos.SelectedRows[0];

                try
                {
                    int equipamentoId = Convert.ToInt32(row.Cells["Id"].Value);
                    string nomeEquipamento = row.Cells["Nome"].Value.ToString();
                    decimal preco = Convert.ToDecimal(row.Cells["Preco"].Value);

                    // Registrar a compra
                    RegistrarCompra(equipamentoId, nomeEquipamento, preco);

                    MessageBox.Show($"Compra realizada com sucesso!\nEquipamento: {nomeEquipamento}");
                    LimparCampos();
                    CarregarEquipamentos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro na compra: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Selecione um equipamento para comprar!");
            }
        }

        private void RegistrarCompra(int equipamentoId, string nomeEquipamento, decimal preco)
        {
            // Registrar a compra
            string sqlCompra = @"
                INSERT INTO Compras (EquipamentoId, Quantidade, PrecoUnitario, NumeroFatura) 
                VALUES (@EquipamentoId, @Quantidade, @PrecoUnitario, @NumeroFatura)";

            var parametrosCompra = new List<SqlParameter>
            {
                new SqlParameter("@EquipamentoId", equipamentoId),
                new SqlParameter("@Quantidade", 1),
                new SqlParameter("@PrecoUnitario", preco),
                new SqlParameter("@NumeroFatura", $"FAT-{DateTime.Now:yyyyMMdd-HHmmss}")
            };

            bd.ExecutarSQL(sqlCompra, parametrosCompra);

            // Atualizar estado do equipamento para vendido (se tiveres coluna Estado)
            try
            {
                string updateEquipamento = "UPDATE Equipamentos SET Estado = 0 WHERE Id = @Id";
                var parametrosUpdate = new List<SqlParameter>
                {
                    new SqlParameter("@Id", equipamentoId)
                };
                bd.ExecutarSQL(updateEquipamento, parametrosUpdate);
            }
            catch
            {
                // Se não tiver coluna Estado, ignora
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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
            btnFiltrar_Click(sender, e);
        }

        private void cmbCompatibilidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando seleciona uma compatibilidade
            btnFiltrar_Click(sender, e);
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando digita no nome
            btnFiltrar_Click(sender, e);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            // Filtra automaticamente quando digita no modelo
            btnFiltrar_Click(sender, e);
        }
    }
}