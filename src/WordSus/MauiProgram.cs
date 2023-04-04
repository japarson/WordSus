using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using WordSus.Features.Definition;
using WordSus.Features.GameOver;
using WordSus.Features.Help;
using WordSus.Features.SurvivalMode;
using WordSus.Services;

namespace WordSus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkit()
			.RegisterServices()
			.RegisterViewModels()
			.RegisterPages();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<FakeWordService>();
        mauiAppBuilder.Services.AddSingleton<RandomWordService>();
        mauiAppBuilder.Services.AddSingleton(AudioManager.Current);
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<SurvivalModeViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddTransient<MainPage>();
		mauiAppBuilder.Services.AddTransient<SurvivalModePage>();
		mauiAppBuilder.Services.AddTransient<DefinitionPage>();
		mauiAppBuilder.Services.AddTransient<GameOverPage>();
		mauiAppBuilder.Services.AddTransient<HelpPage>();
		return mauiAppBuilder;
	}
}
