using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPS_SimpleLibrary.Model;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary._Repository
{
    internal class LoanRepository : BaseRepository
    {
        public LoanRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertLoan(LoanModel loan)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO loan (id_member, id_book, dateBorrowed_loan) VALUES (@IdMember, @IdBook, @DateBorrowedLoan)", connection);
                command.Parameters.AddWithValue("@IdMember", loan.IdMember);
                command.Parameters.AddWithValue("@IdBook", loan.IdBook);
                command.Parameters.AddWithValue("@DateBorrowedLoan", loan.DateBorrowedLoan);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<LoanModel> GetAll()
        {
            var loanList = new List<LoanModel>();
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from loan;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var loanModel = new LoanModel();

                        try
                        {
                            loanModel.IDmember = reader[1].ToString();
                            loanModel.IDbook = reader[2].ToString();

                            // Safely parse dates
                            loanModel.DateBorrow = !string.IsNullOrWhiteSpace(reader[3].ToString())
                                ? DateTime.Parse(reader[3].ToString())
                                : DateTime.MinValue;

                            loanModel.DueDate = !string.IsNullOrWhiteSpace(reader[4].ToString())
                                ? DateTime.Parse(reader[4].ToString())
                                : DateTime.MinValue;

                            loanModel.DateReturn = !string.IsNullOrWhiteSpace(reader[5].ToString())
                                ? DateTime.Parse(reader[5].ToString())
                                : DateTime.MinValue;
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Date parsing error: {ex.Message}");
                            // Optionally, log or handle the issue here
                        }


                        loanList.Add(loanModel);
                    }
                }
            }
            return loanList;
        }

        public void Delete(string id)
        {
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                //command.CommandText = "delete from kategori where id_kategori=@id";
                //command.Parameters.AddWithValue("@id", id);

                command.CommandText = "DELETE FROM loan WHERE id_loan = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                //// Then, delete rows where delete_kategori is 1
                //command.CommandText = "DELETE FROM kategori WHERE delete_kategori = 1";
                //command.Parameters.Clear(); // Clear previous parameters if any
                //command.ExecuteNonQuery();

                connection.Close();

            }
        }

    }
}
