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
    
    public partial class AdaugaExercitiuWindow : Window
    {
        public AdaugaExercitiuWindow()
        {
            InitializeComponent();
        }


        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Introduceți un nume pentru exercițiu!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtSerii.Text, out int serii) || serii <= 0)
            {
                MessageBox.Show("Introduceți un număr valid (pozitiv) de serii!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtRepetari.Text, out int repetari) || repetari <= 0)
            {
                MessageBox.Show("Introduceți un număr valid (pozitiv) de repetări!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var exercitiu = new Exercitiu
            {
                Name = name,
                Serii = serii,
                Repetari = repetari
            };

            using (var context = new AppDbContext())
            {
                context.Exercitii.Add(exercitiu);
                context.SaveChanges();
            }

            this.DialogResult = true;
            this.Close();
        }

    }
}
