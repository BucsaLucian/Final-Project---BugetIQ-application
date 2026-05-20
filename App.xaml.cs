using CheltuieliApp.Data;
using CheltuieliApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace CheltuieliApp;

public partial class App : Application
{
    public static AppDbContext DbContext { get; private set; }
    public static VenitService VenitService { get; private set; }
    public static CheltuialaService CheltuialaService { get; private set; }

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        VenitService = serviceProvider.GetService<VenitService>();
        CheltuialaService = serviceProvider.GetService<CheltuialaService>();
        MainPage = new AppShell();
    }


}
