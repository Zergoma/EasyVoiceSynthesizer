namespace EasyVoiceSynthesizer.Application.Interfaces;

public interface IAudioPlayer
{
    event Action? OnSoundStopped;
    void Play();
    void Stop();
    public float Volume { get; set; }

    Task LoadAudioAsync(int audioSampleRate, float[] pcmData);
}
