using EasyVoiceSynthesizer.Application.Interfaces;
using EasyVoiceSynthesizer.Domain.ValueObjects;

namespace EasyVoiceSynthesizer.Application.Orchestrators;

public class TextToSpeechOrchestrator : ISynthetizerOrchestrator, IDisposable
{
    public event Action? OnSoundStopped;

    private readonly IVoiceSynthesizer _voiceSynthesizer;
    private readonly IAudioPlayer _audioPlayer;


    public float Volume { get => _audioPlayer.Volume; set => _audioPlayer.Volume=value; }
    public float Speed { get => _voiceSynthesizer.Speed; set => _voiceSynthesizer.Speed=value; }

    #region ctor/dispose

    public TextToSpeechOrchestrator(
        IVoiceSynthesizer voiceSynthesizer,
        IAudioPlayer audioPlayer)
    {
        _voiceSynthesizer = voiceSynthesizer;
        _audioPlayer = audioPlayer;

        _audioPlayer.OnSoundStopped += _audioPlayer_OnSoundStopped;
    }

    private void _audioPlayer_OnSoundStopped()
    {
        OnSoundStopped?.Invoke();
    }

    public void Dispose()
    {
        _audioPlayer.OnSoundStopped -= _audioPlayer_OnSoundStopped;
        if (_audioPlayer is IDisposable audioDisposable)
        {
            audioDisposable.Dispose();
        }

        if (_voiceSynthesizer is IDisposable voiceSynthesizerDisposable)
        {
            voiceSynthesizerDisposable.Dispose();
        }
    }
    #endregion

    public async Task SpeakAsync(string text)
    {
        Result<AudioSample> resuSample = await _voiceSynthesizer.SpeakAsync(text);
        if(resuSample.Success)
        {
            AudioSample sample = resuSample.GetValue;
            await _audioPlayer.LoadAudioAsync(sample.SampleRate, sample.Samples);
            _audioPlayer.Play();
        }
    }

    public void Stop()
    {
        _audioPlayer.Stop();
        _voiceSynthesizer.Abort();
    }
}
