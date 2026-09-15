
using EasyVoiceSynthesizer.Domain.ValueObjects;

namespace EasyVoiceSynthesizer.Application.Interfaces;

public interface IVoiceSynthesizer
{
    Task<Result<AudioSample>> SpeakAsync(string text);
    void Abort();

    public float Speed { get; set; }
}
