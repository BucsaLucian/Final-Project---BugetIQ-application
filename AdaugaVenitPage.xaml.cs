using CheltuieliApp.Models;
using CheltuieliApp.Services;
using System;
using Microsoft.Maui.Controls;

namespace CheltuieliApp
{
    public partial class AdaugaVenitPage : ContentPage
    {
        private readonly VenitService _venitService;

        public AdaugaVenitPage(VenitService venitService)
        {
            InitializeComponent();
            _venitService = venitService;
        }

        private async void OnSalveazaClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(categorieEntry.Text) ||
                string.IsNullOrWhiteSpace(sumaEntry.Text) ||
                !decimal.TryParse(sumaEntry.Text, out decimal suma))
            {
                mesajLabel.Text = "Completează toate câmpurile corect.";
                mesajLabel.IsVisible = true;
                return;
            }

            var dataCompleta = dataPicker.Date + oraPicker.Time;

            var venit = new Venit
            {
                Categorie = categorieEntry.Text,
                Suma = suma,
                Descriere = descriereEntry.Text,
                Data = dataCompleta
            };

            await _venitService.AdaugaVenitAsync(venit);
            await DisplayAlert("Succes", "Venit adăugat cu succes!", "OK");
            await Navigation.PopAsync(); 
        }

    }
}
