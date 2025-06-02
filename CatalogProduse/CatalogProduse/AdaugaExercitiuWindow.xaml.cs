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
            bool seriiOk = int.TryParse(txtSerii.Text, out int serii);
            bool repetariOk = int.TryParse(txtRepetari.Text, out int repetari);

            if (string.IsNullOrWhiteSpace(name) || !seriiOk || !repetariOk)
            {
                MessageBox.Show("Completează toate câmpurile corect!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
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
