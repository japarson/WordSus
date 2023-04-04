using Plugin.Maui.Audio;

namespace WordSus;

public partial class MainPage : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer themePlayer;

    public MainPage(IAudioManager audioManager)
    {
        InitializeComponent();

        this.audioManager = audioManager;

        Task.Run(PlayTheme);
    }

    private async Task PlayTheme()
    {
        var stream = await FileSystem.OpenAppPackageFileAsync("theme.mp3");
        themePlayer = audioManager.CreatePlayer(stream);
        themePlayer.Loop = true;
        themePlayer.Play();
    }

    private async void SurvivalModeButton_Clicked(object sender, EventArgs e)
    {
        themePlayer.Pause();
        await Shell.Current.GoToAsync("survival");
    }

    private async void HelpButton_Clicked(object sender, EventArgs e)
    {
        themePlayer.Pause();
        await Shell.Current.GoToAsync("help");
    }
}
