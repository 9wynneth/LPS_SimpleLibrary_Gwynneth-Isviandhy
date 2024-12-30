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
    public partial class StaffView : Form
    {

        private string message;
        private bool isSuccessful;
        private bool isEdit;
        private BindingSource staffndingSource;

        public StaffView()
        {
            InitializeComponent();
        }
        

        public void SetBindingSource(BindingSource bindingSource)
        {
            staffndingSource = bindingSource;
            //dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dataGridView1.DataSource = categoryBindingSource;
        }
        public string StaffName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        public string StaffPass
        {
            get { return textBoxPassword.Text; }
            set { textBoxPassword.Text = value; }
        }

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler CancelEvent;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //SearchEvent?.Invoke(this, EventArgs.Empty);
            //if (Message != null)
            //{
            //    MessageBox.Show(Message);
            //}
            //else
            //{
            //    MessageBox.Show("An error occurred: No message to display.");
            //}
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string staffNama = textBoxName.Text;
            string staffPassword = textBoxPassword.Text;

            if (string.IsNullOrEmpty(staffNama) || string.IsNullOrEmpty(staffPassword))
            {
                MessageBox.Show("Please enter both ID and Password.");
                return;
            }

            try
            {
                // Create connection
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to check if the provided ID and password match a staff member
                    string query = "SELECT COUNT(*) FROM STAFF WHERE nama_staff = @nama AND PASSWORD_STAFF = @password and delete_staff = 0;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@nama", staffNama);
                        command.Parameters.AddWithValue("@password", staffPassword);  // Ensure password is stored securely

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Login successful.");
                            this.Close();

                            // Close Form1 (if it's another instance, find and close it)
                            foreach (Form form in Application.OpenForms)
                            {
                                if (form is Form1)
                                {
                                    form.Hide();
                                    break; // Exit loop after closing Form1
                                }
                            }

                            // Open StaffDashboardView
                            StaffDashboardView staffDashboard = new StaffDashboardView();
                            staffDashboard.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid ID or password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CancelEvent?.Invoke(this, EventArgs.Empty);
            this.Close();

        }
    }
}
