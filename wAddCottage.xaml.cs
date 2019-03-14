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
    /// Interaction logic for wAddCottage.xaml
    /// </summary>
    public partial class wAddCottage : Window
    {
        dbHFFDataContext db = new dbHFFDataContext();
        public wAddCottage()
        {
            InitializeComponent();
            SetData();
        }
        void SetData()
        {
            // Datagrid koppelen aan data van tabel Cottagetype
            dgCottageType.ItemsSource = db.cottagetypes.ToList();
            dgCottages.ItemsSource = db.cottages.ToList();
            dgCottageSize.ItemsSource = db.cottagesizes.ToList();
            cbSize.ItemsSource = db.cottagesizes.ToList();
            cbSize.DisplayMemberPath = "name";
            cbType.ItemsSource = db.cottagetypes.ToList();
            cbType.DisplayMemberPath = "name";
            cbCot.ItemsSource = db.cottages.ToList();
            cbCot.DisplayMemberPath = "house_number";
            cbCottType.ItemsSource = db.cottagetypes.ToList();
            cbCottType.DisplayMemberPath = "name";
            cbCottType.SelectedValuePath = "id";
            cbCottSize.ItemsSource = db.cottagesizes.ToList();
            cbCottSize.DisplayMemberPath = "name";
            cbCottSize.SelectedValuePath = "id";
            /// var list = (from pricehistory in db.pricehistories
            ///               select new (pricehistory.price)
            ///               );

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow myWindow = new MainWindow();
            myWindow.Show();
            this.Close();
        }

        private void btnSaveNewType_Click(object sender, RoutedEventArgs e)
        {
            if (cbType.SelectedItem != null)
            {
                // Het geselecteerde item uit de combobox kan worden gewijzigd.
                cottagetype t = (cottagetype)cbType.SelectedItem;

                t.name = txtTypename.Text;

                MessageBox.Show("The type " + t.name + " has been successfully modified!");

                txtTypename.Text = string.Empty;
                cbType.SelectedItem = null;
                txtTypename.Focus();
              
                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                SetData();
            }
            else if (txtTypename.Text != string.Empty)
            {
                // Er is niks om te veranderen, maar wel iets om op te slaan in de database!
                //Ingevoerde data ophalen
                string sTypename = txtTypename.Text;
                cottagetype cottype = new cottagetype();
                cottype.name = txtTypename.Text;

                // Product klaarzetten om op te slaan in de database
                db.cottagetypes.InsertOnSubmit(cottype);
                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                // Data opnieuw ophalen uit de database (het nieuwe product tonen)
                SetData();

                txtTypename.Text = string.Empty;

                MessageBox.Show("The cottage type " + cottype.name + " has been successfully added!");
                txtTypename.Focus();
            }
            else
            {
                // Er is niks ingevuld om aan te passen in de database
                MessageBox.Show("There is nothing to save");
                txtTypename.Focus();
            }
        }

        private void btnSaveSize_Click(object sender, RoutedEventArgs e)
        {
            if (cbSize.SelectedItem != null)
            {
                // Er kan iets worden gewijzigd in de database
                cottagesize ctz = (cottagesize)cbSize.SelectedItem;

                ctz.name = txtSize.Text;
                ctz.size = Convert.ToInt32(txtSizeCottage.Text);

                MessageBox.Show("The size " + ctz.name + " has been successfully modified!");

                txtSize.Text = string.Empty;
                txtSizeCottage.Text = string.Empty;
                cbSize.SelectedItem = null;
                txtSize.Focus();
                
                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                SetData();
            }
            else if (txtSize.Text != string.Empty && txtSizeCottage.Text != string.Empty)
                // Er wordt iets opgeslagen in de database!!
            {
                //Ingevoerde data ophalen
                string sSize = txtSize.Text;
                cottagesize cotsize = new cottagesize();
                cotsize.name = sSize;
                cotsize.size = Convert.ToInt32(txtSizeCottage.Text);

                // Product klaarzetten om op te slaan in de database
                db.cottagesizes.InsertOnSubmit(cotsize);
                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                // Data opnieuw ophalen uit de database (het nieuwe product tonen)
                SetData();

                txtSize.Text = string.Empty;
                txtSizeCottage.Text = string.Empty;

                MessageBox.Show("The cottage size " + cotsize.name + " has been successfully added!");
                txtSize.Focus();
            }
            else
            {
                // Er is niks ingevuld, waardoor er niks is aan te passen!
                MessageBox.Show("There is nothing to save");
                txtSize.Focus();
            }
        }

        private void btnSelectSize_Click(object sender, RoutedEventArgs e)
        {
            // Controle of er iets is geselecteerd in de ComboBox
            if (cbSize.SelectedItem != null)
            {
                // Size ophalen uit ComboBox
                cottagesize mySize = (cottagesize)cbSize.SelectedItem;

                // Data van de size laten zien in een MessageBox
                MessageBox.Show("Selected size: " + mySize.name);
                txtSize.Text = mySize.name;
                txtSizeCottage.Text = mySize.size.ToString();
                txtSize.Focus();
            }
            else
            {
                // Er is geen size geselecteerd
                MessageBox.Show("You did not select a size!");
            }
        }

        private void btnDeleteSize_Click(object sender, RoutedEventArgs e)
        {
            if (cbSize.SelectedItem != null)
            {
                // De geselecteerde size wordt verwijderd!
                cottagesize cs = (cottagesize)cbSize.SelectedItem;

                db.cottagesizes.DeleteOnSubmit(cs);

                db.SubmitChanges();

                SetData();

                txtSize.Text = string.Empty;
                txtSizeCottage.Text = string.Empty;

                MessageBox.Show($"The size {cs.name} has been successfully removed!");
                txtSize.Focus();
            }
            else
            {
                // Er is geen size geselecteerd
                MessageBox.Show("You did not select a size!");
            }
        }

        private void btnSelectType_Click(object sender, RoutedEventArgs e)
        {
            // Controle of er iets is geselecteerd in de ComboBox
            if (cbType.SelectedItem != null)
            {
                // Type ophalen uit ComboBox
                cottagetype myType = (cottagetype)cbType.SelectedItem;

                // Data van de Type laten zien in een MessageBox
                MessageBox.Show("Selected type: " + myType.name);
                txtTypename.Text = myType.name;
                txtTypename.Focus();
            }
            else
            {
                // Er is geen type geselecteerd!!
                MessageBox.Show("You did not select a type!");
            }
        }

        private void btnDeleteType_Click(object sender, RoutedEventArgs e)
        {
            if (cbType.SelectedItem != null)
            {
                // Het type wat geselecteerd is wordt verwijderd uit de database!!
                cottagetype ct = (cottagetype)cbType.SelectedItem;

                db.cottagetypes.DeleteOnSubmit(ct);

                db.SubmitChanges();

                SetData();

                txtTypename.Text = string.Empty;

                MessageBox.Show($"The type {ct.name} has been successfully removed!");
                txtTypename.Focus();
            }
            else
            {
                // Er is nog geen type geselecteerd om aan te passen!
                MessageBox.Show("You did not select a type!");
            }
        }

        private void btnSelectCottage_Click(object sender, RoutedEventArgs e)
        {
            // Controle of er iets is geselecteerd in de ComboBox
            if (cbCot.SelectedItem != null)
            {
                // Size ophalen uit ComboBox
                cottage myCottage = (cottage)cbCot.SelectedItem;
                
                // Data van de size laten zien in een MessageBox
                MessageBox.Show("Selected cottage: " + myCottage.house_number);
                txtCotnumber.Text = myCottage.house_number;
                cbCottType.SelectedValue = myCottage.typeId;
                cbCottSize.SelectedValue = myCottage.sizeId;
                //txtCotsize.Text = myCottage.sizeId.ToString();

                txtSize.Focus();
            }
            else
            {
                // Er is geen size geselecteerd
                MessageBox.Show("You did not select a cottage!");
            }
        }

        private void btnSaveCottage_Click(object sender, RoutedEventArgs e)
        {
            if (cbCot.SelectedItem != null)
            {
                // Er kan iets worden gewijzigd in de database
                cottage ct = (cottage)cbCot.SelectedItem;

                ct.house_number = txtCotnumber.Text;
                ct.sizeId = ((cottagesize)cbCottSize.SelectedItem).id;
                ct.typeId = ((cottagetype)cbCottType.SelectedItem).id;
                MessageBox.Show("The cottage " + ct.house_number + " has been successfully modified!");

                txtCotnumber.Text = string.Empty;
                txtCotprice.Text = string.Empty;
                cbCottSize.SelectedItem = null;
                cbCottType.SelectedItem = null;
                cbCot.SelectedItem = null;
                txtCotnumber.Focus();

                // Voer de wijzigingen door op de database

                db.SubmitChanges();

                SetData();
            }
            else if (txtCotnumber.Text != string.Empty && cbCottType.SelectedItem != null && cbCottSize != null)
            {
                // Cottage opslaan in database
                cottage cott = new cottage();
                cott.house_number = txtCotnumber.Text;
                cott.cottagetype = (cottagetype)cbCottType.SelectedItem;
                cott.cottagesize = (cottagesize)cbCottSize.SelectedItem;
                
                // Product klaarzetten om op te slaan in de database
                db.cottages.InsertOnSubmit(cott);
                // Voer de wijzigingen door op de database
                db.SubmitChanges();

                // Data opnieuw ophalen uit de database (het nieuwe product tonen)
                SetData();

                txtCotnumber.Text = string.Empty;
                cbCottSize.SelectedItem = null;
                cbCottType.SelectedItem = null;
                txtCotprice.Text = string.Empty;

                MessageBox.Show("The cottage " + cott.house_number + " has been successfully added!");
                txtCotnumber.Focus();
                
                // Niuewe prijs aanmaken
                
                // Prijs opslaan in database    

            }
            else
            {
                // Er is niks ingevuld, waardoor er niks is aan te passen!
                MessageBox.Show("There is nothing to save");
                txtSize.Focus();
            }
            
        }

        private void btnDeleteCott_Click(object sender, RoutedEventArgs e)
        {
            if (cbCot.SelectedItem != null)
            {
                // De geselecteerde size wordt verwijderd!
                cottage cts = (cottage)cbCot.SelectedItem;

                db.cottages.DeleteOnSubmit(cts);

                db.SubmitChanges();

                SetData();

                txtCotnumber.Text = string.Empty;
                cbCottSize.SelectedItem = null;
                cbCottType.SelectedItem = null;

                MessageBox.Show($"The size {cts.house_number} has been successfully removed!");
                txtSize.Focus();
            }
            else
            {
                // Er is geen size geselecteerd
                MessageBox.Show("You did not select a cottage!");
            }
        }
    }
}
