namespace WordSus.Features.GameOver;

[QueryProperty(nameof(FinalScore), "FinalScore")]
public partial class GameOverPage : ContentPage
{
    private int finalScore;

	public GameOverPage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    public int FinalScore
    {
        get => finalScore;

        set
        {
            finalScore = value;
            OnPropertyChanged();
        }
    }

    private async void PlayAgainButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../..");
    }
}
