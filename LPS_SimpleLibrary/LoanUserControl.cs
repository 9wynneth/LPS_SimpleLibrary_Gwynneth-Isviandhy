﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary
{
    public partial class LoanUserControl : UserControl
    {
        public LoanUserControl()
        {
            InitializeComponent();

            tabControl1.TabPages.Remove(tabPageLoanDetail);
            dataGridViewLoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;


            LoadLoanData();
            LoadMemberAndBookData();


        }
        private bool isEditMode = false;
        private int loanId;


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false; // Set to Add mode
            tabControl1.TabPages.Remove(tabPageLoanList);
            tabControl1.TabPages.Add(tabPageLoanDetail);
            LoadMemberAndBookData();
            // Clear comboboxes and date pickers for new entry
            comboBoxBooks.SelectedIndex = -1;
            comboBoxMembers.SelectedIndex = -1;
            dateTimeBookIssue.Value = DateTime.Now;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            isEditMode = true; // Set to Edit mode
            tabControl1.TabPages.Remove(tabPageLoanList);
            tabControl1.TabPages.Add(tabPageLoanDetail);
            LoadMemberAndBookData();
            if (dataGridViewLoan.SelectedCells.Count > 0)
            {
                // Get the selected cell and find its row
                var selectedCell = dataGridViewLoan.SelectedCells[0];
                var selectedRow = selectedCell.OwningRow; // Get the row that contains the selected cell

                loanId = (int)selectedRow.Cells["id_loan"].Value;  // Store loanId here

                // Populate combo boxes and DateTimePicker with selected loan data
                comboBoxMembers.SelectedValue = selectedRow.Cells["id_member"].Value;
                comboBoxBooks.SelectedValue = selectedRow.Cells["id_book"].Value;
                dateTimeBookIssue.Value = (DateTime)selectedRow.Cells["dateBorrowed_loan"].Value;
            }
            else
            {
                MessageBox.Show("Please select a cell to edit.");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            string selectedMember = comboBoxMembers.SelectedItem?.ToString()?.Trim() ?? string.Empty;
            string selectedBook = comboBoxBooks.SelectedItem?.ToString()?.Trim() ?? string.Empty; 


            if (string.IsNullOrEmpty(selectedMember) || string.IsNullOrEmpty(selectedBook))
            {
                MessageBox.Show("Please select both the member and books.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckForDuplicates(selectedMember, selectedBook))
            {
                MessageBox.Show("A loan with the same member, books, and date already exists.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isEditMode)
            {
                // Add new loan record
                InsertNewLoanRecord();

            }
            else
            {
                string memberId = comboBoxMembers.SelectedValue.ToString(); 
                string bookId = comboBoxBooks.SelectedValue.ToString();     
                DateTime dateBorrowed = dateTimeBookIssue.Value;            


                UpdateLoanRecord(memberId, bookId, dateBorrowed);

            }

            // Switch back to the loan list page
            tabControl1.TabPages.Remove(tabPageLoanDetail);
            tabControl1.TabPages.Add(tabPageLoanList);
        }




        private void buttonDelete_Click(object sender, EventArgs e)
        {
            LoadMemberAndBookData();
            if (dataGridViewLoan.SelectedRows.Count > 0)
            {
                // Get the selected loan ID from the DataGridView (assuming it's in the first column)
                int loanId = Convert.ToInt32(dataGridViewLoan.SelectedRows[0].Cells["id_loan"].Value);
                string bookId = dataGridViewLoan.SelectedRows[0].Cells["id_book"].Value.ToString();

                // Show confirmation message box
                if (MessageBox.Show("Are you sure you want to delete this loan record?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Proceed with the deletion
                    DeleteLoanRecord(loanId, bookId);
                }
            }
            else
            {
                MessageBox.Show("Please select a loan record to delete.");
            }
        }
        private void LoadLoanData()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = "select l.id_loan,l.id_member, l.id_book, m.nama_member, b.name_book, l.dateborrowed_loan, l.duedate_loan from loan l\r\nleft join `member` m on m.id_member = l.id_member left join book b on b.id_book = l.id_book" +
                "           where delete_loan = 0;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                DataTable loanTable = new DataTable();
                connection.Open();
                adapter.Fill(loanTable);
                dataGridViewLoan.DataSource = loanTable;
                
                //dataGridViewLoan.Columns["id_member"].Visible = false;
                //dataGridViewLoan.Columns["id_book"].Visible = false;

                //labelDataRow.Text = $"Showing {loanTable.Rows.Count.ToString()} rows" ;
                if (loanTable.Rows.Count == 0)
                {
                    dataGridViewLoan.DataSource = null;
                    dataGridViewLoan.Rows.Clear();
                    dataGridViewLoan.Columns.Clear();
                    dataGridViewLoan.Columns.Add("Message", "");
                    dataGridViewLoan.Rows.Add("No records found.");
                    dataGridViewLoan.ClearSelection();
                    Console.WriteLine("00000");
                    labelDataRow.Text = $"Showing {loanTable.Rows.Count.ToString()} rows";

                }
                else
                {
                    dataGridViewLoan.DataSource = loanTable;

                    dataGridViewLoan.Columns["id_loan"].HeaderText = "Loan ID";
                    dataGridViewLoan.Columns["nama_member"].HeaderText = "Member Name";
                    dataGridViewLoan.Columns["name_book"].HeaderText = "Book Title";
                    dataGridViewLoan.Columns["dateborrowed_loan"].HeaderText = "Date Borrow";
                    dataGridViewLoan.Columns["duedate_loan"].HeaderText = "Due Date";
                    //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                    labelDataRow.Text = $"Showing {loanTable.Rows.Count.ToString()} rows";

                }
            }
        }


        private void InsertNewLoanRecord()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"INSERT INTO loan (id_member, id_book, dateBorrowed_loan) VALUES (@id_member, @id_book, @dateBorrowed_loan);
                             UPDATE book 
                                    SET status_book = 1 
                                    WHERE id_book = @id_book";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_member", comboBoxMembers.SelectedValue.ToString()); // Ensure it's a string
                command.Parameters.AddWithValue("@id_book", comboBoxBooks.SelectedValue.ToString()); // Ensure it's a string
                command.Parameters.AddWithValue("@dateBorrowed_loan", dateTimeBookIssue.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Reload loan data
            LoadLoanData();
            LoadMemberAndBookData();

            tabControl1.TabPages.Remove(tabPageLoanDetail); // Go back to Loan List tab
        }




        private void UpdateLoanRecord(string memberId, string bookId, DateTime dateBorrowed)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = "UPDATE loan SET id_member = @id_member, id_book = @id_book, dateBorrowed_loan = @dateBorrowed_loan WHERE id_loan = @id_loan";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_member", memberId);
                command.Parameters.AddWithValue("@id_book", bookId);
                command.Parameters.AddWithValue("@dateBorrowed_loan", dateBorrowed);
                command.Parameters.AddWithValue("@id_loan", loanId);  // Use the stored loanId for update

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Reload loan data
            LoadLoanData();
            LoadMemberAndBookData();
            tabControl1.TabPages.Remove(tabPageLoanDetail); // Go back to Loan List tab
        }




        private void DeleteLoanRecord(int loanId, string bookId)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            if (MessageBox.Show("Are you sure you want to delete this loan record?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string queryDelete = @"update loan set delete_loan = 1 where id_book = @id_book;
                                       UPDATE book 
                                    SET status_book = 0 
                                    WHERE id_book = @id_book      ";

                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand(queryDelete, connection))
                {
                    command.Parameters.AddWithValue("@id_loan", loanId);
                    command.Parameters.AddWithValue("@id_book", bookId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                // Refresh the loan data grid
                LoadLoanData();
                LoadMemberAndBookData();
            }
        }

        private void LoadMemberAndBookData()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            // Load member data
            string queryMembers = "SELECT id_member, nama_member FROM member where delete_member = 0;";
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(queryMembers, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                DataTable memberTable = new DataTable();
                connection.Open();
                adapter.Fill(memberTable);
                comboBoxMembers.DataSource = memberTable;
                comboBoxMembers.DisplayMember = "nama_member";
                comboBoxMembers.ValueMember = "id_member";
            }

            // Load book data
            string queryBooks = "SELECT id_book, name_book FROM book where status_book = 0 and delete_book = 0;";
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(queryBooks, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
                DataTable bookTable = new DataTable();
                connection.Open();
                adapter.Fill(bookTable);
                comboBoxBooks.DataSource = bookTable;
                comboBoxBooks.DisplayMember = "name_book";
                comboBoxBooks.ValueMember = "id_book";
            }
        }
        private void SearchDataGridView(string loanID, string memberID, string bookID, string memberName, string bookName, string dateBorrowLoan, string dueDateLoan)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";

            string query = @"select l.id_loan,l.id_member, l.id_book, m.nama_member, b.name_book, 
                            l.dateborrowed_loan, l.duedate_loan from loan l
                            left join `member` m on m.id_member = l.id_member 
                            left join book b on b.id_book = l.id_book           
                            where l.id_loan like @Loanid or m.nama_member like @MemberName or 
                            b.name_book like @BookName or
                            l.dateborrowed_loan like @DateBorrowLoan or l.duedate_loan like @DueDateLoan;     ";


            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            using (var adapter = new MySqlDataAdapter(command))
            {
              
                command.Parameters.AddWithValue("@Loanid", "%" + loanID + "%");
                command.Parameters.AddWithValue("@Memberid", "%" + memberID + "%");
                command.Parameters.AddWithValue("@Bookid", "%" + bookID + "%");
                command.Parameters.AddWithValue("@MemberName", "%" + memberName + "%");
                command.Parameters.AddWithValue("@BookName", "%" + bookName + "%");
                command.Parameters.AddWithValue("@DateBorrowLoan", "%" + dateBorrowLoan + "%");
                command.Parameters.AddWithValue("@DueDateLoan", "%" + dueDateLoan + "%");

                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                if (dataGridViewLoan.Columns["id_member"] != null)
                {
                    dataGridViewLoan.Columns["id_member"].Visible = false;
                }
                if (dataGridViewLoan.Columns["id_book"] != null)
                {
                    dataGridViewLoan.Columns["id_book"].Visible = false;
                }

                if (dataTable.Rows.Count == 0)
                {
                    dataGridViewLoan.DataSource = null;
                    dataGridViewLoan.Rows.Clear();
                    dataGridViewLoan.Columns.Clear();
                    dataGridViewLoan.Columns.Add("Message", "");
                    dataGridViewLoan.Rows.Add("No records found.");
                    dataGridViewLoan.ClearSelection();
                    Console.WriteLine("00000");
                    labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

                }
                else
                {
                    dataGridViewLoan.DataSource = dataTable;

                    dataGridViewLoan.Columns["id_loan"].HeaderText = "Loan ID";
                    dataGridViewLoan.Columns["nama_member"].HeaderText = "Member Name";
                    dataGridViewLoan.Columns["name_book"].HeaderText = "Book Title";
                    dataGridViewLoan.Columns["dateborrowed_loan"].HeaderText = "Date Borrow";
                    dataGridViewLoan.Columns["duedate_loan"].HeaderText = "Due Date";
                    //dataGridViewMember.Columns["delete_member"].HeaderText = "Status";


                    labelDataRow.Text = $"Showing {dataTable.Rows.Count.ToString()} rows";

                }
            }
            
            // Update the DataGridView
            //dataGridViewLoan.DataSource = dataTable;
            //labelDataRow.Text = $"Showing {dataTable.Rows.Count} rows.";

        }

        private bool CheckForDuplicates(string idBook, string idMember)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"SELECT COUNT(*) 
                     FROM loan 
                     WHERE id_book = @idBook AND id_member = @member AND delete_loan = 0";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idBook", idBook);
                command.Parameters.AddWithValue("@member", idMember);


                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Return true if duplicates exist
            }
        }

        private void dataGridViewLoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lab_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;

            // Update DataGridView rows directly
            SearchDataGridView(searchValue, searchValue, searchValue, searchValue, searchValue, searchValue, searchValue);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPageLoanDetail_Click(object sender, EventArgs e)
        {

        }

        private void tabPageLoanList_Click(object sender, EventArgs e)
        {

        }

        private void labelDataRow_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxMembers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimeBookIssue_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPageLoanDetail);
            tabControl1.TabPages.Add(tabPageLoanList);

        }
    }
}
