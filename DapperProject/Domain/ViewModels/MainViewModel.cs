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
                    var data = connection.Query<Book>("SP_InsertBook",parameters,commandType: CommandType.StoredProcedure);         
                    //MainView.mydatagrid.ItemsSource = data.ToList();  
                    MainView.mydatagrid.ItemsSource = GetAll();  
                    
                }
                MessageBox.Show("Book Added");

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
