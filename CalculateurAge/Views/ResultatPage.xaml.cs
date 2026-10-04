namespace CalculateurAge.Views;

// Reçoit le paramètre "resume" envoyé par le ViewModel.
[QueryProperty(nameof(Resume), "resume")]
public partial class ResultatPage : ContentPage
{
    public string Resume { get; set; } = "";

    public ResultatPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblResultat.Text = Resume;
    }

    private async void OnRetourClicked(object s, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}