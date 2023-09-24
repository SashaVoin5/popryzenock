using popryzenock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CreateNewAgent.xaml
    /// </summary>
    public partial class CreateNewAgent : Page
    {
        public CreateNewAgent()
        {
            InitializeComponent();           
            AgentType.ItemsSource = AgentTypeMas;           
            
            

        }
        public string[] productType { get; set; } =
        {
            "Для маленьких деток",
            "Для больших деток",
            "Взрослый",
            "Цифровой",
            "Упругий"
        };
        public string[] AgentTypeMas { get; set; } =
      {
            
            "МКК",
            "ОАО",
            "ООО",
            "ЗАО",
            "МФО",
            "ПАО"


        };

        private void addAgent(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.AgentTitle.Text == "")
                {
                    MessageBox.Show("Введите имя агента!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                ;
                if (!(new Regex(@"\d{10}|\d{12}")).IsMatch(this.INN.Text))
                {
                    MessageBox.Show("Введите ИНН!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (!(new Regex(@"\d{4}[\dA-Z][\dA-Z]\d{3}")).IsMatch(this.KPP.Text))
                {
                    MessageBox.Show("Введите КПП!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (!(new Regex(@"^\+?\d{0,2}\-?\d{3}\-?\d{3}\-?\d{4}")).IsMatch(this.Phone.Text))
                {
                    MessageBox.Show("Введите телефон!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if ((this.Email.Text != "") && (!(new Regex(@"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)")).IsMatch(this.Email.Text)))
                {
                    MessageBox.Show("Введите электронную почту!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return ;
                };

                string priority = Priority.Text.ToString();
                int Prir = Convert.ToInt32(priority);
                Agent NewAgent = new Agent()
                {
                    Title = AgentTitle.Text.ToString(),
                    Address = Address.Text.ToString(),
                    INN = INN.Text.ToString(),
                    KPP = KPP.Text.ToString(),
                    DirectorName = DirectorName.Text.ToString(),
                    Phone = Phone.Text.ToString(),
                    Priority = Prir,
                    Email = Email.Text.ToString(),
                    AgentTypeID = AgentType.SelectedIndex +1,
                    Logo = logo.Text.ToString(),

                };
                popryzenockEntities.GetContext().Agent.Add(NewAgent);
                popryzenockEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка "+ex,  "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            
            
            

        }
    }
}
