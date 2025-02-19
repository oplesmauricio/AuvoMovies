using AuvoMovies.Pages;
using AuvoMovies.Services;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;
using Microsoft.Extensions.Logging;

namespace AuvoMovies
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IFilmeService, FilmeService>();

            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<ISettings, Settings>();

            builder.Services.AddSingleton<FilmesPage>();
            builder.Services.AddSingleton<FilmesViewModel>();
            builder.Services.AddScoped<FilmeDetailPage>();
            builder.Services.AddScoped<FilmesDetailViewModel>();


            return builder.Build();
        }
    }
}
