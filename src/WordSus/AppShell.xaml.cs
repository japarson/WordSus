namespace WordSus;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("survival", typeof(Features.SurvivalMode.SurvivalModePage));
        Routing.RegisterRoute("survival/definition", typeof(Features.Definition.DefinitionPage));
        Routing.RegisterRoute("survival/gameover", typeof(Features.GameOver.GameOverPage));
    }
}
