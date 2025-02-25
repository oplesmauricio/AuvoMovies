using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuvoMovies.Models.Sends;
using AuvoMovies.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace AuvoMovies.Services
{
    public class FirebaseService : IFirebaseService
    {
        private string _deviceToken { get; set; }

        public FirebaseService()
        {
            
        }

        public async Task EnviarNotificacao(string titulo, string decricao)
        {
            await AtribuirDeviceToken();
            await ReadFireBaseAdminSdk();
            await Enviar(titulo, decricao);
        }

        private async Task AtribuirDeviceToken()
        {
            if (Preferences.ContainsKey("DeviceToken"))
                _deviceToken = Preferences.Get("DeviceToken", "");
        }

        private async Task ReadFireBaseAdminSdk()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("auvomovies-firebase-adminsdk-fbsvc-be3a054835.json");
            var reader = new StreamReader(stream);

            var jsonContent = reader.ReadToEnd();


            if (FirebaseMessaging.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonContent)
                });
            }
        }

        private async Task Enviar(string titulo, string decricao)
        {
            var androidNotificationObject = new Dictionary<string, string>();
            androidNotificationObject.Add("NavigationID", "2");

            var obj = new Message
            {
                Token = _deviceToken,
                Notification = new Notification
                {
                    Title = titulo,
                    Body = decricao
                },
                Data = androidNotificationObject
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(obj);
        }
    }
}
