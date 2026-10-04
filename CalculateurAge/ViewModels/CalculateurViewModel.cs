using CalculateurAge.Views;

namespace CalculateurAge.ViewModels;

// Contient l'ÉTAT de l'écran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _statut = "";
    private string _anniversaire = "";

    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value)) CalculerCommand.Rafraichir(); }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    public string Statut
    {
        get => _statut;
        set => SetField(ref _statut, value);
    }

    public string Anniversaire
    {
        get => _anniversaire;
        set => SetField(ref _anniversaire, value);
    }

    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }
    // Nouveauté 4 : envoie le résultat à ResultatPage.
    public RelayCommand VoirDetailCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));
        EffacerCommand = new RelayCommand(Effacer);
        // Actif seulement quand un vrai résultat est affiché.
        VoirDetailCommand = new RelayCommand(
            VoirDetail,
            () => ResultatVisible && Statut != "");
    }

    private void Calculer()
    {
        DateTime aujourdhui = DateTime.Today;

        // Garde-fou : une date de naissance dans le futur n'a pas de sens.
        if (DateNaissance.Date > aujourdhui)
        {
            Resultat = "La date de naissance est dans le futur.";
            Statut = "";
            Anniversaire = "";
            ResultatVisible = true;
            VoirDetailCommand.Rafraichir();
            return;
        }

        int age = aujourdhui.Year - DateNaissance.Year;
        if (DateNaissance.Date > aujourdhui.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Statut = age >= 18 ? "Statut : majeur" : "Statut : mineur";

        // Prochain anniversaire = date de naissance + (âge + 1) ans.
        DateTime prochain = DateNaissance.Date.AddYears(age + 1);
        if (DateNaissance.Date.AddYears(age) == aujourdhui)
            Anniversaire = "Joyeux anniversaire !";
        else
            Anniversaire = $"Prochain anniversaire dans {(prochain - aujourdhui).Days} jour(s)";

        ResultatVisible = true;
        VoirDetailCommand.Rafraichir();
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Statut = "";
        Anniversaire = "";
        ResultatVisible = false;
        VoirDetailCommand.Rafraichir();
    }

    // Envoie le résultat à ResultatPage comme PARAMÈTRE OBJET (pas dans l'URL).
    private async void VoirDetail()
    {
        string resume = $"{Resultat}\n{Statut}\n{Anniversaire}";

        await Shell.Current.GoToAsync(
            nameof(ResultatPage),
            new ShellNavigationQueryParameters { { "resume", resume } });
    }
}