namespace CalculateurAge
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCalculerClicked(object sender, EventArgs e)
        {
         

            if (string.IsNullOrWhiteSpace(entryNom.Text))
            {
                DisplayAlertAsync("erreur", "entrez un nom", "ok");
                return;
            }

            DateTime d = pickerDate.Date ?? DateTime.Today;
            int age = DateTime.Today.Year - d.Year;

            if (d.Date > DateTime.Today.AddYears(-age)) age--;

            lblResultat.Text = $"{entryNom.Text}, vous avez {age} ans";
            lblResultat.IsVisible = true;
          
        }
    }
}
