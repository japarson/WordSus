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
        // await Navigation.PushAsync(new SurvivalModePage());
    }

    private void TimedModeButton_Clicked(object sender, EventArgs e)
    {

    }

    private void HelpButton_Clicked(object sender, EventArgs e)
    {

    }
}
