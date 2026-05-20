using CheltuieliApp.Models;
using CheltuieliApp.Services;

namespace CheltuieliApp;

public partial class ListaTranzactiiPage : ContentPage
{
    private readonly VenitService _venitService;
    private readonly CheltuialaService _cheltuialaService;
    public ListaTranzactiiPage()
    {
        InitializeComponent();
        _venitService = App.VenitService;
        _cheltuialaService = App.CheltuialaService;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var venituri = await _venitService.GetVenituriAsync();
        var cheltuieli = await _cheltuialaService.GetCheltuieliAsync();

        var tranzactii = venituri.Select(v => new
        {
            Tip = "Venit",
            v.Categorie,
            SumaAfisata = $"+{v.Suma} RON",
            v.Descriere,
            DataAfisata = v.Data.ToString("dd MMM yyyy HH:mm")
        }).Concat(cheltuieli.Select(c => new
        {
            Tip = "Cheltuială",
            c.Categorie,
            SumaAfisata = $"-{c.Suma} RON",
            c.Descriere,
            DataAfisata = c.Data.ToString("dd MMM yyyy HH:mm")
        }))
        .OrderByDescending(t => DateTime.Parse(t.DataAfisata))
        .ToList();

        tranzactiiList.ItemsSource = tranzactii;
    }
}
