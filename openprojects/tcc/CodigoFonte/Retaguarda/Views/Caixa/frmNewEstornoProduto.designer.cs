namespace FuturaDataTCC.Views.Caixa
{
    partial class frmNewEstornoProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewEstornoProduto));
            this.label1 = new System.Windows.Forms.Label();
            this.tbxNumeroIDProduto = new System.Windows.Forms.TextBox();
            this.pctPFAjudaSubProduto = new System.Windows.Forms.Button();
            this.tbxQuantidade = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxValorEstorno = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEstornoProduto = new System.Windows.Forms.Button();
            this.tbxInformacoesDevolucao = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxNumeroOrcamento = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxCliente = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxValorTotalVenda = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lvwItensOrcamento = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxIDCliente = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbItemVenda = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbProdutoGarantia = new System.Windows.Forms.RadioButton();
            this.rdbProdutoVoltaEstoque = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Insira o ID do Produto ou Venda:";
            // 
            // tbxNumeroIDProduto
            // 
            this.tbxNumeroIDProduto.Location = new System.Drawing.Point(250, 15);
            this.tbxNumeroIDProduto.Name = "tbxNumeroIDProduto";
            this.tbxNumeroIDProduto.Size = new System.Drawing.Size(97, 20);
            this.tbxNumeroIDProduto.TabIndex = 0;
            this.tbxNumeroIDProduto.Leave += new System.EventHandler(this.tbxNumeroIDProduto_Leave);
            // 
            // pctPFAjudaSubProduto
            // 
            this.pctPFAjudaSubProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pctPFAjudaSubProduto.ForeColor = System.Drawing.SystemColors.Window;
            this.pctPFAjudaSubProduto.Image = global::FuturaDataTCC.Properties.Resources.interrogacao;
            this.pctPFAjudaSubProduto.Location = new System.Drawing.Point(348, 14);
            this.pctPFAjudaSubProduto.Name = "pctPFAjudaSubProduto";
            this.pctPFAjudaSubProduto.Size = new System.Drawing.Size(25, 23);
            this.pctPFAjudaSubProduto.TabIndex = 1;
            this.pctPFAjudaSubProduto.UseVisualStyleBackColor = true;
            this.pctPFAjudaSubProduto.Click += new System.EventHandler(this.pctPFAjudaSubProduto_Click);
            // 
            // tbxQuantidade
            // 
            this.tbxQuantidade.Location = new System.Drawing.Point(5, 267);
            this.tbxQuantidade.Name = "tbxQuantidade";
            this.tbxQuantidade.ReadOnly = true;
            this.tbxQuantidade.Size = new System.Drawing.Size(94, 20);
            this.tbxQuantidade.TabIndex = 10;
            this.tbxQuantidade.Leave += new System.EventHandler(this.tbxQuantidade_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 323;
            this.label7.Text = "Qtd (a estornar):";
            // 
            // tbxValorEstorno
            // 
            this.tbxValorEstorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxValorEstorno.Location = new System.Drawing.Point(5, 315);
            this.tbxValorEstorno.Name = "tbxValorEstorno";
            this.tbxValorEstorno.ReadOnly = true;
            this.tbxValorEstorno.Size = new System.Drawing.Size(121, 20);
            this.tbxValorEstorno.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 299);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 13);
            this.label8.TabIndex = 325;
            this.label8.Text = "Valor Total Estorno:";
            // 
            // btnEstornoProduto
            // 
            this.btnEstornoProduto.Image = global::FuturaDataTCC.Properties.Resources.Visita;
            this.btnEstornoProduto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstornoProduto.Location = new System.Drawing.Point(536, 309);
            this.btnEstornoProduto.Name = "btnEstornoProduto";
            this.btnEstornoProduto.Size = new System.Drawing.Size(121, 23);
            this.btnEstornoProduto.TabIndex = 15;
            this.btnEstornoProduto.Text = "Confirmar Estorno";
            this.btnEstornoProduto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEstornoProduto.UseVisualStyleBackColor = true;
            this.btnEstornoProduto.Click += new System.EventHandler(this.btnEstornar_Click);
            // 
            // tbxInformacoesDevolucao
            // 
            this.tbxInformacoesDevolucao.Location = new System.Drawing.Point(102, 267);
            this.tbxInformacoesDevolucao.MaxLength = 150;
            this.tbxInformacoesDevolucao.Name = "tbxInformacoesDevolucao";
            this.tbxInformacoesDevolucao.Size = new System.Drawing.Size(555, 20);
            this.tbxInformacoesDevolucao.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 13);
            this.label9.TabIndex = 328;
            this.label9.Text = "Informações sobre a Devolução:";
            // 
            // tbxNumeroOrcamento
            // 
            this.tbxNumeroOrcamento.Location = new System.Drawing.Point(8, 62);
            this.tbxNumeroOrcamento.Name = "tbxNumeroOrcamento";
            this.tbxNumeroOrcamento.ReadOnly = true;
            this.tbxNumeroOrcamento.Size = new System.Drawing.Size(79, 20);
            this.tbxNumeroOrcamento.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 332;
            this.label10.Text = "Numero Orc:";
            // 
            // tbxCliente
            // 
            this.tbxCliente.Location = new System.Drawing.Point(93, 62);
            this.tbxCliente.Name = "tbxCliente";
            this.tbxCliente.ReadOnly = true;
            this.tbxCliente.Size = new System.Drawing.Size(386, 20);
            this.tbxCliente.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(90, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 334;
            this.label11.Text = "Cliente:";
            // 
            // tbxValorTotalVenda
            // 
            this.tbxValorTotalVenda.Location = new System.Drawing.Point(565, 62);
            this.tbxValorTotalVenda.Name = "tbxValorTotalVenda";
            this.tbxValorTotalVenda.ReadOnly = true;
            this.tbxValorTotalVenda.Size = new System.Drawing.Size(92, 20);
            this.tbxValorTotalVenda.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(562, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 336;
            this.label12.Text = "Valor Total:";
            // 
            // lvwItensOrcamento
            // 
            this.lvwItensOrcamento.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwItensOrcamento.FullRowSelect = true;
            this.lvwItensOrcamento.Location = new System.Drawing.Point(5, 105);
            this.lvwItensOrcamento.Name = "lvwItensOrcamento";
            this.lvwItensOrcamento.Size = new System.Drawing.Size(652, 138);
            this.lvwItensOrcamento.TabIndex = 339;
            this.lvwItensOrcamento.UseCompatibleStateImageBehavior = false;
            this.lvwItensOrcamento.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 13);
            this.label2.TabIndex = 340;
            this.label2.Text = "Itens (produtos) que serão Estornados:";
            // 
            // tbxIDCliente
            // 
            this.tbxIDCliente.Location = new System.Drawing.Point(485, 62);
            this.tbxIDCliente.Name = "tbxIDCliente";
            this.tbxIDCliente.ReadOnly = true;
            this.tbxIDCliente.Size = new System.Drawing.Size(74, 20);
            this.tbxIDCliente.TabIndex = 345;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbItemVenda);
            this.groupBox1.Location = new System.Drawing.Point(379, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 46);
            this.groupBox1.TabIndex = 346;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Pesquisa";
            // 
            // rdbItemVenda
            // 
            this.rdbItemVenda.AutoSize = true;
            this.rdbItemVenda.Checked = true;
            this.rdbItemVenda.Location = new System.Drawing.Point(7, 20);
            this.rdbItemVenda.Name = "rdbItemVenda";
            this.rdbItemVenda.Size = new System.Drawing.Size(93, 17);
            this.rdbItemVenda.TabIndex = 0;
            this.rdbItemVenda.TabStop = true;
            this.rdbItemVenda.Text = "ID Item Venda";
            this.rdbItemVenda.UseVisualStyleBackColor = true;
            this.rdbItemVenda.Click += new System.EventHandler(this.rdbItemVenda_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbProdutoGarantia);
            this.groupBox2.Controls.Add(this.rdbProdutoVoltaEstoque);
            this.groupBox2.Location = new System.Drawing.Point(132, 299);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 37);
            this.groupBox2.TabIndex = 347;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Movimento de Estoque:";
            // 
            // rdbProdutoGarantia
            // 
            this.rdbProdutoGarantia.AutoSize = true;
            this.rdbProdutoGarantia.Location = new System.Drawing.Point(212, 15);
            this.rdbProdutoGarantia.Name = "rdbProdutoGarantia";
            this.rdbProdutoGarantia.Size = new System.Drawing.Size(156, 17);
            this.rdbProdutoGarantia.TabIndex = 1;
            this.rdbProdutoGarantia.Text = "Defeito - Troca Fornecedor.";
            this.rdbProdutoGarantia.UseVisualStyleBackColor = true;
            // 
            // rdbProdutoVoltaEstoque
            // 
            this.rdbProdutoVoltaEstoque.AutoSize = true;
            this.rdbProdutoVoltaEstoque.Checked = true;
            this.rdbProdutoVoltaEstoque.Location = new System.Drawing.Point(11, 15);
            this.rdbProdutoVoltaEstoque.Name = "rdbProdutoVoltaEstoque";
            this.rdbProdutoVoltaEstoque.Size = new System.Drawing.Size(195, 17);
            this.rdbProdutoVoltaEstoque.TabIndex = 0;
            this.rdbProdutoVoltaEstoque.TabStop = true;
            this.rdbProdutoVoltaEstoque.Text = "Devolução - Produto Volta Estoque.";
            this.rdbProdutoVoltaEstoque.UseVisualStyleBackColor = true;
            // 
            // frmNewEstornoProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 341);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbxIDCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvwItensOrcamento);
            this.Controls.Add(this.tbxValorTotalVenda);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbxCliente);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbxNumeroOrcamento);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxInformacoesDevolucao);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnEstornoProduto);
            this.Controls.Add(this.tbxValorEstorno);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxQuantidade);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pctPFAjudaSubProduto);
            this.Controls.Add(this.tbxNumeroIDProduto);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewEstornoProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estorno de Produto ou Pedido Inteiro";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxNumeroIDProduto;
        private System.Windows.Forms.Button pctPFAjudaSubProduto;
        private System.Windows.Forms.TextBox tbxQuantidade;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxValorEstorno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEstornoProduto;
        private System.Windows.Forms.TextBox tbxInformacoesDevolucao;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxNumeroOrcamento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxCliente;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxValorTotalVenda;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.ListView lvwItensOrcamento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxIDCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbItemVenda;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbProdutoGarantia;
        private System.Windows.Forms.RadioButton rdbProdutoVoltaEstoque;
    }
}