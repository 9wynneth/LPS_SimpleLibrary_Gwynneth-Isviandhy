using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary
{
    public partial class MemberUserControl : UserControl
    {
        private bool isEditMode = false;
        private string currentMemberId;
        public MemberUserControl()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPageMemberDetail);
            dataGridViewMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            LoadMemberData();
        }

        private void LoadMemberData()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = "select id_member, nama_member, email_member from `member` where delete_member = 0;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                DataTable memberTable = new DataTable();
                connection.Open();
                adapter.Fill(memberTable);
                if (memberTable.Rows.Count == 0)
                {
                    dataGridViewMember.DataSource = null;
                    dataGridViewMember.Rows.Clear();
                    dataGridViewMember.Columns.Clear();
                    dataGridViewMember.Columns.Add("Message", "");
                    dataGridViewMember.Rows.Add("No records found.");
                    dataGridViewMember.ClearSelection();
                    Console.WriteLine("00000");
                    labelDataRow.Text = $"Showing {memberTable.Rows.Count.ToString()} rows";

                }
                else
                {
                    dataGridViewMember.DataSource = memberTable;

                    dataGridViewMember.Columns["id_member"].HeaderText = "Member ID";
                    dataGridViewMember.Columns["nama_member"].HeaderText = "Name";
                    dataGridViewMember.Columns["email_member"].HeaderText = "Email";
                    //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                    labelDataRow.Text = $"Showing {memberTable.Rows.Count.ToString()} rows";

                }
                //dataGridViewMember.Columns["id_member"].Visible = false;
                //dataGridViewMember.DataSource = memberTable;
                //labelDataRow.Text = $"Showing {memberTable.Rows.Count.ToString()} rows";
            }

            

        }
        private void InsertNewMemberRecord()
        {
            var newMember = new MemberClass
            {
                Name = textBoxName.Text,
                Email = textBoxEmail.Text
            };

            newMember.Insert();
            LoadMemberData();
            tabControl1.TabPages.Remove(tabPageMemberDetail);
        }

        private void UpdateMemberData(string idMember)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = "UPDATE `member` SET nama_member = @nama, email_member = @pass " +
                           "WHERE id_member = @idMember;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@pass", textBoxEmail.Text);
                command.Parameters.AddWithValue("@idMember", idMember);  // idMember is treated as a string

                connection.Open();
                command.ExecuteNonQuery();
            }

            LoadMemberData();
            tabControl1.TabPages.Remove(tabPageMemberDetail);
        }

        private void DeleteMemberRecord(string idMember)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            if (MessageBox.Show("Are you sure you want to delete this member record?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string queryDelete = @" 
                               UPDATE `member` 
                            SET delete_member = 1 
                            WHERE id_member = @idMember;";

                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand(queryDelete, connection))
                {
                    command.Parameters.AddWithValue("@idMember", idMember);  // idMember is treated as a string

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                LoadMemberData();
            }
        }


        private void SearchDataGridView(string name, string email)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"select * from `member`       
                            where delete_member = 0 and (nama_member like @name or email_member like @pass)  ;";

            DataTable dataTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {

                command.Parameters.AddWithValue("@name", "%" + name + "%");
                command.Parameters.AddWithValue("@pass", "%" + email + "%");



                connection.Open();
                adapter.Fill(dataTable);

            }

            if (dataTable.Rows.Count == 0)
            {
                dataGridViewMember.DataSource = null;
                dataGridViewMember.Rows.Clear();
                dataGridViewMember.Columns.Clear();
                dataGridViewMember.Columns.Add("Message", "");
                dataGridViewMember.Rows.Add("No records found.");
                dataGridViewMember.ClearSelection();
                Console.WriteLine("00000");
                labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

            }
            else
            {
                dataGridViewMember.DataSource = dataTable;

                dataGridViewMember.Columns["id_member"].HeaderText = "Member ID";
                dataGridViewMember.Columns["nama_member"].HeaderText = "Name";
                dataGridViewMember.Columns["email_member"].HeaderText = "Email";
                //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

            }

        }

        private bool CheckForDuplicates(string name, string email)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT COUNT(*) 
                     FROM `member` 
                     WHERE nama_member = @name AND email_member = @pass AND delete_member = 0";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", email);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Return true if duplicates exist
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false; // Set to Add mode
            tabControl1.TabPages.Remove(tabPageMemberList);
            tabControl1.TabPages.Add(tabPageMemberDetail);

            textBoxName.Text = "";
            textBoxEmail.Text = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPageMemberDetail);
            tabControl1.TabPages.Add(tabPageMemberList);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            // Check if both fields are filled
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Both Name and Email fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check for duplicates
            if (CheckForDuplicates(name, email))
            {
                MessageBox.Show("A member with the same Name and Email already exists.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isEditMode)
            {
                InsertNewMemberRecord();
            }
            else
            {
                UpdateMemberData(currentMemberId);
            }

            tabControl1.TabPages.Remove(tabPageMemberDetail);
            tabControl1.TabPages.Add(tabPageMemberList);
        }

        private void dataGridViewMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                isEditMode = true;

                currentMemberId = dataGridViewMember.Rows[e.RowIndex].Cells["id_member"].Value.ToString();
                textBoxName.Text = dataGridViewMember.Rows[e.RowIndex].Cells["nama_member"].Value.ToString();
                textBoxEmail.Text = dataGridViewMember.Rows[e.RowIndex].Cells["email_member"].Value.ToString();

                tabControl1.TabPages.Remove(tabPageMemberList);
                tabControl1.TabPages.Add(tabPageMemberDetail);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteMemberRecord(currentMemberId);

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;
            SearchDataGridView(searchValue, searchValue);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            isEditMode = true; // Set to Edit mode
            tabControl1.TabPages.Remove(tabPageMemberList);
            tabControl1.TabPages.Add(tabPageMemberDetail);

            if (dataGridViewMember.SelectedCells.Count > 0)
            {
                var selectedCell = dataGridViewMember.SelectedCells[0];
                var selectedRow = selectedCell.OwningRow; 

                currentMemberId = selectedRow.Cells["id_member"].Value.ToString();  

                textBoxName.Text = selectedRow.Cells["nama_member"].Value.ToString();
                textBoxEmail.Text = selectedRow.Cells["email_member"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Please select a cell to edit.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPageMemberList_Click(object sender, EventArgs e)
        {

        }

        private void labelDataRow_Click(object sender, EventArgs e)
        {

        }

        private void tabPageMemberDetail_Click(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lab_Click(object sender, EventArgs e)
        {

        }
    }
}
