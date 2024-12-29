using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPS_SimpleLibrary.Model;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary._Repository
{
    internal class BookRepository: BaseRepository
    {
        public BookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public IEnumerable<BookModel> GetAll()
        {
            var bukuList = new List<BookModel>();
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select id_book, name_book, genre_book, author_book, CASE 
                                        WHEN status_book = 0 THEN 'Available'
                                        WHEN status_book = 1 THEN 'Not Available'
                                        END AS status_book from book;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bukuModel = new BookModel();

                        bukuModel.Id = reader[0].ToString();
                        bukuModel.Name = reader[1].ToString();
                        bukuModel.Genre = reader[2].ToString();
                        bukuModel.Author = reader[3].ToString();
                        bukuModel.Status = reader[4].ToString();
                        bukuList.Add(bukuModel);
                    }
                }
            }
            return bukuList;
        }
        public IEnumerable<BookModel> GetByValue(string value)
        {
            var bukuList = new List<BookModel>();
            string bukuId = value;
            string bukuName = value;
            string bukuGenre = value;
            string bukuAuthor = value;
            string bukuStatus = value;

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select id_book, name_book, genre_book, author_book, CASE 
                                        WHEN status_book = 0 THEN 'Available'
                                        WHEN status_book = 1 THEN 'Not Available'
                                        END AS status_book from book
                                        where id_book like @id or name_book like @name or 
                                        genre_book like @genre or
                                        author_book like @author or 
                                        status_book like @status 
                                        order by 1 asc";
                command.Parameters.AddWithValue("@id", "%" + bukuId + "%");
                command.Parameters.AddWithValue("@name", "%" + bukuName + "%");
                command.Parameters.AddWithValue("@genre", "%" + bukuGenre + "%");
                command.Parameters.AddWithValue("@author", "%" + bukuAuthor + "%");
                command.Parameters.AddWithValue("@status", "%" + bukuStatus + "%");


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bukuModel = new BookModel();
                        bukuModel.Id = reader[0].ToString();
                        bukuModel.Name = reader[1].ToString();
                        bukuModel.Genre = reader[2].ToString();
                        bukuModel.Author = reader[3].ToString();
                        bukuModel.Status = reader[4].ToString();

                        bukuList.Add(bukuModel);
                    }
                }
            }
            return bukuList;
        }
        public IEnumerable<BookModel> GetBooksByGenre(string genre)
        {
            var books = new List<BookModel>();
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                SELECT id_book, name_book, genre_book, author_book, 
                    CASE 
                        WHEN status_book = 0 THEN 'Available' 
                        WHEN status_book = 1 THEN 'Not Available' 
                    END AS status_book 
                FROM book 
                WHERE genre_book = @genre;";
                command.Parameters.AddWithValue("@genre", genre);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new BookModel
                        {
                            Id = reader["id_book"].ToString(),
                            Name = reader["name_book"].ToString(),
                            Genre = reader["genre_book"].ToString(),
                            Author = reader["author_book"].ToString(),
                            Status = reader["status_book"].ToString()
                        };
                        books.Add(book);
                    }
                }
            }
            return books;
        }
        public IEnumerable<BookModel> GetGenre(string value)
        {
            var bukuList = new List<BookModel>();
            string bukuId = value;
            string bukuName = value;
            string bukuGenre = value;
            string bukuAuthor = value;
            string bukuStatus = value;

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select distinct genre_book from book;";
                command.Parameters.AddWithValue("@genre", "%" + bukuGenre + "%");
             


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bukuModel = new BookModel();
                     
                        bukuModel.Genre = reader[0].ToString();
                    

                        bukuList.Add(bukuModel);
                    }
                }
            }
            return bukuList;
        }
        //public void Add(BukuModel bukuModel)
        //{
        //    using (var connection = new MySqlConnection(connectionString))
        //    using (var command = new MySqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "insert into buku(nama_buku, penulis_buku, penerbit_buku, isbn_buku) values (@name, @author, @publisher, @isbn)";

        //        command.Parameters.AddWithValue("@name", bukuModel.Name);
        //        command.Parameters.AddWithValue("@author", bukuModel.Author);
        //        command.Parameters.AddWithValue("@publisher", bukuModel.Publisher);
        //        command.Parameters.AddWithValue("@isbn", bukuModel.ISBN);

        //        command.ExecuteNonQuery();

        //        connection.Close();

        //    }
        //}
        //public void Delete(string id)
        //{
        //    using (var connection = new MySqlConnection(connectionString))
        //    using (var command = new MySqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "SET SQL_SAFE_UPDATES = 0;";
        //        command.ExecuteNonQuery();
        //        command.CommandText = "DELETE FROM buku_kategori WHERE id_buku = @id";
        //        command.Parameters.AddWithValue("@id", id);
        //        command.ExecuteNonQuery();
        //        command.CommandText = "SET SQL_SAFE_UPDATES = 1;";
        //        command.ExecuteNonQuery();

        //        // Then, delete the book itself
        //        command.CommandText = "DELETE FROM buku WHERE id_buku = @id";
        //        command.ExecuteNonQuery();

        //        connection.Close();

        //    }
        //}
        //public void Edit(BukuModel bukuModel)
        //{
        //    using (var connection = new MySqlConnection(connectionString))
        //    using (var command = new MySqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = @"UPDATE buku
        //                        SET nama_buku = @name, penulis_buku = @author, penerbit_buku = @publisher,
        //                        isbn_buku = @isbn
        //                        WHERE id_buku = @id;
        //                        DELETE FROM buku_kategori where id_buku = @id;
        //                        ";


        //        command.Parameters.AddWithValue("@name", bukuModel.Name);
        //        command.Parameters.AddWithValue("@author", bukuModel.Author);
        //        command.Parameters.AddWithValue("@publisher", bukuModel.Publisher);
        //        command.Parameters.AddWithValue("@isbn", bukuModel.ISBN);

        //        command.Parameters.AddWithValue("@id", bukuModel.Id);

        //        command.ExecuteNonQuery();

        //    }
        //}
    }
}
