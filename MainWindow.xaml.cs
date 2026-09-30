





using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace memoriaJatek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] sizes = { "2x2", "4x4", "6x6" };
            lstbox_size.ItemsSource = sizes;
        }

        private void lstbox_size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grd_game.Children.Clear();
            grd_game.RowDefinitions.Clear();
            grd_game.ColumnDefinitions.Clear();

            string selectedSize = lstbox_size.SelectedItem as string;
            int numb = int.Parse(selectedSize.Split('x')[0]);
            for (int i = 0; i < numb; i++)
            {
                grd_game.RowDefinitions.Add(new RowDefinition());
                grd_game.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < numb; i++)
            {
                for (int j = 0; j < numb; j++)
                {
                    Button btn = new Button();
                    btn.Content = "X";
                    btn.FontSize = 20;
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    grd_game.Children.Add(btn);
                }
            }

        }
    }
}