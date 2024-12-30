using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace LPS_SimpleLibrary
{
    public class BaseUserControl: UserControl
    {
        protected string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

        // Method to load data into a DataGridView
        protected void LoadData(string query, DataGridView dataGridView, string[] columns, string noDataMessage = "No records found.")
        {
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    dataGridView.DataSource = null;
                    dataGridView.Rows.Clear();
                    dataGridView.Columns.Clear();
                    dataGridView.Columns.Add("Message", "");
                    dataGridView.Rows.Add(noDataMessage);
                    dataGridView.ClearSelection();
                }
                else
                {
                    dataGridView.DataSource = dataTable;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        dataGridView.Columns[i].HeaderText = columns[i];
                    }
                }
            }
        }
    }
}
