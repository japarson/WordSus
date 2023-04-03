using WordSus.Models;

namespace WordSus.Features.Definition;

[QueryProperty(nameof(OptionWord), "OptionWord")]
public partial class DefinitionPage : ContentPage
{
    OptionWord optionWord;

    public DefinitionPage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    public OptionWord OptionWord
    {
        get => optionWord;

        set
        {
            optionWord = value;
            OnPropertyChanged();
        }
    }
}
