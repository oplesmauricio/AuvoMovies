using System.Globalization;
using AuvoMovies.Handlers;
using AuvoMovies.Pages;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;


#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

#if IOS || MACCATALYST
using UIKit;
#endif

namespace AuvoMovies
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            //Borderless entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if __ANDROID__
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif __IOS__
                    handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                    handler.PlatformView.Layer.BorderWidth = 0;
                    handler.PlatformView.Layer.BorderColor = UIColor.White.CGColor;
                    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                }
            });

            //var loginViewModel = Handler.MauiContext.Services.GetService<LoginViewModel>();
            var service = Handler?.MauiContext.Services.GetService<IFilmeService>();
            var settings = Handler?.MauiContext.Services.GetService<ISettings>();

            MainPage = new LoginPage(service, settings);
        }
    }
}
