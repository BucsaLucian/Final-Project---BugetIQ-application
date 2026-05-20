using System;
using System.Linq;
using Microsoft.Maui.Controls;
using CheltuieliApp.Models;
using System.Collections.Generic;

namespace CheltuieliApp
{
    public partial class RaportAnualPage : ContentPage
    {
        public List<RaportAnual> RaportAnualList { get; set; } = new();
        public RaportAnualPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AfiseazaRaportAnual();
        }


        private async Task AfiseazaRaportAnual()
        {
            var venituri = await App.VenitService.GetToateVeniturileAsync();
            var cheltuieli = await App.CheltuialaService.GetToateCheltuielileAsync();

            var ani = venituri.Select((Venit v) => v.Data.Year)
                .Concat(cheltuieli.Select((Cheltuiala c) => c.Data.Year))
                .Distinct()
                .OrderBy(an => an)
                .ToList();

            foreach (var an in ani)
            {
                var totalVenituri = venituri
                    .Where(v => v.Data.Year == an)
                    .Sum(v => v.Suma);

                var totalCheltuieli = cheltuieli
                    .Where(c => c.Data.Year == an)
                    .Sum(c => c.Suma);

                RaportAnualList.Add(new RaportAnual
                {
                    An = an,
                    TotalVenituri = totalVenituri,
                    TotalCheltuieli = totalCheltuieli,
                    Sold = totalVenituri - totalCheltuieli
                });
            }

            raportAnualListView.ItemsSource = RaportAnualList;

        }

    }
}
