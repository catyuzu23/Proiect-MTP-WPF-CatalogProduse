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

    private void IncarcaExercitii()
    {
        using (var context = new AppDbContext())
        {
            var exercitii = context.Exercitii.AsNoTracking().ToList();
            dataGridExercitii.ItemsSource = exercitii;
        }
    }
}