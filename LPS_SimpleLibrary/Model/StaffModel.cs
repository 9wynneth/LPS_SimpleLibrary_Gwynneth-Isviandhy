using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LPS_SimpleLibrary.Model
{
    internal class StaffModel
    {
        //Fields
        private string name;
        private string password;

        //Properties - Validations
        [DisplayName("Staff Name:")]
        [Required(ErrorMessage = "Staff name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Staff name must consist between 3-50 characters")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DisplayName("Staff Password:")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password must consist between 3-50 characters")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
