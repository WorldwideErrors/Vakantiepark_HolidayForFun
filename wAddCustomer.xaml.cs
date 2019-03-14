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
using System.Windows.Shapes;

namespace HolidayForFun
{
    /// <summary>
    /// Interaction logic for wAddCustomer.xaml
    /// </summary>
    public partial class wAddCustomer : Window
    {
        dbHFFDataContext db = new dbHFFDataContext();
        public wAddCustomer()
        {
            InitializeComponent();
            SetData();
        }

        void SetData()
        {
            // Datagrid koppelen aan data van tabel Cottagetype
            dgCustomer.ItemsSource = db.customers.ToList();
            cbWijzigKlant.ItemsSource = db.customers.ToList();
            cbWijzigKlant.DisplayMemberPath = "lastname";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow myWindow = new MainWindow();
            myWindow.Show();
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbWijzigKlant.SelectedItem != null)
            {
                customer c = (customer)cbWijzigKlant.SelectedItem;

                c.firstname = txtFirstname.Text;
                c.lastname = txtLastname.Text;
                c.adress = txtAdress.Text;
                c.city = txtCity.Text;
                c.phonenumber = txtPhonenumber.Text;
                c.email = txtEmail.Text;
                c.birthday = dpBirthday.SelectedDate.Value;
                MessageBox.Show("De klant " + c.firstname + " " + c.lastname + " has been successfully modified!");

                txtFirstname.Text = string.Empty;
                txtLastname.Text = string.Empty;
                txtAdress.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhonenumber.Text = string.Empty;
                dpBirthday.SelectedDate = null;
                txtFirstname.Focus();
                cbWijzigKlant.SelectedItem = null;

                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                SetData();
            }
            else if (txtFirstname.Text != string.Empty && txtLastname.Text != string.Empty && txtAdress.Text != string.Empty && txtCity.Text != string.Empty && txtEmail.Text != string.Empty && txtPhonenumber.Text != string.Empty && dpBirthday.SelectedDate != null)
            {
                string sFirstname = txtFirstname.Text;
                string sLastname = txtLastname.Text;
                string sAdress = txtAdress.Text;
                string sCity = txtCity.Text;
                string sEmail = txtEmail.Text;
                string sPhone = txtPhonenumber.Text;

                customer cus = new customer();
                cus.firstname = txtFirstname.Text;
                cus.lastname = txtLastname.Text;
                cus.adress = txtAdress.Text;
                cus.city = txtCity.Text;
                cus.email = txtEmail.Text;
                cus.phonenumber = txtPhonenumber.Text;
                cus.birthday = dpBirthday.SelectedDate.Value;

                // Product klaarzetten om op te slaan in de database
                db.customers.InsertOnSubmit(cus);
                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                // Data opnieuw ophalen uit de database (het nieuwe product tonen)
                SetData();

                txtFirstname.Text = string.Empty;
                txtLastname.Text = string.Empty;
                txtAdress.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhonenumber.Text = string.Empty;
                dpBirthday.SelectedDate = null;

                MessageBox.Show("Customer " + cus.firstname + " " + cus.lastname + " has been successfully added!");
                txtFirstname.Focus();
            }
            else
            {
                MessageBox.Show("There is nothing to save");
                txtFirstname.Focus();
            }
        }
    
        private void btnWijzigKlant_Click(object sender, RoutedEventArgs e)
        {
            // Controle of er iemand is geselecteerd in de ComboBox
            if (cbWijzigKlant.SelectedItem != null)
            {
                // Student ophalen uit ComboBox
                customer myCustomer = (customer)cbWijzigKlant.SelectedItem;

                // Data van de student laten zien in een MessageBox
                MessageBox.Show("Selected customer: " + myCustomer.firstname + " " + myCustomer.lastname);
                txtFirstname.Text = myCustomer.firstname;
                txtLastname.Text = myCustomer.lastname;
                txtAdress.Text = myCustomer.adress;
                txtCity.Text = myCustomer.city;
                txtPhonenumber.Text = myCustomer.phonenumber;
                txtEmail.Text = myCustomer.email;
                dpBirthday.SelectedDate = myCustomer.birthday;
            }
            else
            {
                    MessageBox.Show("You did not select a customer!");
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (cbWijzigKlant.SelectedItem != null)
            {
                customer c = (customer)cbWijzigKlant.SelectedItem;

                db.customers.DeleteOnSubmit(c);

                db.SubmitChanges();

                SetData();

                txtFirstname.Text = string.Empty;
                txtLastname.Text = string.Empty;
                txtAdress.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhonenumber.Text = string.Empty;
                dpBirthday.SelectedDate = null;

                MessageBox.Show($"De klant {c.firstname} {c.lastname} has been successfully removed!");
            }
            else
            {
                MessageBox.Show("You did not select a customer!");
            }
        }
    }
}
