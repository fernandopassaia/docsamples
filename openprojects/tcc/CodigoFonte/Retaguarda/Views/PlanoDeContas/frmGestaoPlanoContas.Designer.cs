namespace FuturaDataTCC.Views.PlanoDeContas
{
    partial class frmGestaoPlanoContas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGestaoPlanoContas));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIncluirPlanoMestreDespesa = new System.Windows.Forms.Button();
            this.btnIncluirPlanoMestreReceita = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lvwPlanoDeContas = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 27);
            this.panel1.TabIndex = 309;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Duplo Clique sobre um Plano de Contas para Verificar a Movimentação do Plano.";
            // 
            // btnIncluirPlanoMestreDespesa
            // 
            this.btnIncluirPlanoMestreDespesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncluirPlanoMestreDespesa.Image = global::FuturaDataTCC.Properties.Resources.moneyOut;
            this.btnIncluirPlanoMestreDespesa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncluirPlanoMestreDespesa.Location = new System.Drawing.Point(280, 302);
            this.btnIncluirPlanoMestreDespesa.Name = "btnIncluirPlanoMestreDespesa";
            this.btnIncluirPlanoMestreDespesa.Size = new System.Drawing.Size(204, 45);
            this.btnIncluirPlanoMestreDespesa.TabIndex = 305;
            this.btnIncluirPlanoMestreDespesa.Text = "Incluir Sub-Categoria Plano Conta Saída/Despesa";
            this.btnIncluirPlanoMestreDespesa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIncluirPlanoMestreDespesa.UseVisualStyleBackColor = true;
            this.btnIncluirPlanoMestreDespesa.Click += new System.EventHandler(this.btnIncluirPlanoMestreDespesa_Click);
            // 
            // btnIncluirPlanoMestreReceita
            // 
            this.btnIncluirPlanoMestreReceita.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncluirPlanoMestreReceita.Image = global::FuturaDataTCC.Properties.Resources.moneyIn;
            this.btnIncluirPlanoMestreReceita.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncluirPlanoMestreReceita.Location = new System.Drawing.Point(60, 302);
            this.btnIncluirPlanoMestreReceita.Name = "btnIncluirPlanoMestreReceita";
            this.btnIncluirPlanoMestreReceita.Size = new System.Drawing.Size(220, 45);
            this.btnIncluirPlanoMestreReceita.TabIndex = 304;
            this.btnIncluirPlanoMestreReceita.Text = "Incluir Categoria Mestre Plano Conta Entrada/Receita";
            this.btnIncluirPlanoMestreReceita.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIncluirPlanoMestreReceita.UseVisualStyleBackColor = true;
            this.btnIncluirPlanoMestreReceita.Visible = false;
            this.btnIncluirPlanoMestreReceita.Click += new System.EventHandler(this.btnIncluirPlanoMestreReceita_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(775, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 316;
            this.label4.Text = "-";
            // 
            // lvwPlanoDeContas
            // 
            this.lvwPlanoDeContas.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwPlanoDeContas.FullRowSelect = true;
            this.lvwPlanoDeContas.GridLines = true;
            this.lvwPlanoDeContas.Location = new System.Drawing.Point(2, 31);
            this.lvwPlanoDeContas.Name = "lvwPlanoDeContas";
            this.lvwPlanoDeContas.Size = new System.Drawing.Size(482, 269);
            this.lvwPlanoDeContas.TabIndex = 317;
            this.lvwPlanoDeContas.UseCompatibleStateImageBehavior = false;
            this.lvwPlanoDeContas.View = System.Windows.Forms.View.Details;
            this.lvwPlanoDeContas.DoubleClick += new System.EventHandler(this.lvwPlanoDeContas_DoubleClick);
            // 
            // frmGestaoPlanoContas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 348);
            this.Controls.Add(this.lvwPlanoDeContas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnIncluirPlanoMestreDespesa);
            this.Controls.Add(this.btnIncluirPlanoMestreReceita);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGestaoPlanoContas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro e Analíse dos Planos de Contas.";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIncluirPlanoMestreDespesa;
        private System.Windows.Forms.Button btnIncluirPlanoMestreReceita;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ListView lvwPlanoDeContas;
    }
}