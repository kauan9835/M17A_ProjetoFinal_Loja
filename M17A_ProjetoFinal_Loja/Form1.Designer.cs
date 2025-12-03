namespace M17A_ProjetoFinal_Loja
{
    partial class Form1
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
            this.btnCompras = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnEquipamentos = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCompras
            // 
            this.btnCompras.Location = new System.Drawing.Point(27, 24);
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Size = new System.Drawing.Size(127, 50);
            this.btnCompras.TabIndex = 0;
            this.btnCompras.Text = "COMPRAS";
            this.btnCompras.UseVisualStyleBackColor = true;
            this.btnCompras.Click += new System.EventHandler(this.btnCompras_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(180, 94);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(127, 47);
            this.btnSair.TabIndex = 1;
            this.btnSair.Text = "SAIR";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnEquipamentos
            // 
            this.btnEquipamentos.Location = new System.Drawing.Point(338, 24);
            this.btnEquipamentos.Name = "btnEquipamentos";
            this.btnEquipamentos.Size = new System.Drawing.Size(127, 50);
            this.btnEquipamentos.TabIndex = 2;
            this.btnEquipamentos.Text = "EQUIPAMENTOS";
            this.btnEquipamentos.UseVisualStyleBackColor = true;
            this.btnEquipamentos.Click += new System.EventHandler(this.btnEquipamentos_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Location = new System.Drawing.Point(180, 24);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(127, 50);
            this.btnClientes.TabIndex = 3;
            this.btnClientes.Text = "CLIENTES";
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 661);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnEquipamentos);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnCompras);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnEquipamentos;
        private System.Windows.Forms.Button btnClientes;
    }
}

