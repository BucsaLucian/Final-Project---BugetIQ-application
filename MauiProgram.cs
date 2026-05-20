using CheltuieliApp.Data;
using CheltuieliApp.Services;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Maui;

namespace CheltuieliApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>(app => new App(app))
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
            });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "cheltuieli.db");
            options.UseSqlite($"Filename={dbPath}");
        });

        builder.Services.AddSingleton<VenitService>();
        builder.Services.AddSingleton<CheltuialaService>();
        builder.Services.AddTransient<ListaTotalaPage>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AdaugaVenitPage>();
        builder.Services.AddTransient<AdaugaCheltuialaPage>();
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<RaportAnualPage>();
        builder.Services.AddTransient<RaportPeDataPage>();

        return builder.Build();
    }
}
