//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LPS_SimpleLibrary._Repository;
//using LPS_SimpleLibrary.Model;
//using System.Windows.Forms;

//namespace LPS_SimpleLibrary.Presenter
//{

//    internal class LoanPresenter
//    {
//        private readonly StaffDashboardView view;
//        private readonly LoanRepository repository;
//        private BindingSource loanBindingSource;
//        private IEnumerable<LoanModel> loanList;

//        public LoanPresenter(StaffDashboardView view, LoanRepository repository)
//        {
//            this.view = view;
//            this.repository = repository;
//            this.loanBindingSource = new BindingSource();
//            //LoadStaff();
//            //this.view.SetBindingSource(this.loanBindingSource);
//            //view.OnLoanSubmit += HandleLoanSubmit;
//            LoadLoan();

//            //this.view.SearchEvent += SearchStaff;
//            //this.view.CancelEvent += Cancel;
//        }

//        //private void HandleLoanSubmit(object sender, EventArgs e)
//        //{
//        //    var loan = new LoanModel();
//        //    {
//        //        loan.IdMember = view.SelectedMemberId;
//        //        loan.IdBook = view.SelectedBookId;
//        //        loan.DateBorrow = view.SelectedDate;
//        //    };

//        //    repository.InsertLoan(loan);
//        //}

//        private void LoadLoan()
//        {
//            loanList = (IEnumerable<LoanModel>)repository.GetAll();
//            loanBindingSource.DataSource = loanList;

//        }


//        //private void LoadSelectedCategoryToEdit(object sender, EventArgs e)
//        //{
//        //    var category = (CategoryModel)categoryBindingSource.Current;
//        //    categoryView.CategoryID = category.categoryID.ToString();
//        //    categoryView.CategoryName = category.categoryName.ToString();

//        //    categoryView.IsEdit = true;

//        //}

//        private void Save(object sender, EventArgs e)
//        {
//            var model = new LoanModel();

          
//            model.IdMember = view.SelectedMemberId;
//            model.IdBook = view.SelectedBookId;
//            model.DateBorrow = view.SelectedDate;

//            try
//            {
//                new LPS_SimpleLibrary.Model.Common.ModelDataValidation().Validate(model);

//                //if (view.IsEdit)  // If editing, id should be non-zero
//                //{

//                //    if (!string.IsNullOrEmpty(model.id))  // Ensure Id is valid (non-zero)
//                //    {
//                //        //repository.Edit(model);
//                //        view.Message = "Kategori sukses diedit";
//                //        loanList = repository.GetAll();
//                //        categoryBindingSource.DataSource = categoryList;
//                //        LoadAllCategory();

//                //    }
//                //    else
//                //    {
//                //        categoryView.Message = "Invalid ID Buku. Tidak bisa edit buku.";
//                //        categoryView.IsSuccessful = false;
//                //        return;
//                //    }

//                //}
//                //else  // tambah
//                //{
//                    repository.InsertLoan(model);
//                    view.Message = "Loan sukses ditambahkan";
//                //}

//                view.IsSuccessful = true;
//                LoadLoan();
//                CleanviewFields();
//            }
//            catch (Exception ex)
//            {
//                view.IsSuccessful = false;
//                view.Message = ex.Message;
//            }
//        }

//        private void CleanviewFields()
//        {
//            //view.CategoryID = "-";
//            //categoryView.CategoryName = "";
//            //view.BookAuthor = "";
//            //view.BookPublisher = "";
//            //view.BookISBN = "";


//        }
//        private void CancelAction(object sender, EventArgs e)
//        {
//            CleanviewFields();
//        }
//        private void DeleteSelectedCategory(object sender, EventArgs e)
//        {
//            try
//            {
//                var category = (LoanModel)loanBindingSource.Current;
//                repository.Delete(category.id);
//                view.IsSuccessful = true;
//                view.Message = "Kategori sukses dihapus";
//                LoadLoan();
//            }
//            catch (Exception ex)
//            {
//                view.IsSuccessful = false;
//                view.Message = "Terjadi error, tidak bisa menghapus kategori";
//            }
//        }
//    }
//}
