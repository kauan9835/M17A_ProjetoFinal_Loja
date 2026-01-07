namespace M17A_ProjetoFinal_Loja
{
    partial class F_equipamentos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnInserir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Modelo = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.dtpDataEntrada = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCompatibilidade = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGarantia = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgv_equipamentos = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnInserirImagem = new System.Windows.Forms.Button();
            this.lblImagem = new System.Windows.Forms.Label();
            this.btnRemoverImagem = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lb_feedback = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_equipamentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(350, 554);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(168, 68);
            this.btnEditar.TabIndex = 65;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnInserir
            // 
            this.btnInserir.Location = new System.Drawing.Point(176, 554);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(168, 68);
            this.btnInserir.TabIndex = 64;
            this.btnInserir.Text = "INSERIR";
            this.btnInserir.UseVisualStyleBackColor = true;
            this.btnInserir.Click += new System.EventHandler(this.bt_guardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(2, 554);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(168, 68);
            this.btnCancelar.TabIndex = 63;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(84, 152);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(259, 22);
            this.txtCodigo.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 16);
            this.label6.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 59;
            this.label4.Text = "Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 58;
            this.label3.Text = "Data de entrada";
            // 
            // Modelo
            // 
            this.Modelo.AutoSize = true;
            this.Modelo.Location = new System.Drawing.Point(25, 152);
            this.Modelo.Name = "Modelo";
            this.Modelo.Size = new System.Drawing.Size(51, 16);
            this.Modelo.TabIndex = 57;
            this.Modelo.Text = "Codigo";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(75, 36);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(268, 22);
            this.txtNome.TabIndex = 69;
            // 
            // dtpDataEntrada
            // 
            this.dtpDataEntrada.Location = new System.Drawing.Point(135, 100);
            this.dtpDataEntrada.Name = "dtpDataEntrada";
            this.dtpDataEntrada.Size = new System.Drawing.Size(208, 22);
            this.dtpDataEntrada.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "Categoria";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Items.AddRange(new object[] {
            "Grafica",
            "Processador"});
            this.cmbCategoria.Location = new System.Drawing.Point(98, 216);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(245, 24);
            this.cmbCategoria.TabIndex = 72;
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(75, 283);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(268, 22);
            this.txtMarca.TabIndex = 74;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 73;
            this.label5.Text = "Marca";
            // 
            // cmbCompatibilidade
            // 
            this.cmbCompatibilidade.FormattingEnabled = true;
            this.cmbCompatibilidade.Location = new System.Drawing.Point(135, 345);
            this.cmbCompatibilidade.Name = "cmbCompatibilidade";
            this.cmbCompatibilidade.Size = new System.Drawing.Size(208, 24);
            this.cmbCompatibilidade.TabIndex = 76;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 16);
            this.label7.TabIndex = 75;
            this.label7.Text = "Compatibilidade";
            // 
            // txtGarantia
            // 
            this.txtGarantia.Location = new System.Drawing.Point(84, 394);
            this.txtGarantia.Name = "txtGarantia";
            this.txtGarantia.Size = new System.Drawing.Size(259, 22);
            this.txtGarantia.TabIndex = 78;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 394);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 77;
            this.label8.Text = "Garantia";
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(74, 437);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(269, 22);
            this.txtPreco.TabIndex = 80;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 437);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 16);
            this.label9.TabIndex = 79;
            this.label9.Text = "Preço";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(95, 476);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(248, 61);
            this.txtDescricao.TabIndex = 82;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 476);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 16);
            this.label10.TabIndex = 81;
            this.label10.Text = "Descrição";
            // 
            // dgv_equipamentos
            // 
            this.dgv_equipamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_equipamentos.Location = new System.Drawing.Point(635, 21);
            this.dgv_equipamentos.Name = "dgv_equipamentos";
            this.dgv_equipamentos.RowHeadersWidth = 51;
            this.dgv_equipamentos.RowTemplate.Height = 24;
            this.dgv_equipamentos.Size = new System.Drawing.Size(601, 267);
            this.dgv_equipamentos.TabIndex = 83;
            this.dgv_equipamentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_equipamentos_CellClick);
            this.dgv_equipamentos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_equipamentos_CellContentClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(635, 350);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(601, 201);
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // btnInserirImagem
            // 
            this.btnInserirImagem.Location = new System.Drawing.Point(635, 571);
            this.btnInserirImagem.Name = "btnInserirImagem";
            this.btnInserirImagem.Size = new System.Drawing.Size(168, 68);
            this.btnInserirImagem.TabIndex = 85;
            this.btnInserirImagem.Text = "INSERIR IMAGEM";
            this.btnInserirImagem.UseVisualStyleBackColor = true;
            this.btnInserirImagem.Click += new System.EventHandler(this.btnInserirImagem_Click);
            // 
            // lblImagem
            // 
            this.lblImagem.AutoSize = true;
            this.lblImagem.Location = new System.Drawing.Point(649, 370);
            this.lblImagem.Name = "lblImagem";
            this.lblImagem.Size = new System.Drawing.Size(60, 16);
            this.lblImagem.TabIndex = 86;
            this.lblImagem.Text = "IMAGEM";
            // 
            // btnRemoverImagem
            // 
            this.btnRemoverImagem.Location = new System.Drawing.Point(1068, 571);
            this.btnRemoverImagem.Name = "btnRemoverImagem";
            this.btnRemoverImagem.Size = new System.Drawing.Size(168, 68);
            this.btnRemoverImagem.TabIndex = 87;
            this.btnRemoverImagem.Text = "REMOVER IMAGEM";
            this.btnRemoverImagem.UseVisualStyleBackColor = true;
            this.btnRemoverImagem.Click += new System.EventHandler(this.btnRemoverImagem_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(350, 464);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(168, 68);
            this.btnEliminar.TabIndex = 88;
            this.btnEliminar.Text = "ELIMINAR";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lb_feedback
            // 
            this.lb_feedback.AutoSize = true;
            this.lb_feedback.Location = new System.Drawing.Point(402, 75);
            this.lb_feedback.Name = "lb_feedback";
            this.lb_feedback.Size = new System.Drawing.Size(0, 16);
            this.lb_feedback.TabIndex = 89;
            // 
            // F_equipamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 662);
            this.Controls.Add(this.lb_feedback);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnRemoverImagem);
            this.Controls.Add(this.lblImagem);
            this.Controls.Add(this.btnInserirImagem);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dgv_equipamentos);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtGarantia);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCompatibilidade);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDataEntrada);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Modelo);
            this.Name = "F_equipamentos";
            this.Text = "F_equipamentos";
            this.Load += new System.EventHandler(this.F_equipamentos_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_equipamentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Modelo;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.DateTimePicker dtpDataEntrada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCompatibilidade;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGarantia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgv_equipamentos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnInserirImagem;
        private System.Windows.Forms.Label lblImagem;
        private System.Windows.Forms.Button btnRemoverImagem;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lb_feedback;
    }
}