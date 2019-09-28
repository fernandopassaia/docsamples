namespace FuturaDataTCC.Views.PlanoDeContas
{
    partial class frmCadastroItemPlanoContas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroItemPlanoContas));
            this.chkChamarTEF = new System.Windows.Forms.CheckBox();
            this.chkChamarECF = new System.Windows.Forms.CheckBox();
            this.chkChamarNFe = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInserirPlanoContas = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxMascara = new System.Windows.Forms.TextBox();
            this.tbxDescricaoSubCategoria = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbCategoriaMestre = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxNomeECF = new System.Windows.Forms.TextBox();
            this.tbxResumoNomeECF = new System.Windows.Forms.TextBox();
            this.lblEntradaOuSaida = new System.Windows.Forms.Label();
            this.grpOpcoesPagamento = new System.Windows.Forms.GroupBox();
            this.rdbFormaPagamentoAVista = new System.Windows.Forms.RadioButton();
            this.rdbFormaPagamentoAPrazo = new System.Windows.Forms.RadioButton();
            this.rdbFormaPagamentoAFaturar = new System.Windows.Forms.RadioButton();
            this.grpTiposDocumento = new System.Windows.Forms.GroupBox();
            this.rdbTipoDocCarne = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocOutros = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocVales = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocRecibo = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocPromissoria = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.rdbTipoDocChequeAPrazo = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocBoleto = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocDuplicata = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocCartaoCredito = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocCartaoDebito = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocChequeAVista = new System.Windows.Forms.RadioButton();
            this.rdbTipoDocDinheiro = new System.Windows.Forms.RadioButton();
            this.grpPagamentosPrazo = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxDescontoFixo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudQuantidadeParcelaSemJuros = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxDiaVencimentoFixo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxValorAcrescimoNaForma = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkPrimeiraParcelaAVista = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.nudIntervaloDias = new System.Windows.Forms.NumericUpDown();
            this.nudNumeroParcelas = new System.Windows.Forms.NumericUpDown();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnExplicacaoHabilitarPlanoContas = new System.Windows.Forms.Button();
            this.grpOpcoesPagamento.SuspendLayout();
            this.grpTiposDocumento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.grpPagamentosPrazo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidadeParcelaSemJuros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervaloDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeroParcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // chkChamarTEF
            // 
            this.chkChamarTEF.AutoSize = true;
            this.chkChamarTEF.Location = new System.Drawing.Point(83, 227);
            this.chkChamarTEF.Name = "chkChamarTEF";
            this.chkChamarTEF.Size = new System.Drawing.Size(165, 17);
            this.chkChamarTEF.TabIndex = 0;
            this.chkChamarTEF.Text = "Chamar TEF no Fechamento.";
            this.chkChamarTEF.UseVisualStyleBackColor = true;
            this.chkChamarTEF.Visible = false;
            //this.chkChamarTEF.CheckedChanged += new System.EventHandler(this.chkChamarTEF_CheckedChanged);
            // 
            // chkChamarECF
            // 
            this.chkChamarECF.AutoSize = true;
            this.chkChamarECF.Location = new System.Drawing.Point(267, 227);
            this.chkChamarECF.Name = "chkChamarECF";
            this.chkChamarECF.Size = new System.Drawing.Size(128, 17);
            this.chkChamarECF.TabIndex = 1;
            this.chkChamarECF.Text = "Chamar CupomFiscal.";
            this.chkChamarECF.UseVisualStyleBackColor = true;
            this.chkChamarECF.Visible = false;
            // 
            // chkChamarNFe
            // 
            this.chkChamarNFe.AutoSize = true;
            this.chkChamarNFe.Location = new System.Drawing.Point(434, 227);
            this.chkChamarNFe.Name = "chkChamarNFe";
            this.chkChamarNFe.Size = new System.Drawing.Size(88, 17);
            this.chkChamarNFe.TabIndex = 2;
            this.chkChamarNFe.Text = "Chamar NFe.";
            this.chkChamarNFe.UseVisualStyleBackColor = true;
            this.chkChamarNFe.Visible = false;
            //this.chkChamarNFe.CheckedChanged += new System.EventHandler(this.chkChamarNFe_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nome no ECF:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Resumido ECF:";
            this.label2.Visible = false;
            // 
            // btnInserirPlanoContas
            // 
            this.btnInserirPlanoContas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserirPlanoContas.Image = global::FuturaDataTCC.Properties.Resources.moneyIn;
            this.btnInserirPlanoContas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirPlanoContas.Location = new System.Drawing.Point(174, 87);
            this.btnInserirPlanoContas.Name = "btnInserirPlanoContas";
            this.btnInserirPlanoContas.Size = new System.Drawing.Size(345, 45);
            this.btnInserirPlanoContas.TabIndex = 310;
            this.btnInserirPlanoContas.Text = "Incluir SubCategoria Plano Conta Entrada/Receita";
            this.btnInserirPlanoContas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirPlanoContas.UseVisualStyleBackColor = true;
            this.btnInserirPlanoContas.Click += new System.EventHandler(this.btnInserirPlanoContas_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 309;
            this.label3.Text = "Máscara (seqüencial-auto):";
            // 
            // tbxMascara
            // 
            this.tbxMascara.Location = new System.Drawing.Point(398, 61);
            this.tbxMascara.Name = "tbxMascara";
            this.tbxMascara.ReadOnly = true;
            this.tbxMascara.Size = new System.Drawing.Size(121, 20);
            this.tbxMascara.TabIndex = 308;
            // 
            // tbxDescricaoSubCategoria
            // 
            this.tbxDescricaoSubCategoria.Location = new System.Drawing.Point(229, 8);
            this.tbxDescricaoSubCategoria.MaxLength = 79;
            this.tbxDescricaoSubCategoria.Name = "tbxDescricaoSubCategoria";
            this.tbxDescricaoSubCategoria.Size = new System.Drawing.Size(290, 20);
            this.tbxDescricaoSubCategoria.TabIndex = 307;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 13);
            this.label4.TabIndex = 306;
            this.label4.Text = "Sub-Descrição da Categoria:";
            // 
            // cbbCategoriaMestre
            // 
            this.cbbCategoriaMestre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategoriaMestre.FormattingEnabled = true;
            this.cbbCategoriaMestre.Items.AddRange(new object[] {
            "1 - Entrada/Receita",
            "2 - Saída/Despesa"});
            this.cbbCategoriaMestre.Location = new System.Drawing.Point(229, 31);
            this.cbbCategoriaMestre.Name = "cbbCategoriaMestre";
            this.cbbCategoriaMestre.Size = new System.Drawing.Size(290, 21);
            this.cbbCategoriaMestre.TabIndex = 312;
            this.cbbCategoriaMestre.SelectedIndexChanged += new System.EventHandler(this.cbbCategoriaMestre_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 311;
            this.label5.Text = "Categoria Pai/Mestra:";
            // 
            // tbxNomeECF
            // 
            this.tbxNomeECF.Location = new System.Drawing.Point(116, 175);
            this.tbxNomeECF.MaxLength = 15;
            this.tbxNomeECF.Name = "tbxNomeECF";
            this.tbxNomeECF.Size = new System.Drawing.Size(121, 20);
            this.tbxNomeECF.TabIndex = 313;
            this.tbxNomeECF.Visible = false;
            // 
            // tbxResumoNomeECF
            // 
            this.tbxResumoNomeECF.Location = new System.Drawing.Point(116, 201);
            this.tbxResumoNomeECF.MaxLength = 4;
            this.tbxResumoNomeECF.Name = "tbxResumoNomeECF";
            this.tbxResumoNomeECF.Size = new System.Drawing.Size(121, 20);
            this.tbxResumoNomeECF.TabIndex = 314;
            this.tbxResumoNomeECF.Visible = false;
            // 
            // lblEntradaOuSaida
            // 
            this.lblEntradaOuSaida.AutoSize = true;
            this.lblEntradaOuSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntradaOuSaida.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblEntradaOuSaida.Location = new System.Drawing.Point(42, 64);
            this.lblEntradaOuSaida.Name = "lblEntradaOuSaida";
            this.lblEntradaOuSaida.Size = new System.Drawing.Size(206, 13);
            this.lblEntradaOuSaida.TabIndex = 315;
            this.lblEntradaOuSaida.Text = "Categoria do tipo Entrada/Receita.";
            // 
            // grpOpcoesPagamento
            // 
            this.grpOpcoesPagamento.Controls.Add(this.rdbFormaPagamentoAVista);
            this.grpOpcoesPagamento.Controls.Add(this.rdbFormaPagamentoAPrazo);
            this.grpOpcoesPagamento.Controls.Add(this.rdbFormaPagamentoAFaturar);
            this.grpOpcoesPagamento.Location = new System.Drawing.Point(8, 259);
            this.grpOpcoesPagamento.Name = "grpOpcoesPagamento";
            this.grpOpcoesPagamento.Size = new System.Drawing.Size(615, 48);
            this.grpOpcoesPagamento.TabIndex = 318;
            this.grpOpcoesPagamento.TabStop = false;
            this.grpOpcoesPagamento.Text = "Opções para Forma de Pagamento de Entrada (Entrada/Receita):";
            this.grpOpcoesPagamento.Visible = false;
            //this.grpOpcoesPagamento.Enter += new System.EventHandler(this.grpOpcoesPagamento_Enter);
            // 
            // rdbFormaPagamentoAVista
            // 
            this.rdbFormaPagamentoAVista.AutoSize = true;
            this.rdbFormaPagamentoAVista.Checked = true;
            this.rdbFormaPagamentoAVista.Location = new System.Drawing.Point(6, 23);
            this.rdbFormaPagamentoAVista.Name = "rdbFormaPagamentoAVista";
            this.rdbFormaPagamentoAVista.Size = new System.Drawing.Size(185, 17);
            this.rdbFormaPagamentoAVista.TabIndex = 1;
            this.rdbFormaPagamentoAVista.TabStop = true;
            this.rdbFormaPagamentoAVista.Text = "1 - Forma de Pagamento A VISTA";
            this.rdbFormaPagamentoAVista.UseVisualStyleBackColor = true;
            // 
            // rdbFormaPagamentoAPrazo
            // 
            this.rdbFormaPagamentoAPrazo.AutoSize = true;
            this.rdbFormaPagamentoAPrazo.Location = new System.Drawing.Point(196, 23);
            this.rdbFormaPagamentoAPrazo.Name = "rdbFormaPagamentoAPrazo";
            this.rdbFormaPagamentoAPrazo.Size = new System.Drawing.Size(190, 17);
            this.rdbFormaPagamentoAPrazo.TabIndex = 2;
            this.rdbFormaPagamentoAPrazo.Text = "Forma de Pagamento A RECEBER";
            this.rdbFormaPagamentoAPrazo.UseVisualStyleBackColor = true;
            // 
            // rdbFormaPagamentoAFaturar
            // 
            this.rdbFormaPagamentoAFaturar.AutoSize = true;
            this.rdbFormaPagamentoAFaturar.Location = new System.Drawing.Point(390, 23);
            this.rdbFormaPagamentoAFaturar.Name = "rdbFormaPagamentoAFaturar";
            this.rdbFormaPagamentoAFaturar.Size = new System.Drawing.Size(220, 17);
            this.rdbFormaPagamentoAFaturar.TabIndex = 69;
            this.rdbFormaPagamentoAFaturar.Text = "Forma de Pagamento A FATURAR (Vale)";
            this.rdbFormaPagamentoAFaturar.UseVisualStyleBackColor = true;
            // 
            // grpTiposDocumento
            // 
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocCarne);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocOutros);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocVales);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocRecibo);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocPromissoria);
            this.grpTiposDocumento.Controls.Add(this.pictureBox3);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocChequeAPrazo);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocBoleto);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocDuplicata);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocCartaoCredito);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocCartaoDebito);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocChequeAVista);
            this.grpTiposDocumento.Controls.Add(this.rdbTipoDocDinheiro);
            this.grpTiposDocumento.Location = new System.Drawing.Point(8, 443);
            this.grpTiposDocumento.Name = "grpTiposDocumento";
            this.grpTiposDocumento.Size = new System.Drawing.Size(615, 73);
            this.grpTiposDocumento.TabIndex = 317;
            this.grpTiposDocumento.TabStop = false;
            this.grpTiposDocumento.Text = "Tipo de Documento:";
            this.grpTiposDocumento.Visible = false;
            //this.grpTiposDocumento.Enter += new System.EventHandler(this.grpTiposDocumento_Enter);
            // 
            // rdbTipoDocCarne
            // 
            this.rdbTipoDocCarne.AutoSize = true;
            this.rdbTipoDocCarne.Location = new System.Drawing.Point(124, 47);
            this.rdbTipoDocCarne.Name = "rdbTipoDocCarne";
            this.rdbTipoDocCarne.Size = new System.Drawing.Size(53, 17);
            this.rdbTipoDocCarne.TabIndex = 7;
            this.rdbTipoDocCarne.Text = "Carnê";
            this.rdbTipoDocCarne.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocOutros
            // 
            this.rdbTipoDocOutros.AutoSize = true;
            this.rdbTipoDocOutros.Location = new System.Drawing.Point(523, 47);
            this.rdbTipoDocOutros.Name = "rdbTipoDocOutros";
            this.rdbTipoDocOutros.Size = new System.Drawing.Size(56, 17);
            this.rdbTipoDocOutros.TabIndex = 11;
            this.rdbTipoDocOutros.Text = "Outros";
            this.rdbTipoDocOutros.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocVales
            // 
            this.rdbTipoDocVales.AutoSize = true;
            this.rdbTipoDocVales.Location = new System.Drawing.Point(427, 47);
            this.rdbTipoDocVales.Name = "rdbTipoDocVales";
            this.rdbTipoDocVales.Size = new System.Drawing.Size(51, 17);
            this.rdbTipoDocVales.TabIndex = 10;
            this.rdbTipoDocVales.Text = "Vales";
            this.rdbTipoDocVales.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocRecibo
            // 
            this.rdbTipoDocRecibo.AutoSize = true;
            this.rdbTipoDocRecibo.Location = new System.Drawing.Point(329, 47);
            this.rdbTipoDocRecibo.Name = "rdbTipoDocRecibo";
            this.rdbTipoDocRecibo.Size = new System.Drawing.Size(59, 17);
            this.rdbTipoDocRecibo.TabIndex = 9;
            this.rdbTipoDocRecibo.Text = "Recibo";
            this.rdbTipoDocRecibo.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocPromissoria
            // 
            this.rdbTipoDocPromissoria.AutoSize = true;
            this.rdbTipoDocPromissoria.Location = new System.Drawing.Point(222, 47);
            this.rdbTipoDocPromissoria.Name = "rdbTipoDocPromissoria";
            this.rdbTipoDocPromissoria.Size = new System.Drawing.Size(78, 17);
            this.rdbTipoDocPromissoria.TabIndex = 8;
            this.rdbTipoDocPromissoria.Text = "Promissória";
            this.rdbTipoDocPromissoria.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::FuturaDataTCC.Properties.Resources.btnContas;
            this.pictureBox3.Location = new System.Drawing.Point(4, 24);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(46, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // rdbTipoDocChequeAPrazo
            // 
            this.rdbTipoDocChequeAPrazo.AutoSize = true;
            this.rdbTipoDocChequeAPrazo.Location = new System.Drawing.Point(224, 24);
            this.rdbTipoDocChequeAPrazo.Name = "rdbTipoDocChequeAPrazo";
            this.rdbTipoDocChequeAPrazo.Size = new System.Drawing.Size(101, 17);
            this.rdbTipoDocChequeAPrazo.TabIndex = 2;
            this.rdbTipoDocChequeAPrazo.Text = "Cheque a Prazo";
            this.rdbTipoDocChequeAPrazo.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocBoleto
            // 
            this.rdbTipoDocBoleto.AutoSize = true;
            this.rdbTipoDocBoleto.Location = new System.Drawing.Point(55, 47);
            this.rdbTipoDocBoleto.Name = "rdbTipoDocBoleto";
            this.rdbTipoDocBoleto.Size = new System.Drawing.Size(55, 17);
            this.rdbTipoDocBoleto.TabIndex = 6;
            this.rdbTipoDocBoleto.Text = "Boleto";
            this.rdbTipoDocBoleto.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocDuplicata
            // 
            this.rdbTipoDocDuplicata.AutoSize = true;
            this.rdbTipoDocDuplicata.Location = new System.Drawing.Point(523, 24);
            this.rdbTipoDocDuplicata.Name = "rdbTipoDocDuplicata";
            this.rdbTipoDocDuplicata.Size = new System.Drawing.Size(70, 17);
            this.rdbTipoDocDuplicata.TabIndex = 5;
            this.rdbTipoDocDuplicata.Text = "Duplicata";
            this.rdbTipoDocDuplicata.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocCartaoCredito
            // 
            this.rdbTipoDocCartaoCredito.AutoSize = true;
            this.rdbTipoDocCartaoCredito.Location = new System.Drawing.Point(425, 24);
            this.rdbTipoDocCartaoCredito.Name = "rdbTipoDocCartaoCredito";
            this.rdbTipoDocCartaoCredito.Size = new System.Drawing.Size(92, 17);
            this.rdbTipoDocCartaoCredito.TabIndex = 4;
            this.rdbTipoDocCartaoCredito.Text = "Cartão Crédito";
            this.rdbTipoDocCartaoCredito.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocCartaoDebito
            // 
            this.rdbTipoDocCartaoDebito.AutoSize = true;
            this.rdbTipoDocCartaoDebito.Location = new System.Drawing.Point(329, 24);
            this.rdbTipoDocCartaoDebito.Name = "rdbTipoDocCartaoDebito";
            this.rdbTipoDocCartaoDebito.Size = new System.Drawing.Size(90, 17);
            this.rdbTipoDocCartaoDebito.TabIndex = 3;
            this.rdbTipoDocCartaoDebito.Text = "Cartão Débito";
            this.rdbTipoDocCartaoDebito.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocChequeAVista
            // 
            this.rdbTipoDocChequeAVista.AutoSize = true;
            this.rdbTipoDocChequeAVista.Location = new System.Drawing.Point(124, 24);
            this.rdbTipoDocChequeAVista.Name = "rdbTipoDocChequeAVista";
            this.rdbTipoDocChequeAVista.Size = new System.Drawing.Size(97, 17);
            this.rdbTipoDocChequeAVista.TabIndex = 1;
            this.rdbTipoDocChequeAVista.Text = "Cheque a Vista";
            this.rdbTipoDocChequeAVista.UseVisualStyleBackColor = true;
            // 
            // rdbTipoDocDinheiro
            // 
            this.rdbTipoDocDinheiro.AutoSize = true;
            this.rdbTipoDocDinheiro.Location = new System.Drawing.Point(55, 24);
            this.rdbTipoDocDinheiro.Name = "rdbTipoDocDinheiro";
            this.rdbTipoDocDinheiro.Size = new System.Drawing.Size(64, 17);
            this.rdbTipoDocDinheiro.TabIndex = 0;
            this.rdbTipoDocDinheiro.Text = "Dinheiro";
            this.rdbTipoDocDinheiro.UseVisualStyleBackColor = true;
            // 
            // grpPagamentosPrazo
            // 
            this.grpPagamentosPrazo.Controls.Add(this.label12);
            this.grpPagamentosPrazo.Controls.Add(this.label11);
            this.grpPagamentosPrazo.Controls.Add(this.label10);
            this.grpPagamentosPrazo.Controls.Add(this.tbxDescontoFixo);
            this.grpPagamentosPrazo.Controls.Add(this.label6);
            this.grpPagamentosPrazo.Controls.Add(this.label7);
            this.grpPagamentosPrazo.Controls.Add(this.nudQuantidadeParcelaSemJuros);
            this.grpPagamentosPrazo.Controls.Add(this.label8);
            this.grpPagamentosPrazo.Controls.Add(this.tbxDiaVencimentoFixo);
            this.grpPagamentosPrazo.Controls.Add(this.label9);
            this.grpPagamentosPrazo.Controls.Add(this.tbxValorAcrescimoNaForma);
            this.grpPagamentosPrazo.Controls.Add(this.label13);
            this.grpPagamentosPrazo.Controls.Add(this.chkPrimeiraParcelaAVista);
            this.grpPagamentosPrazo.Controls.Add(this.label14);
            this.grpPagamentosPrazo.Controls.Add(this.label15);
            this.grpPagamentosPrazo.Controls.Add(this.nudIntervaloDias);
            this.grpPagamentosPrazo.Controls.Add(this.nudNumeroParcelas);
            this.grpPagamentosPrazo.Location = new System.Drawing.Point(8, 322);
            this.grpPagamentosPrazo.Name = "grpPagamentosPrazo";
            this.grpPagamentosPrazo.Size = new System.Drawing.Size(615, 108);
            this.grpPagamentosPrazo.TabIndex = 316;
            this.grpPagamentosPrazo.TabStop = false;
            this.grpPagamentosPrazo.Text = "Configuração para Prazo de Pagamento a Prazo (que gera conta a receber)";
            this.grpPagamentosPrazo.Visible = false;
            //this.grpPagamentosPrazo.Enter += new System.EventHandler(this.grpPagamentosPrazo_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(556, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "(0 ignora)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(296, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "(%)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(123, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "(%)";
            // 
            // tbxDescontoFixo
            // 
            this.tbxDescontoFixo.Location = new System.Drawing.Point(237, 76);
            this.tbxDescontoFixo.Name = "tbxDescontoFixo";
            this.tbxDescontoFixo.Size = new System.Drawing.Size(56, 20);
            this.tbxDescontoFixo.TabIndex = 6;
            this.tbxDescontoFixo.Text = "0,0000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Desconto Fixo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(396, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Qtd.Parcela s/Juros:";
            // 
            // nudQuantidadeParcelaSemJuros
            // 
            this.nudQuantidadeParcelaSemJuros.Location = new System.Drawing.Point(506, 39);
            this.nudQuantidadeParcelaSemJuros.Name = "nudQuantidadeParcelaSemJuros";
            this.nudQuantidadeParcelaSemJuros.Size = new System.Drawing.Size(47, 20);
            this.nudQuantidadeParcelaSemJuros.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(556, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "(0 ignora)";
            // 
            // tbxDiaVencimentoFixo
            // 
            this.tbxDiaVencimentoFixo.Location = new System.Drawing.Point(506, 76);
            this.tbxDiaVencimentoFixo.Name = "tbxDiaVencimentoFixo";
            this.tbxDiaVencimentoFixo.Size = new System.Drawing.Size(47, 20);
            this.tbxDiaVencimentoFixo.TabIndex = 7;
            this.tbxDiaVencimentoFixo.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(378, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Dia de Vencimento Fixo:";
            // 
            // tbxValorAcrescimoNaForma
            // 
            this.tbxValorAcrescimoNaForma.Location = new System.Drawing.Point(63, 76);
            this.tbxValorAcrescimoNaForma.Name = "tbxValorAcrescimoNaForma";
            this.tbxValorAcrescimoNaForma.Size = new System.Drawing.Size(56, 20);
            this.tbxValorAcrescimoNaForma.TabIndex = 5;
            this.tbxValorAcrescimoNaForma.Text = "0,0000";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Acrescimo:";
            // 
            // chkPrimeiraParcelaAVista
            // 
            this.chkPrimeiraParcelaAVista.AutoSize = true;
            this.chkPrimeiraParcelaAVista.Checked = true;
            this.chkPrimeiraParcelaAVista.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrimeiraParcelaAVista.Location = new System.Drawing.Point(160, 40);
            this.chkPrimeiraParcelaAVista.Name = "chkPrimeiraParcelaAVista";
            this.chkPrimeiraParcelaAVista.Size = new System.Drawing.Size(140, 17);
            this.chkPrimeiraParcelaAVista.TabIndex = 3;
            this.chkPrimeiraParcelaAVista.Text = "Primeira Parcela a Vista!";
            this.chkPrimeiraParcelaAVista.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(73, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Intervalo Dias:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Nº Parcelas:";
            // 
            // nudIntervaloDias
            // 
            this.nudIntervaloDias.Location = new System.Drawing.Point(76, 39);
            this.nudIntervaloDias.Name = "nudIntervaloDias";
            this.nudIntervaloDias.Size = new System.Drawing.Size(72, 20);
            this.nudIntervaloDias.TabIndex = 2;
            // 
            // nudNumeroParcelas
            // 
            this.nudNumeroParcelas.Location = new System.Drawing.Point(7, 39);
            this.nudNumeroParcelas.Name = "nudNumeroParcelas";
            this.nudNumeroParcelas.Size = new System.Drawing.Size(63, 20);
            this.nudNumeroParcelas.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(24, 540);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(197, 13);
            this.linkLabel1.TabIndex = 320;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Ajuda para Entender o Plano de Contas.";
            this.linkLabel1.Visible = false;
            //this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnExplicacaoHabilitarPlanoContas
            // 
            this.btnExplicacaoHabilitarPlanoContas.FlatAppearance.BorderSize = 0;
            this.btnExplicacaoHabilitarPlanoContas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExplicacaoHabilitarPlanoContas.Image = global::FuturaDataTCC.Properties.Resources.interrogacaoMenor;
            this.btnExplicacaoHabilitarPlanoContas.Location = new System.Drawing.Point(3, 538);
            this.btnExplicacaoHabilitarPlanoContas.Name = "btnExplicacaoHabilitarPlanoContas";
            this.btnExplicacaoHabilitarPlanoContas.Size = new System.Drawing.Size(17, 17);
            this.btnExplicacaoHabilitarPlanoContas.TabIndex = 319;
            this.btnExplicacaoHabilitarPlanoContas.TabStop = false;
            this.btnExplicacaoHabilitarPlanoContas.UseVisualStyleBackColor = true;
            this.btnExplicacaoHabilitarPlanoContas.Visible = false;
            //this.btnExplicacaoHabilitarPlanoContas.Click += new System.EventHandler(this.btnExplicacaoHabilitarPlanoContas_Click);
            // 
            // frmCadastroItemPlanoContas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 142);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnExplicacaoHabilitarPlanoContas);
            this.Controls.Add(this.grpOpcoesPagamento);
            this.Controls.Add(this.grpTiposDocumento);
            this.Controls.Add(this.grpPagamentosPrazo);
            this.Controls.Add(this.lblEntradaOuSaida);
            this.Controls.Add(this.tbxResumoNomeECF);
            this.Controls.Add(this.tbxNomeECF);
            this.Controls.Add(this.cbbCategoriaMestre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnInserirPlanoContas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxMascara);
            this.Controls.Add(this.tbxDescricaoSubCategoria);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkChamarNFe);
            this.Controls.Add(this.chkChamarECF);
            this.Controls.Add(this.chkChamarTEF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCadastroItemPlanoContas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Sub-Categoria da Categoria - Plano de Contas";
            this.grpOpcoesPagamento.ResumeLayout(false);
            this.grpOpcoesPagamento.PerformLayout();
            this.grpTiposDocumento.ResumeLayout(false);
            this.grpTiposDocumento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.grpPagamentosPrazo.ResumeLayout(false);
            this.grpPagamentosPrazo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantidadeParcelaSemJuros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervaloDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeroParcelas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkChamarTEF;
        private System.Windows.Forms.CheckBox chkChamarECF;
        private System.Windows.Forms.CheckBox chkChamarNFe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInserirPlanoContas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxMascara;
        private System.Windows.Forms.TextBox tbxDescricaoSubCategoria;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbCategoriaMestre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxNomeECF;
        private System.Windows.Forms.TextBox tbxResumoNomeECF;
        private System.Windows.Forms.Label lblEntradaOuSaida;
        private System.Windows.Forms.GroupBox grpOpcoesPagamento;
        private System.Windows.Forms.RadioButton rdbFormaPagamentoAVista;
        private System.Windows.Forms.RadioButton rdbFormaPagamentoAPrazo;
        private System.Windows.Forms.RadioButton rdbFormaPagamentoAFaturar;
        private System.Windows.Forms.GroupBox grpTiposDocumento;
        private System.Windows.Forms.RadioButton rdbTipoDocCarne;
        private System.Windows.Forms.RadioButton rdbTipoDocOutros;
        private System.Windows.Forms.RadioButton rdbTipoDocVales;
        private System.Windows.Forms.RadioButton rdbTipoDocRecibo;
        private System.Windows.Forms.RadioButton rdbTipoDocPromissoria;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.RadioButton rdbTipoDocChequeAPrazo;
        private System.Windows.Forms.RadioButton rdbTipoDocBoleto;
        private System.Windows.Forms.RadioButton rdbTipoDocDuplicata;
        private System.Windows.Forms.RadioButton rdbTipoDocCartaoCredito;
        private System.Windows.Forms.RadioButton rdbTipoDocCartaoDebito;
        private System.Windows.Forms.RadioButton rdbTipoDocChequeAVista;
        private System.Windows.Forms.RadioButton rdbTipoDocDinheiro;
        private System.Windows.Forms.GroupBox grpPagamentosPrazo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxDescontoFixo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudQuantidadeParcelaSemJuros;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxDiaVencimentoFixo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxValorAcrescimoNaForma;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkPrimeiraParcelaAVista;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudIntervaloDias;
        private System.Windows.Forms.NumericUpDown nudNumeroParcelas;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnExplicacaoHabilitarPlanoContas;
    }
}