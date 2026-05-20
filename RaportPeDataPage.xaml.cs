using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using CheltuieliApp.Models;
using CheltuieliApp.Services;

namespace CheltuieliApp;

public partial class RaportPeDataPage : ContentPage
{
    private readonly VenitService _venitService;
    private readonly CheltuialaService _cheltuialaService;

    public RaportPeDataPage()
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

        var raport = venituri
        .GroupBy(v => new { v.Data.Year, v.Data.Month })
        .Select(g => new RaportLunar
                {
                    DataLunii = new DateTime(g.Key.Year, g.Key.Month, 1),
                    TotalVenit = g.Sum(v => v.Suma),
                    TotalCheltuiala = cheltuieli
                        .Where(c => c.Data.Year == g.Key.Year && c.Data.Month == g.Key.Month)
                        .Sum(c => c.Suma)
                })
        .OrderByDescending(r => r.DataLunii)
        .ToList();

        raportListView.ItemsSource = raport;
    }
    public class RaportLunar
    {
        public DateTime DataLunii { get; set; }  
        public string Luna => DataLunii.ToString("MMM yyyy");  
        public decimal TotalVenit { get; set; }
        public decimal TotalCheltuiala { get; set; }
        public decimal Sold => TotalVenit - TotalCheltuiala;
        public string SoldColor => Sold > 0 ? "Green" : Sold < 0 ? "Red" : "Black";
    }
}
