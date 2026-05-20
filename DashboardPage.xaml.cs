using CheltuieliApp.Models;
using CheltuieliApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;   
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using System.IO;

namespace CheltuieliApp
{
    public partial class DashboardPage : ContentPage
    {
        private readonly VenitService _venitService;
        private readonly CheltuialaService _cheltuialaService;

        public DashboardPage(VenitService venitService, CheltuialaService cheltuialaService)
        {
            InitializeComponent();
            _venitService = venitService;
            _cheltuialaService = cheltuialaService;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshAsync();

        }
        private async void OnAdaugaVenitClicked(object sender, EventArgs e)
        {
            var pagina = new AdaugaVenitPage(_venitService);
            pagina.Disappearing += async (_, __) => await RefreshAsync();
            await Navigation.PushAsync(pagina);
        }
        private async void OnAdaugaCheltuialaClicked(object sender, EventArgs e)
        {
            var pagina = new AdaugaCheltuialaPage(_cheltuialaService);
            await Navigation.PushAsync(pagina);
            pagina.Disappearing += async (_, __) => await RefreshAsync();
        }
        private async void OnRaportPeDataClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RaportPeDataPage));

        }
        private async void OnVeziTotClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaTotalaPage(_venitService, _cheltuialaService));
        }
        private async void OnExportCsvClicked(object sender, EventArgs e)
        {
            try
            {
            var venituri = await _venitService.GetVenituriAsync();
            var cheltuieli = await _cheltuialaService.GetCheltuieliAsync();

        if (!venituri.Any() && !cheltuieli.Any())
        {
            await DisplayAlert("Export anulat", "Nu există tranzacții de exportat.", "OK");
            return;
        }

        var csvLines = new List<string> { "Tip,Categorie,Sumă (RON),Descriere,Dată" };

        csvLines.AddRange(venituri.Select(v =>
            $"Venit,{v.Categorie},{(int)v.Suma},{v.Descriere},{v.Data:dd.MM.yyyy HH:mm}"));

        csvLines.AddRange(cheltuieli.Select(c =>
            $"Cheltuială,{c.Categorie},{(int)c.Suma},{c.Descriere},{c.Data:dd.MM.yyyy HH:mm}"));

        var fileName = $"tranzactii_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        var localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        File.WriteAllLines(localPath, csvLines, new System.Text.UTF8Encoding(true));

                bool saveInDownloads = await DisplayAlert(
            "Export realizat",
            "Fișierul a fost generat. Vrei să-l salvezi în folderul public 'Download'?",
            "Da, salvează în Download",
            "Nu, doar deschide");

        if (saveInDownloads)
        {
#if ANDROID
            // salvare în /storage/emulated/0/Download
            var publicPath = Path.Combine("/storage/emulated/0/Download", fileName);
            File.Copy(localPath, publicPath, true);
            await DisplayAlert("Salvat", $"Fișierul a fost salvat în: {publicPath}", "OK");
#else
            await DisplayAlert("Nedisponibil", "Această funcție este disponibilă doar pe Android.", "OK");
#endif
        }
        else
        {
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(localPath)
            });
        }
    }
    catch (Exception ex)
    {
        await DisplayAlert("Eroare", $"Exportul a eșuat:\n{ex.Message}", "OK");
    }
}

        private async void OnRaportAnualClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RaportAnualPage));

        }
        public async Task RefreshAsync()
        {
            var venituri = await _venitService.GetVenituriAsync();
            var cheltuieli = await _cheltuialaService.GetCheltuieliAsync();

            double totalVenituri = venituri.Sum(v => (double)v.Suma);
            double totalCheltuieli = cheltuieli.Sum(c => (double)c.Suma);
            double sold = totalVenituri - totalCheltuieli;

            var tranzactii = venituri.Select(v => new TranzactieAfisata
            {
                Tip = "Venit",
                Categorie = v.Categorie,
                SumaAfisata = $"+{v.Suma:0} RON",
                DataAfisata = v.Data.ToString("dd MMM yyyy HH:mm")
            })
            .Concat(cheltuieli.Select(c => new TranzactieAfisata
            {
                Tip = "Cheltuială",
                Categorie = c.Categorie,
                SumaAfisata = $"-{c.Suma:0} RON",
                DataAfisata = c.Data.ToString("dd MMM yyyy HH:mm")
            }))
            .OrderByDescending(t => DateTime.Parse(t.DataAfisata))
            .Take(10)
            .ToList();

            BindingContext = new DashboardViewModel
            {
                TotalVenituriText = $"Venituri: {totalVenituri} RON",
                TotalCheltuieliText = $"Cheltuieli: {totalCheltuieli} RON",
                SoldText = $"Sold: {sold} RON",
                TranzactiiRecente = tranzactii
            };
        }

    }
}
