using popryzenock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace popryzenock.Windows
{
    /// <summary>
    /// Логика взаимодействия для AgentsView.xaml
    /// </summary>
    public partial class AgentsView : Page
    {
        private int start = 0;

        private int fullCount = 0;

        private int order = 0;

        public AgentsView(Frame frame)
        {
            InitializeComponent();
            Load();
            ComboSort.ItemsSource = SortingList;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void revButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Load()
        {
            try
            {
                fullCount = popryzenockEntities.GetContext().Agent.Count();
                if (order == 0) DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.ID).ToList();
                if (order == 1) DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Title).ToList();
                if (order == 2) DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Title).ToList();
                if (order == 3) DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).ToList();
                if (order == 4) DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Priority).ToList();

            }
            catch
            {
                return;
            };

           

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            order = ComboSort.SelectedIndex;            
            Load();


        }
        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "По возрастанию названия",
            "По убыванию названия",
            "По возастанию приоритета",
            "По убыванию приоритета"



        };



    }
}
