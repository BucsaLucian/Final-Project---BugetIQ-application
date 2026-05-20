using CheltuieliApp.Models;
using CheltuieliApp.Services;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheltuieliApp
{
    public partial class ListaTotalaPage : ContentPage
    {
        private readonly VenitService _venitService;
        private readonly CheltuialaService _cheltuialaService;

        public ListaTotalaPage(VenitService venitService, CheltuialaService cheltuialaService)
        {
            InitializeComponent();
            _venitService = venitService;
            _cheltuialaService = cheltuialaService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await IncarcaDate();
        }

        private async Task IncarcaDate()
        {
            venituriList.ItemsSource = await _venitService.GetVenituriAsync();
            cheltuieliList.ItemsSource = await _cheltuialaService.GetCheltuieliAsync();
        }

        private async void OnStergeVenitClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Venit venit)
            {
                bool confirm = await DisplayAlert("Confirmare", $"Ștergi venitul „{venit.Categorie}” de {venit.Suma:0} RON?", "Da", "Nu");
                if (confirm)
                {
                    await _venitService.StergeVenitAsync(venit);
                    await Toast.Make("Venitul a fost șters.", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                    await IncarcaDate();
                }
            }
        }
        private async void OnStergeCheltuialaClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Cheltuiala cheltuiala)
            {
                bool confirm = await DisplayAlert("Confirmare", $"Ștergi cheltuiala „{cheltuiala.Categorie}” de {cheltuiala.Suma:0} RON?", "Da", "Nu");
                if (confirm)
                {
                    await _cheltuialaService.StergeCheltuialaAsync(cheltuiala);
                    await Toast.Make("Cheltuiala a fost ștearsă.", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                    await IncarcaDate();
                }
            }
        }

    }
}
