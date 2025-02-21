namespace AuvoMovies
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //// Inicializa as notificações push
            //FirebasePushNotificationManager.Initialize(this);

            //// Inscrever-se em um tópico (opcional)
            //FirebasePushNotificationManager.Subscribe("news");

            //// Configura o evento de notificação recebida
            //FirebasePushNotificationManager.OnNotificationReceived += (sender, e) =>
            //{
            //    // A lógica para quando uma notificação for recebida
            //    Console.WriteLine($"Título: {e.Title}, Mensagem: {e.Message}");
            //};

            MainPage = new AppShell();
        }
    }
}
