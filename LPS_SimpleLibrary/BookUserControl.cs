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
    public partial class BookUserControl : UserControl
    {
        private bool isEditMode = false;
        private string currentBookId;
        public BookUserControl()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPageBookDetail);

            LoadBookData();
        }


        private void LoadBookData()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = "select id_book, name_book, genre_book, author_book, status_book" +
                "            from book " +
                "           where delete_book = 0;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                DataTable memberTable = new DataTable();
                connection.Open();
                adapter.Fill(memberTable);
                dataGridViewBook.DataSource = memberTable;
                labelDataRow.Text = $"Showing {memberTable.Rows.Count.ToString()} rows";
            }

        }
        private void InsertNewBookRecord()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"INSERT INTO book(name_book, genre_book, author_book) 
                            VALUES (@nama, @genre, @author);
                             ";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@genre", textBoxGenre.Text);
                command.Parameters.AddWithValue("@author", textBoxAuthor.Text);


                connection.Open();
                command.ExecuteNonQuery();
            }

            LoadBookData();
            tabControl1.TabPages.Remove(tabPageBookDetail);
        }

        private void UpdateBookData(string idBook)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = "UPDATE book SET name_book = @nama, genre_book = @genre, author_book = @author " +
                "           WHERE id_book = @idMember;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", textBoxName.Text);
                command.Parameters.AddWithValue("@pass", textBoxGenre.Text);
                command.Parameters.AddWithValue("@author", textBoxAuthor.Text);

                command.Parameters.AddWithValue("@idMember", idBook);

                connection.Open();
                command.ExecuteNonQuery();
            }

            LoadBookData();
            tabControl1.TabPages.Remove(tabPageBookDetail);
        }
        private void DeleteBookRecord(string idBook)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            if (MessageBox.Show("Are you sure you want to delete this member record?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string queryDelete = @" 
                                       UPDATE book 
                                    SET delete_book = 1 
                                    WHERE id_book = @idMember      ";

                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand(queryDelete, connection))
                {
                    command.Parameters.AddWithValue("@idMember", idBook);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                LoadBookData();
            }
        }

        private void SearchDataGridView(string name, string genre, string author, string status)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"select id_book, name_book, genre_book, author_book, status_book from book       
                            where delete_book = 0 and (name_book like @name or genre_book like @pass, 
                            author_book like @author or status_book like @status)  ;";

            DataTable dataTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {

                command.Parameters.AddWithValue("@name", "%" + name + "%");
                command.Parameters.AddWithValue("@pass", "%" + genre + "%");
                command.Parameters.AddWithValue("@author", "%" + author + "%");
                command.Parameters.AddWithValue("@status", "%" + status + "%");

                connection.Open();
                adapter.Fill(dataTable);

            }

            dataGridViewBook.DataSource = dataTable;
            labelDataRow.Text = $"Showing {dataTable.Rows.Count} rows.";

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false; // Set to Add mode
            tabControl1.TabPages.Remove(tabPageBookList);
            tabControl1.TabPages.Add(tabPageBookDetail);

            textBoxName.Text = "";
            textBoxGenre.Text = "";
            textBoxAuthor.Text = "";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPageBookDetail);
            tabControl1.TabPages.Add(tabPageBookList);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                InsertNewBookRecord();
            }
            else
            {
                UpdateBookData(currentBookId);
            }

            tabControl1.TabPages.Remove(tabPageBookDetail);
            tabControl1.TabPages.Add(tabPageBookList);
        }

        private void dataGridViewBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                isEditMode = true;

                currentBookId = dataGridViewBook.Rows[e.RowIndex].Cells["id_book"].Value.ToString();
                textBoxName.Text = dataGridViewBook.Rows[e.RowIndex].Cells["name_book"].Value.ToString();
                textBoxGenre.Text = dataGridViewBook.Rows[e.RowIndex].Cells["genre_book"].Value.ToString();
                textBoxAuthor.Text = dataGridViewBook.Rows[e.RowIndex].Cells["author_book"].Value.ToString();

                tabControl1.TabPages.Remove(tabPageBookList);
                tabControl1.TabPages.Add(tabPageBookDetail);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteBookRecord(currentBookId);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;
            SearchDataGridView(searchValue, searchValue, searchValue, searchValue);
        }
    }
}
