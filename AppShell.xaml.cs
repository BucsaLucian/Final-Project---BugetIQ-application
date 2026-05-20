namespace CheltuieliApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("AdaugaVenitPage", typeof(AdaugaVenitPage));
        Routing.RegisterRoute("AdaugaCheltuialaPage", typeof(AdaugaCheltuialaPage));
        Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
        Routing.RegisterRoute(nameof(RaportAnualPage), typeof(RaportAnualPage));
        Routing.RegisterRoute(nameof(RaportPeDataPage), typeof(RaportPeDataPage));
    }
}
