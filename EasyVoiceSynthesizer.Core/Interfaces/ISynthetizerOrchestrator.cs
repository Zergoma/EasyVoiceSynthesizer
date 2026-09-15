namespace EasyVoiceSynthesizer.Application.Interfaces;

public interface ISynthetizerOrchestrator
{
    event Action? OnSoundStopped;
    void Stop();
    public float Volume { get; set; }
    public float Speed { get; set; }
    Task SpeakAsync(string text);
}
