using System;
using System.Collections.Generic;
using System.Linq;
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
using popryzenock.Model;


namespace popryzenock.Windows
{
    /// <summary>
    /// Логика взаимодействия для editAgent.xaml
    /// </summary>
    public partial class editAgent : Page
    {
        Agent ag;
        private int curSelPr;

        public editAgent(Agent agent)
        {
            InitializeComponent();
            try
            {
                var prod = popryzenockEntities.GetContext().Product;
                AgentType.ItemsSource = AgentTypeMas;
                foreach (var item in prod)
                {
                    TypeProduct.Items.Add(item.Title.ToString());
                }

               
                ag = agent;
            }
            catch { };
            if (ag != null)
            {
                var AgType = popryzenockEntities.GetContext().AgentType.Where(p => p.ID == ag.AgentTypeID).FirstOrDefault();
                AgentType.Text = AgType.Title;
                AgentTitle.Text = ag.Title;
                Address.Text = ag.Address;
                INN.Text = ag.INN;
                KPP.Text = ag.KPP;
                DirectorName.Text = ag.DirectorName;
                Phone.Text = ag.Phone;
                Priority.Text = ag.Priority.ToString();
                Email.Text = ag.Email;
                
                historyGrid.ItemsSource = popryzenockEntities.GetContext().ProductSale.Where(ProductSale => ProductSale.AgentID == ag.ID).ToList();
            }
            else
            {
                agent = new Agent();
               
            }
            

        }
       
        public string[] AgentTypeMas { get; set; } =
      {

            "МКК",
            "ОАО",
            "ООО",
            "ЗАО",
            "МФО",
            "ПАО"


        };

        private void mask_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            string fnd = ((TextBox)sender).Text;
            try
            {
                TypeProduct.ItemsSource = popryzenockEntities.GetContext().Product.Where(Product => Product.Title.Contains(fnd)).ToList();
            }
            catch { };
            */

        }

        

        private void addSale(object sender, RoutedEventArgs e)
        {
            int cnt = 0;
            try
            {
                cnt = Convert.ToInt32(mask.Text);
            }
            catch
            {
                return;
            }
            string dt = date.ToString();
            curSelPr = TypeProduct.SelectedIndex + 1;
            if (curSelPr > 0 && dt != "" && cnt > 0)
            {
                ProductSale pr = new ProductSale();
                pr.AgentID = ag.ID;
                pr.ProductID = curSelPr;
                pr.SaleDate = (DateTime)date.SelectedDate;
                pr.ProductCount = cnt;
                try
                {
                    popryzenockEntities.GetContext().ProductSale.Add(pr);
                    popryzenockEntities.GetContext().SaveChanges();
                    historyGrid.ItemsSource = popryzenockEntities.GetContext().ProductSale.Where(ProductSale => ProductSale.AgentID == ag.ID).ToList();
                }
                catch
                {
                    return;
                }
            }

        }

        private void DeleteSale(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < historyGrid.SelectedItems.Count; i++)
            {
                ProductSale prs = historyGrid.SelectedItems[i] as ProductSale;
                if (prs != null)
                {
                    popryzenockEntities.GetContext().ProductSale.Remove(prs);
                }
            }
            try
            {
                popryzenockEntities.GetContext().SaveChanges();
                historyGrid.ItemsSource = popryzenockEntities.GetContext().ProductSale.Where(ProductSale => ProductSale.AgentID == ag.ID).ToList();
            }
            catch { return; };

        }

        private void EditData(object sender, RoutedEventArgs e)
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
            if (!(new Regex(@"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)")).IsMatch(Email.Text))
            {
                MessageBox.Show("Введите электронную почту!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (Email.Text == "")
            {
                MessageBox.Show("Введите электронную почту!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            
            ag.Title = AgentTitle.Text;
            ag.Address = Address.Text;
            ag.INN = INN.Text;
            ag.KPP = KPP.Text;
            ag.DirectorName = DirectorName.Text;
            ag.Phone = Phone.Text;
            ag.Priority = Convert.ToInt32(Priority.Text);
            ag.Email = Email.Text;
            ag.AgentTypeID = AgentType.SelectedIndex + 1;


           
            popryzenockEntities.GetContext().SaveChanges();
            MessageBox.Show("Данные успешно изменены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();

        }
    }
}

