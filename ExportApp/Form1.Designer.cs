namespace ExportApp
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
            this.components = new System.ComponentModel.Container();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectContactBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmdContacts = new System.Windows.Forms.Button();
            this.cmdDocuments = new System.Windows.Forms.Button();
            this.cmdJobSite = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmdUpdateParent = new System.Windows.Forms.Button();
            this.cmdDocRev = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectContactBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(583, 4);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(130, 23);
            this.cmdConnect.TabIndex = 0;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.projectContactBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(22, 184);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(691, 383);
            this.dataGridView1.TabIndex = 1;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 500;
            // 
            // projectContactBindingSource
            // 
            this.projectContactBindingSource.DataSource = typeof(ExportApp.ProjectContact);
            // 
            // cmdContacts
            // 
            this.cmdContacts.Location = new System.Drawing.Point(48, 35);
            this.cmdContacts.Name = "cmdContacts";
            this.cmdContacts.Size = new System.Drawing.Size(130, 23);
            this.cmdContacts.TabIndex = 2;
            this.cmdContacts.Text = "Contacts CSV";
            this.cmdContacts.UseVisualStyleBackColor = true;
            this.cmdContacts.Click += new System.EventHandler(this.cmdContacts_Click);
            // 
            // cmdDocuments
            // 
            this.cmdDocuments.Location = new System.Drawing.Point(435, 55);
            this.cmdDocuments.Name = "cmdDocuments";
            this.cmdDocuments.Size = new System.Drawing.Size(130, 23);
            this.cmdDocuments.TabIndex = 3;
            this.cmdDocuments.Text = "Documents CSV";
            this.cmdDocuments.UseVisualStyleBackColor = true;
            this.cmdDocuments.Click += new System.EventHandler(this.cmdDocuments_Click);
            // 
            // cmdJobSite
            // 
            this.cmdJobSite.Location = new System.Drawing.Point(48, 11);
            this.cmdJobSite.Name = "cmdJobSite";
            this.cmdJobSite.Size = new System.Drawing.Size(130, 23);
            this.cmdJobSite.TabIndex = 4;
            this.cmdJobSite.Text = "Job Sites CSV";
            this.cmdJobSite.UseVisualStyleBackColor = true;
            this.cmdJobSite.Click += new System.EventHandler(this.cmdJobSite_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(219, 42);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(144, 23);
            this.cmdUpdate.TabIndex = 5;
            this.cmdUpdate.Text = "Update Revision";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(35, 126);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(531, 23);
            this.pgBar.TabIndex = 6;
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Location = new System.Drawing.Point(32, 152);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(0, 13);
            this.lblStatusText.TabIndex = 7;
            // 
            // customerBindingSource
            // 
            this.customerBindingSource.DataSource = typeof(ExportApp.Customer);
            // 
            // cmdUpdateParent
            // 
            this.cmdUpdateParent.Location = new System.Drawing.Point(219, 71);
            this.cmdUpdateParent.Name = "cmdUpdateParent";
            this.cmdUpdateParent.Size = new System.Drawing.Size(144, 23);
            this.cmdUpdateParent.TabIndex = 8;
            this.cmdUpdateParent.Text = "Update Parent";
            this.cmdUpdateParent.UseVisualStyleBackColor = true;
            this.cmdUpdateParent.Click += new System.EventHandler(this.cmdUpdateParent_Click);
            // 
            // cmdDocRev
            // 
            this.cmdDocRev.Location = new System.Drawing.Point(435, 84);
            this.cmdDocRev.Name = "cmdDocRev";
            this.cmdDocRev.Size = new System.Drawing.Size(162, 23);
            this.cmdDocRev.TabIndex = 9;
            this.cmdDocRev.Text = "Update Document Revision";
            this.cmdDocRev.UseVisualStyleBackColor = true;
            this.cmdDocRev.Click += new System.EventHandler(this.cmdDocRev_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 579);
            this.Controls.Add(this.cmdDocRev);
            this.Controls.Add(this.cmdUpdateParent);
            this.Controls.Add(this.lblStatusText);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdJobSite);
            this.Controls.Add(this.cmdDocuments);
            this.Controls.Add(this.cmdContacts);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmdConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectContactBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource customerBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource projectContactBindingSource;
        private System.Windows.Forms.Button cmdContacts;
        private System.Windows.Forms.Button cmdDocuments;
        private System.Windows.Forms.Button cmdJobSite;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Button cmdUpdateParent;
        private System.Windows.Forms.Button cmdDocRev;
    }
}

