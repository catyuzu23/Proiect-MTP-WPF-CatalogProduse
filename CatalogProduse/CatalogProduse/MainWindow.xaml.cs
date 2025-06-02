using CatalogProduse.Database;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;


namespace CatalogProduse;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        IncarcaExercitii();
    }

    private List<Exercitiu> toateExercitiile = new();

    private void IncarcaExercitii()
    {
        //using (var context = new AppDbContext())
        //{
        //    var exercitii = context.Exercitii.AsNoTracking().ToList();
        //    dataGridExercitii.ItemsSource = exercitii;
        //}

        using (var context = new AppDbContext())
        {
            toateExercitiile = context.Exercitii.AsNoTracking().ToList();
            dataGridExercitii.ItemsSource = toateExercitiile;
        }

    }

    private void BtnAdaugaExercitiu_Click(object sender, RoutedEventArgs e)
    {
        var fereastra = new AdaugaExercitiuWindow();
        fereastra.Owner = this;

        if (fereastra.ShowDialog() == true)
        {
            IncarcaExercitii(); // Refresh după adăugare
        }
    }

    private void BtnIesire_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown(); // închide aplicația complet
    }

    private void txtCautare_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = txtCautare.Text.Trim().ToLower();

        var rezultate = toateExercitiile
            .Where(x => x.Name.ToLower().Contains(text))
            .ToList();

        dataGridExercitii.ItemsSource = rezultate;
    }

    private void BtnModificaExercitiu_Click(object sender, RoutedEventArgs e)
    {
        var fereastra = new ModificaExercitiuWindow();
        fereastra.Owner = this;

        if (fereastra.ShowDialog() == true)
        {
            IncarcaExercitii(); // refresh după modificare
        }
    }

    private void BtnStergeExercitiu_Click(object sender, RoutedEventArgs e)
    {
        if (dataGridExercitii.SelectedItem is not Exercitiu exercitiuSelectat)
        {
            MessageBox.Show("Selectează un exercițiu din listă pentru a-l șterge.");
            return;
        }

        var confirmare = MessageBox.Show(
            $"Sigur vrei să ștergi exercițiul \"{exercitiuSelectat.Name}\"?",
            "Confirmare ștergere",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirmare == MessageBoxResult.Yes)
        {
            using var context = new AppDbContext();
            var exercitiu = context.Exercitii.Find(exercitiuSelectat.Id);

            if (exercitiu != null)
            {
                context.Exercitii.Remove(exercitiu);
                context.SaveChanges();
                MessageBox.Show("Exercițiul a fost șters cu succes.");
                IncarcaExercitii(); // Refresh la listă
            }
            else
            {
                MessageBox.Show("Eroare: nu s-a găsit exercițiul în baza de date.");
            }
        }
    }



}