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
                DataTable dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);
                //dataGridViewBook.DataSource = memberTable;
                //labelDataRow.Text = $"Showing {memberTable.Rows.Count.ToString()} rows";
                if (dataTable.Rows.Count == 0)
                {
                    dataGridViewBook.DataSource = null;
                    dataGridViewBook.Rows.Clear();
                    dataGridViewBook.Columns.Clear();
                    dataGridViewBook.Columns.Add("Message", "");
                    dataGridViewBook.Rows.Add("No records found.");
                    dataGridViewBook.ClearSelection();
                    Console.WriteLine("00000");
                    labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

                }
                else
                {
                    dataGridViewBook.DataSource = dataTable;

                    dataGridViewBook.Columns["id_book"].HeaderText = "Book ID";
                    dataGridViewBook.Columns["name_book"].HeaderText = "Name";
                    dataGridViewBook.Columns["genre_book"].HeaderText = "Genre";
                    dataGridViewBook.Columns["author_book"].HeaderText = "Author";
                    dataGridViewBook.Columns["status_book"].HeaderText = "Status";
                    //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                    labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

                }
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
                command.Parameters.AddWithValue("@genre", textBoxGenre.Text);
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
                            where delete_book = 0 and (name_book like @name or genre_book like @pass or 
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
            if (dataTable.Rows.Count == 0)
            {
                dataGridViewBook.DataSource = null;
                dataGridViewBook.Rows.Clear();
                dataGridViewBook.Columns.Clear();
                dataGridViewBook.Columns.Add("Message", "");
                dataGridViewBook.Rows.Add("No records found.");
                dataGridViewBook.ClearSelection();
                Console.WriteLine("00000");
                labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

            }
            else
            {
                dataGridViewBook.DataSource = dataTable;

                dataGridViewBook.Columns["id_book"].HeaderText = "Book ID";
                dataGridViewBook.Columns["name_book"].HeaderText = "Name";
                dataGridViewBook.Columns["genre_book"].HeaderText = "Genre";
                dataGridViewBook.Columns["author_book"].HeaderText = "Author";
                dataGridViewBook.Columns["status_book"].HeaderText = "Status";
                //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

            }
            //dataGridViewBook.DataSource = dataTable;
            //labelDataRow.Text = $"Showing {dataTable.Rows.Count} rows.";

        }

        private bool CheckForDuplicates(string name, string genre, string author)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT COUNT(*) 
                     FROM book 
                     WHERE name_book = @name AND genre_book = @pass AND author_book = @author AND delete_book = 0";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", genre);
                command.Parameters.AddWithValue("@author", author);


                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Return true if duplicates exist
            }
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
            string name = textBoxName.Text.Trim();
            string genre = textBoxGenre.Text.Trim();
            string author = textBoxAuthor.Text.Trim();


            // Check if both fields are filled
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(author))
            {
                MessageBox.Show("All name, genre, and author fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check for duplicates
            if (CheckForDuplicates(name, genre, author))
            {
                MessageBox.Show("A book with the same name, genre, and author already exists.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            //if (e.RowIndex >= 0)
            //{
            //    isEditMode = true;

            //    currentBookId = dataGridViewBook.Rows[e.RowIndex].Cells["id_book"].Value.ToString();
            //    textBoxName.Text = dataGridViewBook.Rows[e.RowIndex].Cells["name_book"].Value.ToString();
            //    textBoxGenre.Text = dataGridViewBook.Rows[e.RowIndex].Cells["genre_book"].Value.ToString();
            //    textBoxAuthor.Text = dataGridViewBook.Rows[e.RowIndex].Cells["author_book"].Value.ToString();

            //    tabControl1.TabPages.Remove(tabPageBookList);
            //    tabControl1.TabPages.Add(tabPageBookDetail);
            //}
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

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            isEditMode = true; // Set to Edit mode
            tabControl1.TabPages.Remove(tabPageBookList);
            tabControl1.TabPages.Add(tabPageBookDetail);

            if (dataGridViewBook.SelectedCells.Count > 0)
            {
                // Get the selected cell and find its row
                var selectedCell = dataGridViewBook.SelectedCells[0];
                var selectedRow = selectedCell.OwningRow; // Get the row that contains the selected cell

                currentBookId = selectedRow.Cells["id_book"].Value.ToString();  // Store loanId here

                textBoxName.Text = selectedRow.Cells["name_book"].Value.ToString();
                textBoxGenre.Text = selectedRow.Cells["genre_book"].Value.ToString();
                textBoxAuthor.Text = selectedRow.Cells["author_book"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Please select a cell to edit.");
            }
        }

        private void dataGridViewBook_CellClick(object sender, DataGridViewCellEventArgs e)
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
    }
}
