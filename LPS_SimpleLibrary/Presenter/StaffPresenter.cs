using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPS_SimpleLibrary._Repository;
using LPS_SimpleLibrary.Model;

namespace LPS_SimpleLibrary.Presenter
{

    internal class StaffPresenter
    {
        private readonly StaffView view;
        private readonly StaffRepository repository;
        private BindingSource staffBindingSource;
        private IEnumerable<StaffModel> staffList;

        public StaffPresenter(StaffView view, StaffRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.staffBindingSource = new BindingSource();
            LoadStaff();
            this.view.SetBindingSource(this.staffBindingSource);
            this.view.SearchEvent += SearchStaff;
            this.view.CancelEvent += Cancel;
        }
        private void LoadStaff()
        {
            staffList = repository.GetAll();
            staffBindingSource.DataSource = staffList;


        }
        private void SearchStaff(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.StaffName) || string.IsNullOrWhiteSpace(this.view.StaffPass);

            if (emptyValue)
            {
                this.view.Message = "Please enter both staff name and password."; // This will set the message
                this.view.IsSuccessful = false;
                return;
            }

            // Attempt to find the staff member by name or password
            var staffList = repository.GetByValue(this.view.StaffName);
            var matchedStaff = staffList.FirstOrDefault(staff =>
                staff.Name.Equals(this.view.StaffName, StringComparison.OrdinalIgnoreCase) &&
                staff.Password.Equals(this.view.StaffPass));

            if (matchedStaff != null)
            {
                this.view.Message = "Login successful."; // This will set the message
                this.view.IsSuccessful = true;
            }
            else
            {
                this.view.Message = "Invalid staff name or password."; // This will set the message
                this.view.IsSuccessful = false;
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            view.Close();
        }


    }
}
