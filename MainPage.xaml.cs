using CheltuieliApp;

namespace CheltuieliApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private async void OnAdaugaVenitClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdaugaVenitPage(App.VenitService));
    }
    private async void OnAdaugaCheltuialaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdaugaCheltuialaPage(App.CheltuialaService));

    }
    private async void OnVeziToateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ListaTranzactiiPage());
    }
    private async void OnRaportPeDataClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RaportPeDataPage());
    }
}
