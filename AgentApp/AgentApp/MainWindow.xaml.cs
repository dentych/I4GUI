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
using System.Windows.Navigation;
using System.Windows.Shapes;
using I4GUI;
using MvvmFoundation.Wpf;

namespace AgentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Agents agentList = new Agents();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            listbox.DataContext = agentList;
            listSize.Content = "0";
        }

        public void ClickAddNew(object sender, RoutedEventArgs e)
        {
            Agent agent = new Agent();

            agent.ID = "N/A";
            agent.CodeName = "New agent";
            agent.Speciality = "";
            agent.Assignment = "";

            agentList.Add(agent);

            listbox.SelectedItem = agent;

            listSize.Content = agentList.Count.ToString();
        }

        public void ClickNext(object sender, RoutedEventArgs e)
        {
            int currentIndex = listbox.SelectedIndex;

            if (currentIndex < 0) return;

            if (currentIndex < listbox.Items.Count)
            {
                listbox.SelectedIndex = currentIndex + 1;
            }
        }

        public void ClickPrevious(object sender, RoutedEventArgs e)
        {
            int currentIndex = listbox.SelectedIndex;

            if (currentIndex < 0) return;

            if (currentIndex > 0)
            {
                listbox.SelectedIndex = currentIndex - 1;
            }
        }

        private void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textId.Text = "ChangedEvent!";
            ListBox box = sender as ListBox;
            Agent agent = box.SelectedItem as Agent;

            textId.DataContext = agent;
            textCodename.DataContext = agent;
            textSpeciality.DataContext = agent;
            textAssignment.DataContext = agent;
        }
    }
}
