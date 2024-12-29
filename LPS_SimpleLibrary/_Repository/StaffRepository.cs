using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPS_SimpleLibrary.Model;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary._Repository
{
    internal class StaffRepository: BaseRepository
    {
        //Constructor
        public StaffRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public IEnumerable<StaffModel> GetByValue(string value)
        {
            var staffList = new List<StaffModel>();
            string staffName = value;
            string staffPassword = value;
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select * from staff
                                        where nama_staff like @name and staff_password like @pass
                                        order by 1 asc";
                command.Parameters.AddWithValue("@name", "%" + staffName + "%"); 
                command.Parameters.AddWithValue("@pass", "%" + staffPassword + "%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var staffModel = new StaffModel();
                        staffModel.Name = reader[0].ToString();
                        staffModel.Password = reader[1].ToString();
                        staffList.Add(staffModel);
                    }
                }
            }
            return staffList;
        }

        public IEnumerable<StaffModel> GetAll()
        {
            var staffList = new List<StaffModel>();
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from staff;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bukuModel = new StaffModel();

                        bukuModel.Name = reader[0].ToString();
                        bukuModel.Password = reader[1].ToString();
                     
                        staffList.Add(bukuModel);
                    }
                }
            }
            return staffList;
        }

        public void Add(StaffModel staffModel)
        {
            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into staff(nama_staff, password_staff) values (@name, @pass)";

                command.Parameters.AddWithValue("@name", staffModel.Name);
                command.Parameters.AddWithValue("@pass", staffModel.Password);


                command.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}
