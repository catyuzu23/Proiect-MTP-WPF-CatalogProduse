using CatalogProduse.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CatalogProduse
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        AppDbContext db = new AppDbContext();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Completează toate câmpurile.";
                return;
            }

            var user = db.Users.FirstOrDefault(x=>x.Name == username);

            if (user == null || user.Password != password)
            {
                lblError.Text = "Date invalide";
                return;
            }
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }
    }
}
