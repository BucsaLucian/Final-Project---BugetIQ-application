using CheltuieliApp.Models;
using CheltuieliApp.Services;

namespace CheltuieliApp;

public partial class AdaugaCheltuialaPage : ContentPage
{
    private readonly CheltuialaService _cheltuialaService;
    public AdaugaCheltuialaPage(CheltuialaService cheltuialaService)
    {
        InitializeComponent();
        _cheltuialaService = cheltuialaService;
    }
    private async void OnSalveazaClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(categorieEntry.Text) ||
            string.IsNullOrWhiteSpace(sumaEntry.Text) ||
            !decimal.TryParse(sumaEntry.Text, out decimal suma))
        {
            mesajLabel.Text = "Completează toate câmpurile corect!";
            mesajLabel.IsVisible = true;
            return;
        }

        var cheltuiala = new Cheltuiala
        {
            Categorie = categorieEntry.Text,
            Suma = suma,
            Descriere = string.IsNullOrWhiteSpace(descriereEntry.Text) ? "" : descriereEntry.Text,
            Data = dataPicker.Date + oraPicker.Time
        };

        await _cheltuialaService.AdaugaCheltuialaAsync(cheltuiala);

        await DisplayAlert("Succes", "Cheltuiala a fost salvată!", "OK");
        await Navigation.PopAsync(); 
    }
}
