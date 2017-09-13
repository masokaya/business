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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace business.Acountant
{
    /// <summary>
    /// Interaction logic for CashBook.xaml
    /// </summary>
    public partial class CashBook : Window, IView
    {
        public CashBook()
        {
            InitializeComponent();
            Dat.Text = System.DateTime.Today.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            XElement student = new XElement("student");
            student.Add(new XAttribute("Id", fName.Text));
            XElement name = new XElement("Name");
            name.Add(new XElement("firstName", fName.Text));
            name.Add(new XElement("middleName", mName.Text));
            name.Add(new XElement("lastName", lName.Text));
            XElement studentDetail = new XElement("studentDetail");
            studentDetail.Add(new XElement("registrationDate", Dat.Text));
            studentDetail.Add(new XElement("Email", eMail.Text));
            studentDetail.Add(new XElement("phoneNumber", pNumber.Text));
            studentDetail.Add(new XElement("homeAddress", hAddress.Text));
            studentDetail.Add(new XElement("Nationality", country.Text));
            studentDetail.Add(new XElement("Form", form.Text));
            XElement account = new XElement("Account");
            XElement schoolFee = new XElement("schoolFee");
            schoolFee.Add(new XElement("feePayed", fPayed.Text));
            schoolFee.Add(new XElement("feeLeft", fLeft.Text));
            schoolFee.Add(new XElement("feeTotal", fTotal.Text));
            account.Add(new XElement(schoolFee));
            account.Add(new XElement("pocketMoney", pMoney.Text));
            student.Add(new XElement(name));
            student.Add(new XElement(studentDetail));
            student.Add(new XElement(account));
            try
            {
                //string fileName = @"c:\users\masokaya\studentRegistration.xml";
                string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\secondary data\studentRegistration.xml";
                string studentList = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\secondary data\studentNames.xml";
                if (File.Exists(fileName))
                {
                    XDocument doc = XDocument.Load(fileName);
                    doc.Root.AddFirst(student);
                    doc.Save(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                else
                {
                    XDocument doc = new XDocument(
                    new XElement("StudentRegistration",
                    new XElement(student)));
                    doc.Save(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                if (File.Exists(studentList))
                {
                    XDocument doc = XDocument.Load(studentList);
                    XElement st = new XElement("student");
                    st.Add(new XAttribute("Id", fName.Text));
                    st.Add(new XElement(name));
                    st.Add(new XElement("Form", form.Text));
                    doc.Root.AddFirst(st);
                    doc.Save(studentList);
                    System.Diagnostics.Process.Start(studentList);
                }
                else
                {
                    XElement st = new XElement("student");
                    st.Add(new XAttribute("Id", fName.Text));
                    st.Add(new XElement(name));
                    st.Add(new XElement("Form", form.Text));
                    XDocument doc = new XDocument(
                    new XElement("StudentList",
                    new XElement(st)));
                    doc.Save(studentList);
                    System.Diagnostics.Process.Start(studentList);
                }
                fName.Text = "";
                mName.Text = ""; eMail.Text = "";
                lName.Text = ""; fPayed.Text = ""; fTotal.Text = "";
                hAddress.Text = ""; pNumber.Text = ""; pMoney.Text = ""; fLeft.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void Unpayed_Click(object sender, RoutedEventArgs e)
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\secondary data\studentRegistration.xml";
            if (File.Exists(fileName))
            {

                XDocument doc = XDocument.Load(fileName);
                IList<UnpayedStudentsDisplayModel> list = new List<UnpayedStudentsDisplayModel>();
                IEnumerable<XElement> student = doc.Elements("feeLeft");
                foreach (XElement x in doc.Descendants())
                {
                    if (x.Name == "feeLeft")
                    {
                        int tem = Convert.ToInt32(x.Value);
                        if (tem > 50000)
                        {
                            var temp = x.Parent.Parent.Parent.Descendants("Name");
                            string s = "";
                            foreach (XElement i in temp)
                            {
                                if (i.HasElements)
                                {
                                    IEnumerable<XElement> xel = i.Elements();
                                    foreach (XElement h in xel)
                                    {
                                        s += h.Value + " ";
                                    }
                                    list.Add(new UnpayedStudentsDisplayModel() { Name = s, Amount = tem });
                                }
                            }
                        }
                    }
                }
                result.ItemsSource = list;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var pd = new PrintDialog();
            var res = pd.ShowDialog();
            if (res.HasValue && res.Value)
                pd.PrintVisual(result, "Student who have not Payed");
        }
    }
}
