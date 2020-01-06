namespace FuturaDataTCC.Iniciar
{
    partial class frmLoginSistema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginSistema));
            this.tbxSenhaUsuario = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxLoginUsuario = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnentrar = new System.Windows.Forms.Button();
            this.lblDataEHoraSist = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.CNetHelpProvider = new System.Windows.Forms.HelpProvider();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblInstanciaBanco1 = new System.Windows.Forms.Label();
            this.lblStatusEmpresa1 = new System.Windows.Forms.Label();
            this.lblServidorEmpresa1 = new System.Windows.Forms.Label();
            this.lblBancoEmpresa1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxSenhaUsuario
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxSenhaUsuario, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_tbxSenhaUsuario");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxSenhaUsuario, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxSenhaUsuario.Location = new System.Drawing.Point(11, 85);
            this.tbxSenhaUsuario.Name = "tbxSenhaUsuario";
            this.tbxSenhaUsuario.PasswordChar = '*';
            this.CNetHelpProvider.SetShowHelp(this.tbxSenhaUsuario, true);
            this.tbxSenhaUsuario.Size = new System.Drawing.Size(132, 20);
            this.tbxSenhaUsuario.TabIndex = 18;
            this.tbxSenhaUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLoginSistema_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label1, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_label1");
            this.CNetHelpProvider.SetHelpNavigator(this.label1, System.Windows.Forms.HelpNavigator.Topic);
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.CNetHelpProvider.SetShowHelp(this.label1, true);
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Login Usuario/ID:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(464, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::FuturaDataTCC.Properties.Resources.Conectados;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(84, 20);
            this.toolStripMenuItem1.Text = "Conexão";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::FuturaDataTCC.Properties.Resources.estoque2;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 20);
            this.toolStripMenuItem2.Text = "Config.Sistema";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.label2, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_label2");
            this.CNetHelpProvider.SetHelpNavigator(this.label2, System.Windows.Forms.HelpNavigator.Topic);
            this.label2.Location = new System.Drawing.Point(9, 67);
            this.label2.Name = "label2";
            this.CNetHelpProvider.SetShowHelp(this.label2, true);
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Senha/Password:";
            // 
            // tbxLoginUsuario
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.tbxLoginUsuario, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_tbxLoginUsuario");
            this.CNetHelpProvider.SetHelpNavigator(this.tbxLoginUsuario, System.Windows.Forms.HelpNavigator.Topic);
            this.tbxLoginUsuario.Location = new System.Drawing.Point(11, 41);
            this.tbxLoginUsuario.Name = "tbxLoginUsuario";
            this.CNetHelpProvider.SetShowHelp(this.tbxLoginUsuario, true);
            this.tbxLoginUsuario.Size = new System.Drawing.Size(132, 20);
            this.tbxLoginUsuario.TabIndex = 17;
            this.tbxLoginUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLoginSistema_KeyPress);
            
            // 
            // statusStrip1
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.statusStrip1, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_statusStrip1");
            this.CNetHelpProvider.SetHelpNavigator(this.statusStrip1, System.Windows.Forms.HelpNavigator.Topic);
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.CNetHelpProvider.SetShowHelp(this.statusStrip1, true);
            this.statusStrip1.Size = new System.Drawing.Size(464, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnentrar);
            this.groupBox1.Controls.Add(this.tbxLoginUsuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxSenhaUsuario);
            this.CNetHelpProvider.SetHelpKeyword(this.groupBox1, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_groupBox1");
            this.CNetHelpProvider.SetHelpNavigator(this.groupBox1, System.Windows.Forms.HelpNavigator.Topic);
            this.groupBox1.Location = new System.Drawing.Point(109, 36);
            this.groupBox1.Name = "groupBox1";
            this.CNetHelpProvider.SetShowHelp(this.groupBox1, true);
            this.groupBox1.Size = new System.Drawing.Size(246, 122);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações de Login";
            // 
            // btnentrar
            // 
            this.btnentrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNetHelpProvider.SetHelpKeyword(this.btnentrar, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_btnentrar");
            this.CNetHelpProvider.SetHelpNavigator(this.btnentrar, System.Windows.Forms.HelpNavigator.Topic);
            this.btnentrar.Image = ((System.Drawing.Image)(resources.GetObject("btnentrar.Image")));
            this.btnentrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnentrar.Location = new System.Drawing.Point(147, 81);
            this.btnentrar.Name = "btnentrar";
            this.CNetHelpProvider.SetShowHelp(this.btnentrar, true);
            this.btnentrar.Size = new System.Drawing.Size(93, 30);
            this.btnentrar.TabIndex = 19;
            this.btnentrar.Text = "Entrar";
            this.btnentrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnentrar.UseVisualStyleBackColor = true;
            this.btnentrar.Click += new System.EventHandler(this.btnentrar_Click_1);
            // 
            // lblDataEHoraSist
            // 
            this.lblDataEHoraSist.AutoSize = true;
            this.CNetHelpProvider.SetHelpKeyword(this.lblDataEHoraSist, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_lblDataEHoraSist");
            this.CNetHelpProvider.SetHelpNavigator(this.lblDataEHoraSist, System.Windows.Forms.HelpNavigator.Topic);
            this.lblDataEHoraSist.Location = new System.Drawing.Point(12, 167);
            this.lblDataEHoraSist.Name = "lblDataEHoraSist";
            this.CNetHelpProvider.SetShowHelp(this.lblDataEHoraSist, true);
            this.lblDataEHoraSist.Size = new System.Drawing.Size(161, 13);
            this.lblDataEHoraSist.TabIndex = 24;
            this.lblDataEHoraSist.Text = "Data/Hora Sistema Operacional:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.CNetHelpProvider.SetHelpKeyword(this.label3, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_label3");
            this.CNetHelpProvider.SetHelpNavigator(this.label3, System.Windows.Forms.HelpNavigator.Topic);
            this.label3.Location = new System.Drawing.Point(12, 183);
            this.label3.Name = "label3";
            this.CNetHelpProvider.SetShowHelp(this.label3, true);
            this.label3.Size = new System.Drawing.Size(215, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Clique aqui caso a Data/Hora esteja errada!";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // CNetHelpProvider
            // 
            this.CNetHelpProvider.HelpNamespace = "FuturaDataCorporateERP.chm";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNetHelpProvider.SetHelpKeyword(this.button1, "FuturaDataCorporateERP_Form_frmLoginSistema.htm#frmLoginSistema_button1");
            this.CNetHelpProvider.SetHelpNavigator(this.button1, System.Windows.Forms.HelpNavigator.Topic);
            this.button1.Image = global::FuturaDataTCC.Properties.Resources.sair2;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(406, 179);
            this.button1.Name = "button1";
            this.CNetHelpProvider.SetShowHelp(this.button1, true);
            this.button1.Size = new System.Drawing.Size(54, 21);
            this.button1.TabIndex = 54;
            this.button1.Text = "Sair";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(242, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Primeiro Acesso - Senha Padrão.";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblInstanciaBanco1);
            this.groupBox2.Controls.Add(this.lblStatusEmpresa1);
            this.groupBox2.Controls.Add(this.lblServidorEmpresa1);
            this.groupBox2.Controls.Add(this.lblBancoEmpresa1);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(3, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 89);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conectado a:";
            // 
            // lblInstanciaBanco1
            // 
            this.lblInstanciaBanco1.AutoSize = true;
            this.lblInstanciaBanco1.Location = new System.Drawing.Point(85, 35);
            this.lblInstanciaBanco1.Name = "lblInstanciaBanco1";
            this.lblInstanciaBanco1.Size = new System.Drawing.Size(53, 13);
            this.lblInstanciaBanco1.TabIndex = 13;
            this.lblInstanciaBanco1.Text = "Instância:";
            // 
            // lblStatusEmpresa1
            // 
            this.lblStatusEmpresa1.AutoSize = true;
            this.lblStatusEmpresa1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusEmpresa1.Location = new System.Drawing.Point(85, 70);
            this.lblStatusEmpresa1.Name = "lblStatusEmpresa1";
            this.lblStatusEmpresa1.Size = new System.Drawing.Size(47, 13);
            this.lblStatusEmpresa1.TabIndex = 12;
            this.lblStatusEmpresa1.Text = "Status:";
            // 
            // lblServidorEmpresa1
            // 
            this.lblServidorEmpresa1.AutoSize = true;
            this.lblServidorEmpresa1.Location = new System.Drawing.Point(85, 18);
            this.lblServidorEmpresa1.Name = "lblServidorEmpresa1";
            this.lblServidorEmpresa1.Size = new System.Drawing.Size(49, 13);
            this.lblServidorEmpresa1.TabIndex = 9;
            this.lblServidorEmpresa1.Text = "Servidor:";
            // 
            // lblBancoEmpresa1
            // 
            this.lblBancoEmpresa1.AutoSize = true;
            this.lblBancoEmpresa1.Location = new System.Drawing.Point(85, 52);
            this.lblBancoEmpresa1.Name = "lblBancoEmpresa1";
            this.lblBancoEmpresa1.Size = new System.Drawing.Size(41, 13);
            this.lblBancoEmpresa1.TabIndex = 10;
            this.lblBancoEmpresa1.Text = "Banco:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FuturaDataTCC.Properties.Resources.conexaoAtivaPequena;
            this.pictureBox1.Location = new System.Drawing.Point(6, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 71);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmLoginSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 318);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDataEHoraSist);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.CNetHelpProvider.SetHelpKeyword(this, "FuturaDataCorporateERP_Form_frmLoginSistema.htm");
            this.CNetHelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginSistema";
            this.CNetHelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login no Sistema FuturaData Business v.2014 SE";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLoginSistema_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnentrar;
        private System.Windows.Forms.MaskedTextBox tbxSenhaUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxLoginUsuario;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDataEHoraSist;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.HelpProvider CNetHelpProvider;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblInstanciaBanco1;
        private System.Windows.Forms.Label lblStatusEmpresa1;
        private System.Windows.Forms.Label lblServidorEmpresa1;
        private System.Windows.Forms.Label lblBancoEmpresa1;
    }
}