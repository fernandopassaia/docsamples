namespace FuturaDataTCC.Views.Caixa
{
    partial class frmNewFechtCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewFechtCaixa));
            this.pctStatusCaixa = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxPKIDCaixa = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxIdentificacaoCaixa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxValorFechamento = new System.Windows.Forms.TextBox();
            this.btnFechamentoCaixa = new System.Windows.Forms.Button();
            this.tbxObservacao = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxECF = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctStatusCaixa)).BeginInit();
            this.SuspendLayout();
            // 
            // pctStatusCaixa
            // 
            this.pctStatusCaixa.Image = global::FuturaDataTCC.Properties.Resources.caixaFechado;
            this.pctStatusCaixa.Location = new System.Drawing.Point(4, 4);
            this.pctStatusCaixa.Name = "pctStatusCaixa";
            this.pctStatusCaixa.Size = new System.Drawing.Size(120, 80);
            this.pctStatusCaixa.TabIndex = 323;
            this.pctStatusCaixa.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(542, 13);
            this.label1.TabIndex = 324;
            this.label1.Text = "Atenção: Antes de Efetuar o Fechamento de Caixa, não se esqueça de Efetuar a Conf" +
    "erência.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(127, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 339;
            this.label13.Text = "Pk/ID:";
            // 
            // tbxPKIDCaixa
            // 
            this.tbxPKIDCaixa.Location = new System.Drawing.Point(130, 21);
            this.tbxPKIDCaixa.Name = "tbxPKIDCaixa";
            this.tbxPKIDCaixa.ReadOnly = true;
            this.tbxPKIDCaixa.Size = new System.Drawing.Size(54, 20);
            this.tbxPKIDCaixa.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(184, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 13);
            this.label10.TabIndex = 338;
            this.label10.Text = "Identificação do Caixa:";
            // 
            // tbxIdentificacaoCaixa
            // 
            this.tbxIdentificacaoCaixa.Location = new System.Drawing.Point(186, 21);
            this.tbxIdentificacaoCaixa.Name = "tbxIdentificacaoCaixa";
            this.tbxIdentificacaoCaixa.ReadOnly = true;
            this.tbxIdentificacaoCaixa.Size = new System.Drawing.Size(147, 20);
            this.tbxIdentificacaoCaixa.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 342;
            this.label2.Text = "Valor Fechamento:";
            // 
            // tbxValorFechamento
            // 
            this.tbxValorFechamento.Location = new System.Drawing.Point(233, 54);
            this.tbxValorFechamento.Name = "tbxValorFechamento";
            this.tbxValorFechamento.Size = new System.Drawing.Size(100, 20);
            this.tbxValorFechamento.TabIndex = 1;
            // 
            // btnFechamentoCaixa
            // 
            this.btnFechamentoCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechamentoCaixa.Image = global::FuturaDataTCC.Properties.Resources.moneyOut;
            this.btnFechamentoCaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechamentoCaixa.Location = new System.Drawing.Point(339, 42);
            this.btnFechamentoCaixa.Name = "btnFechamentoCaixa";
            this.btnFechamentoCaixa.Size = new System.Drawing.Size(168, 42);
            this.btnFechamentoCaixa.TabIndex = 2;
            this.btnFechamentoCaixa.Text = "Fechamento Caixa";
            this.btnFechamentoCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechamentoCaixa.UseVisualStyleBackColor = true;
            this.btnFechamentoCaixa.Click += new System.EventHandler(this.btnFechamentoCaixa_Click);
            // 
            // tbxObservacao
            // 
            this.tbxObservacao.Location = new System.Drawing.Point(4, 132);
            this.tbxObservacao.MaxLength = 249;
            this.tbxObservacao.Name = "tbxObservacao";
            this.tbxObservacao.Size = new System.Drawing.Size(542, 74);
            this.tbxObservacao.TabIndex = 3;
            this.tbxObservacao.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 346;
            this.label3.Text = "Observação (opcional):";
            // 
            // tbxECF
            // 
            this.tbxECF.Location = new System.Drawing.Point(446, 110);
            this.tbxECF.Name = "tbxECF";
            this.tbxECF.ReadOnly = true;
            this.tbxECF.Size = new System.Drawing.Size(100, 20);
            this.tbxECF.TabIndex = 347;
            // 
            // frmNewFechtCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 208);
            this.Controls.Add(this.tbxECF);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxObservacao);
            this.Controls.Add(this.btnFechamentoCaixa);
            this.Controls.Add(this.tbxValorFechamento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbxPKIDCaixa);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxIdentificacaoCaixa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pctStatusCaixa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewFechtCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fechamento de Caixa";
            ((System.ComponentModel.ISupportInitialize)(this.pctStatusCaixa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctStatusCaixa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbxPKIDCaixa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxIdentificacaoCaixa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxValorFechamento;
        private System.Windows.Forms.Button btnFechamentoCaixa;
        private System.Windows.Forms.RichTextBox tbxObservacao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxECF;
    }
}