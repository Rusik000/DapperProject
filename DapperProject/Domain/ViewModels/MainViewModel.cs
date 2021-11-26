using Dapper;
using DapperProject.Commands;
using DapperProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DapperProject.Domain.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainWindow MainView { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public string Conn { get; set; } = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;


        public MainViewModel()
        {
            AddCommand = new RelayCommand(sender =>
            {
                double price = double.Parse(MainView.PriceTxtBx.Text);
                string bookname = MainView.NameTxtBx.Text;
                string authorname = MainView.AuthornameTxtBx.Text;

                using (var connection = new SqlConnection(Conn))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", bookname, DbType.String);
                    parameters.Add("@Authorname", authorname, DbType.String);
                    parameters.Add("@Price", price, DbType.Double);
                    var data = connection.Query<Book>("SP_InsertBook", parameters, commandType: CommandType.StoredProcedure);
                    MainView.mydatagrid.ItemsSource = GetAll();

                }
                MessageBox.Show("Book Added");

            });

            UpdateCommand = new RelayCommand(sender =>
            {
                int id1 = int.Parse(MainView.IdTxtBx.Text);
                double price1 = double.Parse(MainView.PriceTxtBx.Text);
                string bookname1 = MainView.NameTxtBx.Text;
                string authorname1 = MainView.AuthornameTxtBx.Text;

                using (var connection = new SqlConnection(Conn))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id1, DbType.Int32);
                    parameters.Add("@Name", bookname1, DbType.String);
                    parameters.Add("@Authorname", authorname1, DbType.String);
                    parameters.Add("@Price", price1, DbType.Double);
                    var data = connection.Query<Book>("SP_UpdateBook", parameters, commandType: CommandType.StoredProcedure);
                    MainView.mydatagrid.ItemsSource = GetAll();

                }
                MessageBox.Show("Book Updated");
            });

            DeleteCommand = new RelayCommand(sender =>
            {
                int id2 = int.Parse(MainView.IdTxtBx.Text);
                

                using (var connection = new SqlConnection(Conn))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id2, DbType.Int32);
                    var data = connection.Query<Book>("SP_DeleteBook", parameters, commandType: CommandType.StoredProcedure);
                    MainView.mydatagrid.ItemsSource = GetAll();

                }
                MessageBox.Show("Book Deleted");
            });




        }

        public List<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            using (var connection = new SqlConnection(Conn))
            {
                books = connection.Query<Book>("SELECT Id,Name,Authorname,Price from Books").ToList();
            }
            return books;
        }

        public void Insert(Book book)
        {
            using (var connection = new SqlConnection(Conn))
            {
                connection.Execute(@"INSERT INTO Books(Name,Authorname,Price)
                                        VALUES(@BName,@BAuthorname,@BPrice)",
                                     new { @BName = book.Name, @BAuthorname = book.Authorname, @BPrice = book.Price });
            }
            MessageBox.Show("Book added successfully");
        }

    }
}
