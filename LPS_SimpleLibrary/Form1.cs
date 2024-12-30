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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            // SQL query to count total books
            string bookQuery = "SELECT COUNT(*) FROM book where status_book = 0 and delete_book = 0;";
            int totalBooks = 0;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(bookQuery, connection))
                {
                    totalBooks = Convert.ToInt32(command.ExecuteScalar()); // Get the count of books
                }
            }

            // SQL query to count total members
            string memberQuery = "SELECT COUNT(*) FROM member where delete_member = 0;";
            int totalMembers = 0;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(memberQuery, connection))
                {
                    totalMembers = Convert.ToInt32(command.ExecuteScalar()); // Get the count of members
                }
            }

            // Update labels with the total counts
            labelBooksAvailable.Text = totalBooks.ToString();
            labelTotalMembers.Text = totalMembers.ToString();
        }
        private void buttonStaff_Click(object sender, EventArgs e)
        {
            StaffView verificationForm = new StaffView();
            verificationForm.ShowDialog();
        }

        private void buttonMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            MemberView memberView = new MemberView();
            memberView.Show();
        }
    }
}
