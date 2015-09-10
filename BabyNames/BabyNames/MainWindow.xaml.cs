using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<BabyName>[] yearList = new List<BabyName>[11];

        public MainWindow()
        {
            InitializeComponent();
            InitializeYearList();
        }

        private void InitializeYearList()
        {
            for (int i = 0; i < 11; i++)
            {
                yearList[i] = new List<BabyName>();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT files (*.txt)|*.txt";

            var result = dlg.ShowDialog();

            if (result != true)
            {
                Close();
                return;
            }

            var fileName = dlg.FileName;

            StreamReader file = new StreamReader(fileName);

            string line;
            while ((line = file.ReadLine()) != null)
            {
                BabyName baby = new BabyName(line);
                babynames.Add(baby);
            }

            FillTopLists();

            SortLists();
        }

        private void FillTopLists()
        {
            foreach (var baby in babynames)
            {
                for (int i = 0; i < 11; i++)
                {
                    int babyRank = baby.Rank(1900 + (10 * i));
                    if (babyRank <= 10 && babyRank > 0)
                    {
                        yearList[i].Add(baby);
                    }
                }
            }
        }

        private void SortLists()
        {
            // Sorter hver liste
            for (int i = 0; i < yearList.Length; i++)
            {
                var list = yearList[i];
                var pivot = list.Count;
                var year = 1900 + (10 * i);
                // For hvert element, kør selection sort
                while (pivot > 0)
                {
                    // Variabel til baby med mindste rank
                    var baby = list.First();
                    // Kør listen af babyer igennem, find mindste.
                    for (int j = 1; j < pivot; j++)
                    {
                        var curBaby = list[j];
                        if (curBaby.Rank(year) < baby.Rank(year))
                        {
                            baby = curBaby;
                        }
                    }

                    list.Remove(baby);
                    list.Add(baby);
                    pivot--;
                }
            }
        }

        private void Select_Decade(object sender, MouseButtonEventArgs e)
        {
            Stopwatch watch = Stopwatch.StartNew();

            ListBoxItem item = sender as ListBoxItem;
            var year = int.Parse(item.Content.ToString());
            var index = (year - 1900) / 10;
            var list = yearList[index];
            var namesPerRank = 2;
            var currentNames = 0;
            var output = "";

            babyNamesList.Items.Clear();
            foreach (var baby in list)
            {
                // New list
                if (currentNames == 0)
                {
                    output = "";
                    output += baby.Rank(year) + ". ";
                    output += baby.Name;
                    currentNames++;
                }
                else
                {
                    output += " and ";
                    output += baby.Name;
                    currentNames++;
                }

                if (currentNames != namesPerRank) continue;

                babyNamesList.Items.Add(output);
                currentNames = 0;
            }

            watch.Stop();
            labelQueryTime.Content = string.Format("{0:0.00} us", (watch.Elapsed.TotalMilliseconds * 1000));
        }

        private void Button_Search(object sender, RoutedEventArgs routedEventArgs)
        {
            var watch = Stopwatch.StartNew();

            var searchString = searchName.Text;

            avgRank.Clear();
            trendBox.Clear();
            listRankAndYear.Items.Clear();

            if (searchString.Length <= 0) return;

            var baby = SearchDB(searchString);

            if (baby == null)
            {
                listRankAndYear.Items.Add("No entries for that name!");
                watch.Stop();
                labelQueryTime.Content = string.Format("{0:0.00} us", (watch.Elapsed.TotalMilliseconds * 1000));
                return;
            }

            for (int i = 0; i < 11; i++)
            {
                var year = (1900 + (10 * i));
                var rank = (baby.Rank(year) > 0 ? baby.Rank(year).ToString() : "1000+");
                string output = "";
                output += year + "\t\t" + rank;
                listRankAndYear.Items.Add(output);
            }

            avgRank.Text = baby.AverageRank().ToString();

            int trend = baby.Trend();
            if (trend > 0)
            {
                trendBox.Text = "More popular!";
            }
            else if (trend < 0)
            {
                trendBox.Text = "Less popular :(";
            }
            else
            {
                trendBox.Text = "Samefagging";
            }

            watch.Stop();
            labelQueryTime.Content = string.Format("{0:0.00} us", (watch.Elapsed.TotalMilliseconds * 1000));
        }

        private BabyName SearchDB(string searchString)
        {
            foreach (var baby in babynames)
            {
                if (baby.Name == searchString) return baby;
            }

            return null;
        }

        private void MenuItem_ChangeFontSize(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;

            if (item.Header.ToString() == "Small")
                MainWin.FontSize = (double) new FontSizeConverter().ConvertFrom("8pt");
            else if (item.Header.ToString() == "Medium")
                MainWin.FontSize = (double)new FontSizeConverter().ConvertFrom("9pt");
            else
                MainWin.FontSize = (double)new FontSizeConverter().ConvertFrom("10pt");
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
