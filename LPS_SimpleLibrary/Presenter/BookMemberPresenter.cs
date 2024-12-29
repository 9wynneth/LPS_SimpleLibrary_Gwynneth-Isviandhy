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
    internal class BookMemberPresenter
    {
        private readonly MemberView view;
        private readonly BookRepository repository;
        private BindingSource bookBindingSource;

        private IEnumerable<BookModel> bukuList;

        public BookMemberPresenter(MemberView view, BookRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.bookBindingSource = new BindingSource();

            this.view.SetBindingSource(this.bookBindingSource);
            LoadBooks();
            LoadGenres();
            this.view.SearchEvent += Search;
            this.view.GenreSelected += View_GenreSelected;
            this.view.RefreshEvent += REFRESH;



            //this.categoryView.CategoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            //this.categoryView.CategoryDataGridView.DataSource = categoryBindingSource;


        }
        private void LoadBooks()
        {
            bukuList = repository.GetAll();
            bookBindingSource.DataSource = bukuList;
            bookBindingSource.ResetBindings(false);  // Ensure the BindingSource is refreshed



            this.view.CategoryCountLabel.Text = "Number of data: " + bukuList.Count().ToString();
        }
        private void REFRESH(object sender, EventArgs e)
        {
            LoadBooks();

        }
        private void Search(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                bukuList = repository.GetByValue(this.view.SearchValue);
            else bukuList = repository.GetAll();
            bookBindingSource.DataSource = bukuList;

            this.view.CategoryCountLabel.Text = "Number of data: " + bukuList.Count().ToString();
            Console.WriteLine(bukuList.Count().ToString());

        }

        private void LoadGenres()
        {
            var genres = repository.GetGenre("");

            view.SetGenreComboBoxDataSource(genres);

        }
        private void View_GenreSelected(object sender, string selectedGenre)
        {
            if (string.IsNullOrEmpty(selectedGenre) || selectedGenre == "All")
            {
                // Load all books if "All" is selected or if the genre is empty
                LoadBooks();
            }
            else
            {
                FilterBooksByGenre(selectedGenre);
            }
        }

        public void FilterBooksByGenre(string selectedGenre)
        {
            // Get books filtered by genre from the repository
            var books = repository.GetBooksByGenre(selectedGenre);

            // Update the view (you could bind the filtered books to the UI here)
            var bindingSource = new BindingSource { DataSource = books.ToList() };
            view.SetBindingSource(bindingSource);
            this.view.CategoryCountLabel.Text = "Number of data: " + books.Count().ToString();

        }
    }
}
