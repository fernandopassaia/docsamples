namespace FuturaDataTCC.Relatorios.Caixa
{
    partial class frmNewRelatAnalCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewRelatAnalCaixa));
            this.tbcRelatorioCaixa = new System.Windows.Forms.TabControl();
            this.tbpPedidosFechados = new System.Windows.Forms.TabPage();
            this.rpwRelatCaixaPedidosFechados = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tbpMovimentoEstoque = new System.Windows.Forms.TabPage();
            this.rpwRelatCaixaMovimentoEstoque = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tbpMovimentacaoFinanceira = new System.Windows.Forms.TabPage();
            this.rpwRelatCaixaMovimFinanceira = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tbpInformacoesGeraisCaixa = new System.Windows.Forms.TabPage();
            this.rpwRelatCaixaConfFechamento = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.prgEnvioEmail = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAguardando = new System.Windows.Forms.Label();
            this.btnEnvioEmail = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxEnviarEmailPara = new System.Windows.Forms.TextBox();
            this.tbxTituloEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxMensagemAdicionalEmail = new System.Windows.Forms.TextBox();
            this.tbcRelatorioCaixa.SuspendLayout();
            this.tbpPedidosFechados.SuspendLayout();
            this.tbpMovimentoEstoque.SuspendLayout();
            this.tbpMovimentacaoFinanceira.SuspendLayout();
            this.tbpInformacoesGeraisCaixa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcRelatorioCaixa
            // 
            this.tbcRelatorioCaixa.Controls.Add(this.tbpPedidosFechados);
            this.tbcRelatorioCaixa.Controls.Add(this.tbpMovimentoEstoque);
            this.tbcRelatorioCaixa.Controls.Add(this.tbpMovimentacaoFinanceira);
            this.tbcRelatorioCaixa.Controls.Add(this.tbpInformacoesGeraisCaixa);
            this.tbcRelatorioCaixa.Location = new System.Drawing.Point(4, 2);
            this.tbcRelatorioCaixa.Name = "tbcRelatorioCaixa";
            this.tbcRelatorioCaixa.SelectedIndex = 0;
            this.tbcRelatorioCaixa.Size = new System.Drawing.Size(961, 596);
            this.tbcRelatorioCaixa.TabIndex = 0;
            // 
            // tbpPedidosFechados
            // 
            this.tbpPedidosFechados.Controls.Add(this.rpwRelatCaixaPedidosFechados);
            this.tbpPedidosFechados.Location = new System.Drawing.Point(4, 22);
            this.tbpPedidosFechados.Name = "tbpPedidosFechados";
            this.tbpPedidosFechados.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPedidosFechados.Size = new System.Drawing.Size(953, 570);
            this.tbpPedidosFechados.TabIndex = 0;
            this.tbpPedidosFechados.Text = "Pedidos Fechados";
            this.tbpPedidosFechados.UseVisualStyleBackColor = true;
            // 
            // rpwRelatCaixaPedidosFechados
            // 
            this.rpwRelatCaixaPedidosFechados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rpwRelatCaixaPedidosFechados.Location = new System.Drawing.Point(3, 3);
            this.rpwRelatCaixaPedidosFechados.Name = "rpwRelatCaixaPedidosFechados";
            this.rpwRelatCaixaPedidosFechados.ShowBackButton = false;
            this.rpwRelatCaixaPedidosFechados.ShowDocumentMapButton = false;
            this.rpwRelatCaixaPedidosFechados.ShowFindControls = false;
            this.rpwRelatCaixaPedidosFechados.ShowRefreshButton = false;
            this.rpwRelatCaixaPedidosFechados.ShowStopButton = false;
            this.rpwRelatCaixaPedidosFechados.Size = new System.Drawing.Size(947, 564);
            this.rpwRelatCaixaPedidosFechados.TabIndex = 292;
            // 
            // tbpMovimentoEstoque
            // 
            this.tbpMovimentoEstoque.Controls.Add(this.rpwRelatCaixaMovimentoEstoque);
            this.tbpMovimentoEstoque.Location = new System.Drawing.Point(4, 22);
            this.tbpMovimentoEstoque.Name = "tbpMovimentoEstoque";
            this.tbpMovimentoEstoque.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMovimentoEstoque.Size = new System.Drawing.Size(953, 570);
            this.tbpMovimentoEstoque.TabIndex = 1;
            this.tbpMovimentoEstoque.Text = "Movimento Estoque";
            this.tbpMovimentoEstoque.UseVisualStyleBackColor = true;
            // 
            // rpwRelatCaixaMovimentoEstoque
            // 
            this.rpwRelatCaixaMovimentoEstoque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rpwRelatCaixaMovimentoEstoque.Location = new System.Drawing.Point(3, 3);
            this.rpwRelatCaixaMovimentoEstoque.Name = "rpwRelatCaixaMovimentoEstoque";
            this.rpwRelatCaixaMovimentoEstoque.ShowBackButton = false;
            this.rpwRelatCaixaMovimentoEstoque.ShowDocumentMapButton = false;
            this.rpwRelatCaixaMovimentoEstoque.ShowFindControls = false;
            this.rpwRelatCaixaMovimentoEstoque.ShowRefreshButton = false;
            this.rpwRelatCaixaMovimentoEstoque.ShowStopButton = false;
            this.rpwRelatCaixaMovimentoEstoque.Size = new System.Drawing.Size(947, 564);
            this.rpwRelatCaixaMovimentoEstoque.TabIndex = 293;
            // 
            // tbpMovimentacaoFinanceira
            // 
            this.tbpMovimentacaoFinanceira.Controls.Add(this.rpwRelatCaixaMovimFinanceira);
            this.tbpMovimentacaoFinanceira.Location = new System.Drawing.Point(4, 22);
            this.tbpMovimentacaoFinanceira.Name = "tbpMovimentacaoFinanceira";
            this.tbpMovimentacaoFinanceira.Size = new System.Drawing.Size(953, 570);
            this.tbpMovimentacaoFinanceira.TabIndex = 2;
            this.tbpMovimentacaoFinanceira.Text = "Movimentação Financeira";
            this.tbpMovimentacaoFinanceira.UseVisualStyleBackColor = true;
            // 
            // rpwRelatCaixaMovimFinanceira
            // 
            this.rpwRelatCaixaMovimFinanceira.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rpwRelatCaixaMovimFinanceira.Location = new System.Drawing.Point(3, 3);
            this.rpwRelatCaixaMovimFinanceira.Name = "rpwRelatCaixaMovimFinanceira";
            this.rpwRelatCaixaMovimFinanceira.ShowBackButton = false;
            this.rpwRelatCaixaMovimFinanceira.ShowDocumentMapButton = false;
            this.rpwRelatCaixaMovimFinanceira.ShowFindControls = false;
            this.rpwRelatCaixaMovimFinanceira.ShowRefreshButton = false;
            this.rpwRelatCaixaMovimFinanceira.ShowStopButton = false;
            this.rpwRelatCaixaMovimFinanceira.Size = new System.Drawing.Size(947, 564);
            this.rpwRelatCaixaMovimFinanceira.TabIndex = 293;
            // 
            // tbpInformacoesGeraisCaixa
            // 
            this.tbpInformacoesGeraisCaixa.Controls.Add(this.rpwRelatCaixaConfFechamento);
            this.tbpInformacoesGeraisCaixa.Location = new System.Drawing.Point(4, 22);
            this.tbpInformacoesGeraisCaixa.Name = "tbpInformacoesGeraisCaixa";
            this.tbpInformacoesGeraisCaixa.Size = new System.Drawing.Size(953, 570);
            this.tbpInformacoesGeraisCaixa.TabIndex = 3;
            this.tbpInformacoesGeraisCaixa.Text = "Informações Gerais, Conferência e Fechamento.";
            this.tbpInformacoesGeraisCaixa.UseVisualStyleBackColor = true;
            // 
            // rpwRelatCaixaConfFechamento
            // 
            this.rpwRelatCaixaConfFechamento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rpwRelatCaixaConfFechamento.Location = new System.Drawing.Point(3, 3);
            this.rpwRelatCaixaConfFechamento.Name = "rpwRelatCaixaConfFechamento";
            this.rpwRelatCaixaConfFechamento.ShowBackButton = false;
            this.rpwRelatCaixaConfFechamento.ShowDocumentMapButton = false;
            this.rpwRelatCaixaConfFechamento.ShowFindControls = false;
            this.rpwRelatCaixaConfFechamento.ShowRefreshButton = false;
            this.rpwRelatCaixaConfFechamento.ShowStopButton = false;
            this.rpwRelatCaixaConfFechamento.Size = new System.Drawing.Size(947, 564);
            this.rpwRelatCaixaConfFechamento.TabIndex = 293;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 622);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 312;
            this.label4.Text = "Título do E-mail:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FuturaDataTCC.Properties.Resources.enviarEmailPDF;
            this.pictureBox1.Location = new System.Drawing.Point(2, 601);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 100);
            this.pictureBox1.TabIndex = 311;
            this.pictureBox1.TabStop = false;
            // 
            // prgEnvioEmail
            // 
            this.prgEnvioEmail.Location = new System.Drawing.Point(130, 669);
            this.prgEnvioEmail.Maximum = 4;
            this.prgEnvioEmail.Name = "prgEnvioEmail";
            this.prgEnvioEmail.Size = new System.Drawing.Size(337, 10);
            this.prgEnvioEmail.TabIndex = 310;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 601);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 13);
            this.label2.TabIndex = 309;
            this.label2.Text = "Envie esse Relatório por E-mail (formato PDF Automático):";
            // 
            // lblAguardando
            // 
            this.lblAguardando.AutoSize = true;
            this.lblAguardando.Location = new System.Drawing.Point(127, 688);
            this.lblAguardando.Name = "lblAguardando";
            this.lblAguardando.Size = new System.Drawing.Size(74, 13);
            this.lblAguardando.TabIndex = 308;
            this.lblAguardando.Text = "Aguardando...";
            // 
            // btnEnvioEmail
            // 
            this.btnEnvioEmail.Image = global::FuturaDataTCC.Properties.Resources.btnproxcad;
            this.btnEnvioEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnvioEmail.Location = new System.Drawing.Point(862, 674);
            this.btnEnvioEmail.Name = "btnEnvioEmail";
            this.btnEnvioEmail.Size = new System.Drawing.Size(101, 28);
            this.btnEnvioEmail.TabIndex = 305;
            this.btnEnvioEmail.Text = "Enviar E-Mail";
            this.btnEnvioEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnvioEmail.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 646);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 307;
            this.label1.Text = "Enviar para:";
            // 
            // tbxEnviarEmailPara
            // 
            this.tbxEnviarEmailPara.Location = new System.Drawing.Point(221, 643);
            this.tbxEnviarEmailPara.Name = "tbxEnviarEmailPara";
            this.tbxEnviarEmailPara.Size = new System.Drawing.Size(247, 20);
            this.tbxEnviarEmailPara.TabIndex = 303;
            // 
            // tbxTituloEmail
            // 
            this.tbxTituloEmail.Location = new System.Drawing.Point(221, 619);
            this.tbxTituloEmail.Name = "tbxTituloEmail";
            this.tbxTituloEmail.Size = new System.Drawing.Size(247, 20);
            this.tbxTituloEmail.TabIndex = 302;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 601);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 306;
            this.label3.Text = "Mensagem Adicional do E-mail:";
            // 
            // tbxMensagemAdicionalEmail
            // 
            this.tbxMensagemAdicionalEmail.Location = new System.Drawing.Point(474, 619);
            this.tbxMensagemAdicionalEmail.Multiline = true;
            this.tbxMensagemAdicionalEmail.Name = "tbxMensagemAdicionalEmail";
            this.tbxMensagemAdicionalEmail.Size = new System.Drawing.Size(489, 44);
            this.tbxMensagemAdicionalEmail.TabIndex = 304;
            // 
            // frmNewRelatAnalCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 705);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.prgEnvioEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAguardando);
            this.Controls.Add(this.btnEnvioEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxEnviarEmailPara);
            this.Controls.Add(this.tbxTituloEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxMensagemAdicionalEmail);
            this.Controls.Add(this.tbcRelatorioCaixa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewRelatAnalCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios Completos de Caixa";
            this.Load += new System.EventHandler(this.frmNewRelatAnalCaixa_Load);
            this.tbcRelatorioCaixa.ResumeLayout(false);
            this.tbpPedidosFechados.ResumeLayout(false);
            this.tbpMovimentoEstoque.ResumeLayout(false);
            this.tbpMovimentacaoFinanceira.ResumeLayout(false);
            this.tbpInformacoesGeraisCaixa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbcRelatorioCaixa;
        private System.Windows.Forms.TabPage tbpPedidosFechados;
        private System.Windows.Forms.TabPage tbpMovimentoEstoque;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar prgEnvioEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAguardando;
        private System.Windows.Forms.Button btnEnvioEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxEnviarEmailPara;
        private System.Windows.Forms.TextBox tbxTituloEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxMensagemAdicionalEmail;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRelatCaixaPedidosFechados;
        private System.Windows.Forms.TabPage tbpMovimentacaoFinanceira;
        private System.Windows.Forms.TabPage tbpInformacoesGeraisCaixa;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRelatCaixaMovimentoEstoque;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRelatCaixaMovimFinanceira;
        private Microsoft.Reporting.WinForms.ReportViewer rpwRelatCaixaConfFechamento;
    }
}