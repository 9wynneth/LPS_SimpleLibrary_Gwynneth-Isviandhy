using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary
{
    public partial class StaffUserControl : BaseUserControl
    {
        private bool isEditMode = false;
        private string currentStaffId; 

        public StaffUserControl()
        {
            InitializeComponent();
            LoadStaffData();
            dataGridViewStaff.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tabControl1.TabPages.Remove(tabPageStaffDetail);

        }


        private void LoadStaffData()
        {
            string query = "SELECT id_staff, nama_staff, password_staff FROM staff WHERE delete_staff = 0;";
            string[] columns = { "Staff ID", "Name", "Password" };
            LoadData(query, dataGridViewStaff, columns);
            dataGridViewStaff.Columns["id_staff"].Visible = false;
        }

        private void InsertNewStaffRecord()
        {
            string query = @"INSERT INTO staff (nama_staff, password_staff) 
                             VALUES (@nama, @password);";
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@password", textBoxPassword.Text);

                connection.Open();
                command.ExecuteNonQuery();
            }
            LoadStaffData();
            tabControl1.TabPages.Remove(tabPageStaffDetail);
        }

        private void UpdateStaffData(string idStaff)
        {
            string query = "UPDATE staff SET nama_staff = @nama, password_staff = @pass WHERE id_staff = @idStaff;";
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@pass", textBoxPassword.Text);
                command.Parameters.AddWithValue("@idStaff", idStaff);

                connection.Open();
                command.ExecuteNonQuery();
            }
            LoadStaffData();
            tabControl1.TabPages.Remove(tabPageStaffDetail);
        }

        private void DeleteStaffRecord(string idStaff)
        {
            if (MessageBox.Show("Are you sure you want to delete this staff record?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string queryDelete = @"UPDATE staff SET delete_staff = 1 WHERE id_staff = @idStaff";
                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand(queryDelete, connection))
                {
                    command.Parameters.AddWithValue("@idStaff", idStaff);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                LoadStaffData();
            }
        }

        private void SearchDataGridView(string staffName, string staffPass)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"select * from staff       
                            where  delete_staff = 0 and (nama_staff like @name or password_staff like @pass);";

            DataTable dataTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                
                command.Parameters.AddWithValue("@name", "%" + staffName + "%");
                command.Parameters.AddWithValue("@pass", "%" + staffPass + "%");
              


                connection.Open();
                adapter.Fill(dataTable);
         
            }
            if (dataTable.Rows.Count == 0)
            {
                dataGridViewStaff.DataSource = null;
                dataGridViewStaff.Rows.Clear();
                dataGridViewStaff.Columns.Clear();
                dataGridViewStaff.Columns.Add("Message", "");
                dataGridViewStaff.Rows.Add("No records found.");
                dataGridViewStaff.ClearSelection();
                Console.WriteLine("00000");
                labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

            }
            else
            {
                dataGridViewStaff.DataSource = dataTable;

                dataGridViewStaff.Columns["id_staff"].HeaderText = "Staff ID";
                dataGridViewStaff.Columns["nama_staff"].HeaderText = "Name";
                dataGridViewStaff.Columns["password_staff"].HeaderText = "Password";
            
                //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

            }
            //dataGridViewStaff.DataSource = dataTable;
            //labelDataRow.Text = $"Showing {dataTable.Rows.Count} rows.";

        }
        private bool CheckForDuplicates(string name, string pass)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT COUNT(*) 
                     FROM staff 
                     WHERE nama_staff = @name AND password_staff = @pass AND delete_staff = 0";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", pass);


                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; 
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false; // Set to Add mode
            tabControl1.TabPages.Remove(tabPageStaffList);
            tabControl1.TabPages.Add(tabPageStaffDetail);

            textBoxName.Text = "";
            textBoxPassword.Text = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPageStaffDetail);
            tabControl1.TabPages.Add(tabPageStaffList);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string pass = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Both Name and Password fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckForDuplicates(name, pass))
            {
                MessageBox.Show("A staff with the same Name and Password already exists.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isEditMode)
            {
                InsertNewStaffRecord();
            }
            else
            {
                UpdateStaffData(currentStaffId);
            }

            tabControl1.TabPages.Remove(tabPageStaffDetail);
            tabControl1.TabPages.Add(tabPageStaffList);
        }

        private void dataGridViewStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Enable editing mode and load selected staff details
                isEditMode = true;

                currentStaffId = dataGridViewStaff.Rows[e.RowIndex].Cells["id_staff"].Value.ToString();
                textBoxName.Text = dataGridViewStaff.Rows[e.RowIndex].Cells["nama_staff"].Value.ToString();
                textBoxPassword.Text = dataGridViewStaff.Rows[e.RowIndex].Cells["password_staff"].Value.ToString();

                // Switch to detail page
                tabControl1.TabPages.Remove(tabPageStaffList);
                tabControl1.TabPages.Add(tabPageStaffDetail);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteStaffRecord(currentStaffId);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;
            SearchDataGridView(searchValue, searchValue);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            isEditMode = true; // Set to Edit mode
            tabControl1.TabPages.Remove(tabPageStaffList);
            tabControl1.TabPages.Add(tabPageStaffDetail);

            if (dataGridViewStaff.SelectedCells.Count > 0)
            {
                var selectedCell = dataGridViewStaff.SelectedCells[0];
                var selectedRow = selectedCell.OwningRow;

                currentStaffId = selectedRow.Cells["id_staff"].Value.ToString(); 

                // Populate combo boxes and DateTimePicker with selected loan data
                textBoxName.Text = selectedRow.Cells["nama_staff"].Value.ToString();
                textBoxPassword.Text = selectedRow.Cells["password_staff"].Value.ToString();
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

        private void dataGridViewStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void labelDataRow_Click(object sender, EventArgs e)
        {

        }

        private void tabPageStaffDetail_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPageStaffList_Click(object sender, EventArgs e)
        {

        }

        private void lab_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
