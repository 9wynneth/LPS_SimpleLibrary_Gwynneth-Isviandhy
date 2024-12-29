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
    public partial class MemberInputForBorrow : Form
    {
        public string SelectedMemberId { get; private set; }

        public MemberInputForBorrow()
        {
            InitializeComponent();
            LoadMembers();
        }

        private void LoadMembers()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = "SELECT id_member, nama_member FROM member";

            DataTable memberTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(memberTable);
            }

            comboBox1.DataSource = memberTable;
            comboBox1.DisplayMember = "nama_member"; // The column to display
            comboBox1.ValueMember = "id_member";     // The value used for selection
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            // Set the SelectedMemberId property to the selected value
            SelectedMemberId = comboBox1.SelectedValue.ToString();

            // Close the form and return the selected value
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
