using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.ComponentModel;
using WordSus.Features.Definition;

namespace WordSus.Features.SurvivalMode;

public partial class SurvivalModePage : ContentPage
{
	public SurvivalModePage(SurvivalModeViewModel survivalModeViewModel)
	{
		InitializeComponent();

        BindingContext = survivalModeViewModel;

        survivalModeViewModel.PropertyChanged += SurvivalModeViewModel_PropertyChanged;
    }

    private void QuitButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void Option1_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        vm.ValidateOption1();
    }

    private void Option2_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        vm.ValidateOption2();
    }

    private void Option3_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        vm.ValidateOption3();
    }

    private void Option4_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        vm.ValidateOption4();
    }

    private async void Option1Definition_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        var navigationParameter = new Dictionary<string, object>
        {
            { "OptionWord", vm.Option1 }
        };

        await Shell.Current.GoToAsync("definition", navigationParameter);
    }

    private async void Option2Definition_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        var navigationParameter = new Dictionary<string, object>
        {
            { "OptionWord", vm.Option2 }
        };

        await Shell.Current.GoToAsync("definition", navigationParameter);
    }

    private async void Option3Definition_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        var navigationParameter = new Dictionary<string, object>
        {
            { "OptionWord", vm.Option3 }
        };

        await Shell.Current.GoToAsync("definition", navigationParameter);
    }

    private async void Option4Definition_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        var navigationParameter = new Dictionary<string, object>
        {
            { "OptionWord", vm.Option4 }
        };

        await Shell.Current.GoToAsync("definition", navigationParameter);
    }

    private async void NextButton_Clicked(object sender, EventArgs e)
    {
        var vm = (SurvivalModeViewModel)BindingContext;
        await vm.GoToNextQuestionAsync();
    }

    private void SurvivalModeViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CorrectAnswer")
        {
            MainThread.BeginInvokeOnMainThread(async () => await ShowToastAsync("Correct!"));
        }
        else if (e.PropertyName == "WrongAnswer")
        {
            MainThread.BeginInvokeOnMainThread(async () => await ShowToastAsync("Wrong!"));
        }
        else if (e.PropertyName == "GameOver")
        {
            var vm = (SurvivalModeViewModel)BindingContext;
            var navigationParameter = new Dictionary<string, object>
            {
                { "FinalScore", vm.Level }
            };

            MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("gameover", navigationParameter));
        }
    }

    private static async Task ShowToastAsync(string definition)
    {
        CancellationTokenSource cancellationTokenSource = new();

        var duration = ToastDuration.Long;
        var fontSize = 16;

        var toast = Toast.Make(definition, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
}
