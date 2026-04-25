namespace Dự_án_Quản_lí_thuốc.NET.User_control.UC_DuocSi
{
    partial class UC_ThemThuoc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbTongLuongThuoc = new System.Windows.Forms.Label();
            this.lbTongThuoc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTongLuongThuoc
            // 
            this.lbTongLuongThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTongLuongThuoc.Location = new System.Drawing.Point(181, 31);
            this.lbTongLuongThuoc.Name = "lbTongLuongThuoc";
            this.lbTongLuongThuoc.Size = new System.Drawing.Size(156, 31);
            this.lbTongLuongThuoc.TabIndex = 2;
            this.lbTongLuongThuoc.Text = "0";
            this.lbTongLuongThuoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTongThuoc
            // 
            this.lbTongThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTongThuoc.Location = new System.Drawing.Point(181, 0);
            this.lbTongThuoc.Name = "lbTongThuoc";
            this.lbTongThuoc.Size = new System.Drawing.Size(156, 31);
            this.lbTongThuoc.TabIndex = 2;
            this.lbTongThuoc.Text = "0";
            this.lbTongThuoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tổng số lượng thuốc:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tổng số thuốc:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.35294F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.64706F));
            this.tableLayoutPanel1.Controls.Add(this.lbTongLuongThuoc, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTongThuoc, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 62);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // UC_ThemThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "UC_ThemThuoc";
            this.Size = new System.Drawing.Size(340, 62);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbTongLuongThuoc;
        private System.Windows.Forms.Label lbTongThuoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
