namespace AuvoMovies.Pages.Base;

public partial class BasePage : ContentPage
{
    public IList<Microsoft.Maui.IView> PageContent => PageContentGrid.Children;
    public BasePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    public async void GoBack_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }

    public static BindableProperty BaseTitleProperty = 
        BindableProperty.Create(
            nameof(BaseTitle),
            typeof(string),
            typeof(BasePage),
            string.Empty
        );

    public string BaseTitle
    {
        get => (string)this.GetValue(BaseTitleProperty);
        set => this.SetValue(BaseTitleProperty, value);
    }

    public static BindableProperty BaseTitleColorProperty =
        BindableProperty.Create(
            nameof(BaseTitleColor),
            typeof(Color),
            typeof(BasePage),
            defaultValue: Color.FromArgb("#000000")
        );

    public Color BaseTitleColor
    {
        get { return (Color)GetValue(BaseTitleColorProperty); }
        set { SetValue(BaseTitleColorProperty, value); }
    }

    public bool INternetConectada()
    {
        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}