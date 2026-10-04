using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculateurAge.ViewModels;

// Classe mère de tous les ViewModels.
public class BaseViewModel : INotifyPropertyChanged
{
    // L'ÉVÈNEMENT : le moteur de binding s'y abonne.
    public event PropertyChangedEventHandler? PropertyChanged;

    // Prévient la vue qu'une propriété a changé.
    // Si personne n'est abonné, ne fait rien.
    protected void OnPropertyChanged([CallerMemberName] string? nom = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));

    // Affecte une valeur ET notifie, en une seule ligne.
    // Renvoie true si la valeur a réellement changé.
    protected bool SetField<T>(ref T champ, T valeur,
        [CallerMemberName] string? nom = null)
    {
        // Garde-fou : évite les notifications inutiles et les boucles infinies (TwoWay).
        if (EqualityComparer<T>.Default.Equals(champ, valeur)) return false;
        champ = valeur;
        OnPropertyChanged(nom);
        return true;
    }
}