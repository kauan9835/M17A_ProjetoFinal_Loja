namespace M17A_ProjetoFinal_Loja
{
    partial class F_compras
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
            this.btnComprar = new System.Windows.Forms.Button();
            this.dataGridViewEquipamentos = new System.Windows.Forms.DataGridView();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Modelo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbCompatibilidade = new System.Windows.Forms.ComboBox();
            this.lb_feedback = new System.Windows.Forms.Label();
            this.lblSelecionarCliente = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.panelTalao = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipamentos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnComprar
            // 
            this.btnComprar.Location = new System.Drawing.Point(227, 363);
            this.btnComprar.Name = "btnComprar";
            this.btnComprar.Size = new System.Drawing.Size(168, 68);
            this.btnComprar.TabIndex = 52;
            this.btnComprar.Text = "COMPRAR";
            this.btnComprar.UseVisualStyleBackColor = true;
            this.btnComprar.Click += new System.EventHandler(this.btnComprar_Click);
            // 
            // dataGridViewEquipamentos
            // 
            this.dataGridViewEquipamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEquipamentos.Location = new System.Drawing.Point(703, 37);
            this.dataGridViewEquipamentos.Name = "dataGridViewEquipamentos";
            this.dataGridViewEquipamentos.RowHeadersWidth = 51;
            this.dataGridViewEquipamentos.RowTemplate.Height = 24;
            this.dataGridViewEquipamentos.Size = new System.Drawing.Size(756, 544);
            this.dataGridViewEquipamentos.TabIndex = 51;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(53, 363);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(168, 68);
            this.btnFiltrar.TabIndex = 50;
            this.btnFiltrar.Text = "FILTRAR";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(142, 437);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(168, 68);
            this.btnCancelar.TabIndex = 49;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(109, 273);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(286, 22);
            this.txtModelo.TabIndex = 48;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(100, 103);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(292, 22);
            this.txtNome.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 16);
            this.label6.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 37;
            this.label4.Text = "Categoria";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Compatibilidade";
            // 
            // Modelo
            // 
            this.Modelo.AutoSize = true;
            this.Modelo.Location = new System.Drawing.Point(50, 273);
            this.Modelo.Name = "Modelo";
            this.Modelo.Size = new System.Drawing.Size(53, 16);
            this.Modelo.TabIndex = 35;
            this.Modelo.Text = "Modelo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 34;
            this.label1.Text = "Nome";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(221, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 16);
            this.label9.TabIndex = 53;
            this.label9.Text = "Filtro";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(122, 157);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(270, 24);
            this.cmbCategoria.TabIndex = 54;
            // 
            // cmbCompatibilidade
            // 
            this.cmbCompatibilidade.FormattingEnabled = true;
            this.cmbCompatibilidade.Location = new System.Drawing.Point(162, 218);
            this.cmbCompatibilidade.Name = "cmbCompatibilidade";
            this.cmbCompatibilidade.Size = new System.Drawing.Size(230, 24);
            this.cmbCompatibilidade.TabIndex = 55;
            // 
            // lb_feedback
            // 
            this.lb_feedback.AutoSize = true;
            this.lb_feedback.Location = new System.Drawing.Point(433, 37);
            this.lb_feedback.Name = "lb_feedback";
            this.lb_feedback.Size = new System.Drawing.Size(0, 16);
            this.lb_feedback.TabIndex = 56;
            // 
            // lblSelecionarCliente
            // 
            this.lblSelecionarCliente.AutoSize = true;
            this.lblSelecionarCliente.Location = new System.Drawing.Point(327, 37);
            this.lblSelecionarCliente.Name = "lblSelecionarCliente";
            this.lblSelecionarCliente.Size = new System.Drawing.Size(116, 16);
            this.lblSelecionarCliente.TabIndex = 57;
            this.lblSelecionarCliente.Text = "Selecionar Cliente";
            // 
            // cmbClientes
            // 
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(439, 37);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(201, 24);
            this.cmbClientes.TabIndex = 58;
            // 
            // panelTalao
            // 
            this.panelTalao.Location = new System.Drawing.Point(440, 103);
            this.panelTalao.Name = "panelTalao";
            this.panelTalao.Size = new System.Drawing.Size(200, 328);
            this.panelTalao.TabIndex = 59;
            // 
            // F_compras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 619);
            this.Controls.Add(this.panelTalao);
            this.Controls.Add(this.cmbClientes);
            this.Controls.Add(this.lblSelecionarCliente);
            this.Controls.Add(this.lb_feedback);
            this.Controls.Add(this.cmbCompatibilidade);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnComprar);
            this.Controls.Add(this.dataGridViewEquipamentos);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Modelo);
            this.Controls.Add(this.label1);
            this.Name = "F_compras";
            this.Text = "F_compras";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipamentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnComprar;
        private System.Windows.Forms.DataGridView dataGridViewEquipamentos;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Modelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.ComboBox cmbCompatibilidade;
        private System.Windows.Forms.Label lb_feedback;
        private System.Windows.Forms.Label lblSelecionarCliente;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Panel panelTalao;
    }
}