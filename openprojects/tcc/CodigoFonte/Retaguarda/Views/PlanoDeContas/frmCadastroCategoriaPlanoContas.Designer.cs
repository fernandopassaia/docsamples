namespace FuturaDataTCC.Views.PlanoDeContas
{
    partial class frmCadastroCategoriaPlanoContas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroCategoriaPlanoContas));
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDescricaoCategoria = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxMascara = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTipoDoMovimento = new System.Windows.Forms.ComboBox();
            this.btnInserirPlanoContas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Descrição da Categoria:";
            // 
            // tbxDescricaoCategoria
            // 
            this.tbxDescricaoCategoria.Location = new System.Drawing.Point(139, 5);
            this.tbxDescricaoCategoria.MaxLength = 79;
            this.tbxDescricaoCategoria.Name = "tbxDescricaoCategoria";
            this.tbxDescricaoCategoria.Size = new System.Drawing.Size(247, 20);
            this.tbxDescricaoCategoria.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Máscara (seqüencial-auto):";
            // 
            // tbxMascara
            // 
            this.tbxMascara.Location = new System.Drawing.Point(277, 55);
            this.tbxMascara.Name = "tbxMascara";
            this.tbxMascara.ReadOnly = true;
            this.tbxMascara.Size = new System.Drawing.Size(109, 20);
            this.tbxMascara.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo do Movimento:";
            // 
            // cbbTipoDoMovimento
            // 
            this.cbbTipoDoMovimento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTipoDoMovimento.FormattingEnabled = true;
            this.cbbTipoDoMovimento.Items.AddRange(new object[] {
            "1 - Entrada/Receita",
            "2 - Saída/Despesa"});
            this.cbbTipoDoMovimento.Location = new System.Drawing.Point(139, 29);
            this.cbbTipoDoMovimento.Name = "cbbTipoDoMovimento";
            this.cbbTipoDoMovimento.Size = new System.Drawing.Size(247, 21);
            this.cbbTipoDoMovimento.TabIndex = 293;
            this.cbbTipoDoMovimento.SelectedIndexChanged += new System.EventHandler(this.cbbTipoDoMovimento_SelectedIndexChanged);
            // 
            // btnInserirPlanoContas
            // 
            this.btnInserirPlanoContas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserirPlanoContas.Image = global::FuturaDataTCC.Properties.Resources.moneyIn;
            this.btnInserirPlanoContas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirPlanoContas.Location = new System.Drawing.Point(61, 81);
            this.btnInserirPlanoContas.Name = "btnInserirPlanoContas";
            this.btnInserirPlanoContas.Size = new System.Drawing.Size(325, 45);
            this.btnInserirPlanoContas.TabIndex = 305;
            this.btnInserirPlanoContas.Text = "Incluir Categoria Plano Conta Entrada/Receita";
            this.btnInserirPlanoContas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirPlanoContas.UseVisualStyleBackColor = true;
            this.btnInserirPlanoContas.Click += new System.EventHandler(this.btnInserirPlanoContas_Click);
            // 
            // frmCadastroCategoriaPlanoContas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 128);
            this.Controls.Add(this.btnInserirPlanoContas);
            this.Controls.Add(this.cbbTipoDoMovimento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxMascara);
            this.Controls.Add(this.tbxDescricaoCategoria);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCadastroCategoriaPlanoContas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Categoria de Plano de Contas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDescricaoCategoria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxMascara;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbTipoDoMovimento;
        private System.Windows.Forms.Button btnInserirPlanoContas;
    }
}