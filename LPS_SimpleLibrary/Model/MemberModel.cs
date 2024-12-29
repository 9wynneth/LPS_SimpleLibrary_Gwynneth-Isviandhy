using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS_SimpleLibrary.Model
{
    public class MemberModel
    {
        private string id;
        private string name;
        private string email;

        //Properties - Validations
        [DisplayName("Staff Name:")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [DisplayName("Name:")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must consist between 3-50 characters")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DisplayName("Email:")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must consist between 3-50 characters")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

    }
}
