using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using EasyVoiceSynthesizer.Application.Interfaces;
using EasyVoiceSynthesizer.ViewModels.Presenters;

using Loca = EasyVoiceSynthesizer.Localization.EasyVoiceResources;

namespace EasyVoiceSynthesizer.ViewModels;

public partial class SynthesizerViewModel : ObservableObject
{
    private readonly IDialogShow _presenter;
    private readonly ISynthetizerOrchestrator _synthetizerOrchestrator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SynthetizerButtonText))]
    [NotifyPropertyChangedFor(nameof(IsNoPlayingSound))]
    public partial bool IsPlayingSound { get; set; }

    public bool IsNoPlayingSound => !IsPlayingSound;


    public float Volume
    {
        get => _synthetizerOrchestrator.Volume;
        set => _synthetizerOrchestrator.Volume = value;
    }

    public float Speed
    {
        get => _synthetizerOrchestrator.Speed;
        set => _synthetizerOrchestrator.Speed = value;
    }

    public SynthesizerViewModel(
        IDialogShow presenter,
        ISynthetizerOrchestrator audio)
    {
        _presenter = presenter;
        _synthetizerOrchestrator = audio;
        _synthetizerOrchestrator.OnSoundStopped += Audio_OnStopped;
        IsPlayingSound = false;
    }

    private void Audio_OnStopped()
    {
        IsPlayingSound = false;
    }

    #region Localization

    public static string SynthetizerPageTitle => Loca.SynthetizerPageTitle;
    public string SynthetizerButtonText => IsPlayingSound ? Loca.StopPlayingSound : Loca.GenerateSound;

    public static string SoundVolumeText => Loca.Volume;
    public static string SpeedSpeechText => Loca.SpeedSpeech;
    #endregion

    [ObservableProperty]
    public partial string TextToPlay { get; set; } = string.Empty;



    [RelayCommand]
    public async Task ShowMe()
    {
        if (!IsPlayingSound)
        {
            if (!string.IsNullOrWhiteSpace(TextToPlay))
            {
                IsPlayingSound = true;
                await _synthetizerOrchestrator.SpeakAsync(TextToPlay);
            }
        }
        else
        {
            _synthetizerOrchestrator.Stop();
        }
    }
}
