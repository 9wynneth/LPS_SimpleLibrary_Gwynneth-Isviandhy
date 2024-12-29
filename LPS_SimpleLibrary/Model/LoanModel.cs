using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS_SimpleLibrary.Model
{
    public class LoanModel
    {
      
        //Fields
        public string id;
        public string IdMember;
        public string IdBook;
        public DateTime DateBorrowedLoan;
        public DateTime DueDateLoan;
        public DateTime DateReturn;


        //Properties - Validations
        [DisplayName("Loan ID")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [DisplayName("Member ID")]
        [Required(ErrorMessage = "Name is required")]

        public string IDmember
        {
            get { return IdMember; }
            set { IdMember = value; }
        }
        [DisplayName("Book ID")]
        [Required(ErrorMessage = "Name is required")]
        public string IDbook
        {
            get { return IdBook; }
            set { IdBook = value; }
        }

        [DisplayName("Date Borrow")]
        [Required(ErrorMessage = "Name is required")]
        public DateTime DateBorrow
        {
            get { return DateBorrowedLoan; }
            set { DateBorrowedLoan = value; }
        }


        [DisplayName("Date Borrow")]
        public DateTime DueDate
        {
            get { return DueDateLoan; }
            set { DueDateLoan = value; }
        }

        [DisplayName("Date Return")]
        public DateTime DateREturn
        {
            get { return DateReturn; }
            set { DateReturn = value; }
        }

    }
}
