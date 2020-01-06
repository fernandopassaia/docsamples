namespace MsSqlBackup
{
    partial class MsSqlBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsSqlBackup));
            this.tbcPagControl = new System.Windows.Forms.TabControl();
            this.tbpBackupRestauracao = new System.Windows.Forms.TabPage();
            this.chkTranslateToENG = new System.Windows.Forms.CheckBox();
            this.lblNumeroBackupsManter = new System.Windows.Forms.Label();
            this.nudLimiteBackups = new System.Windows.Forms.NumericUpDown();
            this.chkApagarBackupsMaisAntigosAutomaticamente = new System.Windows.Forms.CheckBox();
            this.chkDesligarPCAoConcluir = new System.Windows.Forms.CheckBox();
            this.cbbDriversRemoviveis = new System.Windows.Forms.ComboBox();
            this.prgAndamentoBackup = new System.Windows.Forms.ProgressBar();
            this.lblProgressoBackup = new System.Windows.Forms.Label();
            this.btnRestaurarBackup = new System.Windows.Forms.Button();
            this.lnkEntendaBackup = new System.Windows.Forms.LinkLabel();
            this.lblPeridiocidade = new System.Windows.Forms.Label();
            this.nudBackupDeQuantosEmQuantosDias = new System.Windows.Forms.NumericUpDown();
            this.tbxHorarioBackup = new System.Windows.Forms.MaskedTextBox();
            this.chkAtivarBackupAutomatico = new System.Windows.Forms.CheckBox();
            this.btnSalvarBackupAgora = new System.Windows.Forms.Button();
            this.btnSelecionarPenDriveBackup = new System.Windows.Forms.Button();
            this.lblCaminhoRemovivel = new System.Windows.Forms.Label();
            this.tbxCaminhoBackupLocal = new System.Windows.Forms.TextBox();
            this.lblCaminhoDoBackup = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lvwBackupsLocais = new System.Windows.Forms.ListView();
            this.tbpGerenciadorBancoDados = new System.Windows.Forms.TabPage();
            this.grpExecutorSQL = new System.Windows.Forms.GroupBox();
            this.tbxComandoSqlExecutar = new System.Windows.Forms.TextBox();
            this.btnExecutarSQL = new System.Windows.Forms.Button();
            this.dgwRetornoSQL = new System.Windows.Forms.DataGridView();
            this.grpConexao = new System.Windows.Forms.GroupBox();
            this.btnLiberarFirewall = new System.Windows.Forms.Button();
            this.lblDBNomeInstancia = new System.Windows.Forms.Label();
            this.lblDBNomeBaseDados = new System.Windows.Forms.Label();
            this.lblDBSerialMaquina = new System.Windows.Forms.Label();
            this.lblBDNomeMaquina = new System.Windows.Forms.Label();
            this.lblDBServicoBancoDados = new System.Windows.Forms.Label();
            this.lblDBEdicaoBancoDados = new System.Windows.Forms.Label();
            this.lblDBServicePackBancoDados = new System.Windows.Forms.Label();
            this.lblDBVersaoBancoDados = new System.Windows.Forms.Label();
            this.lblDBNomeServidorBancoDados = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tmrBackup = new System.Windows.Forms.Timer(this.components);
            this.notNotificador = new System.Windows.Forms.NotifyIcon(this.components);
            this.tbcPagControl.SuspendLayout();
            this.tbpBackupRestauracao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimiteBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackupDeQuantosEmQuantosDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tbpGerenciadorBancoDados.SuspendLayout();
            this.grpExecutorSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRetornoSQL)).BeginInit();
            this.grpConexao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcPagControl
            // 
            this.tbcPagControl.Controls.Add(this.tbpBackupRestauracao);
            this.tbcPagControl.Controls.Add(this.tbpGerenciadorBancoDados);
            this.tbcPagControl.Location = new System.Drawing.Point(5, 5);
            this.tbcPagControl.Name = "tbcPagControl";
            this.tbcPagControl.SelectedIndex = 0;
            this.tbcPagControl.Size = new System.Drawing.Size(1311, 677);
            this.tbcPagControl.TabIndex = 0;
            // 
            // tbpBackupRestauracao
            // 
            this.tbpBackupRestauracao.Controls.Add(this.chkTranslateToENG);
            this.tbpBackupRestauracao.Controls.Add(this.lblNumeroBackupsManter);
            this.tbpBackupRestauracao.Controls.Add(this.nudLimiteBackups);
            this.tbpBackupRestauracao.Controls.Add(this.chkApagarBackupsMaisAntigosAutomaticamente);
            this.tbpBackupRestauracao.Controls.Add(this.chkDesligarPCAoConcluir);
            this.tbpBackupRestauracao.Controls.Add(this.cbbDriversRemoviveis);
            this.tbpBackupRestauracao.Controls.Add(this.prgAndamentoBackup);
            this.tbpBackupRestauracao.Controls.Add(this.lblProgressoBackup);
            this.tbpBackupRestauracao.Controls.Add(this.btnRestaurarBackup);
            this.tbpBackupRestauracao.Controls.Add(this.lnkEntendaBackup);
            this.tbpBackupRestauracao.Controls.Add(this.lblPeridiocidade);
            this.tbpBackupRestauracao.Controls.Add(this.nudBackupDeQuantosEmQuantosDias);
            this.tbpBackupRestauracao.Controls.Add(this.tbxHorarioBackup);
            this.tbpBackupRestauracao.Controls.Add(this.chkAtivarBackupAutomatico);
            this.tbpBackupRestauracao.Controls.Add(this.btnSalvarBackupAgora);
            this.tbpBackupRestauracao.Controls.Add(this.btnSelecionarPenDriveBackup);
            this.tbpBackupRestauracao.Controls.Add(this.lblCaminhoRemovivel);
            this.tbpBackupRestauracao.Controls.Add(this.tbxCaminhoBackupLocal);
            this.tbpBackupRestauracao.Controls.Add(this.lblCaminhoDoBackup);
            this.tbpBackupRestauracao.Controls.Add(this.pictureBox2);
            this.tbpBackupRestauracao.Controls.Add(this.lvwBackupsLocais);
            this.tbpBackupRestauracao.Location = new System.Drawing.Point(4, 22);
            this.tbpBackupRestauracao.Name = "tbpBackupRestauracao";
            this.tbpBackupRestauracao.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBackupRestauracao.Size = new System.Drawing.Size(1303, 651);
            this.tbpBackupRestauracao.TabIndex = 0;
            this.tbpBackupRestauracao.Text = "Backup e Restauração";
            this.tbpBackupRestauracao.UseVisualStyleBackColor = true;
            // 
            // chkTranslateToENG
            // 
            this.chkTranslateToENG.AutoSize = true;
            this.chkTranslateToENG.Location = new System.Drawing.Point(1178, 87);
            this.chkTranslateToENG.Name = "chkTranslateToENG";
            this.chkTranslateToENG.Size = new System.Drawing.Size(119, 19);
            this.chkTranslateToENG.TabIndex = 348;
            this.chkTranslateToENG.Text = "Translate to ENG";
            this.chkTranslateToENG.UseVisualStyleBackColor = true;
            this.chkTranslateToENG.CheckedChanged += new System.EventHandler(this.chkTranslateToENG_CheckedChanged);
            // 
            // lblNumeroBackupsManter
            // 
            this.lblNumeroBackupsManter.AutoSize = true;
            this.lblNumeroBackupsManter.Location = new System.Drawing.Point(935, 37);
            this.lblNumeroBackupsManter.Name = "lblNumeroBackupsManter";
            this.lblNumeroBackupsManter.Size = new System.Drawing.Size(351, 15);
            this.lblNumeroBackupsManter.TabIndex = 347;
            this.lblNumeroBackupsManter.Text = "Número de Backups a manter (0 = nunca apagar mais antigos)";
            // 
            // nudLimiteBackups
            // 
            this.nudLimiteBackups.Location = new System.Drawing.Point(855, 35);
            this.nudLimiteBackups.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLimiteBackups.Name = "nudLimiteBackups";
            this.nudLimiteBackups.Size = new System.Drawing.Size(44, 20);
            this.nudLimiteBackups.TabIndex = 346;
            this.nudLimiteBackups.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // chkApagarBackupsMaisAntigosAutomaticamente
            // 
            this.chkApagarBackupsMaisAntigosAutomaticamente.AutoSize = true;
            this.chkApagarBackupsMaisAntigosAutomaticamente.Checked = true;
            this.chkApagarBackupsMaisAntigosAutomaticamente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApagarBackupsMaisAntigosAutomaticamente.Location = new System.Drawing.Point(855, 61);
            this.chkApagarBackupsMaisAntigosAutomaticamente.Name = "chkApagarBackupsMaisAntigosAutomaticamente";
            this.chkApagarBackupsMaisAntigosAutomaticamente.Size = new System.Drawing.Size(442, 19);
            this.chkApagarBackupsMaisAntigosAutomaticamente.TabIndex = 345;
            this.chkApagarBackupsMaisAntigosAutomaticamente.Text = "Se disp.removível estiver cheio, apague automaticamente backups + antigos";
            this.chkApagarBackupsMaisAntigosAutomaticamente.UseVisualStyleBackColor = true;
            // 
            // chkDesligarPCAoConcluir
            // 
            this.chkDesligarPCAoConcluir.AutoSize = true;
            this.chkDesligarPCAoConcluir.Checked = true;
            this.chkDesligarPCAoConcluir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDesligarPCAoConcluir.Location = new System.Drawing.Point(855, 87);
            this.chkDesligarPCAoConcluir.Name = "chkDesligarPCAoConcluir";
            this.chkDesligarPCAoConcluir.Size = new System.Drawing.Size(208, 19);
            this.chkDesligarPCAoConcluir.TabIndex = 344;
            this.chkDesligarPCAoConcluir.Text = "Desligar Computador ao Concluir";
            this.chkDesligarPCAoConcluir.UseVisualStyleBackColor = true;
            // 
            // cbbDriversRemoviveis
            // 
            this.cbbDriversRemoviveis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDriversRemoviveis.FormattingEnabled = true;
            this.cbbDriversRemoviveis.Location = new System.Drawing.Point(108, 80);
            this.cbbDriversRemoviveis.Name = "cbbDriversRemoviveis";
            this.cbbDriversRemoviveis.Size = new System.Drawing.Size(237, 21);
            this.cbbDriversRemoviveis.TabIndex = 343;
            // 
            // prgAndamentoBackup
            // 
            this.prgAndamentoBackup.Location = new System.Drawing.Point(8, 130);
            this.prgAndamentoBackup.Maximum = 8;
            this.prgAndamentoBackup.Name = "prgAndamentoBackup";
            this.prgAndamentoBackup.Size = new System.Drawing.Size(170, 13);
            this.prgAndamentoBackup.TabIndex = 341;
            // 
            // lblProgressoBackup
            // 
            this.lblProgressoBackup.AutoSize = true;
            this.lblProgressoBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.792453F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressoBackup.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblProgressoBackup.Location = new System.Drawing.Point(182, 130);
            this.lblProgressoBackup.Name = "lblProgressoBackup";
            this.lblProgressoBackup.Size = new System.Drawing.Size(0, 13);
            this.lblProgressoBackup.TabIndex = 340;
            // 
            // btnRestaurarBackup
            // 
            this.btnRestaurarBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.792453F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurarBackup.Image = ((System.Drawing.Image)(resources.GetObject("btnRestaurarBackup.Image")));
            this.btnRestaurarBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestaurarBackup.Location = new System.Drawing.Point(1167, 123);
            this.btnRestaurarBackup.Name = "btnRestaurarBackup";
            this.btnRestaurarBackup.Size = new System.Drawing.Size(130, 28);
            this.btnRestaurarBackup.TabIndex = 337;
            this.btnRestaurarBackup.Text = "Restaurar Backup";
            this.btnRestaurarBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestaurarBackup.UseVisualStyleBackColor = true;
            this.btnRestaurarBackup.Click += new System.EventHandler(this.btnRestaurarBackup_Click);
            // 
            // lnkEntendaBackup
            // 
            this.lnkEntendaBackup.AutoSize = true;
            this.lnkEntendaBackup.Location = new System.Drawing.Point(827, 129);
            this.lnkEntendaBackup.Name = "lnkEntendaBackup";
            this.lnkEntendaBackup.Size = new System.Drawing.Size(172, 15);
            this.lnkEntendaBackup.TabIndex = 336;
            this.lnkEntendaBackup.TabStop = true;
            this.lnkEntendaBackup.Text = "Entenda o Sistema de Backup";
            this.lnkEntendaBackup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEntendaBackup_LinkClicked);
            // 
            // lblPeridiocidade
            // 
            this.lblPeridiocidade.AutoSize = true;
            this.lblPeridiocidade.Location = new System.Drawing.Point(1085, 11);
            this.lblPeridiocidade.Name = "lblPeridiocidade";
            this.lblPeridiocidade.Size = new System.Drawing.Size(201, 15);
            this.lblPeridiocidade.TabIndex = 334;
            this.lblPeridiocidade.Text = "Periodicidade (1 = backup todo dia)";
            // 
            // nudBackupDeQuantosEmQuantosDias
            // 
            this.nudBackupDeQuantosEmQuantosDias.Location = new System.Drawing.Point(1034, 10);
            this.nudBackupDeQuantosEmQuantosDias.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudBackupDeQuantosEmQuantosDias.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBackupDeQuantosEmQuantosDias.Name = "nudBackupDeQuantosEmQuantosDias";
            this.nudBackupDeQuantosEmQuantosDias.Size = new System.Drawing.Size(44, 20);
            this.nudBackupDeQuantosEmQuantosDias.TabIndex = 333;
            this.nudBackupDeQuantosEmQuantosDias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbxHorarioBackup
            // 
            this.tbxHorarioBackup.Location = new System.Drawing.Point(989, 10);
            this.tbxHorarioBackup.Mask = "99:99";
            this.tbxHorarioBackup.Name = "tbxHorarioBackup";
            this.tbxHorarioBackup.Size = new System.Drawing.Size(39, 20);
            this.tbxHorarioBackup.TabIndex = 332;
            this.tbxHorarioBackup.Text = "1815";
            // 
            // chkAtivarBackupAutomatico
            // 
            this.chkAtivarBackupAutomatico.AutoSize = true;
            this.chkAtivarBackupAutomatico.Checked = true;
            this.chkAtivarBackupAutomatico.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivarBackupAutomatico.Location = new System.Drawing.Point(855, 10);
            this.chkAtivarBackupAutomatico.Name = "chkAtivarBackupAutomatico";
            this.chkAtivarBackupAutomatico.Size = new System.Drawing.Size(131, 19);
            this.chkAtivarBackupAutomatico.TabIndex = 331;
            this.chkAtivarBackupAutomatico.Text = "Backup Automático";
            this.chkAtivarBackupAutomatico.UseVisualStyleBackColor = true;
            // 
            // btnSalvarBackupAgora
            // 
            this.btnSalvarBackupAgora.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.792453F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarBackupAgora.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvarBackupAgora.Image")));
            this.btnSalvarBackupAgora.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvarBackupAgora.Location = new System.Drawing.Point(1005, 123);
            this.btnSalvarBackupAgora.Name = "btnSalvarBackupAgora";
            this.btnSalvarBackupAgora.Size = new System.Drawing.Size(158, 28);
            this.btnSalvarBackupAgora.TabIndex = 330;
            this.btnSalvarBackupAgora.Text = "Fazer Backup Agora";
            this.btnSalvarBackupAgora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvarBackupAgora.UseVisualStyleBackColor = true;
            this.btnSalvarBackupAgora.Click += new System.EventHandler(this.btnSalvarBackupAgora_Click);
            // 
            // btnSelecionarPenDriveBackup
            // 
            this.btnSelecionarPenDriveBackup.Image = ((System.Drawing.Image)(resources.GetObject("btnSelecionarPenDriveBackup.Image")));
            this.btnSelecionarPenDriveBackup.Location = new System.Drawing.Point(349, 76);
            this.btnSelecionarPenDriveBackup.Name = "btnSelecionarPenDriveBackup";
            this.btnSelecionarPenDriveBackup.Size = new System.Drawing.Size(28, 28);
            this.btnSelecionarPenDriveBackup.TabIndex = 329;
            this.btnSelecionarPenDriveBackup.UseVisualStyleBackColor = true;
            this.btnSelecionarPenDriveBackup.Click += new System.EventHandler(this.btnSelecionarPenDriveBackup_Click);
            // 
            // lblCaminhoRemovivel
            // 
            this.lblCaminhoRemovivel.AutoSize = true;
            this.lblCaminhoRemovivel.Location = new System.Drawing.Point(105, 61);
            this.lblCaminhoRemovivel.Name = "lblCaminhoRemovivel";
            this.lblCaminhoRemovivel.Size = new System.Drawing.Size(240, 15);
            this.lblCaminhoRemovivel.TabIndex = 327;
            this.lblCaminhoRemovivel.Text = "Caminho Removível (Pendrive/HdExterno):";
            // 
            // tbxCaminhoBackupLocal
            // 
            this.tbxCaminhoBackupLocal.Location = new System.Drawing.Point(108, 34);
            this.tbxCaminhoBackupLocal.Name = "tbxCaminhoBackupLocal";
            this.tbxCaminhoBackupLocal.ReadOnly = true;
            this.tbxCaminhoBackupLocal.Size = new System.Drawing.Size(237, 20);
            this.tbxCaminhoBackupLocal.TabIndex = 326;
            this.tbxCaminhoBackupLocal.Text = "C:\\MsSqlServerBackup\\Backups";
            // 
            // lblCaminhoDoBackup
            // 
            this.lblCaminhoDoBackup.AutoSize = true;
            this.lblCaminhoDoBackup.Location = new System.Drawing.Point(105, 15);
            this.lblCaminhoDoBackup.Name = "lblCaminhoDoBackup";
            this.lblCaminhoDoBackup.Size = new System.Drawing.Size(154, 15);
            this.lblCaminhoDoBackup.TabIndex = 325;
            this.lblCaminhoDoBackup.Text = "Caminho do Backup Local:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 21);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(90, 78);
            this.pictureBox2.TabIndex = 324;
            this.pictureBox2.TabStop = false;
            // 
            // lvwBackupsLocais
            // 
            this.lvwBackupsLocais.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwBackupsLocais.FullRowSelect = true;
            this.lvwBackupsLocais.GridLines = true;
            this.lvwBackupsLocais.Location = new System.Drawing.Point(8, 155);
            this.lvwBackupsLocais.Name = "lvwBackupsLocais";
            this.lvwBackupsLocais.Size = new System.Drawing.Size(1289, 490);
            this.lvwBackupsLocais.TabIndex = 319;
            this.lvwBackupsLocais.UseCompatibleStateImageBehavior = false;
            this.lvwBackupsLocais.View = System.Windows.Forms.View.Details;
            // 
            // tbpGerenciadorBancoDados
            // 
            this.tbpGerenciadorBancoDados.Controls.Add(this.grpExecutorSQL);
            this.tbpGerenciadorBancoDados.Controls.Add(this.grpConexao);
            this.tbpGerenciadorBancoDados.Controls.Add(this.pictureBox3);
            this.tbpGerenciadorBancoDados.Location = new System.Drawing.Point(4, 22);
            this.tbpGerenciadorBancoDados.Name = "tbpGerenciadorBancoDados";
            this.tbpGerenciadorBancoDados.Size = new System.Drawing.Size(1303, 651);
            this.tbpGerenciadorBancoDados.TabIndex = 8;
            this.tbpGerenciadorBancoDados.Text = "Gerenciador do Banco de Dados e Acesso via Rede";
            this.tbpGerenciadorBancoDados.UseVisualStyleBackColor = true;
            // 
            // grpExecutorSQL
            // 
            this.grpExecutorSQL.Controls.Add(this.tbxComandoSqlExecutar);
            this.grpExecutorSQL.Controls.Add(this.btnExecutarSQL);
            this.grpExecutorSQL.Controls.Add(this.dgwRetornoSQL);
            this.grpExecutorSQL.Location = new System.Drawing.Point(9, 250);
            this.grpExecutorSQL.Name = "grpExecutorSQL";
            this.grpExecutorSQL.Size = new System.Drawing.Size(1290, 393);
            this.grpExecutorSQL.TabIndex = 40;
            this.grpExecutorSQL.TabStop = false;
            this.grpExecutorSQL.Text = "Executor de Operações e Consultas no Banco de Dados (Operations Query Executor):";
            // 
            // tbxComandoSqlExecutar
            // 
            this.tbxComandoSqlExecutar.Location = new System.Drawing.Point(8, 25);
            this.tbxComandoSqlExecutar.Multiline = true;
            this.tbxComandoSqlExecutar.Name = "tbxComandoSqlExecutar";
            this.tbxComandoSqlExecutar.Size = new System.Drawing.Size(492, 58);
            this.tbxComandoSqlExecutar.TabIndex = 33;
            // 
            // btnExecutarSQL
            // 
            this.btnExecutarSQL.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecutarSQL.Image = ((System.Drawing.Image)(resources.GetObject("btnExecutarSQL.Image")));
            this.btnExecutarSQL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutarSQL.Location = new System.Drawing.Point(506, 55);
            this.btnExecutarSQL.Name = "btnExecutarSQL";
            this.btnExecutarSQL.Size = new System.Drawing.Size(122, 28);
            this.btnExecutarSQL.TabIndex = 31;
            this.btnExecutarSQL.Text = "Executar SQL";
            this.btnExecutarSQL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecutarSQL.UseVisualStyleBackColor = true;
            this.btnExecutarSQL.Click += new System.EventHandler(this.btnExecutarSQL_Click);
            // 
            // dgwRetornoSQL
            // 
            this.dgwRetornoSQL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgwRetornoSQL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgwRetornoSQL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwRetornoSQL.Location = new System.Drawing.Point(8, 89);
            this.dgwRetornoSQL.Name = "dgwRetornoSQL";
            this.dgwRetornoSQL.Size = new System.Drawing.Size(1276, 298);
            this.dgwRetornoSQL.TabIndex = 35;
            // 
            // grpConexao
            // 
            this.grpConexao.Controls.Add(this.btnLiberarFirewall);
            this.grpConexao.Controls.Add(this.lblDBNomeInstancia);
            this.grpConexao.Controls.Add(this.lblDBNomeBaseDados);
            this.grpConexao.Controls.Add(this.lblDBSerialMaquina);
            this.grpConexao.Controls.Add(this.lblBDNomeMaquina);
            this.grpConexao.Controls.Add(this.lblDBServicoBancoDados);
            this.grpConexao.Controls.Add(this.lblDBEdicaoBancoDados);
            this.grpConexao.Controls.Add(this.lblDBServicePackBancoDados);
            this.grpConexao.Controls.Add(this.lblDBVersaoBancoDados);
            this.grpConexao.Controls.Add(this.lblDBNomeServidorBancoDados);
            this.grpConexao.Location = new System.Drawing.Point(122, 9);
            this.grpConexao.Name = "grpConexao";
            this.grpConexao.Size = new System.Drawing.Size(1171, 221);
            this.grpConexao.TabIndex = 17;
            this.grpConexao.TabStop = false;
            this.grpConexao.Text = "Conexão e Info sobre Banco Dados:";
            // 
            // btnLiberarFirewall
            // 
            this.btnLiberarFirewall.Image = ((System.Drawing.Image)(resources.GetObject("btnLiberarFirewall.Image")));
            this.btnLiberarFirewall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLiberarFirewall.Location = new System.Drawing.Point(550, 187);
            this.btnLiberarFirewall.Name = "btnLiberarFirewall";
            this.btnLiberarFirewall.Size = new System.Drawing.Size(615, 26);
            this.btnLiberarFirewall.TabIndex = 37;
            this.btnLiberarFirewall.Text = "Executar Automaticamente os Passos (Liberar Portas e Programas) para SQL Server f" +
    "uncionar em rede.";
            this.btnLiberarFirewall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLiberarFirewall.UseVisualStyleBackColor = true;
            this.btnLiberarFirewall.Click += new System.EventHandler(this.btnLiberarFirewall_Click);
            // 
            // lblDBNomeInstancia
            // 
            this.lblDBNomeInstancia.AutoSize = true;
            this.lblDBNomeInstancia.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBNomeInstancia.Location = new System.Drawing.Point(12, 91);
            this.lblDBNomeInstancia.Name = "lblDBNomeInstancia";
            this.lblDBNomeInstancia.Size = new System.Drawing.Size(97, 12);
            this.lblDBNomeInstancia.TabIndex = 30;
            this.lblDBNomeInstancia.Text = "Nome Instância:";
            // 
            // lblDBNomeBaseDados
            // 
            this.lblDBNomeBaseDados.AutoSize = true;
            this.lblDBNomeBaseDados.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBNomeBaseDados.Location = new System.Drawing.Point(12, 69);
            this.lblDBNomeBaseDados.Name = "lblDBNomeBaseDados";
            this.lblDBNomeBaseDados.Size = new System.Drawing.Size(130, 12);
            this.lblDBNomeBaseDados.TabIndex = 29;
            this.lblDBNomeBaseDados.Text = "Nome Base de Dados:";
            // 
            // lblDBSerialMaquina
            // 
            this.lblDBSerialMaquina.AutoSize = true;
            this.lblDBSerialMaquina.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBSerialMaquina.Location = new System.Drawing.Point(12, 201);
            this.lblDBSerialMaquina.Name = "lblDBSerialMaquina";
            this.lblDBSerialMaquina.Size = new System.Drawing.Size(110, 12);
            this.lblDBSerialMaquina.TabIndex = 28;
            this.lblDBSerialMaquina.Text = "Serial da Máquina:";
            // 
            // lblBDNomeMaquina
            // 
            this.lblBDNomeMaquina.AutoSize = true;
            this.lblBDNomeMaquina.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBDNomeMaquina.Location = new System.Drawing.Point(12, 179);
            this.lblBDNomeMaquina.Name = "lblBDNomeMaquina";
            this.lblBDNomeMaquina.Size = new System.Drawing.Size(111, 12);
            this.lblBDNomeMaquina.TabIndex = 27;
            this.lblBDNomeMaquina.Text = "Nome da Máquina:";
            // 
            // lblDBServicoBancoDados
            // 
            this.lblDBServicoBancoDados.AutoSize = true;
            this.lblDBServicoBancoDados.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBServicoBancoDados.Location = new System.Drawing.Point(12, 47);
            this.lblDBServicoBancoDados.Name = "lblDBServicoBancoDados";
            this.lblDBServicoBancoDados.Size = new System.Drawing.Size(179, 12);
            this.lblDBServicoBancoDados.TabIndex = 21;
            this.lblDBServicoBancoDados.Text = "Nome Serviço Banco de Dados:";
            // 
            // lblDBEdicaoBancoDados
            // 
            this.lblDBEdicaoBancoDados.AutoSize = true;
            this.lblDBEdicaoBancoDados.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBEdicaoBancoDados.Location = new System.Drawing.Point(12, 157);
            this.lblDBEdicaoBancoDados.Name = "lblDBEdicaoBancoDados";
            this.lblDBEdicaoBancoDados.Size = new System.Drawing.Size(157, 12);
            this.lblDBEdicaoBancoDados.TabIndex = 20;
            this.lblDBEdicaoBancoDados.Text = "Edição do Banco de Dados:";
            // 
            // lblDBServicePackBancoDados
            // 
            this.lblDBServicePackBancoDados.AutoSize = true;
            this.lblDBServicePackBancoDados.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBServicePackBancoDados.Location = new System.Drawing.Point(12, 135);
            this.lblDBServicePackBancoDados.Name = "lblDBServicePackBancoDados";
            this.lblDBServicePackBancoDados.Size = new System.Drawing.Size(187, 12);
            this.lblDBServicePackBancoDados.TabIndex = 19;
            this.lblDBServicePackBancoDados.Text = "ServicePack do Banco de Dados:";
            // 
            // lblDBVersaoBancoDados
            // 
            this.lblDBVersaoBancoDados.AutoSize = true;
            this.lblDBVersaoBancoDados.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBVersaoBancoDados.Location = new System.Drawing.Point(12, 113);
            this.lblDBVersaoBancoDados.Name = "lblDBVersaoBancoDados";
            this.lblDBVersaoBancoDados.Size = new System.Drawing.Size(191, 12);
            this.lblDBVersaoBancoDados.TabIndex = 18;
            this.lblDBVersaoBancoDados.Text = "Versão Servidor Banco de Dados:";
            // 
            // lblDBNomeServidorBancoDados
            // 
            this.lblDBNomeServidorBancoDados.AutoSize = true;
            this.lblDBNomeServidorBancoDados.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBNomeServidorBancoDados.Location = new System.Drawing.Point(12, 25);
            this.lblDBNomeServidorBancoDados.Name = "lblDBNomeServidorBancoDados";
            this.lblDBNomeServidorBancoDados.Size = new System.Drawing.Size(185, 12);
            this.lblDBNomeServidorBancoDados.TabIndex = 17;
            this.lblDBNomeServidorBancoDados.Text = "Nome Servidor Banco de Dados:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(9, 44);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(101, 100);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // tmrBackup
            // 
            this.tmrBackup.Enabled = true;
            this.tmrBackup.Interval = 60000;
            this.tmrBackup.Tick += new System.EventHandler(this.tmrBackup_Tick);
            // 
            // notNotificador
            // 
            this.notNotificador.Icon = ((System.Drawing.Icon)(resources.GetObject("notNotificador.Icon")));
            this.notNotificador.Text = "MsSqlServer Backup";
            this.notNotificador.DoubleClick += new System.EventHandler(this.notNotificador_DoubleClick);
            // 
            // MsSqlBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 685);
            this.Controls.Add(this.tbcPagControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MsSqlBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsSqlServer Backup (by Fernando Passaia)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncServer_FormClosing);
            this.Resize += new System.EventHandler(this.MsSqlBackup_Resize);
            this.tbcPagControl.ResumeLayout(false);
            this.tbpBackupRestauracao.ResumeLayout(false);
            this.tbpBackupRestauracao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimiteBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackupDeQuantosEmQuantosDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tbpGerenciadorBancoDados.ResumeLayout(false);
            this.grpExecutorSQL.ResumeLayout(false);
            this.grpExecutorSQL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRetornoSQL)).EndInit();
            this.grpConexao.ResumeLayout(false);
            this.grpConexao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcPagControl;
        private System.Windows.Forms.TabPage tbpBackupRestauracao;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.ListView lvwBackupsLocais;
        private System.Windows.Forms.Label lblCaminhoRemovivel;
        private System.Windows.Forms.TextBox tbxCaminhoBackupLocal;
        private System.Windows.Forms.Label lblCaminhoDoBackup;
        private System.Windows.Forms.Button btnSelecionarPenDriveBackup;
        private System.Windows.Forms.Button btnSalvarBackupAgora;
        private System.Windows.Forms.Label lblPeridiocidade;
        private System.Windows.Forms.NumericUpDown nudBackupDeQuantosEmQuantosDias;
        private System.Windows.Forms.MaskedTextBox tbxHorarioBackup;
        private System.Windows.Forms.CheckBox chkAtivarBackupAutomatico;
        private System.Windows.Forms.Button btnRestaurarBackup;
        private System.Windows.Forms.ProgressBar prgAndamentoBackup;
        private System.Windows.Forms.Label lblProgressoBackup;
        private System.Windows.Forms.ComboBox cbbDriversRemoviveis;
        private System.Windows.Forms.CheckBox chkApagarBackupsMaisAntigosAutomaticamente;
        private System.Windows.Forms.CheckBox chkDesligarPCAoConcluir;
        private System.Windows.Forms.Timer tmrBackup;
        private System.Windows.Forms.NumericUpDown nudLimiteBackups;
        private System.Windows.Forms.Label lblNumeroBackupsManter;
        private System.Windows.Forms.TabPage tbpGerenciadorBancoDados;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox grpConexao;
        private System.Windows.Forms.Label lblDBServicoBancoDados;
        private System.Windows.Forms.Label lblDBEdicaoBancoDados;
        private System.Windows.Forms.Label lblDBServicePackBancoDados;
        private System.Windows.Forms.Label lblDBVersaoBancoDados;
        private System.Windows.Forms.Label lblDBNomeServidorBancoDados;
        private System.Windows.Forms.Button btnLiberarFirewall;
        private System.Windows.Forms.Label lblDBSerialMaquina;
        private System.Windows.Forms.Label lblBDNomeMaquina;
        private System.Windows.Forms.CheckBox chkTranslateToENG;
        private System.Windows.Forms.GroupBox grpExecutorSQL;
        private System.Windows.Forms.TextBox tbxComandoSqlExecutar;
        private System.Windows.Forms.Button btnExecutarSQL;
        private System.Windows.Forms.DataGridView dgwRetornoSQL;
        private System.Windows.Forms.Label lblDBNomeBaseDados;
        private System.Windows.Forms.Label lblDBNomeInstancia;
        private System.Windows.Forms.LinkLabel lnkEntendaBackup;
        private System.Windows.Forms.NotifyIcon notNotificador;
    }
}

