﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using WordSus.Extensions;
using WordSus.Models;
using WordSus.Services;

namespace WordSus.Features.SurvivalMode;

public class SurvivalModeViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly FakeWordService fakeWordService;
    private readonly RandomWordService randomWordService;

    private int level;

    private bool isResultEnabled;

    private OptionWord option1;
    private OptionWord option2;
    private OptionWord option3;
    private OptionWord option4;

    private int remainingLives;

    public SurvivalModeViewModel(
        FakeWordService fakeWordService,
        RandomWordService randomWordService)
    {
        this.fakeWordService = fakeWordService;
        this.randomWordService = randomWordService;

        level = 1;
        isResultEnabled = false;
        remainingLives = 3;

        Task.Run(RefreshOptionsAsync);
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
            OnPropertyChanged();
        }
    }

    public bool IsResultEnabled
    {
        get
        {
            return isResultEnabled;
        }

        set
        {
            isResultEnabled = value;
            OnPropertyChanged();
        }
    }

    public OptionWord Option1
    { 
        get
        {
            return option1;
        }
        
        set
        {
            option1 = value;
            OnPropertyChanged();
        }
    }

    public OptionWord Option2
    {
        get
        {
            return option2;
        }

        set
        {
            option2 = value;
            OnPropertyChanged();
        }
    }

    public OptionWord Option3
    {
        get
        {
            return option3;
        }

        set
        {
            option3 = value;
            OnPropertyChanged();
        }
    }

    public OptionWord Option4
    {
        get
        {
            return option4;
        }

        set
        {
            option4 = value;
            OnPropertyChanged();
        }
    }

    public bool ShowOption1Definition
    {
        get
        {
            return IsResultEnabled && !Option1.IsFake;
        }
    }

    public bool ShowOption2Definition
    {
        get
        {
            return IsResultEnabled && !Option2.IsFake;
        }
    }

    public bool ShowOption3Definition
    {
        get
        {
            return IsResultEnabled && !Option3.IsFake;
        }
    }

    public bool ShowOption4Definition
    {
        get
        {
            return IsResultEnabled && !Option4.IsFake;
        }
    }

    public bool ShowOption1IsFake
    {
        get
        {
            return IsResultEnabled && Option1.IsFake;
        }
    }

    public bool ShowOption2IsFake
    {
        get
        {
            return IsResultEnabled && Option2.IsFake;
        }
    }

    public bool ShowOption3IsFake
    {
        get
        {
            return IsResultEnabled && Option3.IsFake;
        }
    }

    public bool ShowOption4IsFake
    {
        get
        {
            return IsResultEnabled && Option4.IsFake;
        }
    }

    public int RemainingLives
    {
        get
        {
            return remainingLives;
        }

        set
        {
            remainingLives = value;

            OnPropertyChanged(nameof(ShowFirstStrike));
            OnPropertyChanged(nameof(ShowSecondStrike));
            OnPropertyChanged(nameof(ShowThirdStrike));
        }
    }

    public bool ShowFirstStrike
    {
        get
        {
            return remainingLives < 3;
        }
    }

    public bool ShowSecondStrike
    {
        get
        {
            return remainingLives < 2;
        }
    }

    public bool ShowThirdStrike
    {
        get
        {
            return remainingLives < 1;
        }
    }

    private async Task RefreshOptionsAsync()
    {
        var randomWord1 = await randomWordService.GetRandomWordAsync();
        var randomWord2 = await randomWordService.GetRandomWordAsync();
        var randomWord3 = await randomWordService.GetRandomWordAsync();
        var fakeWord = await fakeWordService.GetFakeWordAsync();

        var option1 = new OptionWord()
        {
            Word = randomWord1.Word.ToLower(),
            Definition = randomWord1.Definition,
            IsFake = false,
        };

        var option2 = new OptionWord()
        {
            Word = randomWord2.Word.ToLower(),
            Definition = randomWord2.Definition,
            IsFake = false,
        };

        var option3 = new OptionWord()
        {
            Word = randomWord3.Word.ToLower(),
            Definition = randomWord3.Definition,
            IsFake = false,
        };

        var option4 = new OptionWord()
        {
            Word = fakeWord.ToLower(),
            IsFake = true,
        };

        var dict = new Dictionary<int, OptionWord>
        {
            [1] = option1,
            [2] = option2,
            [3] = option3,
            [4] = option4,
        };

        var ind = new int[] { 1, 2, 3, 4 };
        var rng = new Random();
        rng.Shuffle(ind);

        Option1 = dict[ind[0]];
        Option2 = dict[ind[1]];
        Option3 = dict[ind[2]];
        Option4 = dict[ind[3]];
    }

    public void ValidateOption1()
    {
        ValidateOption(Option1.IsFake);
    }

    public void ValidateOption2()
    {
        ValidateOption(Option2.IsFake);
    }

    public void ValidateOption3()
    {
        ValidateOption(Option3.IsFake);
    }

    public void ValidateOption4()
    {
        ValidateOption(Option4.IsFake);
    }

    private void ValidateOption(bool isFake)
    {
        if (isFake)
        {
            OnPropertyChanged("CorrectAnswer");
        }
        else
        {
            OnPropertyChanged("WrongAnswer");

            RemainingLives -= 1;
        }

        IsResultEnabled = true;

        PropertyChangeResultViews();
    }

    public async Task GoToNextQuestionAsync()
    {
        if (RemainingLives > 0)
        {
            IsResultEnabled = false;
            Level += 1;

            await RefreshOptionsAsync();
            PropertyChangeResultViews();
        }
        else
        {
            OnPropertyChanged("GameOver");
        }
    }

    private void PropertyChangeResultViews()
    {
        OnPropertyChanged(nameof(ShowOption1Definition));
        OnPropertyChanged(nameof(ShowOption2Definition));
        OnPropertyChanged(nameof(ShowOption3Definition));
        OnPropertyChanged(nameof(ShowOption4Definition));
        OnPropertyChanged(nameof(ShowOption1IsFake));
        OnPropertyChanged(nameof(ShowOption2IsFake));
        OnPropertyChanged(nameof(ShowOption3IsFake));
        OnPropertyChanged(nameof(ShowOption4IsFake));
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}