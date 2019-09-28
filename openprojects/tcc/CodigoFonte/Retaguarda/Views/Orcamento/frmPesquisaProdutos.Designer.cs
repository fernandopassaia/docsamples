namespace FuturaDataTCC.Views.Orcamento
{
    partial class frmPesquisaProdutos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesquisaProdutos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbBuscaCodFabricante = new System.Windows.Forms.RadioButton();
            this.rdbBuscaAplicacao = new System.Windows.Forms.RadioButton();
            this.rdbBuscaDescricao = new System.Windows.Forms.RadioButton();
            this.pctTipoPesquisa = new System.Windows.Forms.PictureBox();
            this.tbxFiltroPesquisa = new System.Windows.Forms.TextBox();
            this.lblTipoPesquisa = new System.Windows.Forms.Label();
            this.gbpResultadoPesquisa = new System.Windows.Forms.GroupBox();
            this.lvwPesquisaProdutos = new System.Windows.Forms.ListView();
            this.ctmEscolhaSubProdutoESimilares = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verificarProdutosSimilaresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbxPrecoNormal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIgnorarMudancasPrecos = new System.Windows.Forms.CheckBox();
            this.tbxCodigoProduto = new System.Windows.Forms.TextBox();
            this.lblProdutoAdicionado = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxValorDescontoAcrescimo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxTipoDescontoAcrescimo = new System.Windows.Forms.TextBox();
            this.tbxCodigoFabricante = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnFinalizarPesquisa = new System.Windows.Forms.Button();
            this.tbxValorTotal = new System.Windows.Forms.TextBox();
            this.lblPrecoRevParc = new System.Windows.Forms.Label();
            this.tbxQuantidade = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxPrecoRevParcelado = new System.Windows.Forms.TextBox();
            this.tbxValorDigitado = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxQuantidadeEstoque = new System.Windows.Forms.TextBox();
            this.tbxPrecoVendaParcelado = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxDescricaoAplicacaoEFilho = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pctImagemProduto = new System.Windows.Forms.PictureBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adicionarSubProdutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctTipoPesquisa)).BeginInit();
            this.gbpResultadoPesquisa.SuspendLayout();
            this.ctmEscolhaSubProdutoESimilares.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctImagemProduto)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbBuscaCodFabricante);
            this.groupBox1.Controls.Add(this.rdbBuscaAplicacao);
            this.groupBox1.Controls.Add(this.rdbBuscaDescricao);
            this.groupBox1.Controls.Add(this.pctTipoPesquisa);
            this.groupBox1.Controls.Add(this.tbxFiltroPesquisa);
            this.groupBox1.Controls.Add(this.lblTipoPesquisa);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(960, 99);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Pesquisa";
            // 
            // rdbBuscaCodFabricante
            // 
            this.rdbBuscaCodFabricante.AutoSize = true;
            this.rdbBuscaCodFabricante.Location = new System.Drawing.Point(413, 67);
            this.rdbBuscaCodFabricante.Name = "rdbBuscaCodFabricante";
            this.rdbBuscaCodFabricante.Size = new System.Drawing.Size(151, 17);
            this.rdbBuscaCodFabricante.TabIndex = 28;
            this.rdbBuscaCodFabricante.Text = "Busca Cod.Fabricante (F3)";
            this.rdbBuscaCodFabricante.UseVisualStyleBackColor = true;
            this.rdbBuscaCodFabricante.CheckedChanged += new System.EventHandler(this.rdbBuscaCodFabricante_CheckedChanged);
            // 
            // rdbBuscaAplicacao
            // 
            this.rdbBuscaAplicacao.AutoSize = true;
            this.rdbBuscaAplicacao.Location = new System.Drawing.Point(570, 67);
            this.rdbBuscaAplicacao.Name = "rdbBuscaAplicacao";
            this.rdbBuscaAplicacao.Size = new System.Drawing.Size(126, 17);
            this.rdbBuscaAplicacao.TabIndex = 29;
            this.rdbBuscaAplicacao.Text = "Busca Aplicação (F4)";
            this.rdbBuscaAplicacao.UseVisualStyleBackColor = true;
            this.rdbBuscaAplicacao.CheckedChanged += new System.EventHandler(this.rdbBuscaAplicacao_CheckedChanged);
            // 
            // rdbBuscaDescricao
            // 
            this.rdbBuscaDescricao.AutoSize = true;
            this.rdbBuscaDescricao.Checked = true;
            this.rdbBuscaDescricao.Location = new System.Drawing.Point(280, 67);
            this.rdbBuscaDescricao.Name = "rdbBuscaDescricao";
            this.rdbBuscaDescricao.Size = new System.Drawing.Size(127, 17);
            this.rdbBuscaDescricao.TabIndex = 27;
            this.rdbBuscaDescricao.TabStop = true;
            this.rdbBuscaDescricao.Text = "Busca Descrição (F2)";
            this.rdbBuscaDescricao.UseVisualStyleBackColor = true;
            this.rdbBuscaDescricao.CheckedChanged += new System.EventHandler(this.rdbBuscaDescricao_CheckedChanged);
            // 
            // pctTipoPesquisa
            // 
            this.pctTipoPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pctTipoPesquisa.Image = global::FuturaDataTCC.Properties.Resources.pesquisaProdutos;
            this.pctTipoPesquisa.Location = new System.Drawing.Point(3, 19);
            this.pctTipoPesquisa.Name = "pctTipoPesquisa";
            this.pctTipoPesquisa.Size = new System.Drawing.Size(92, 76);
            this.pctTipoPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctTipoPesquisa.TabIndex = 26;
            this.pctTipoPesquisa.TabStop = false;
            // 
            // tbxFiltroPesquisa
            // 
            this.tbxFiltroPesquisa.Location = new System.Drawing.Point(368, 41);
            this.tbxFiltroPesquisa.Name = "tbxFiltroPesquisa";
            this.tbxFiltroPesquisa.Size = new System.Drawing.Size(328, 20);
            this.tbxFiltroPesquisa.TabIndex = 0;
            this.tbxFiltroPesquisa.TextChanged += new System.EventHandler(this.tbxFiltroPesquisa_TextChanged);
            this.tbxFiltroPesquisa.Enter += new System.EventHandler(this.tbxFiltroPesquisa_Enter);
            this.tbxFiltroPesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxFiltroPesquisa_KeyDown);
            this.tbxFiltroPesquisa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxFiltroPesquisa_KeyPress);
            this.tbxFiltroPesquisa.Leave += new System.EventHandler(this.tbxFiltroPesquisa_Leave);
            // 
            // lblTipoPesquisa
            // 
            this.lblTipoPesquisa.AutoSize = true;
            this.lblTipoPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPesquisa.Location = new System.Drawing.Point(197, 44);
            this.lblTipoPesquisa.Name = "lblTipoPesquisa";
            this.lblTipoPesquisa.Size = new System.Drawing.Size(168, 13);
            this.lblTipoPesquisa.TabIndex = 7;
            this.lblTipoPesquisa.Text = "Pesquisa Por Descrição(F2):";
            // 
            // gbpResultadoPesquisa
            // 
            this.gbpResultadoPesquisa.Controls.Add(this.lvwPesquisaProdutos);
            this.gbpResultadoPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbpResultadoPesquisa.Location = new System.Drawing.Point(3, 110);
            this.gbpResultadoPesquisa.Name = "gbpResultadoPesquisa";
            this.gbpResultadoPesquisa.Size = new System.Drawing.Size(960, 308);
            this.gbpResultadoPesquisa.TabIndex = 2;
            this.gbpResultadoPesquisa.TabStop = false;
            this.gbpResultadoPesquisa.Text = "Resultado Pesquisa";
            // 
            // lvwPesquisaProdutos
            // 
            this.lvwPesquisaProdutos.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwPesquisaProdutos.BackColor = System.Drawing.Color.White;
            this.lvwPesquisaProdutos.ContextMenuStrip = this.ctmEscolhaSubProdutoESimilares;
            this.lvwPesquisaProdutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwPesquisaProdutos.FullRowSelect = true;
            this.lvwPesquisaProdutos.GridLines = true;
            this.lvwPesquisaProdutos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lvwPesquisaProdutos.Location = new System.Drawing.Point(6, 22);
            this.lvwPesquisaProdutos.MultiSelect = false;
            this.lvwPesquisaProdutos.Name = "lvwPesquisaProdutos";
            this.lvwPesquisaProdutos.Size = new System.Drawing.Size(949, 280);
            this.lvwPesquisaProdutos.TabIndex = 0;
            this.lvwPesquisaProdutos.UseCompatibleStateImageBehavior = false;
            this.lvwPesquisaProdutos.View = System.Windows.Forms.View.Details;
            this.lvwPesquisaProdutos.SelectedIndexChanged += new System.EventHandler(this.lvwPesquisaProdutos_SelectedIndexChanged);
            this.lvwPesquisaProdutos.DoubleClick += new System.EventHandler(this.lvwPesquisaProdutos_DoubleClick);
            this.lvwPesquisaProdutos.Enter += new System.EventHandler(this.lvwPesquisaProdutos_Enter);
            this.lvwPesquisaProdutos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwPesquisaProdutos_KeyDown);
            this.lvwPesquisaProdutos.Leave += new System.EventHandler(this.lvwPesquisaProdutos_Leave);
            // 
            // ctmEscolhaSubProdutoESimilares
            // 
            this.ctmEscolhaSubProdutoESimilares.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verificarProdutosSimilaresToolStripMenuItem});
            this.ctmEscolhaSubProdutoESimilares.Name = "ctmEscolhaSubProdutoESimilares";
            this.ctmEscolhaSubProdutoESimilares.Size = new System.Drawing.Size(219, 26);
            // 
            // verificarProdutosSimilaresToolStripMenuItem
            // 
            this.verificarProdutosSimilaresToolStripMenuItem.Name = "verificarProdutosSimilaresToolStripMenuItem";
            this.verificarProdutosSimilaresToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.verificarProdutosSimilaresToolStripMenuItem.Text = "Verificar Produtos Similares";
            this.verificarProdutosSimilaresToolStripMenuItem.Click += new System.EventHandler(this.verificarProdutosSimilaresToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.pctImagemProduto);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(4, 430);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(959, 172);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Inserção do Produto (Dados do Produto Selecionado)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbxPrecoNormal);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.chkIgnorarMudancasPrecos);
            this.groupBox4.Controls.Add(this.tbxCodigoProduto);
            this.groupBox4.Controls.Add(this.lblProdutoAdicionado);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.tbxValorDescontoAcrescimo);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.tbxTipoDescontoAcrescimo);
            this.groupBox4.Controls.Add(this.tbxCodigoFabricante);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.btnFinalizarPesquisa);
            this.groupBox4.Controls.Add(this.tbxValorTotal);
            this.groupBox4.Controls.Add(this.lblPrecoRevParc);
            this.groupBox4.Controls.Add(this.tbxQuantidade);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tbxPrecoRevParcelado);
            this.groupBox4.Controls.Add(this.tbxValorDigitado);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.tbxQuantidadeEstoque);
            this.groupBox4.Controls.Add(this.tbxPrecoVendaParcelado);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.tbxDescricaoAplicacaoEFilho);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(171, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(783, 150);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Informações Sobre o Produto Selecionado:";
            // 
            // tbxPrecoNormal
            // 
            this.tbxPrecoNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPrecoNormal.Location = new System.Drawing.Point(222, 39);
            this.tbxPrecoNormal.Name = "tbxPrecoNormal";
            this.tbxPrecoNormal.ReadOnly = true;
            this.tbxPrecoNormal.Size = new System.Drawing.Size(130, 21);
            this.tbxPrecoNormal.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(219, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 68;
            this.label1.Text = "Venda:";
            // 
            // chkIgnorarMudancasPrecos
            // 
            this.chkIgnorarMudancasPrecos.AutoSize = true;
            this.chkIgnorarMudancasPrecos.Location = new System.Drawing.Point(272, 100);
            this.chkIgnorarMudancasPrecos.Name = "chkIgnorarMudancasPrecos";
            this.chkIgnorarMudancasPrecos.Size = new System.Drawing.Size(335, 17);
            this.chkIgnorarMudancasPrecos.TabIndex = 66;
            this.chkIgnorarMudancasPrecos.Text = "Ign.Mudança de Preços (Desc/Acre). Preço Digitado=Real (F12).";
            this.chkIgnorarMudancasPrecos.UseVisualStyleBackColor = true;
            this.chkIgnorarMudancasPrecos.Visible = false;
            // 
            // tbxCodigoProduto
            // 
            this.tbxCodigoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoProduto.Location = new System.Drawing.Point(6, 39);
            this.tbxCodigoProduto.Name = "tbxCodigoProduto";
            this.tbxCodigoProduto.ReadOnly = true;
            this.tbxCodigoProduto.Size = new System.Drawing.Size(74, 21);
            this.tbxCodigoProduto.TabIndex = 0;
            // 
            // lblProdutoAdicionado
            // 
            this.lblProdutoAdicionado.AutoSize = true;
            this.lblProdutoAdicionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdutoAdicionado.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblProdutoAdicionado.Location = new System.Drawing.Point(275, 126);
            this.lblProdutoAdicionado.Name = "lblProdutoAdicionado";
            this.lblProdutoAdicionado.Size = new System.Drawing.Size(0, 18);
            this.lblProdutoAdicionado.TabIndex = 60;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 101);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 13);
            this.label14.TabIndex = 65;
            this.label14.Text = "Acrescimo ou Desconto:";
            // 
            // tbxValorDescontoAcrescimo
            // 
            this.tbxValorDescontoAcrescimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxValorDescontoAcrescimo.Location = new System.Drawing.Point(176, 98);
            this.tbxValorDescontoAcrescimo.Multiline = true;
            this.tbxValorDescontoAcrescimo.Name = "tbxValorDescontoAcrescimo";
            this.tbxValorDescontoAcrescimo.ReadOnly = true;
            this.tbxValorDescontoAcrescimo.Size = new System.Drawing.Size(88, 24);
            this.tbxValorDescontoAcrescimo.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Cod.Int Prod:";
            // 
            // tbxTipoDescontoAcrescimo
            // 
            this.tbxTipoDescontoAcrescimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTipoDescontoAcrescimo.Location = new System.Drawing.Point(135, 98);
            this.tbxTipoDescontoAcrescimo.Multiline = true;
            this.tbxTipoDescontoAcrescimo.Name = "tbxTipoDescontoAcrescimo";
            this.tbxTipoDescontoAcrescimo.ReadOnly = true;
            this.tbxTipoDescontoAcrescimo.Size = new System.Drawing.Size(39, 24);
            this.tbxTipoDescontoAcrescimo.TabIndex = 63;
            // 
            // tbxCodigoFabricante
            // 
            this.tbxCodigoFabricante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoFabricante.Location = new System.Drawing.Point(86, 39);
            this.tbxCodigoFabricante.Name = "tbxCodigoFabricante";
            this.tbxCodigoFabricante.ReadOnly = true;
            this.tbxCodigoFabricante.Size = new System.Drawing.Size(130, 21);
            this.tbxCodigoFabricante.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(83, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Cod.Fabric:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(664, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 16);
            this.label12.TabIndex = 41;
            this.label12.Text = "Vlr.Total (F11):";
            // 
            // btnFinalizarPesquisa
            // 
            this.btnFinalizarPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizarPesquisa.Image = global::FuturaDataTCC.Properties.Resources.Agenda;
            this.btnFinalizarPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinalizarPesquisa.Location = new System.Drawing.Point(644, 93);
            this.btnFinalizarPesquisa.Name = "btnFinalizarPesquisa";
            this.btnFinalizarPesquisa.Size = new System.Drawing.Size(123, 28);
            this.btnFinalizarPesquisa.TabIndex = 3;
            this.btnFinalizarPesquisa.Text = "Adicionar Item";
            this.btnFinalizarPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFinalizarPesquisa.UseVisualStyleBackColor = true;
            this.btnFinalizarPesquisa.Click += new System.EventHandler(this.btnFinalizarPesquisa_Click);
            // 
            // tbxValorTotal
            // 
            this.tbxValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxValorTotal.Location = new System.Drawing.Point(667, 63);
            this.tbxValorTotal.Multiline = true;
            this.tbxValorTotal.Name = "tbxValorTotal";
            this.tbxValorTotal.ReadOnly = true;
            this.tbxValorTotal.Size = new System.Drawing.Size(100, 24);
            this.tbxValorTotal.TabIndex = 40;
            this.tbxValorTotal.Text = "0,0000";
            this.tbxValorTotal.Leave += new System.EventHandler(this.tbxValorTotal_Leave);
            // 
            // lblPrecoRevParc
            // 
            this.lblPrecoRevParc.AutoSize = true;
            this.lblPrecoRevParc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecoRevParc.Location = new System.Drawing.Point(309, 205);
            this.lblPrecoRevParc.Name = "lblPrecoRevParc";
            this.lblPrecoRevParc.Size = new System.Drawing.Size(74, 15);
            this.lblPrecoRevParc.TabIndex = 57;
            this.lblPrecoRevParc.Text = "Pr.Rev.Parc:";
            // 
            // tbxQuantidade
            // 
            this.tbxQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQuantidade.Location = new System.Drawing.Point(473, 63);
            this.tbxQuantidade.Multiline = true;
            this.tbxQuantidade.Name = "tbxQuantidade";
            this.tbxQuantidade.Size = new System.Drawing.Size(85, 24);
            this.tbxQuantidade.TabIndex = 1;
            this.tbxQuantidade.Text = "1,0000";
            this.tbxQuantidade.Enter += new System.EventHandler(this.tbxQuantidade_Enter);
            this.tbxQuantidade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxQuantidade_KeyDown);
            this.tbxQuantidade.Leave += new System.EventHandler(this.tbxQuantidade_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(559, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Valor Unit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(470, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Quant(F10):";
            // 
            // tbxPrecoRevParcelado
            // 
            this.tbxPrecoRevParcelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPrecoRevParcelado.Location = new System.Drawing.Point(312, 223);
            this.tbxPrecoRevParcelado.Name = "tbxPrecoRevParcelado";
            this.tbxPrecoRevParcelado.ReadOnly = true;
            this.tbxPrecoRevParcelado.Size = new System.Drawing.Size(92, 21);
            this.tbxPrecoRevParcelado.TabIndex = 56;
            // 
            // tbxValorDigitado
            // 
            this.tbxValorDigitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxValorDigitado.Location = new System.Drawing.Point(562, 63);
            this.tbxValorDigitado.Multiline = true;
            this.tbxValorDigitado.Name = "tbxValorDigitado";
            this.tbxValorDigitado.Size = new System.Drawing.Size(100, 24);
            this.tbxValorDigitado.TabIndex = 2;
            this.tbxValorDigitado.Text = "0,0000";
            this.tbxValorDigitado.Enter += new System.EventHandler(this.tbxValor_Enter);
            this.tbxValorDigitado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxValor_KeyDown);
            this.tbxValorDigitado.Leave += new System.EventHandler(this.tbxValor_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(105, 205);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 15);
            this.label18.TabIndex = 51;
            this.label18.Text = "Pr.Venda.Parc:";
            // 
            // tbxQuantidadeEstoque
            // 
            this.tbxQuantidadeEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQuantidadeEstoque.Location = new System.Drawing.Point(358, 39);
            this.tbxQuantidadeEstoque.Name = "tbxQuantidadeEstoque";
            this.tbxQuantidadeEstoque.ReadOnly = true;
            this.tbxQuantidadeEstoque.Size = new System.Drawing.Size(109, 21);
            this.tbxQuantidadeEstoque.TabIndex = 36;
            // 
            // tbxPrecoVendaParcelado
            // 
            this.tbxPrecoVendaParcelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPrecoVendaParcelado.Location = new System.Drawing.Point(108, 223);
            this.tbxPrecoVendaParcelado.Name = "tbxPrecoVendaParcelado";
            this.tbxPrecoVendaParcelado.ReadOnly = true;
            this.tbxPrecoVendaParcelado.Size = new System.Drawing.Size(92, 21);
            this.tbxPrecoVendaParcelado.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(355, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 15);
            this.label10.TabIndex = 37;
            this.label10.Text = "Qtd.Est:";
            // 
            // tbxDescricaoAplicacaoEFilho
            // 
            this.tbxDescricaoAplicacaoEFilho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDescricaoAplicacaoEFilho.Location = new System.Drawing.Point(115, 66);
            this.tbxDescricaoAplicacaoEFilho.MaxLength = 799;
            this.tbxDescricaoAplicacaoEFilho.Name = "tbxDescricaoAplicacaoEFilho";
            this.tbxDescricaoAplicacaoEFilho.ReadOnly = true;
            this.tbxDescricaoAplicacaoEFilho.Size = new System.Drawing.Size(352, 21);
            this.tbxDescricaoAplicacaoEFilho.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 15);
            this.label9.TabIndex = 35;
            this.label9.Text = "Descrição (F9 Alt.)";
            // 
            // pctImagemProduto
            // 
            this.pctImagemProduto.Location = new System.Drawing.Point(4, 32);
            this.pctImagemProduto.Name = "pctImagemProduto";
            this.pctImagemProduto.Size = new System.Drawing.Size(160, 122);
            this.pctImagemProduto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctImagemProduto.TabIndex = 61;
            this.pctImagemProduto.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Column4";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Column5";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Column6";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // adicionarSubProdutoToolStripMenuItem
            // 
            this.adicionarSubProdutoToolStripMenuItem.Name = "adicionarSubProdutoToolStripMenuItem";
            this.adicionarSubProdutoToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.adicionarSubProdutoToolStripMenuItem.Text = "Adicionar Sub-Produto";
            // 
            // frmPesquisaProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 614);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbpResultadoPesquisa);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPesquisaProdutos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisa por Produtos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPesquisaProdutos_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctTipoPesquisa)).EndInit();
            this.gbpResultadoPesquisa.ResumeLayout(false);
            this.ctmEscolhaSubProdutoESimilares.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctImagemProduto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTipoPesquisa;
        private System.Windows.Forms.TextBox tbxFiltroPesquisa;
        private System.Windows.Forms.GroupBox gbpResultadoPesquisa;
        private System.Windows.Forms.PictureBox pctTipoPesquisa;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        internal System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        internal System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.RadioButton rdbBuscaCodFabricante;
        private System.Windows.Forms.RadioButton rdbBuscaDescricao;
        private System.Windows.Forms.RadioButton rdbBuscaAplicacao;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFinalizarPesquisa;
        private System.Windows.Forms.TextBox tbxValorDigitado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip ctmEscolhaSubProdutoESimilares;
        private System.Windows.Forms.ToolStripMenuItem verificarProdutosSimilaresToolStripMenuItem;
        private System.Windows.Forms.ListView lvwPesquisaProdutos;
        public System.Windows.Forms.TextBox tbxCodigoProduto;
        public System.Windows.Forms.TextBox tbxQuantidade;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox tbxQuantidadeEstoque;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tbxDescricaoAplicacaoEFilho;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tbxCodigoFabricante;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox tbxValorTotal;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox tbxPrecoVendaParcelado;
        private System.Windows.Forms.Label lblPrecoRevParc;
        public System.Windows.Forms.TextBox tbxPrecoRevParcelado;
        private System.Windows.Forms.Label lblProdutoAdicionado;
        private System.Windows.Forms.PictureBox pctImagemProduto;
        private System.Windows.Forms.TextBox tbxValorDescontoAcrescimo;
        private System.Windows.Forms.TextBox tbxTipoDescontoAcrescimo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkIgnorarMudancasPrecos;
        private System.Windows.Forms.ToolStripMenuItem adicionarSubProdutoToolStripMenuItem;
        public System.Windows.Forms.TextBox tbxPrecoNormal;
        private System.Windows.Forms.Label label1;
    }
}