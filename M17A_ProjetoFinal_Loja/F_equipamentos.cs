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
        string imagem = "";
        BaseDados bd;
        int equipamentoId = 0;

        public F_equipamentos(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        // Botão para procurar imagem
        private void bt_procurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ficheiro = new OpenFileDialog();
            ficheiro.InitialDirectory = "C:\\";
            ficheiro.Multiselect = false;
            ficheiro.Filter = "Imagens |*.jpg;*.jpeg;*.png;*.bmp | Todos os ficheiros |*.*";

            if (ficheiro.ShowDialog() == DialogResult.OK)
            {
                string temp = ficheiro.FileName;
                if (System.IO.File.Exists(temp))
                {
                    pb_imagem.Image = Image.FromFile(temp);
                    imagem = temp;
                }
            }
        }

        // Guardar o equipamento na base de dados
        private void bt_guardar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Equipamento
            Equipamento novo = new Equipamento(bd);

            // Preencher os dados do equipamento
            novo.Nome = txtNome.Text;
            novo.CodigoProduto = txtCodigo.Text;
            novo.Categoria = cmbCategoria.Text;
            novo.Marca = txtMarca.Text;
            novo.Compatibilidade = txtCompatibilidade.Text;
            novo.Garantia = txtGarantia.Text;
            novo.Preco = decimal.Parse(txtPreco.Text);
            novo.Descricao = txtDescricao.Text;
            novo.DataEntrada = dtpData.Value;
            novo.Imagem = Utils.PastaDoPrograma("M17A_Loja") + @"\" + novo.CodigoProduto + ".jpg";

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

            // Copiar a imagem para a pasta do programa
            if (imagem != "")
            {
                if (System.IO.File.Exists(imagem))
                    System.IO.File.Copy(imagem, novo.Imagem, true);
            }

            // Limpar o formulário
            LimparForm();

            // Atualizar a lista dos equipamentos no DataGrid
            ListarEquipamentos();

            // Feedback ao utilizador
            lb_feedback.Text = "Equipamento adicionado com sucesso.";
            lb_feedback.ForeColor = Color.Black;
        }

        // Atualizar a lista dos equipamentos no DataGridView
        private void ListarEquipamentos()
        {
            dgv_equipamentos.AllowUserToAddRows = false;
            dgv_equipamentos.ReadOnly = true;
            dgv_equipamentos.AllowUserToDeleteRows = false;
            dgv_equipamentos.MultiSelect = false;
            dgv_equipamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            Equipamento e = new Equipamento(bd);
            dgv_equipamentos.DataSource = e.Listar();
        }

        // Limpar as TextBox do formulário
        private void LimparForm()
        {
            equipamentoId = 0;
            txtNome.Text = "";
            txtCodigo.Text = "";
            cmbCategoria.SelectedIndex = -1;
            txtMarca.Text = "";
            txtCompatibilidade.Text = "";
            txtGarantia.Text = "";
            txtPreco.Text = "";
            txtDescricao.Text = "";
            pb_imagem.Image = null;
            imagem = "";
            dtpData.Value = DateTime.Now;

            // Mostrar botão guardar, esconder editar/eliminar
            bt_guardar.Visible = true;
            bt_editar.Visible = false;
            bt_eliminar.Visible = false;
        }

        private void F_equipamentos_Load(object sender, EventArgs e)
        {
            ListarEquipamentos();
            // Esconder botões editar/eliminar inicialmente
            bt_editar.Visible = false;
            bt_eliminar.Visible = false;
        }

        private void dgv_equipamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dgv_equipamentos.CurrentCell.RowIndex;
            if (linha == -1)
                return;

            equipamentoId = int.Parse(dgv_equipamentos.Rows[linha].Cells[0].Value.ToString());

            // Esconder o botão de adicionar novo
            bt_guardar.Visible = false;

            // Preencher o formulário com os dados do equipamento selecionado
            Equipamento equip = new Equipamento(bd);
            equip.Id = equipamentoId;
            equip.Procurar();

            txtNome.Text = equip.Nome;
            txtCodigo.Text = equip.CodigoProduto;
            cmbCategoria.Text = equip.Categoria;
            txtMarca.Text = equip.Marca;
            txtCompatibilidade.Text = equip.Compatibilidade;
            txtGarantia.Text = equip.Garantia;
            txtPreco.Text = equip.Preco.ToString();
            txtDescricao.Text = equip.Descricao;
            dtpData.Value = equip.DataEntrada;

            if (System.IO.File.Exists(equip.Imagem))
                pb_imagem.Image = Image.FromFile(equip.Imagem);

            // Mostrar os botões editar/eliminar
            bt_editar.Visible = true;
            bt_eliminar.Visible = true;
        }

        // Eliminar o equipamento selecionado
        private void bt_eliminar_Click(object sender, EventArgs e)
        {
            if (equipamentoId == 0)
            {
                MessageBox.Show("Tem de selecionar um equipamento primeiro.");
                return;
            }

            // Confirmar
            if (MessageBox.Show("Tem a certeza que pretende remover o equipamento selecionado?",
                "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Equipamento apagar = new Equipamento(bd);
                apagar.Id = equipamentoId;
                apagar.Apagar();
                ListarEquipamentos();
                LimparForm();
            }
        }

        // Botão cancelar limpa o formulário
        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        // Botão para atualizar o registo selecionado
        private void bt_editar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Equipamento
            Equipamento novo = new Equipamento(bd);
            novo.Id = equipamentoId;

            // Preencher os dados do equipamento
            novo.Nome = txtNome.Text;
            novo.CodigoProduto = txtCodigo.Text;
            novo.Categoria = cmbCategoria.Text;
            novo.Marca = txtMarca.Text;
            novo.Compatibilidade = txtCompatibilidade.Text;
            novo.Garantia = txtGarantia.Text;
            novo.Preco = decimal.Parse(txtPreco.Text);
            novo.Descricao = txtDescricao.Text;
            novo.DataEntrada = dtpData.Value;
            novo.Imagem = Utils.PastaDoPrograma("M17A_Loja") + @"\" + novo.CodigoProduto + ".jpg";

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

            // Copiar a imagem para a pasta do programa
            if (imagem != "")
            {
                if (System.IO.File.Exists(imagem))
                    System.IO.File.Copy(imagem, novo.Imagem, true);
            }

            // Limpar o formulário
            LimparForm();

            // Atualizar a lista dos equipamentos no DataGrid
            ListarEquipamentos();

            // Feedback ao utilizador
            lb_feedback.Text = "Equipamento atualizado com sucesso.";
            lb_feedback.ForeColor = Color.Black;
        }

        // Pesquisar equipamentos cujo nome é "parecido" com o texto da TextBox
        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            Equipamento equip = new Equipamento(bd);
            dgv_equipamentos.DataSource = equip.Procurar("Nome", txt_pesquisa.Text);
        }
    }
}