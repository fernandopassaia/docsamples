namespace WinAsyncAwaitSample
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxReturnOfAsync1 = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.rdbRunAsyncronous = new System.Windows.Forms.RadioButton();
            this.rdbRunSyncronous = new System.Windows.Forms.RadioButton();
            this.btnGo2 = new System.Windows.Forms.Button();
            this.tbxReturnOfAsync2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbRunSyncronous2 = new System.Windows.Forms.RadioButton();
            this.rdbRunAsyncronous2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Result / Operation:";
            // 
            // tbxReturnOfAsync1
            // 
            this.tbxReturnOfAsync1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxReturnOfAsync1.Location = new System.Drawing.Point(151, 26);
            this.tbxReturnOfAsync1.Multiline = true;
            this.tbxReturnOfAsync1.Name = "tbxReturnOfAsync1";
            this.tbxReturnOfAsync1.Size = new System.Drawing.Size(503, 23);
            this.tbxReturnOfAsync1.TabIndex = 1;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(488, 55);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(166, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "GO First Thread (10s)!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // rdbRunAsyncronous
            // 
            this.rdbRunAsyncronous.AutoSize = true;
            this.rdbRunAsyncronous.Checked = true;
            this.rdbRunAsyncronous.Location = new System.Drawing.Point(178, 58);
            this.rdbRunAsyncronous.Name = "rdbRunAsyncronous";
            this.rdbRunAsyncronous.Size = new System.Drawing.Size(194, 17);
            this.rdbRunAsyncronous.TabIndex = 4;
            this.rdbRunAsyncronous.TabStop = true;
            this.rdbRunAsyncronous.Text = "Runs Asyncronous (Thread-Parallel)";
            this.rdbRunAsyncronous.UseVisualStyleBackColor = true;
            // 
            // rdbRunSyncronous
            // 
            this.rdbRunSyncronous.AutoSize = true;
            this.rdbRunSyncronous.Location = new System.Drawing.Point(378, 58);
            this.rdbRunSyncronous.Name = "rdbRunSyncronous";
            this.rdbRunSyncronous.Size = new System.Drawing.Size(104, 17);
            this.rdbRunSyncronous.TabIndex = 5;
            this.rdbRunSyncronous.Text = "Run Syncronous";
            this.rdbRunSyncronous.UseVisualStyleBackColor = true;
            // 
            // btnGo2
            // 
            this.btnGo2.Location = new System.Drawing.Point(488, 57);
            this.btnGo2.Name = "btnGo2";
            this.btnGo2.Size = new System.Drawing.Size(166, 23);
            this.btnGo2.TabIndex = 6;
            this.btnGo2.Text = "GO Second Thread FOR (15s)!";
            this.btnGo2.UseVisualStyleBackColor = true;
            this.btnGo2.Click += new System.EventHandler(this.btnGo2_Click);
            // 
            // tbxReturnOfAsync2
            // 
            this.tbxReturnOfAsync2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxReturnOfAsync2.Location = new System.Drawing.Point(151, 28);
            this.tbxReturnOfAsync2.Multiline = true;
            this.tbxReturnOfAsync2.Name = "tbxReturnOfAsync2";
            this.tbxReturnOfAsync2.Size = new System.Drawing.Size(503, 23);
            this.tbxReturnOfAsync2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Result / Operation:";
            // 
            // rdbRunSyncronous2
            // 
            this.rdbRunSyncronous2.AutoSize = true;
            this.rdbRunSyncronous2.Location = new System.Drawing.Point(378, 60);
            this.rdbRunSyncronous2.Name = "rdbRunSyncronous2";
            this.rdbRunSyncronous2.Size = new System.Drawing.Size(104, 17);
            this.rdbRunSyncronous2.TabIndex = 10;
            this.rdbRunSyncronous2.Text = "Run Syncronous";
            this.rdbRunSyncronous2.UseVisualStyleBackColor = true;
            // 
            // rdbRunAsyncronous2
            // 
            this.rdbRunAsyncronous2.AutoSize = true;
            this.rdbRunAsyncronous2.Checked = true;
            this.rdbRunAsyncronous2.Location = new System.Drawing.Point(178, 60);
            this.rdbRunAsyncronous2.Name = "rdbRunAsyncronous2";
            this.rdbRunAsyncronous2.Size = new System.Drawing.Size(194, 17);
            this.rdbRunAsyncronous2.TabIndex = 9;
            this.rdbRunAsyncronous2.TabStop = true;
            this.rdbRunAsyncronous2.Text = "Runs Asyncronous (Thread-Parallel)";
            this.rdbRunAsyncronous2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxReturnOfAsync1);
            this.groupBox1.Controls.Add(this.rdbRunAsyncronous);
            this.groupBox1.Controls.Add(this.rdbRunSyncronous);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 94);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First Process (Take 10seconds on a Delay):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxReturnOfAsync2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rdbRunSyncronous2);
            this.groupBox2.Controls.Add(this.rdbRunAsyncronous2);
            this.groupBox2.Controls.Add(this.btnGo2);
            this.groupBox2.Location = new System.Drawing.Point(12, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(665, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Second Process (a FOR in a 5 million ints, takes aprox.5s):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 236);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxReturnOfAsync1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.RadioButton rdbRunAsyncronous;
        private System.Windows.Forms.RadioButton rdbRunSyncronous;
        private System.Windows.Forms.Button btnGo2;
        private System.Windows.Forms.TextBox tbxReturnOfAsync2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbRunSyncronous2;
        private System.Windows.Forms.RadioButton rdbRunAsyncronous2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

