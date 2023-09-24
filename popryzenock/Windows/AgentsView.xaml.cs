using popryzenock.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
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

        private string fnd = "";

        private int Filter = -1;

        public AgentsView(Frame frame)
        {
            InitializeComponent();
            Load();
            ComboSort.ItemsSource = SortingList;
            ComboSort.Text = "Без сортировки";
            ComboFilt.ItemsSource = FiltList;
            ComboFilt.Text = "Без фильтрации";
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataView.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите агента для продолжения", "Внимание",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                var Ag = DataView.SelectedItem as Agent;
                NavigationService.Navigate(new editAgent(Ag));
                Load();
            }
          
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataView.SelectedIndex == -1)
            {
                MessageBox.Show("Выделите строку для удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if ((DataView.SelectedIndex != -1))
            {
                try
                {
                    if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        
                        var idi = (DataView.SelectedItem as Agent).ID;
                        var AgentSale = popryzenockEntities.GetContext().ProductSale.Where(p => p.AgentID == idi).Count();
                        if (AgentSale > 0)
                        {
                            if (MessageBox.Show("Есть информация о продаже агента, удалить ее?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes);
                            {
                                var SaleAgentDelete = popryzenockEntities.GetContext().ProductSale.Where(p => p.AgentID == idi).FirstOrDefault();

                                popryzenockEntities.GetContext().ProductSale.Remove(SaleAgentDelete);
                                popryzenockEntities.GetContext().SaveChanges();
                                MessageBox.Show("Запись о продаже удалена", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        var AgentDelete = popryzenockEntities.GetContext().Agent.Where(p => p.ID == idi).FirstOrDefault();

                        popryzenockEntities.GetContext().Agent.Remove(AgentDelete);
                        popryzenockEntities.GetContext().SaveChanges();
                        MessageBox.Show("Агент удален", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления (возможно есть еще одна запись о продаже, повторите попытку", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNewAgent());

            
        }

        private void revButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Load()
        {
            fullCount = popryzenockEntities.GetContext().Agent.Count();
            full.Text = fullCount.ToString();

            int ost = fullCount % 10;
            int pag = (fullCount - ost) / 10;
            if (ost > 0) pag++;
            pagin.Children.Clear();
            for (int i = 0; i < pag; i++)
            {
                Button myButton = new Button();
                myButton.Height = 30;
                myButton.Content = i + 1;
                myButton.Width = 20;
                myButton.HorizontalAlignment = HorizontalAlignment.Center;
                myButton.Tag = i;
                myButton.Click += new RoutedEventHandler(paginButto_Click); ;
                pagin.Children.Add(myButton);
            }
            turnButton();


            try
            {

                if (order == 0)
                {


                        var countData1 = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.ID).ToList();
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.ID).ToList().Skip(start * 10).Take(10);
                        var countData = countData1.Count();
                        full.Text = countData.ToString();
                   


                }
                if (order == 1)
                {
                    if (Filter > 0)
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).OrderBy(Agent => Agent.Title).ToList().Skip(start * 10).Take(10);
                        
                        var countData = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).Count();
                        full.Text = countData.ToString();
                       
                    }
                    else
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Title).ToList().Skip(start * 10).Take(10);
                        var countData = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Title).Count();
                        full.Text = countData.ToString();
                       
                        
                    }

                }
                if (order == 2)
                {
                    if (Filter > 0)
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).OrderByDescending(Agent => Agent.Title).ToList().Skip(start * 10).Take(10);
                        
                        var countData = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).Count();
                        full.Text = countData.ToString();
                        
                    }
                    else
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Title).ToList().Skip(start * 10).Take(10);
                        var countData = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Title).Count();
                        full.Text = countData.ToString();
                        
                    }

                }
                if (order == 3)
                {
                    if (Filter > 0)
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).OrderBy(Agent => Agent.Priority).ToList().Skip(start * 10).Take(10);
                        
                        var countData = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).Count();
                        full.Text = countData.ToString();
                        
                    }
                    else
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).ToList().Skip(start * 10).Take(10);
                        var countData = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).Count();
                        full.Text = countData.ToString();
                        
                    }

                }
                if (order == 4)
                {
                    if (Filter > 0)
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).OrderByDescending(Agent => Agent.Priority).ToList().Skip(start * 10).Take(10);
                        //Рабочий момент
                        var countData = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).Count();
                        full.Text = countData.ToString();
                        
                        

                       


                    }
                    else
                    {
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Priority).ToList().Skip(start * 10).Take(10);
                        var countData = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Priority).Count();
                        full.Text = countData.ToString();
                        

                    }

                    




                    





                }
                if (order == 0 && Filter > 0)
                {
                    
                        DataView.ItemsSource = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).ToList().Skip(start * 10).Take(10);
                        Filter = -1;
                        var countData = popryzenockEntities.GetContext().Agent.Where(p => p.AgentTypeID == Filter).Count();
                        full.Text = countData.ToString();
                   

                }
                
                if (fnd != "")
                {
                    if (order == 1)
                    {
                        var serachCount = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Title).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).Count();
                        var serach = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Title).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList();
                        DataView.ItemsSource = serach;
                        full.Text = serachCount.ToString();
                        if (Filter > 0)
                        {
                            var data = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Title).Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);

                            var countData = data.Count();
                            full.Text = countData.ToString();
                        }


                    }
                    if (order == 2)
                    {
                        var serachCount = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Title).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).Count();
                        var serach = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Title).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList();
                        DataView.ItemsSource = serach;
                        full.Text = serachCount.ToString();
                        if (Filter > 0)
                        {
                            var data = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Title).Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            
                            var countData = data.Count();
                            full.Text = countData.ToString();
                        }

                    }
                    if (order == 3)
                    {
                        var serachCount = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).Count();
                        var serach = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                        DataView.ItemsSource = serach;
                        full.Text = serachCount.ToString();
                        
                        if (Filter > 0)
                        {
                            var data = popryzenockEntities.GetContext().Agent.Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderBy(Agent => Agent.Priority).Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            
                            var countData = data.Count();
                            full.Text = countData.ToString();
                        }

                    }
                    if (order == 4)
                    {
                        var serachCount = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Priority).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).Count();
                        var serach = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Priority).Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            DataView.ItemsSource = serach;
                        full.Text = serachCount.ToString();
                        if (Filter > 0)
                        {
                            var data = popryzenockEntities.GetContext().Agent.Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            DataView.ItemsSource = popryzenockEntities.GetContext().Agent.OrderByDescending(Agent => Agent.Priority).Where(Agent => Agent.AgentTypeID == Filter && Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList().Skip(start * 10).Take(10);
                            
                            var countData = data.Count();
                            full.Text = countData.ToString();
                        }

                    }
                    if (order == 0)
                    {
                        var agCount = popryzenockEntities.GetContext().Agent.Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).Count();
                        var ag = popryzenockEntities.GetContext().Agent.Where(Agent => Agent.Title.Contains(fnd) || Agent.Phone.Contains(fnd) || Agent.Email.Contains(fnd)).ToList();
                        DataView.ItemsSource = ag;
                        full.Text = agCount.ToString();
                    }
                           
                }
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
        private void turnButton()
        {
            if (start == 0) 
            { 
                back.IsEnabled = false; 
            }
            else 
            { 
                back.IsEnabled = true; 
            };
            if ((start + 1) * 10 > fullCount) 
            { 
                forward.IsEnabled = false;
            }
            else 
            { 
                forward.IsEnabled = true; 
            };


        }

        private void paginButto_Click(object sender, RoutedEventArgs e)
        {
            start = Convert.ToInt32(((Button)sender).Tag.ToString());
            Load();

        }

        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "По возрастанию названия",
            "По убыванию названия",
            "По возрастанию приоритета",
            "По убыванию приоритета"



        };
        public string[] FiltList { get; set; } =
       {
            "Без фильтрации",
            "МКК",
            "ОАО",
            "ООО",
            "ЗАО",
            "МФО",
            "ПАО"



        };

        private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter = ComboFilt.SelectedIndex;
            Load();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            fnd = Search.Text;
            Load();

        }

        private void agent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataView.SelectedItems.Count > 0)
            {
                Agent agent = DataView.SelectedItem as Agent;

                if (agent != null)
                {
                    NavigationService.Navigate(new editAgent(agent));
                }
            }

        }

        private void frame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                AgentsView PG = (AgentsView)e.Content;
                PG.Load();
            }
            catch
            {

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            start--;
            Load();

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            start++;
            Load();

        }
    }
}
