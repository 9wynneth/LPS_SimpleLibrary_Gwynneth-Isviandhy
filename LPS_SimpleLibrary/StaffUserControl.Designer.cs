namespace LPS_SimpleLibrary
{
    partial class StaffUserControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelDataRow = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.dataGridViewStaff = new System.Windows.Forms.DataGridView();
            this.tabPageStaffDetail = new System.Windows.Forms.TabPage();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageStaffList = new System.Windows.Forms.TabPage();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.lab = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaff)).BeginInit();
            this.tabPageStaffDetail.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageStaffList.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Staff Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
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
            // dataGridViewStaff
            // 
            this.dataGridViewStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStaff.Location = new System.Drawing.Point(10, 79);
            this.dataGridViewStaff.Name = "dataGridViewStaff";
            this.dataGridViewStaff.RowHeadersWidth = 62;
            this.dataGridViewStaff.RowTemplate.Height = 28;
            this.dataGridViewStaff.Size = new System.Drawing.Size(621, 240);
            this.dataGridViewStaff.TabIndex = 4;
            this.dataGridViewStaff.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStaff_CellClick);
            this.dataGridViewStaff.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStaff_CellContentClick);
            // 
            // tabPageStaffDetail
            // 
            this.tabPageStaffDetail.Controls.Add(this.textBoxPassword);
            this.tabPageStaffDetail.Controls.Add(this.textBoxName);
            this.tabPageStaffDetail.Controls.Add(this.buttonCancel);
            this.tabPageStaffDetail.Controls.Add(this.buttonSave);
            this.tabPageStaffDetail.Controls.Add(this.label2);
            this.tabPageStaffDetail.Controls.Add(this.label1);
            this.tabPageStaffDetail.Location = new System.Drawing.Point(4, 29);
            this.tabPageStaffDetail.Name = "tabPageStaffDetail";
            this.tabPageStaffDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaffDetail.Size = new System.Drawing.Size(742, 360);
            this.tabPageStaffDetail.TabIndex = 1;
            this.tabPageStaffDetail.Text = "Staff Details";
            this.tabPageStaffDetail.UseVisualStyleBackColor = true;
            this.tabPageStaffDetail.Click += new System.EventHandler(this.tabPageStaffDetail_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(21, 167);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(235, 28);
            this.textBoxPassword.TabIndex = 9;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(21, 79);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(235, 28);
            this.textBoxName.TabIndex = 8;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageStaffList);
            this.tabControl1.Controls.Add(this.tabPageStaffDetail);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 393);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageStaffList
            // 
            this.tabPageStaffList.Controls.Add(this.buttonDelete);
            this.tabPageStaffList.Controls.Add(this.buttonEdit);
            this.tabPageStaffList.Controls.Add(this.buttonAdd);
            this.tabPageStaffList.Controls.Add(this.labelDataRow);
            this.tabPageStaffList.Controls.Add(this.dataGridViewStaff);
            this.tabPageStaffList.Controls.Add(this.textBoxSearch);
            this.tabPageStaffList.Controls.Add(this.lab);
            this.tabPageStaffList.Location = new System.Drawing.Point(4, 29);
            this.tabPageStaffList.Name = "tabPageStaffList";
            this.tabPageStaffList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaffList.Size = new System.Drawing.Size(742, 360);
            this.tabPageStaffList.TabIndex = 0;
            this.tabPageStaffList.Text = "Staff List";
            this.tabPageStaffList.UseVisualStyleBackColor = true;
            this.tabPageStaffList.Click += new System.EventHandler(this.tabPageStaffList_Click);
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
            // StaffUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "StaffUserControl";
            this.Size = new System.Drawing.Size(750, 393);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaff)).EndInit();
            this.tabPageStaffDetail.ResumeLayout(false);
            this.tabPageStaffDetail.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageStaffList.ResumeLayout(false);
            this.tabPageStaffList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelDataRow;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DataGridView dataGridViewStaff;
        private System.Windows.Forms.TabPage tabPageStaffDetail;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageStaffList;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label lab;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxName;
    }
}
