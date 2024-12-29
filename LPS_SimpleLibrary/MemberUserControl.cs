using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                dataGridViewMember.DataSource = memberTable;
                //dataGridViewMember.Columns["id_member"].Visible = false;

                labelDataRow.Text = $"Showing {memberTable.Rows.Count.ToString()} rows";
            }

        }
        private void InsertNewMemberRecord()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"INSERT INTO `member` (nama_member, email_member) 
                            VALUES (@nama, @email);
                             ";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@email", textBoxEmail.Text);

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Reload loan data
            LoadMemberData();
            tabControl1.TabPages.Remove(tabPageMemberDetail);
        }

        private void UpdateMemberData(string idMember)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = "UPDATE `member` SET nama_member = @nama, email_member = @pass " +
                "           WHERE id_member = @idMember;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@pass", textBoxEmail.Text);
                command.Parameters.AddWithValue("@idMember", idMember);

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
                                    WHERE id_member = @idMember      ";

                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand(queryDelete, connection))
                {
                    command.Parameters.AddWithValue("@idMember", idMember);

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

            dataGridViewMember.DataSource = dataTable;
            labelDataRow.Text = $"Showing {dataTable.Rows.Count} rows.";

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
    }
}
