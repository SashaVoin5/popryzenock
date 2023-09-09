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

        public AgentsView(Frame frame)
        {
            InitializeComponent();
            Load();
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
            DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.ID).Skip(start * 10).Take(10).ToList();
            //fullCount = popryzenockEntities.GetContext().Agent.Count();
            //full.Text = fullCount.ToString() + " агентов";

        }

    }
}
