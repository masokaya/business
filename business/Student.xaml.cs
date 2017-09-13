using business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace business {
    /// <summary>
    /// Interaction logic for Student.xaml
    /// </summary>
     [PrincipalPermission(SecurityAction.Demand, Role = "Administrators")]
    public partial class Student : Window, IView
    {
        public Student()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (log.Content.ToString() == "Login")
            {
                if (username.Text == "busarakabuje" && passBox.Password == "babalao")
                {
                    rst.Visibility = Visibility.Visible;
                    ask.Visibility = Visibility.Visible;
                    lbr.Visibility = Visibility.Visible;
                    log.Content = "Logout";
                    username.Visibility = Visibility.Collapsed;
                    ulable.Visibility = Visibility.Collapsed;
                    plable.Visibility = Visibility.Collapsed;
                    passBox.Visibility = Visibility.Collapsed;
                }
            }
            else if(log.Content.ToString() == "Logout")
            {
                username.Text = ""; passBox.Password = "";
                rst.Visibility = Visibility.Collapsed;
                ask.Visibility = Visibility.Collapsed;
                lbr.Visibility = Visibility.Collapsed;
                log.Content = "Login";
                username.Visibility = Visibility.Visible;
                passBox.Visibility = Visibility.Visible;
                ulable.Visibility = Visibility.Visible;
                plable.Visibility = Visibility.Visible;
            }

        }

        private void askButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #region IView Members

        public IViewModel ViewModel
        {
            get
            {
                return DataContext as IViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion
    }
}
