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

        private void bt_guardar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Equipamento
            Equipamentos novo = new Equipamentos(bd);

            // Preencher os dados do equipamento
            novo.Nome = txtNome.Text;
            novo.CodigoProduto = txtCodigo.Text;
            novo.Categoria = cmbCategoria.Text;
            novo.Marca = txtMarca.Text;
            novo.Compatibilidade = cmbCompatibilidade.Text;
            novo.Garantia = txtGarantia.Text;
            novo.Preco = decimal.Parse(txtPreco.Text);
            novo.Descricao = txtDescricao.Text;
            novo.DataEntrada = dtpDataEntrada.Value;
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

            Equipamentos e = new Equipamentos(bd);

            // Use ListarComStatus() em vez de Listar()
            dgv_equipamentos.DataSource = e.ListarComStatus();

            // Colorir vendidos
            foreach (DataGridViewRow row in dgv_equipamentos.Rows)
            {
                if (row.Cells["Status"]?.Value != null &&
                    row.Cells["Status"].Value.ToString() == "VENDIDO")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        // Limpar as TextBox do formulário
        private void LimparForm()
        {
            equipamentoId = 0;
            txtNome.Text = "";
            txtCodigo.Text = "";
            cmbCategoria.SelectedIndex = -1;
            txtMarca.Text = "";
            cmbCompatibilidade.Text = "";
            txtGarantia.Text = "";
            txtPreco.Text = "";
            txtDescricao.Text = "";
            pictureBox1.Image = null;
            imagem = "";
            dtpDataEntrada.Value = DateTime.Now;

            // Mostrar botão inserir, esconder editar/eliminar
            btnInserir.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
        }

        private void F_equipamentos_Load(object sender, EventArgs e)
        {
            // Esconder botões editar/eliminar inicialmente
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
        }

        private void dgv_equipamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dgv_equipamentos.CurrentCell.RowIndex;
            if (linha == -1)
                return;

            equipamentoId = int.Parse(dgv_equipamentos.Rows[linha].Cells[0].Value.ToString());

            // Esconder o botão de adicionar novo
            btnInserir.Visible = false;

            // Preencher o formulário com os dados do equipamento selecionado
            Equipamentos equip = new Equipamentos(bd);
            equip.Id = equipamentoId;
            equip.Procurar();

            txtNome.Text = equip.Nome;
            txtCodigo.Text = equip.CodigoProduto;
            cmbCategoria.Text = equip.Categoria;
            txtMarca.Text = equip.Marca;
            cmbCompatibilidade.Text = equip.Compatibilidade;
            txtGarantia.Text = equip.Garantia;
            txtPreco.Text = equip.Preco.ToString();
            txtDescricao.Text = equip.Descricao;
            dtpDataEntrada.Value = equip.DataEntrada;

            if (System.IO.File.Exists(equip.Imagem))
                pictureBox1.Image = Image.FromFile(equip.Imagem);

            // Mostrar os botões editar/eliminar
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
        }

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
                Equipamentos apagar = new Equipamentos(bd);
                apagar.Id = equipamentoId;
                apagar.Apagar();
                ListarEquipamentos();
                LimparForm();
            }
        }

        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        private void bt_editar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Equipamento
            Equipamentos novo = new Equipamentos(bd);
            novo.Id = equipamentoId;

            // Preencher os dados do equipamento
            novo.Nome = txtNome.Text;
            novo.CodigoProduto = txtCodigo.Text;
            novo.Categoria = cmbCategoria.Text;
            novo.Marca = txtMarca.Text;
            novo.Compatibilidade = cmbCompatibilidade.Text;
            novo.Garantia = txtGarantia.Text;
            novo.Preco = decimal.Parse(txtPreco.Text);
            novo.Descricao = txtDescricao.Text;
            novo.DataEntrada = dtpDataEntrada.Value;
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

        private void dgv_equipamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Não é necessário código aqui
        }

        private void F_equipamentos_Load_1(object sender, EventArgs e)
        {
            // Carregar equipamentos ao abrir o formulário
            Equipamentos equip = new Equipamentos(bd);
            dgv_equipamentos.DataSource = equip.Listar();
        }

        private void btnInserirImagem_Click(object sender, EventArgs e)
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
                    pictureBox1.Image = Image.FromFile(temp);
                    imagem = temp;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
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
                Equipamentos apagar = new Equipamentos(bd);
                apagar.Id = equipamentoId;
                apagar.Apagar();
                ListarEquipamentos();
                LimparForm();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Alterado: Fechar apenas este formulário, não toda a aplicação
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Criar um objeto do tipo Equipamento
            Equipamentos novo = new Equipamentos(bd);
            novo.Id = equipamentoId;

            // Preencher os dados do equipamento
            novo.Nome = txtNome.Text;
            novo.CodigoProduto = txtCodigo.Text;
            novo.Categoria = cmbCategoria.Text;
            novo.Marca = txtMarca.Text;
            novo.Compatibilidade = cmbCompatibilidade.Text;
            novo.Garantia = txtGarantia.Text;
            novo.Preco = decimal.Parse(txtPreco.Text);
            novo.Descricao = txtDescricao.Text;
            novo.DataEntrada = dtpDataEntrada.Value;
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

        private void btnRemoverImagem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            imagem = "";
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            // Este botão chama o mesmo método que bt_guardar
            bt_guardar_Click(sender, e);
        }


    }
}