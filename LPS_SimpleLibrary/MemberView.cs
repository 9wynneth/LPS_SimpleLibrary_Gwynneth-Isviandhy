using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPS_SimpleLibrary._Repository;
using LPS_SimpleLibrary.Model;
using LPS_SimpleLibrary.Presenter;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary
{
    public partial class MemberView : Form
    {

        private string message;
        private bool isSuccessful;
        private bool isEdit;
        private BindingSource bookBindingSource;

        public Label CategoryCountLabel
        {
            get { return labelData; }
        }
        public MemberView()
        {
            InitializeComponent();
            LoadBooks();
            LoadGenres();

        }
        private void LoadBooks()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT id_book, name_book, genre_book, author_book, 
             CASE 
                 WHEN status_book = 0 THEN 'Available'
                 WHEN status_book = 1 THEN 'Not Available'
             END AS status_book 
             FROM book where delete_book = 0;";

            DataTable dataTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 0)
            {
                dataGridViewBookCollection.DataSource = null;
                dataGridViewBookCollection.Rows.Clear();
                dataGridViewBookCollection.Columns.Clear();
                dataGridViewBookCollection.Columns.Add("Message", "No Records");
                dataGridViewBookCollection.Rows.Add("No records found.");
                dataGridViewBookCollection.ClearSelection();
                Console.WriteLine("00000");
            }
            else
            {
                dataGridViewBookCollection.DataSource = dataTable;

                dataGridViewBookCollection.Columns["id_book"].HeaderText = "Book ID";
                dataGridViewBookCollection.Columns["name_book"].HeaderText = "Title";
                dataGridViewBookCollection.Columns["genre_book"].HeaderText = "Genre";
                dataGridViewBookCollection.Columns["author_book"].HeaderText = "Author";
                dataGridViewBookCollection.Columns["status_book"].HeaderText = "Status";

                labelData.Text = $"Showing {dataTable.Rows.Count} rows.";
                Console.WriteLine("masukkkkkk");

            }

        }

        private void UpdateDataGridView(string bukuId, string bukuName, string bukuGenre, string bukuAuthor, string bukuStatus)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"SELECT id_book, name_book, genre_book, author_book, 
                     CASE 
                         WHEN status_book = 0 THEN 'Available'
                         WHEN status_book = 1 THEN 'Not Available'
                     END AS status_book 
                     FROM book
                     WHERE delete_book = 0 and ( id_book LIKE @id 
                        OR name_book LIKE @name 
                        OR genre_book LIKE @genre 
                        OR author_book LIKE @author 
                        OR status_book LIKE @status)
                     ORDER BY 1 ASC";

            DataTable dataTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@id", "%" + bukuId + "%");
                command.Parameters.AddWithValue("@name", "%" + bukuName + "%");
                command.Parameters.AddWithValue("@genre", "%" + bukuGenre + "%");
                command.Parameters.AddWithValue("@author", "%" + bukuAuthor + "%");
                command.Parameters.AddWithValue("@status", "%" + bukuStatus + "%");

                connection.Open();
                adapter.Fill(dataTable);
            }


            if (dataTable.Rows.Count == 0)
            {
                dataGridViewBookCollection.DataSource = null;
                dataGridViewBookCollection.Rows.Clear();
                dataGridViewBookCollection.Columns.Clear();
                dataGridViewBookCollection.Columns.Add("Message", "");
                dataGridViewBookCollection.Rows.Add("No records found.");
                dataGridViewBookCollection.ClearSelection();
                Console.WriteLine("00000");
            }
            else
            {
                dataGridViewBookCollection.DataSource = dataTable;

                dataGridViewBookCollection.Columns["id_book"].HeaderText = "Book ID";
                dataGridViewBookCollection.Columns["name_book"].HeaderText = "Title";
                dataGridViewBookCollection.Columns["genre_book"].HeaderText = "Genre";
                dataGridViewBookCollection.Columns["author_book"].HeaderText = "Author";
                dataGridViewBookCollection.Columns["status_book"].HeaderText = "Status";

                labelData.Text = $"Showing {dataTable.Rows.Count} rows.";
                Console.WriteLine("masukkkkkk");

            }

        }

        private void LoadGenres()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT DISTINCT genre_book FROM book";

            DataTable genreTable = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(genreTable);
            }

            // Add "All" as the first item to the genre list
            DataRow allRow = genreTable.NewRow();
            allRow["genre_book"] = "All";
            genreTable.Rows.InsertAt(allRow, 0);

            comboBoxKategori.DataSource = genreTable;
            comboBoxKategori.DisplayMember = "genre_book";  // The name of the column to display
            comboBoxKategori.ValueMember = "genre_book";    // The value used for filtering

            // Automatically load all books when the form loads
            FilterByGenre("All");
        }

        private void FilterByGenre(string selectedGenre)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT id_book, name_book, genre_book, author_book, 
                     CASE 
                         WHEN status_book = 0 THEN 'Available'
                         WHEN status_book = 1 THEN 'Not Available'
                     END AS status_book 
                     FROM book
                     WHERE delete_book = 0 and genre_book LIKE @genre;";

            // If the selected genre is "All", skip the filter on genre
            if (selectedGenre == "All")
            {
                query = @"SELECT id_book, name_book, genre_book, author_book, 
                         CASE 
                             WHEN status_book = 0 THEN 'Available'
                             WHEN status_book = 1 THEN 'Not Available'
                         END AS status_book 
                         FROM book where delete_book = 0;";
            }

            DataTable filteredData = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@genre", "%" + selectedGenre + "%");
                connection.Open();
                adapter.Fill(filteredData);
            }

            // Bind filtered data to the DataGridView
            dataGridViewBookCollection.DataSource = filteredData;

            // Update the label with the number of rows
            labelData.Text = $"Showing {filteredData.Rows.Count} rows.";
        }


        public void SetBindingSource(BindingSource bindingSource)
        {
            bookBindingSource = bindingSource;
            dataGridViewBookCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBookCollection.DataSource = bookBindingSource;

        }
        public void SetGenreComboBoxDataSource(IEnumerable<BookModel> genres)
        {
            comboBoxKategori.DataSource = genres.ToList();
            comboBoxKategori.DisplayMember = "Genre";  // Assuming 'Genre' is the property to display
            comboBoxKategori.ValueMember = "Genre";  // You can use the value you want to bind to (e.g., genre ID)
        }
        public string SearchValue
        {
            get { return textBoxSearchBar.Text; }
            set { textBoxSearchBar.Text = value; }
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
        public event EventHandler FilterEvent;
        public event EventHandler RefreshEvent;

        public event EventHandler<string> GenreSelected;

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void textBoxSearchBar_TextChanged(object sender, EventArgs e)
        {
            //SearchEvent.Invoke(this, EventArgs.Empty);
            string searchValue = textBoxSearchBar.Text;

            // Update DataGridView rows directly
            UpdateDataGridView(searchValue, searchValue, searchValue, searchValue, searchValue);
        }

        private void MemberView_Load(object sender, EventArgs e)
        {

        }
        
        private void comboBoxKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selectedGenre = comboBoxKategori.SelectedValue.ToString();
            //GenreSelected?.Invoke(this, selectedGenre); 
            string selectedGenre = comboBoxKategori.SelectedValue.ToString();
            FilterByGenre(selectedGenre);

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshEvent?.Invoke(this, EventArgs.Empty); 

        }
        private string selectedBookId = "";

        private void dataGridViewBookCollection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dataGridViewBookCollection.Rows[e.RowIndex];
            //    selectedBookId = row.Cells["id_book"].Value.ToString();
            //}
        }

        private void buttonBorrow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookId))
            {
                MessageBox.Show("Please select a book first.");
                return;
            }

            // Create and show the MemberInputForm
            using (var memberInputForm = new MemberInputForBorrow())
            {
                if (memberInputForm.ShowDialog() == DialogResult.OK)
                {
                    string memberIdInput = memberInputForm.SelectedMemberId;

                    // Proceed with borrowing the book
                    BorrowBook(memberIdInput);
                }
            }
        }

        private void BorrowBook(string memberId)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            // Create the connection
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection first
                    connection.Open();

                    // Check if the book is available
                    string checkBookStatusQuery = @"SELECT status_book FROM book WHERE id_book = @bookId";
                    using (var checkCommand = new MySqlCommand(checkBookStatusQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@bookId", selectedBookId);

                        var result = checkCommand.ExecuteScalar();
                        if (result != null && Convert.ToInt32(result) == 1) // Assuming 1 means "Not Available"
                        {
                            MessageBox.Show("This book is currently not available for borrowing.");
                            return; // Stop further execution
                        }
                    }

                    // Begin a transaction
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Update the book's status to "Not Available" (status_book = 1)
                        string updateBookQuery = @"UPDATE book 
                                    SET status_book = 1 
                                    WHERE id_book = @bookId";
                        using (var updateCommand = new MySqlCommand(updateBookQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@bookId", selectedBookId);
                            updateCommand.ExecuteNonQuery();
                        }

                        // Insert the loan entry
                        string insertLoanQuery = @"INSERT INTO loan (id_member, id_book, dateBorrowed_loan) 
                                    VALUES (@memberId, @bookId, @dateBorrowed)";
                        using (var insertCommand = new MySqlCommand(insertLoanQuery, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@memberId", memberId);
                            insertCommand.Parameters.AddWithValue("@bookId", selectedBookId);
                            insertCommand.Parameters.AddWithValue("@dateBorrowed", DateTime.Now); // Use current date
                            insertCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Book successfully borrowed!");
                        LoadBooks();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void dataGridViewBookCollection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewBookCollection.Rows[e.RowIndex];
                selectedBookId = row.Cells["id_book"].Value.ToString();
            }
        }
    }
}
