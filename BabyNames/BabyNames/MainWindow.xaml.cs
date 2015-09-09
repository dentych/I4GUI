using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BabyNames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BabyName> babynames = new List<BabyName>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader file = new StreamReader("../../babynames.txt");

            string line;
            while ((line = file.ReadLine()) != null)
            {
                BabyName baby = new BabyName(line);
                babynames.Add(baby);
            }

            foreach (var baby in babynames)
            {
                int babyRank = baby.Rank(1900);

                if (babyRank <= 10 && babyRank != 0)
                {
                    string str = baby.Name + "\t\t" + baby.Rank(1900);
                    babyNamesList.Items.Add(str);
                }
            }
        }
    }
}
