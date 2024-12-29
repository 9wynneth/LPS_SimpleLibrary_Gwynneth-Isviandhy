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
using Org.BouncyCastle.Asn1.Crmf;

namespace LPS_SimpleLibrary
{
    public partial class StaffDashboardView : Form
    {

        private string message;
        private bool isSuccessful;
        private bool isEdit;
        public event EventHandler OnLoanSubmit;
        private BindingSource loanBindingSource;


        private LoanUserControl userControl;

        public StaffDashboardView()
        {
            InitializeComponent();
            //dataGridViewLoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //PopulateMemberComboBox();
            //PopulateBookComboBox();
            //LoadLoanData();
            //dateTimePickerDateIssue.Enabled = true;
            //dateTimePickerDateIssue.Value = DateTime.Now; 
          

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            labelHeading.Text = "Loan Settings";
            userControl = new LoanUserControl();
            userControl.Dock = DockStyle.Fill;  // Fills the panel (adjust as needed)
            panelMain.Controls.Add(userControl);
        }

        //private void PopulateMemberComboBox()
        //{
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = "SELECT id_member, nama_member FROM member";

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            using (var reader = command.ExecuteReader())
        //            {
        //                var members = new DataTable();
        //                members.Load(reader);
        //                comboBoxMembers.DataSource = members;
        //                comboBoxMembers.DisplayMember = "nama_member"; 
        //                comboBoxMembers.ValueMember = "id_member";
        //            }
        //        }
        //    }
        //}

        //private void PopulateBookComboBox()
        //{
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = "SELECT id_book, name_book FROM book";

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            using (var reader = command.ExecuteReader())
        //            {
        //                var books = new DataTable();
        //                books.Load(reader);
        //                comboBoxBooks.DataSource = books;
        //                comboBoxBooks.DisplayMember = "name_book"; 
        //                comboBoxBooks.ValueMember = "id_book"; 
        //            }
        //        }
        //    }
        //}

        ////public void SetBindingSource(BindingSource bindingSource)
        ////{
        ////    loanBindingSource = bindingSource;
        ////    dataGridViewLoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        ////    dataGridViewLoan.DataSource = loanBindingSource;
        ////}


        //public string SelectedMemberId => (string)comboBoxMembers.SelectedValue;
        //public string SelectedBookId => (string)comboBoxBooks.SelectedValue;
        //public DateTime SelectedDate => dateTimePickerDateIssue.Value;

        //public bool IsEdit
        //{
        //    get { return isEdit; }
        //    set { isEdit = value; }
        //}
        //public bool IsSuccessful
        //{
        //    get { return isSuccessful; }
        //    set { isSuccessful = value; }
        //}
        //public string Message
        //{
        //    get { return message; }
        //    set { message = value; }
        //}




        //private void buttonSave_Click(object sender, EventArgs e)
        //{
        //    //OnLoanSubmit?.Invoke(this, EventArgs.Empty);
        //    // Retrieve values from the form controls
        //    string memberId = comboBoxBooks.SelectedValue.ToString();
        //    string bookId = comboBoxBooks.SelectedValue.ToString();
        //    DateTime borrowedDate = dateTimePickerDateIssue.Value;
        //    // Check if any required fields are blank
        //    if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(bookId))
        //    {
        //        MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return; // Stop further execution if any field is empty
        //    }

        //    string loanId = ""; 

        //    if (dataGridViewLoan.SelectedRows.Count > 0)
        //    {
        //        // Edit mode: Get loanId from the selected row
        //        loanId = dataGridViewLoan.SelectedRows[0].Cells["ID_LOAN"].Value.ToString();

        //        UpdateLoanRecord(loanId, memberId, bookId, borrowedDate);
        //    }
        //    else
        //    {
        //        AddNewLoanRecord(memberId, bookId, borrowedDate);
        //    }

        //    LoadLoanData();
        //    ClearForm();

        //}

        //private void LoadLoanData()
        //{
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = "SELECT l.ID_LOAN, m.nama_member AS MemberName, b.name_book AS BookTitle, l.DATEBORROWED_LOAN , l.DUEDATE_LOAN " +
        //                   "FROM loan l " +
        //                   "JOIN member m ON l.ID_MEMBER = m.id_member " +
        //                   "JOIN book b ON l.ID_BOOK = b.id_book";

        //    DataTable dt = new DataTable();

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            using (var reader = command.ExecuteReader())
        //            {
        //                dt.Load(reader);
        //            }
        //        }
        //    }

        //    dataGridViewLoan.DataSource = dt;
        //}


        //private void AddNewLoanRecord(string memberId, string bookId, DateTime borrowedDate)
        //{
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = @"
        //            INSERT INTO loan (ID_MEMBER, ID_BOOK, DATEBORROWED_LOAN)
        //            VALUES (@memberId, @bookId, @borrowedDate)";

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@memberId", memberId);
        //            command.Parameters.AddWithValue("@bookId", bookId);
        //            command.Parameters.AddWithValue("@borrowedDate", borrowedDate);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private void UpdateLoanRecord(string loanId, string memberId, string bookId, DateTime borrowedDate)
        //{
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = @"
        //                        UPDATE loan
        //                        SET ID_MEMBER = @memberId, ID_BOOK = @bookId, DATEBORROWED_LOAN = @borrowedDate
        //                        WHERE ID_LOAN = @loanId";

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@loanId", loanId);
        //            command.Parameters.AddWithValue("@memberId", memberId);
        //            command.Parameters.AddWithValue("@bookId", bookId);
        //            command.Parameters.AddWithValue("@borrowedDate", borrowedDate);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    Form1 form1 = new Form1();
        //    form1.Show();
        //}

        //private void dataGridViewLoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        // Get the selected loan ID
        //        string loanId = dataGridViewLoan.Rows[e.RowIndex].Cells["ID_LOAN"].Value.ToString();
        //        string memberName = dataGridViewLoan.Rows[e.RowIndex].Cells["MemberName"].Value.ToString();
        //        string bookTitle = dataGridViewLoan.Rows[e.RowIndex].Cells["BookTitle"].Value.ToString();
        //        DateTime borrowedDate = Convert.ToDateTime(dataGridViewLoan.Rows[e.RowIndex].Cells["DATEBORROWED_LOAN"].Value);

        //        // Populate the comboboxes with the relevant data
        //        comboBoxMembers.SelectedValue = GetMemberIdByName(memberName);
        //        comboBoxBooks.SelectedValue = GetBookIdByTitle(bookTitle);
        //        dateTimePickerDateIssue.Value = borrowedDate;
        //    }
        //}
        //private string GetMemberIdByName(string memberName)
        //{
        //    // Get the member ID by name from the database
        //    string memberId = "";
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = "SELECT id_member FROM member WHERE nama_member = @memberName";

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@memberName", memberName);
        //            using (var reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    memberId = reader["id_member"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    return memberId;
        //}

        //private string GetBookIdByTitle(string bookTitle)
        //{
        //    // Get the book ID by title from the database
        //    string bookId = "";
        //    string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
        //    string query = "SELECT id_book FROM book WHERE name_book = @bookTitle";

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@bookTitle", bookTitle);
        //            using (var reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    bookId = reader["id_book"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    return bookId;
        //}
        //private void btnClearAll_Click(object sender, EventArgs e)
        //{
        //    ClearForm();
        //}

        //private void ClearForm()
        //{
        //    comboBoxMembers.SelectedIndex = -1; 
        //    comboBoxBooks.SelectedIndex = -1; 
        //    dateTimePickerDateIssue.Value = DateTime.Now; 
        //}

        //private void buttonClearAll_Click(object sender, EventArgs e)
        //{
        //    ClearForm();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    var childForm = new MemberView();

        //    // Set the ChildForm's TopLevel property to false
        //    childForm.TopLevel = false; // This allows the form to be hosted inside another form

        //    // Dock the ChildForm to fill the right panel
        //    childForm.Dock = DockStyle.Fill;
        //}
    }
}
