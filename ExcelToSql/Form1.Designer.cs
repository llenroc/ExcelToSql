namespace ExcelToSql
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtbSourceFile = new System.Windows.Forms.TextBox();
            this.cbIsRowType = new System.Windows.Forms.CheckBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnImportData = new System.Windows.Forms.Button();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.ProjectName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtbSourceFile
            // 
            this.txtbSourceFile.Location = new System.Drawing.Point(44, 24);
            this.txtbSourceFile.Name = "txtbSourceFile";
            this.txtbSourceFile.Size = new System.Drawing.Size(440, 22);
            this.txtbSourceFile.TabIndex = 0;
            // 
            // cbIsRowType
            // 
            this.cbIsRowType.AutoSize = true;
            this.cbIsRowType.Location = new System.Drawing.Point(383, 67);
            this.cbIsRowType.Name = "cbIsRowType";
            this.cbIsRowType.Size = new System.Drawing.Size(101, 16);
            this.cbIsRowType.TabIndex = 1;
            this.cbIsRowType.Text = "TypeGuessRows";
            this.cbIsRowType.UseVisualStyleBackColor = true;
            this.cbIsRowType.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(512, 22);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "開啟資料";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnImportData
            // 
            this.btnImportData.Location = new System.Drawing.Point(512, 63);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(75, 23);
            this.btnImportData.TabIndex = 3;
            this.btnImportData.Text = "匯入資料";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(116, 58);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(100, 22);
            this.txtProjectName.TabIndex = 4;
            // 
            // ProjectName
            // 
            this.ProjectName.AutoSize = true;
            this.ProjectName.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ProjectName.Location = new System.Drawing.Point(44, 61);
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Size = new System.Drawing.Size(67, 15);
            this.ProjectName.TabIndex = 5;
            this.ProjectName.Text = "專案名稱";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 125);
            this.Controls.Add(this.ProjectName);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.btnImportData);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.cbIsRowType);
            this.Controls.Add(this.txtbSourceFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbSourceFile;
        private System.Windows.Forms.CheckBox cbIsRowType;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label ProjectName;
    }
}

