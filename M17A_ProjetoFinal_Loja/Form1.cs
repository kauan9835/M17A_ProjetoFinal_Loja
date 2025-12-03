using System;
using System.Windows.Forms;

namespace M17A_ProjetoFinal_Loja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Botão COMPRAS
        private void btnCompras_Click(object sender, EventArgs e)
        {
            // Cria a base de dados
            BaseDados bd = new BaseDados("HardwareSystem");

            // Abre o formulário de compras
            F_compras formCompras = new F_compras(bd);
            formCompras.Show();
        }

        // Botão CLIENTES
        private void btnClientes_Click(object sender, EventArgs e)
        {
            // Cria a base de dados
            BaseDados bd = new BaseDados("HardwareSystem");

            // Abre o formulário de clientes
            F_clientes formClientes = new F_clientes(bd);
            formClientes.Show();
        }

        // Botão EQUIPAMENTOS
        private void btnEquipamentos_Click(object sender, EventArgs e)
        {
            // Cria a base de dados
            BaseDados bd = new BaseDados("HardwareSystem");

            // Abre o formulário de equipamentos
            F_equipamentos formEquipamentos = new F_equipamentos(bd);
            formEquipamentos.Show();
        }

        // Botão SAIR
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}