//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Android.Util;
//using Firebase.Messaging;

//namespace AuvoMovies.Platforms.Android
//{
//    public class MyFirebaseMessagingService : FirebaseMessagingService
//    {
//        public override void OnMessageReceived(RemoteMessage message)
//        {
//            // Exemplo simples de notificação
//            string title = message.GetNotification().Title;
//            string body = message.GetNotification().Body;

//            Log.Info("FCM", $"Notificação recebida: {title} - {body}");

//            // Aqui você pode implementar a lógica para exibir a notificação no dispositivo.
//            // Você pode utilizar NotificationManager ou qualquer outro serviço de notificação
//        }
//    }
//}
