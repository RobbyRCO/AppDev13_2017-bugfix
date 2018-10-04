using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Internship.Data.DomainClasses;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Windows.Forms;
using Internship.Data;
using Internship.Data.Repositories;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Internship.Desktop.Data;
using Internship.Api.Models;

namespace Internship.Desktop
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private string FileToImportPath;
        private ObservableCollection<ComboBoxItem> FileTypes = new ObservableCollection<ComboBoxItem>();
        private ParseService parseService = new ParseService(new StagevoorstelDBRepository(ApplicationDbContext.Create()),
            new BedrijfDBRepository(ApplicationDbContext.Create()), new StageopdrachtenDbRepository(ApplicationDbContext.Create()),
            new LectorDBRepository(ApplicationDbContext.Create()), new StudentDBRepository(ApplicationDbContext.Create()));
        private readonly HttpClient _client = new HttpClient();
        private Bedrijf _geselecteerdBedrijf;

        public HomePage(string token)
        {
            InitializeComponent();
            _client.BaseAddress = new Uri(TokenRetriever.baseUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            this.Loaded += HomePage_Loaded;
            SetupTypeComboBox();
        }

        private void SetupTypeComboBox()
        {
            FileTypes.Add(AddTypeComboBoxItem("Selecteer type", typeof(object)));
            FileTypes.Add(AddTypeComboBoxItem("Stagevoorstellen", typeof(OudStagevoorstel)));
            FileTypes.Add(AddTypeComboBoxItem("Stages", typeof(OudeStage)));
            TypeComboBox.ItemsSource = FileTypes;
            TypeComboBox.SelectedIndex = 0;
        }

        private ComboBoxItem AddTypeComboBoxItem(string message, Type type)
        {
            return new ComboBoxItem
            {
                Content = message,
                DataContext = type
            };
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListRegistratieBedrijven();
        }

        private void PickFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog {Filter = "Excel Files|*.tsv;*.csv"};
            if (!(bool) fileDialog.ShowDialog()) return;
            FileToImportPath = fileDialog.FileName;
            FilePathTextBox.Text = FileToImportPath;
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var type = (Type)((ComboBoxItem)TypeComboBox.SelectedItem).DataContext;
            List<string> lijnen = null;
            try
            {
                lijnen = CsvReader.Read(FileToImportPath).ToList();
                lijnen.RemoveAt(0);
            }
            catch (FormatException ex)
            {
                SetStatusMessage(ex.Message, Brushes.Red);
            }
            catch (ArgumentNullException ex)
            {
                SetStatusMessage(ex.Message, Brushes.Red);
                return;
            }

            if (type == typeof(OudStagevoorstel))
            {
                List<OudStagevoorstel> list = new List<OudStagevoorstel>();
                foreach (var lijn in lijnen)
                {
                    try { list.Add(ParseService.ParseOudStageVoorstel(lijn.Split(ParseService.DELIMITER))); }
                    catch (FormatException ex) { SetStatusMessage(ex.Message, Brushes.Orange); }
                }
                ImportedFileDataGrid.DataContext = list;
                RowCountLabel.Content = $"Rijen: {list.Count}";
                foreach (var element in list)
                {
                    await parseService.InsertOudStageVoorstel(element);
                }
            }
            if (type == typeof(OudeStage))
            {
                List<OudeStage> list = new List<OudeStage>();
                foreach (var lijn in lijnen)
                {
                    try { list.Add(ParseService.ParseOudeStage(lijn.Split(ParseService.DELIMITER))); }
                    catch (FormatException ex) { SetStatusMessage(ex.Message, Brushes.Orange); }
                }
                ImportedFileDataGrid.DataContext = list;
                RowCountLabel.Content = $"Rijen: {list.Count}";
            }
        }

        private void SetStatusMessage(string message, SolidColorBrush color)
        {
            StatusLabel.Content = message;
            StatusLabel.Foreground = color;
        }

        private async void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_geselecteerdBedrijf == null) return;
            if (MessageBox.Show(
                    "Bent u zeker dat u de registratie van '" + _geselecteerdBedrijf.Bedrijfsnaam +
                    "' wilt goedkeuren?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                MessageBoxResult.No) return;
            //Yes
            try
            {
                string id = _geselecteerdBedrijf.UserAccountId;
                var response = await _client.GetAsync("/Account/ConfirmEmailManually/" + id);
                response.EnsureSuccessStatusCode(); // Throw on error code.  
                RefreshListRegistratieBedrijven();
                MessageBox.Show("Registratie succesvol");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bedrijf niet goedgekeurd " + ex.Message);
            }
        }

        private async void RefreshListRegistratieBedrijven()
        {
            List<Bedrijf> bedrijven = await GetAllRegistratieBedrijven();
            RequestsDataGrid.ItemsSource = bedrijven;
            RequestsDataGrid.Items.Refresh();
            _geselecteerdBedrijf = null;
        }

        private async void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            if (_geselecteerdBedrijf == null) return;
            if (MessageBox.Show(
                    "Bent u zeker dat u de registratie van " + _geselecteerdBedrijf.Bedrijfsnaam + " wilt afkeuren?",
                    "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
            //Yes
            try
            {
                var response = await _client.DeleteAsync("/bedrijf/" + _geselecteerdBedrijf.Id);
                response.EnsureSuccessStatusCode(); // Throw on error code.  
                RefreshListRegistratieBedrijven();
                MessageBox.Show("Registratie afgekeurd");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bedrijf niet verwijderd " + ex.Message);
            }
        }

        private void RequestsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem != null)
            {
                _geselecteerdBedrijf = (Bedrijf)RequestsDataGrid.SelectedItem;
            }
        }

        public async Task<List<Bedrijf>> GetAllRegistratieBedrijven()
        {
            HttpResponseMessage response = await _client.GetAsync("/bedrijf");
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var bedrijven = await response.Content.ReadAsAsync<List<Bedrijf>>();
            var registratieBedrijven = new List<Bedrijf>();
            foreach (Bedrijf bedrijf in bedrijven)
            {
                try
                {
                    var response2 = await _client.GetAsync("/Account/" + bedrijf.UserAccountId);
                    response.EnsureSuccessStatusCode(); // Throw on error code.
                    var account = await response2.Content.ReadAsAsync<UserAccount>();
                    if (!account.EmailConfirmed)
                    {
                        registratieBedrijven.Add(bedrijf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fout bij ophalen bedrijven " + ex.Message);
                }
            }
            return registratieBedrijven;
        }

        public async Task<List<Bedrijf>> GetAllBedrijven()
        {
            HttpResponseMessage response = await _client.GetAsync("/bedrijf");
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            var bedrijven = await response.Content.ReadAsAsync<List<Bedrijf>>();
            return bedrijven;
        }

        private async void GenerateCompanyListButton_Click(object sender, RoutedEventArgs e)
        {
            List<Bedrijf> bedrijven = await GetAllBedrijven();
            string csv = null;
            csv += "Bedrijfsnaam;Adres;Huisnummer;Postcode;Gemeente;Email;Telefoonnummer" + Environment.NewLine;

            foreach (Bedrijf bedrijf in bedrijven)
            {
                if (bedrijf.AanwezigheidHandshake)
                {
                    csv += bedrijf.Bedrijfsnaam + ";" + bedrijf.Adres + ";" + bedrijf.Huisnummer + ";" + bedrijf.Postcode + ";" + bedrijf.Gemeente + ";" + bedrijf.Email + ";" + bedrijf.Telefoonnummer + Environment.NewLine;
                }
            }

            string folderName = null;
            FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            try
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    folderName = folderBrowserDialog.SelectedPath;
                }
                string path = folderName + "\\LijstBedrijven.csv";
                if (!File.Exists(path))
                {
                    SchrijfBestand(path, csv);
                }
                else
                {
                    if (MessageBox.Show("Bestand 'LijstBedrijven.csv' bestaat al. Bent u zeker dat u dit wilt bestand overschrijven?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //No
                        MessageBox.Show("Lijst niet gegenereerd.");
                    }
                    else
                    {
                        //Yes
                        SchrijfBestand(path, csv);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void SchrijfBestand(string path, string csv)
        {
            File.WriteAllText(path, csv);
            MessageBox.Show("Lijst succesvol gegenereerd.");
        }

        private void TypeComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var item = FileTypes.FirstOrDefault(t => t.Content.Equals("Selecteer type"));
            FileTypes.Remove(item);
        }

        private async void CompanyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!HandShakeTabItem.IsSelected) return;
            List<Bedrijf> bedrijven = await GetAllBedrijven();
            List<Bedrijf> handShakeBedrijven = new List<Bedrijf>();

            foreach (Bedrijf bedrijf in bedrijven)
            {
                if (bedrijf.AanwezigheidHandshake)
                {
                    handShakeBedrijven.Add(bedrijf);
                }
            }
            CompanyDataGrid.ItemsSource = handShakeBedrijven;
            CompanyCountLabel.Content = $"Bedrijven {handShakeBedrijven.Count}";
        }

        private void VernieuwButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshListRegistratieBedrijven();
        }
    }
}
