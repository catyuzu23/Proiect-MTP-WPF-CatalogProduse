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
            if (comboExercitii.SelectedItem is not Exercitiu selectat)
            {
                MessageBox.Show("Selectează un exercițiu.");
                return;
            }

            if (!int.TryParse(txtSerii.Text, out int serii) ||
                !int.TryParse(txtRepetari.Text, out int repetari))
            {
                MessageBox.Show("Introdu valori numerice valide!");
                return;
            }

            using var context = new AppDbContext();
            var exercitiu = context.Exercitii.Find(selectat.Id);

            if (exercitiu != null)
            {
                exercitiu.Serii = serii;
                exercitiu.Repetari = repetari;
                context.SaveChanges();
                MessageBox.Show("Modificat cu succes!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Eroare la modificare!");
            }
        }
    }
}
