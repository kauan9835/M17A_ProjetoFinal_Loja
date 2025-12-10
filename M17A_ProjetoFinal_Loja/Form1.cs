using M17A_ProjetoFinal_Loja;
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
    public partial class Form1 : Form
    {
        BaseDados bd;
        public Form1()
        {
            InitializeComponent();
            bd = new BaseDados("M17A_loja");
        }

        private void cb_consultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_consultas.SelectedIndex <= 0) return;
            string[] consultas = new string[] { "",
                @"SELECT Compras.*, Clientes.Nome, Equipamentos.Nome as NomeEquipamento 
                FROM Compras INNER JOIN Clientes ON Clientes.Id = Compras.ClienteId
                INNER JOIN Equipamentos ON Equipamentos.Id = Compras.EquipamentoId" };
            DataTable dados = bd.DevolveSQL(consultas[cb_consultas.SelectedIndex]);

            dgv_consultas.DataSource = dados;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgv_consultas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ficheiroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void leitoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cb_consultas_Click(object sender, EventArgs e)
        {

        }

        private void dgv_consultas_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cb_consultas_Click_1(object sender, EventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void livrosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            F_equipamentos f = new F_equipamentos(bd);
            f.Show();
        }

        private void empréstimosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            F_compras f = new F_compras(bd);
            f.Show();
        }

        private void leitoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            F_clientes f = new F_clientes(bd);
            f.Show();
        }
    }
}