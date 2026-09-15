using EasyVoiceSynthesizer.Application.Interfaces;
using EasyVoiceSynthesizer.Application.PcmConvertor;

using NAudio.Wave;


namespace EasyVoiceSynthesizer.NAudio.Services;

public class NAudioPlayer : IAudioPlayer, IDisposable
{
    public event Action? OnSoundStopped;


    private WaveOut? _outputDevice = new();

    public float Volume
    {
        get => _outputDevice!.Volume;
        set => _outputDevice!.Volume = Math.Clamp(value, 0.0f, 1.0f);
    }

    public NAudioPlayer()
    {
        _outputDevice.PlaybackStopped += _outputDevice_PlaybackStopped;
    }
    public void Dispose()
    {
        _outputDevice?.Dispose();
        _outputDevice = null;

    }

    private void _outputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
    {
        OnSoundStopped?.Invoke();
    }

    public void Stop()
    {
        var state = _outputDevice?.PlaybackState;
        if (state == PlaybackState.Playing)
        {
            _outputDevice?.Stop();
        }
    }

    public async Task LoadAudioAsync(int audioSampleRate, float[] pcmData)
    {
        Stop();
        byte[] pcmBytes =
            await PcmConvertor.ConvertToPcmAsync(pcmData);

        IWaveProvider buffer =
            RawSourceWavePcmGenerator.GenerateAudioSource(audioSampleRate, pcmBytes);

        _outputDevice?.Init(buffer);
    }

    public void Play()
    {
        _outputDevice?.Play();
    }
}