using M17A_ProjetoFinal_Loja.CLIENTES;
using M17A_ProjetoFinal_Loja.Compras;
using M17A_ProjetoFinal_Loja.EQUIPAMENTOS;
using System;
using System.Windows.Forms;

namespace M17A_ProjetoFinal_Loja
{
    public partial class Form1 : Form
    {
        private BaseDados bd;

        public Form1()
        {
            InitializeComponent();
            bd = new BaseDados("HardwareSystem");
        }

        // Botão COMPRAR
        private void btnComprar_Click(object sender, EventArgs e)
        {
            F_compras formCompras = new F_compras(bd);
            formCompras.ShowDialog();
        }

        // Botão ADICIONAR EQUIPAMENTO
        private void btnAdicionarEquipamento_Click(object sender, EventArgs e)
        {
            F_equipamentos formEquipamentos = new F_equipamentos(bd);
            formEquipamentos.ShowDialog();
        }

        // Botão ADICIONAR CLIENTE
        private void btnAdicionarCliente_Click(object sender, EventArgs e)
        {
            F_clientes formClientes = new F_clientes(bd);
            formClientes.ShowDialog();
        }

        // Botão SAIR
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}