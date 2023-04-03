namespace WordSus;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void SurvivalModeButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("survival");
    }

    private async void HelpButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("help");
    }
}
