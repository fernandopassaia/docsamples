namespace FuturaDataTCC.Views.Gestoes
{
    partial class frmGestaoUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGestaoUsuarios));
            this.tstMenuCadastros = new System.Windows.Forms.ToolStrip();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.btnAlterar = new System.Windows.Forms.ToolStripButton();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrimeiroCad = new System.Windows.Forms.ToolStripButton();
            this.btnCadAnterior = new System.Windows.Forms.ToolStripButton();
            this.btnProximoCad = new System.Windows.Forms.ToolStripButton();
            this.btnUltimoCad = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssTextoStripParteBaixo = new System.Windows.Forms.ToolStripStatusLabel();
            this.CNetHelpProvider = new System.Windows.Forms.HelpProvider();
            this.tbxNomeUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCodigoUsuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxLembreteSenha = new System.Windows.Forms.TextBox();
            this.tbxSenhaUsuario = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxLoginUsuario = new System.Windows.Forms.TextBox();
            this.lblTipoDeSenha = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbCaixa = new System.Windows.Forms.RadioButton();
            this.rdbVendedor = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chkLOG_ModoIntegrado = new System.Windows.Forms.CheckBox();
            this.tbcUsuarios = new System.Windows.Forms.TabControl();
            this.tbpDados = new System.Windows.Forms.TabPage();
            this.tstMenuCadastros.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbcUsuarios.SuspendLayout();
            this.tbpDados.SuspendLayout();
            this.SuspendLayout();
            // 
            // tstMenuCadastros
            // 
            this.tstMenuCadastros.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.tstMenuCadastros.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.tstMenuCadastros.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.CNetHelpProvider.SetHelpKeyword(this.tstMenuCadastros, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_tstMenuCadast" +
        "ros");
            this.CNetHelpProvider.SetHelpNavigator(this.tstMenuCadastros, System.Windows.Forms.HelpNavigator.Topic);
            this.tstMenuCadastros.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo,
            this.btnAlterar,
            this.btnSalvar,
            this.btnExcluir,
            this.btnCancelar,
            this.toolStripSeparator1,
            this.btnPrimeiroCad,
            this.btnCadAnterior,
            this.btnProximoCad,
            this.btnUltimoCad});
            this.tstMenuCadastros.Location = new System.Drawing.Point(0, 0);
            this.tstMenuCadastros.Name = "tstMenuCadastros";
            this.tstMenuCadastros.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.CNetHelpProvider.SetShowHelp(this.tstMenuCadastros, true);
            this.tstMenuCadastros.Size = new System.Drawing.Size(853, 25);
            this.tstMenuCadastros.Stretch = true;
            this.tstMenuCadastros.TabIndex = 49;
            // 
            // btnNovo
            // 
            this.btnNovo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.RightToLeftAutoMirrorImage = true;
            this.btnNovo.Size = new System.Drawing.Size(60, 22);
            this.btnNovo.Text = "&Novo";
            this.btnNovo.ToolTipText = "Criar um Novo Cadastro";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Image = ((System.Drawing.Image)(resources.GetObject("btnAlterar.Image")));
            this.btnAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(73, 22);
            this.btnAlterar.Text = "&Alterar";
            this.btnAlterar.ToolTipText = "Alterar o Cadastro Atual";
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click_1);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(69, 22);
            this.btnSalvar.Text = "&Salvar";
            this.btnSalvar.ToolTipText = "Salvar o Cadastro Atual!";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click_1);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(90, 22);
            this.btnExcluir.Text = "&Desativar";
            this.btnExcluir.ToolTipText = "Excluir o Cadastro Atual!";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPrimeiroCad
            // 
            this.btnPrimeiroCad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrimeiroCad.Image = ((System.Drawing.Image)(resources.GetObject("btnPrimeiroCad.Image")));
            this.btnPrimeiroCad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrimeiroCad.Name = "btnPrimeiroCad";
            this.btnPrimeiroCad.Size = new System.Drawing.Size(23, 22);
            this.btnPrimeiroCad.Text = "&Primeiro";
            this.btnPrimeiroCad.ToolTipText = "Ir para o Primeiro Cadastro";
            this.btnPrimeiroCad.Click += new System.EventHandler(this.btnPrimeiroCad_Click_1);
            // 
            // btnCadAnterior
            // 
            this.btnCadAnterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCadAnterior.Image = ((System.Drawing.Image)(resources.GetObject("btnCadAnterior.Image")));
            this.btnCadAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCadAnterior.Name = "btnCadAnterior";
            this.btnCadAnterior.Size = new System.Drawing.Size(23, 22);
            this.btnCadAnterior.Text = "&Anterior";
            this.btnCadAnterior.ToolTipText = "Ir para o Cadastro Anterior";
            this.btnCadAnterior.Click += new System.EventHandler(this.btnCadAnterior_Click_1);
            // 
            // btnProximoCad
            // 
            this.btnProximoCad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProximoCad.Image = ((System.Drawing.Image)(resources.GetObject("btnProximoCad.Image")));
            this.btnProximoCad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProximoCad.Name = "btnProximoCad";
            this.btnProximoCad.Size = new System.Drawing.Size(23, 22);
            this.btnProximoCad.Text = "Pr&oximo";
            this.btnProximoCad.ToolTipText = "Ir para o Próximo Cadastro";
            this.btnProximoCad.Click += new System.EventHandler(this.btnProximoCad_Click_1);
            // 
            // btnUltimoCad
            // 
            this.btnUltimoCad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUltimoCad.Image = ((System.Drawing.Image)(resources.GetObject("btnUltimoCad.Image")));
            this.btnUltimoCad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUltimoCad.Name = "btnUltimoCad";
            this.btnUltimoCad.Size = new System.Drawing.Size(23, 22);
            this.btnUltimoCad.Text = "&Ultimo";
            this.btnUltimoCad.ToolTipText = "Ir para o Ultimo Cadastro";
            this.btnUltimoCad.Click += new System.EventHandler(this.btnUltimoCad_Click_1);
            // 
            // statusStrip1
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.statusStrip1, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_statusStrip1");
            this.CNetHelpProvider.SetHelpNavigator(this.statusStrip1, System.Windows.Forms.HelpNavigator.Topic);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssTextoStripParteBaixo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 149);
            this.statusStrip1.Name = "statusStrip1";
            this.CNetHelpProvider.SetShowHelp(this.statusStrip1, true);
            this.statusStrip1.Size = new System.Drawing.Size(853, 22);
            this.statusStrip1.TabIndex = 52;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssTextoStripParteBaixo
            // 
            this.tssTextoStripParteBaixo.Name = "tssTextoStripParteBaixo";
            this.tssTextoStripParteBaixo.Size = new System.Drawing.Size(276, 17);
            this.tssTextoStripParteBaixo.Text = "Acompanhe aqui informacoes sobre os cadastros...";
            // 
            // CNetHelpProvider
            // 
            this.CNetHelpProvider.HelpNamespace = "FuturaDataCorporateERP.chm";
            // 
            // tbxNomeUsuario
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxNomeUsuario, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_tbxNomeUsuari" +
        "o");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxNomeUsuario, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxNomeUsuario.Location = new System.Drawing.Point(81, 38);
            this.tbxNomeUsuario.MaxLength = 99;
            this.tbxNomeUsuario.Name = "tbxNomeUsuario";
            this.CNetHelpProvider.SetShowHelp(this.tbxNomeUsuario, true);
            this.tbxNomeUsuario.Size = new System.Drawing.Size(170, 20);
            this.tbxNomeUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label1, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_label1");
            this.CNetHelpProvider.SetHelpNavigator(this.label1, System.Windows.Forms.HelpNavigator.Topic);
            this.label1.Location = new System.Drawing.Point(78, 22);
            this.label1.Name = "label1";
            this.CNetHelpProvider.SetShowHelp(this.label1, true);
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Nome Usúario: (Nome/Sobrenome)";
            // 
            // tbxCodigoUsuario
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxCodigoUsuario, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_tbxCodigoUsua" +
        "rio");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxCodigoUsuario, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxCodigoUsuario.Location = new System.Drawing.Point(6, 38);
            this.tbxCodigoUsuario.Name = "tbxCodigoUsuario";
            this.CNetHelpProvider.SetShowHelp(this.tbxCodigoUsuario, true);
            this.tbxCodigoUsuario.Size = new System.Drawing.Size(73, 20);
            this.tbxCodigoUsuario.TabIndex = 0;
            //this.tbxCodigoUsuario.TextChanged += new System.EventHandler(this.tbxCodigoUsuario_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label3, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_label3");
            this.CNetHelpProvider.SetHelpNavigator(this.label3, System.Windows.Forms.HelpNavigator.Topic);
            this.label3.Location = new System.Drawing.Point(3, 21);
            this.label3.Name = "label3";
            this.CNetHelpProvider.SetShowHelp(this.label3, true);
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Código:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label4, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_label4");
            this.CNetHelpProvider.SetHelpNavigator(this.label4, System.Windows.Forms.HelpNavigator.Topic);
            this.label4.Location = new System.Drawing.Point(444, 22);
            this.label4.Name = "label4";
            this.CNetHelpProvider.SetShowHelp(this.label4, true);
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Lembrete:";
            // 
            // tbxLembreteSenha
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxLembreteSenha, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_tbxLembreteSe" +
        "nha");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxLembreteSenha, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxLembreteSenha.Location = new System.Drawing.Point(444, 38);
            this.tbxLembreteSenha.MaxLength = 119;
            this.tbxLembreteSenha.Name = "tbxLembreteSenha";
            this.CNetHelpProvider.SetShowHelp(this.tbxLembreteSenha, true);
            this.tbxLembreteSenha.Size = new System.Drawing.Size(147, 20);
            this.tbxLembreteSenha.TabIndex = 5;
            //this.tbxLembreteSenha.TextChanged += new System.EventHandler(this.tbxLembreteSenha_TextChanged);
            // 
            // tbxSenhaUsuario
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxSenhaUsuario, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_tbxSenhaUsuar" +
        "io");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxSenhaUsuario, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxSenhaUsuario.Location = new System.Drawing.Point(361, 39);
            this.tbxSenhaUsuario.Name = "tbxSenhaUsuario";
            this.tbxSenhaUsuario.PasswordChar = '*';
            this.CNetHelpProvider.SetShowHelp(this.tbxSenhaUsuario, true);
            this.tbxSenhaUsuario.Size = new System.Drawing.Size(77, 20);
            this.tbxSenhaUsuario.TabIndex = 4;
            this.tbxSenhaUsuario.TextChanged += new System.EventHandler(this.tbxSenhaUsuario_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label6, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_label6");
            this.CNetHelpProvider.SetHelpNavigator(this.label6, System.Windows.Forms.HelpNavigator.Topic);
            this.label6.Location = new System.Drawing.Point(358, 22);
            this.label6.Name = "label6";
            this.CNetHelpProvider.SetShowHelp(this.label6, true);
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Senha Usuário:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label5, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_label5");
            this.CNetHelpProvider.SetHelpNavigator(this.label5, System.Windows.Forms.HelpNavigator.Topic);
            this.label5.Location = new System.Drawing.Point(251, 22);
            this.label5.Name = "label5";
            this.CNetHelpProvider.SetShowHelp(this.label5, true);
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 61;
            this.label5.Text = "Login Usuário:";
            // 
            // tbxLoginUsuario
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxLoginUsuario, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_tbxLoginUsuar" +
        "io");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxLoginUsuario, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxLoginUsuario.Location = new System.Drawing.Point(254, 38);
            this.tbxLoginUsuario.MaxLength = 99;
            this.tbxLoginUsuario.Name = "tbxLoginUsuario";
            this.CNetHelpProvider.SetShowHelp(this.tbxLoginUsuario, true);
            this.tbxLoginUsuario.Size = new System.Drawing.Size(101, 20);
            this.tbxLoginUsuario.TabIndex = 3;
            // 
            // lblTipoDeSenha
            // 
            this.lblTipoDeSenha.AutoSize = true;
            this.lblTipoDeSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNetHelpProvider.SetHelpKeyword(this.lblTipoDeSenha, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_lblTipoDeSenh" +
        "a");
            this.CNetHelpProvider.SetHelpNavigator(this.lblTipoDeSenha, System.Windows.Forms.HelpNavigator.Topic);
            this.lblTipoDeSenha.Location = new System.Drawing.Point(631, 61);
            this.lblTipoDeSenha.Name = "lblTipoDeSenha";
            this.CNetHelpProvider.SetShowHelp(this.lblTipoDeSenha, true);
            this.lblTipoDeSenha.Size = new System.Drawing.Size(0, 12);
            this.lblTipoDeSenha.TabIndex = 62;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNetHelpProvider.SetHelpKeyword(this.label7, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_label7");
            this.CNetHelpProvider.SetHelpNavigator(this.label7, System.Windows.Forms.HelpNavigator.Topic);
            this.label7.Location = new System.Drawing.Point(113, 62);
            this.label7.Name = "label7";
            this.CNetHelpProvider.SetShowHelp(this.label7, true);
            this.label7.Size = new System.Drawing.Size(609, 12);
            this.label7.TabIndex = 63;
            this.label7.Text = "Dica: Para senhas mais seguras, use caracters especiais como \'@\', \'#\' \'/\', \'%\' e " +
    "outros. Não use datas de aniversário, nomes e informações comuns.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbCaixa);
            this.groupBox3.Controls.Add(this.rdbVendedor);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.chkLOG_ModoIntegrado);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblTipoDeSenha);
            this.groupBox3.Controls.Add(this.tbxLoginUsuario);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbxSenhaUsuario);
            this.groupBox3.Controls.Add(this.tbxLembreteSenha);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbxCodigoUsuario);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbxNomeUsuario);
            this.CNetHelpProvider.SetHelpKeyword(this.groupBox3, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm#frmGestaoUsuarios_groupBox3");
            this.CNetHelpProvider.SetHelpNavigator(this.groupBox3, System.Windows.Forms.HelpNavigator.Topic);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.CNetHelpProvider.SetShowHelp(this.groupBox3, true);
            this.groupBox3.Size = new System.Drawing.Size(831, 81);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "1º Passo - Informações de Login no Sistema:";
            // 
            // rdbCaixa
            // 
            this.rdbCaixa.AutoSize = true;
            this.rdbCaixa.Location = new System.Drawing.Point(683, 41);
            this.rdbCaixa.Name = "rdbCaixa";
            this.rdbCaixa.Size = new System.Drawing.Size(51, 17);
            this.rdbCaixa.TabIndex = 305;
            this.rdbCaixa.TabStop = true;
            this.rdbCaixa.Text = "Caixa";
            this.rdbCaixa.UseVisualStyleBackColor = true;
            //this.rdbCaixa.CheckedChanged += new System.EventHandler(this.rdbCaixa_CheckedChanged);
            // 
            // rdbVendedor
            // 
            this.rdbVendedor.AutoSize = true;
            this.rdbVendedor.Location = new System.Drawing.Point(606, 40);
            this.rdbVendedor.Name = "rdbVendedor";
            this.rdbVendedor.Size = new System.Drawing.Size(71, 17);
            this.rdbVendedor.TabIndex = 304;
            this.rdbVendedor.TabStop = true;
            this.rdbVendedor.Text = "Vendedor";
            this.rdbVendedor.UseVisualStyleBackColor = true;
            //this.rdbVendedor.CheckedChanged += new System.EventHandler(this.rdbVendedor_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(630, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 303;
            this.label2.Text = "Função";
            //this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkLOG_ModoIntegrado
            // 
            this.chkLOG_ModoIntegrado.AutoSize = true;
            this.chkLOG_ModoIntegrado.Checked = true;
            this.chkLOG_ModoIntegrado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLOG_ModoIntegrado.Location = new System.Drawing.Point(721, 17);
            this.chkLOG_ModoIntegrado.Name = "chkLOG_ModoIntegrado";
            this.chkLOG_ModoIntegrado.Size = new System.Drawing.Size(104, 17);
            this.chkLOG_ModoIntegrado.TabIndex = 301;
            this.chkLOG_ModoIntegrado.Text = "Modo Integrado.";
            this.chkLOG_ModoIntegrado.UseVisualStyleBackColor = true;
            // 
            // tbcUsuarios
            // 
            this.tbcUsuarios.Controls.Add(this.tbpDados);
            this.tbcUsuarios.Location = new System.Drawing.Point(0, 29);
            this.tbcUsuarios.Name = "tbcUsuarios";
            this.tbcUsuarios.SelectedIndex = 0;
            this.tbcUsuarios.Size = new System.Drawing.Size(853, 118);
            this.tbcUsuarios.TabIndex = 53;
            // 
            // tbpDados
            // 
            this.tbpDados.Controls.Add(this.groupBox3);
            this.tbpDados.Location = new System.Drawing.Point(4, 22);
            this.tbpDados.Name = "tbpDados";
            this.tbpDados.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDados.Size = new System.Drawing.Size(845, 92);
            this.tbpDados.TabIndex = 1;
            this.tbpDados.Text = "Dados do Usuário e Funcionário";
            this.tbpDados.UseVisualStyleBackColor = true;
            //this.tbpDados.Click += new System.EventHandler(this.tbpDados_Click);
            // 
            // frmGestaoUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 171);
            this.Controls.Add(this.tbcUsuarios);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tstMenuCadastros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.CNetHelpProvider.SetHelpKeyword(this, "FuturaDataCorporateERP_Form_frmGestaoUsuarios.htm");
            this.CNetHelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGestaoUsuarios";
            this.CNetHelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Usuarios/Funcionários e Configurações de Segurança.";
            this.tstMenuCadastros.ResumeLayout(false);
            this.tstMenuCadastros.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tbcUsuarios.ResumeLayout(false);
            this.tbpDados.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tstMenuCadastros;
        private System.Windows.Forms.ToolStripButton btnNovo;
        private System.Windows.Forms.ToolStripButton btnAlterar;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPrimeiroCad;
        private System.Windows.Forms.ToolStripButton btnCadAnterior;
        private System.Windows.Forms.ToolStripButton btnProximoCad;
        private System.Windows.Forms.ToolStripButton btnUltimoCad;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssTextoStripParteBaixo;
        private System.Windows.Forms.HelpProvider CNetHelpProvider;
        private System.Windows.Forms.TabControl tbcUsuarios;
        private System.Windows.Forms.TabPage tbpDados;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTipoDeSenha;
        private System.Windows.Forms.TextBox tbxLoginUsuario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox tbxSenhaUsuario;
        private System.Windows.Forms.TextBox tbxLembreteSenha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCodigoUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxNomeUsuario;
        private System.Windows.Forms.CheckBox chkLOG_ModoIntegrado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbVendedor;
        private System.Windows.Forms.RadioButton rdbCaixa;
    }
}