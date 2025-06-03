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
using Microsoft.EntityFrameworkCore;


namespace CatalogProduse
{
   
    public partial class ModificaExercitiuWindow : Window
    {
        private List<Exercitiu> toateExercitiile;

        public ModificaExercitiuWindow()
        {
            InitializeComponent();
            IncarcaExercitii();
        }

        private void IncarcaExercitii()
        {
            using var context = new AppDbContext();
            toateExercitiile = context.Exercitii.AsNoTracking().ToList();
            comboExercitii.ItemsSource = toateExercitiile;
        }

        private void comboExercitii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboExercitii.SelectedItem is Exercitiu exercitiu)
            {
                txtSerii.Text = exercitiu.Serii.ToString();
                txtRepetari.Text = exercitiu.Repetari.ToString();
            }
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            if (comboExercitii.SelectedItem == null)
            {
                MessageBox.Show("Selectați un exercițiu din listă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            var exercitiu = comboExercitii.SelectedItem as Exercitiu;

            using (var context = new AppDbContext())
            {
                var ex = context.Exercitii.FirstOrDefault(e => e.Id == exercitiu.Id);
                if (ex != null)
                {
                    ex.Serii = serii;
                    ex.Repetari = repetari;
                    context.SaveChanges();
                }
            }

            this.DialogResult = true;
            this.Close();
        }


    }
}
