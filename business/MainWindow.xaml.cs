
using business.Acountant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace business
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //[PrincipalPermission(SecurityAction.Demand)]
    public partial class MainWindow : Window, IView
    {
        public MainWindow(AuthenticationViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        
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

        Window s = new Window();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((alowed.Text.ToLower()== "Not authenticated!"))
            {
                //myView.Content = home;
            }
        }
         public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }
        private void Student(object sender, RoutedEventArgs e)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                    if (IsAuthenticated)
                    {
                        
                        s = new Student();
                     
                        basicView.Content = s.Content;
                    }
                    
            }
          
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            s.Close();
            var ba = App.Current;
            Application.Current.Shutdown();
        }
        

        // Window a = new Window();
        private void back(object sender, RoutedEventArgs e)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                    if (IsAuthenticated)
                    {
                        s = new CashBook();
                        basicView.Content = s.Content;
                    }
                    
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            DoubleAnimation da = new DoubleAnimation
             (360, 0, new Duration(TimeSpan.FromSeconds(3)));
            TranslateTransform my = new TranslateTransform();
            RotateTransform rt = new RotateTransform();
            image1.RenderTransform = rt;
            image1.RenderTransformOrigin = new Point(0.5, 0.5);
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
            myL.RenderTransform = my;
            DoubleAnimation wao = new DoubleAnimation
             (0, this.Width, new Duration(TimeSpan.FromSeconds(3)));
            wao.RepeatBehavior = RepeatBehavior.Forever;
            my.BeginAnimation(TranslateTransform.XProperty, wao);
        }
    }
}
