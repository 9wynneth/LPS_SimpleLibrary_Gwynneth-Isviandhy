using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using LPS_SimpleLibrary.Presenter;
using LPS_SimpleLibrary._Repository;

namespace LPS_SimpleLibrary
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string sqlConnectionString = "server=localhost;uid=root;pwd=;database=lps_library";
            MySqlConnection connection = new MySqlConnection(sqlConnectionString);
            MemberView viewMember = new MemberView();
            BookRepository repository = new BookRepository(sqlConnectionString);
            new BookMemberPresenter(viewMember, repository);

            StaffDashboardView viewStaff = new StaffDashboardView();
            LoanRepository repository2 = new LoanRepository(sqlConnectionString);
            //new LoanPresenter(viewStaff, repository2);

            //iPetView view = new PetView();
            //iPetRepository repository = new PetRepository(sqlConnectionString);
            Form1 view = new Form1();
            //new MainPresenter(view, sqlConnectionString);
            //new PetPresenter(view, repository);

            Application.Run(new Form1());
        }
    }
}
