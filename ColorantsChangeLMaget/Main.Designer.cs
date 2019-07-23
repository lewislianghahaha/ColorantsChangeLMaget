namespace ColorantsChangeLMaget
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbandname = new System.Windows.Forms.TextBox();
            this.txtprodid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(15, 161);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(66, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "运算";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "品牌参数:";
            // 
            // txtbandname
            // 
            this.txtbandname.Location = new System.Drawing.Point(72, 42);
            this.txtbandname.Name = "txtbandname";
            this.txtbandname.Size = new System.Drawing.Size(100, 21);
            this.txtbandname.TabIndex = 2;
            // 
            // txtprodid
            // 
            this.txtprodid.Location = new System.Drawing.Point(102, 69);
            this.txtprodid.Name = "txtprodid";
            this.txtprodid.Size = new System.Drawing.Size(70, 21);
            this.txtprodid.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "产品系列参数:";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(125, 161);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(66, 23);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "关闭";
            this.btnclose.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 196);
            this.ControlBox = false;
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.txtprodid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbandname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Main";
            this.Text = "色母量转换合并导出";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbandname;
        private System.Windows.Forms.TextBox txtprodid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnclose;
    }
}

