namespace LPS_SimpleLibrary
{
    partial class LoanUserControl
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
            this.lab = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLoanList = new System.Windows.Forms.TabPage();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelDataRow = new System.Windows.Forms.Label();
            this.dataGridViewLoan = new System.Windows.Forms.DataGridView();
            this.tabPageLoanDetail = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.dateTimeBookIssue = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBooks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxMembers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageLoanList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLoan)).BeginInit();
            this.tabPageLoanDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab
            // 
            this.lab.AutoSize = true;
            this.lab.Location = new System.Drawing.Point(6, 11);
            this.lab.Name = "lab";
            this.lab.Size = new System.Drawing.Size(64, 20);
            this.lab.TabIndex = 2;
            this.lab.Text = "Search:";
            this.lab.Click += new System.EventHandler(this.lab_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.AcceptsTab = true;
            this.textBoxSearch.Location = new System.Drawing.Point(10, 35);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(335, 26);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLoanList);
            this.tabControl1.Controls.Add(this.tabPageLoanDetail);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 393);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageLoanList
            // 
            this.tabPageLoanList.Controls.Add(this.buttonDelete);
            this.tabPageLoanList.Controls.Add(this.buttonEdit);
            this.tabPageLoanList.Controls.Add(this.buttonAdd);
            this.tabPageLoanList.Controls.Add(this.labelDataRow);
            this.tabPageLoanList.Controls.Add(this.dataGridViewLoan);
            this.tabPageLoanList.Controls.Add(this.textBoxSearch);
            this.tabPageLoanList.Controls.Add(this.lab);
            this.tabPageLoanList.Location = new System.Drawing.Point(4, 29);
            this.tabPageLoanList.Name = "tabPageLoanList";
            this.tabPageLoanList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLoanList.Size = new System.Drawing.Size(742, 360);
            this.tabPageLoanList.TabIndex = 0;
            this.tabPageLoanList.Text = "Loan List";
            this.tabPageLoanList.UseVisualStyleBackColor = true;
            this.tabPageLoanList.Click += new System.EventHandler(this.tabPageLoanList_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(644, 205);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(86, 47);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Visible = false;
            this.buttonEdit.Location = new System.Drawing.Point(644, 141);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(86, 47);
            this.buttonEdit.TabIndex = 7;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(644, 79);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(86, 47);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // labelDataRow
            // 
            this.labelDataRow.AutoSize = true;
            this.labelDataRow.Location = new System.Drawing.Point(6, 332);
            this.labelDataRow.Name = "labelDataRow";
            this.labelDataRow.Size = new System.Drawing.Size(51, 20);
            this.labelDataRow.TabIndex = 5;
            this.labelDataRow.Text = "label1";
            this.labelDataRow.Click += new System.EventHandler(this.labelDataRow_Click);
            // 
            // dataGridViewLoan
            // 
            this.dataGridViewLoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLoan.Location = new System.Drawing.Point(10, 79);
            this.dataGridViewLoan.Name = "dataGridViewLoan";
            this.dataGridViewLoan.RowHeadersWidth = 62;
            this.dataGridViewLoan.RowTemplate.Height = 28;
            this.dataGridViewLoan.Size = new System.Drawing.Size(621, 240);
            this.dataGridViewLoan.TabIndex = 4;
            this.dataGridViewLoan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLoan_CellContentClick);
            // 
            // tabPageLoanDetail
            // 
            this.tabPageLoanDetail.Controls.Add(this.buttonCancel);
            this.tabPageLoanDetail.Controls.Add(this.buttonSave);
            this.tabPageLoanDetail.Controls.Add(this.dateTimeBookIssue);
            this.tabPageLoanDetail.Controls.Add(this.label3);
            this.tabPageLoanDetail.Controls.Add(this.comboBoxBooks);
            this.tabPageLoanDetail.Controls.Add(this.label2);
            this.tabPageLoanDetail.Controls.Add(this.comboBoxMembers);
            this.tabPageLoanDetail.Controls.Add(this.label1);
            this.tabPageLoanDetail.Location = new System.Drawing.Point(4, 29);
            this.tabPageLoanDetail.Name = "tabPageLoanDetail";
            this.tabPageLoanDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLoanDetail.Size = new System.Drawing.Size(742, 360);
            this.tabPageLoanDetail.TabIndex = 1;
            this.tabPageLoanDetail.Text = "Loan Details";
            this.tabPageLoanDetail.UseVisualStyleBackColor = true;
            this.tabPageLoanDetail.Click += new System.EventHandler(this.tabPageLoanDetail_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(154, 271);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(102, 44);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // dateTimeBookIssue
            // 
            this.dateTimeBookIssue.Location = new System.Drawing.Point(295, 161);
            this.dateTimeBookIssue.Name = "dateTimeBookIssue";
            this.dateTimeBookIssue.Size = new System.Drawing.Size(289, 26);
            this.dateTimeBookIssue.TabIndex = 5;
            this.dateTimeBookIssue.ValueChanged += new System.EventHandler(this.dateTimeBookIssue_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Book Issue:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboBoxBooks
            // 
            this.comboBoxBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBooks.FormattingEnabled = true;
            this.comboBoxBooks.Location = new System.Drawing.Point(21, 161);
            this.comboBoxBooks.Name = "comboBoxBooks";
            this.comboBoxBooks.Size = new System.Drawing.Size(235, 30);
            this.comboBoxBooks.TabIndex = 3;
            this.comboBoxBooks.SelectedIndexChanged += new System.EventHandler(this.comboBoxBooks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Book Issue:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBoxMembers
            // 
            this.comboBoxMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMembers.FormattingEnabled = true;
            this.comboBoxMembers.Location = new System.Drawing.Point(21, 76);
            this.comboBoxMembers.Name = "comboBoxMembers";
            this.comboBoxMembers.Size = new System.Drawing.Size(183, 30);
            this.comboBoxMembers.TabIndex = 1;
            this.comboBoxMembers.SelectedIndexChanged += new System.EventHandler(this.comboBoxMembers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Member:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(21, 271);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 44);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LoanUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "LoanUserControl";
            this.Size = new System.Drawing.Size(750, 393);
            this.tabControl1.ResumeLayout(false);
            this.tabPageLoanList.ResumeLayout(false);
            this.tabPageLoanList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLoan)).EndInit();
            this.tabPageLoanDetail.ResumeLayout(false);
            this.tabPageLoanDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lab;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLoanList;
        private System.Windows.Forms.TabPage tabPageLoanDetail;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelDataRow;
        private System.Windows.Forms.DataGridView dataGridViewLoan;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DateTimePicker dateTimeBookIssue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxBooks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMembers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
    }
}
