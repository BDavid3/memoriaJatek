





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
        private Button firstButton = null;
        private Button secondButton = null;

        private string firstValue = null;
        private string secondValue = null;

        private bool checkingPair = false;


        private string[,] gameMatrix = null;
        private Dictionary<string, string> CountryCapitalDic = new Dictionary<string, string>
        {
            { "Hungary", "Budapest" },
            { "France", "Paris" },
            { "Germany", "Berlin" },
            { "Italy", "Rome" },
            { "Spain", "Madrid" },
            { "Portugal", "Lisbon" },
            { "Austria", "Vienna" },
            { "Poland", "Warsaw" },
            { "Czech Republic", "Prague" },
            { "Slovakia", "Bratislava" },
            { "Romania", "Bucharest" },
            { "Greece", "Athens" },
            { "United Kingdom", "London" },
            { "Ireland", "Dublin" },
            { "Norway", "Oslo" },
            { "Sweden", "Stockholm" },
            { "Finland", "Helsinki" },
            { "Denmark", "Copenhagen" }
        };

        private List<string> CountryCapitalList = new List<string> {
            "Hungary", "Budapest", "France", "Paris", "Germany", "Berlin", "Italy", "Rome", "Spain", "Madrid", "Portugal", "Lisbon", "Austria", "Vienna", "Poland", "Warsaw", "Czech Republic", "Prague", "Slovakia", "Bratislava",
            "Romania", "Bucharest", "Greece", "Athens", "United Kingdom", "London", "Ireland", "Dublin", "Norway", "Oslo", "Sweden", "Stockholm", "Finland", "Helsinki", "Denmark", "Copenhagen"
        };

        public MainWindow()
        {
            InitializeComponent();
            string[] sizes = { "2x2", "4x4", "6x6" };
            lstbox_size.ItemsSource = sizes;
        }

        private void lstbox_size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gameMatrix = null;
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
                    btn.Content = "?";
                    btn.FontSize = 20;
                    btn.Click += Button_Click;
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    grd_game.Children.Add(btn);
                }
            }

            var rnd = new Random();
            gameMatrix = new string[numb,numb];

            int pairsNeeded = (numb * numb) / 2;
            var randomizedPairs = CountryCapitalDic
            .OrderBy(x => rnd.Next())
            .Take(pairsNeeded)
            .ToList();

            var selectedItems = new List<string>();

            foreach (var pair in randomizedPairs)
            {
                selectedItems.Add(pair.Key);
                selectedItems.Add(pair.Value);
            }
            selectedItems = selectedItems
                .OrderBy(x => rnd.Next())
                .ToList();

            int index = 0;
            for (int i = 0; i < numb; i++)
            {
                for (int j = 0; j < numb; j++)
                {
                    gameMatrix[i, j] = selectedItems[index];
                    index++;
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkingPair)
                return;

            Button clickedButton = sender as Button;

            if (clickedButton == firstButton)
                return;

            int row = Grid.GetRow(clickedButton);
            int column = Grid.GetColumn(clickedButton);

            clickedButton.Content = gameMatrix[row, column];

            if (firstButton == null)
            {
                firstButton = clickedButton;
                firstValue = gameMatrix[row, column];
                return;
            }

            secondButton = clickedButton;
            secondValue = gameMatrix[row, column];

            checkingPair = true;

            bool match =
                (CountryCapitalDic.ContainsKey(firstValue) &&
                 CountryCapitalDic[firstValue] == secondValue)
                ||
                (CountryCapitalDic.ContainsKey(secondValue) &&
                 CountryCapitalDic[secondValue] == firstValue);

            if (!match)
            {
                firstButton.Content = "?";
                secondButton.Content = "?";
            }

            firstButton = null;
            secondButton = null;
            firstValue = null;
            secondValue = null;
            checkingPair = false;
        }



    }
}