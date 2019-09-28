namespace FuturaDataTCC.Relatorios.Orcamento
{
    partial class frmImpressaoRelPedido
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// LiteErp up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpressaoRelPedido));
            this.rpwImpressaoRelatorio = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpwImpressaoRelatorio
            // 
            this.rpwImpressaoRelatorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rpwImpressaoRelatorio.Location = new System.Drawing.Point(0, 0);
            this.rpwImpressaoRelatorio.Name = "rpwImpressaoRelatorio";
            this.rpwImpressaoRelatorio.ShowBackButton = false;
            this.rpwImpressaoRelatorio.ShowDocumentMapButton = false;
            this.rpwImpressaoRelatorio.ShowFindControls = false;
            this.rpwImpressaoRelatorio.ShowRefreshButton = false;
            this.rpwImpressaoRelatorio.ShowStopButton = false;
            this.rpwImpressaoRelatorio.Size = new System.Drawing.Size(997, 590);
            this.rpwImpressaoRelatorio.TabIndex = 2;
            // 
            // frmImpressaoRelPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 591);
            this.Controls.Add(this.rpwImpressaoRelatorio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImpressaoRelPedido";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Pedido";
            this.Load += new System.EventHandler(this.frmImpressaoRelPedido_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpwImpressaoRelatorio;
    }
}