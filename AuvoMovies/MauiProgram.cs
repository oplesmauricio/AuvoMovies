using AuvoMovies.Infra;
using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Pages;
using AuvoMovies.Services;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

#if ANDROID
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Core.Platforms.Android;
#endif
namespace AuvoMovies
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("ionicons.ttf", "IonIcons");
                    fonts.AddFont("line-awesome.ttf", "LineAwesome");
                    fonts.AddFont("icon.ttf", "MauiKitIcons");
                    fonts.AddFont("material-icons-outlined-regular.otf", "MaterialDesign");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IFilmeService, FilmeService>();
            builder.Services.AddSingleton<IRepository, Repository>();
            builder.Services.AddSingleton<ISettings, Settings>();

            builder.Services.AddSingleton<FilmesPage>();
            builder.Services.AddSingleton<FilmesViewModel>();
            builder.Services.AddScoped<FilmeDetailPage>();
            builder.Services.AddScoped<FilmesDetailViewModel>();
            builder.Services.AddScoped<FavoritosPage>();
            builder.Services.AddScoped<FavoritosViewModel>();
            builder.Services.AddScoped<LoginPage>();
            //builder.Services.AddScoped<LoginViewModel>();


#if ANDROID
            builder.RegisterFirebaseServices();
#endif

            return builder.Build();
        }

#if ANDROID
        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                CrossFirebase.Initialize(activity)));
            });

            return builder;
        }
#endif
    }
}
