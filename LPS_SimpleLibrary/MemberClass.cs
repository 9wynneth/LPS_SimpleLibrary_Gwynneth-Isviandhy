using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LPS_SimpleLibrary
{
    public class MemberClass: DatabaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public override void Insert()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"INSERT INTO `member` (nama_member, email_member) 
                         VALUES (@nama, @email);";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", this.Name);
                command.Parameters.AddWithValue("@email", this.Email);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public override void Update()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"UPDATE `member` SET nama_member = @nama, email_member = @pass WHERE id_member = @idMember;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nama", this.Name);
                command.Parameters.AddWithValue("@pass", this.Email);
                command.Parameters.AddWithValue("@idMember", this.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public override void Delete()
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            string query = @"UPDATE `member` SET delete_member = 1 WHERE id_member = @idMember;";

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idMember", this.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
