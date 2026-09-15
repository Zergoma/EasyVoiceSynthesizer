using EasyVoiceSynthesizer.Application;
using EasyVoiceSynthesizer.Application.Interfaces;
using EasyVoiceSynthesizer.Domain.ValueObjects;

using SherpaOnnx;

namespace EasyVoiceSynthesizer.SherpaOnnx.Services;


public class SynthesizerSherpaOnnx : IVoiceSynthesizer, IDisposable
{
    private readonly OfflineTts _tts;
    private CancellationTokenSource? _speechCancellation;


    public SynthesizerSherpaOnnx()
    {
        var modelDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "Models",
            "vits-piper-de_DE-thorsten-low");

        var config = new OfflineTtsConfig
        {
            Model = new OfflineTtsModelConfig
            {
                Vits = new OfflineTtsVitsModelConfig
                {
                    Model = Path.Combine(
                        modelDirectory,
                        "de_DE-thorsten-low.onnx"),

                    Tokens = Path.Combine(
                        modelDirectory,
                        "tokens.txt"),

                    DataDir = Path.Combine(
                        modelDirectory,
                        "espeak-ng-data")
                },

                NumThreads = 2,
                Provider = "cpu"
            }
        };

        _tts = new OfflineTts(config);
    }

    private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = Math.Clamp(value, 0.1f, 0.9f);
    }

    public void Abort()
    {
        _speechCancellation?.Cancel();
        _speechCancellation?.Dispose();
        _speechCancellation = null;
    }

    public void Dispose()
    {
        _speechCancellation?.Cancel();
        _speechCancellation?.Dispose();
        _speechCancellation = null;

        _tts.Dispose();
    }

    public async Task<Result<AudioSample>> SpeakAsync(string text)
    {
        _speechCancellation?.Cancel();
        _speechCancellation?.Dispose();

        _speechCancellation = new CancellationTokenSource();

        var cancellationToken = _speechCancellation.Token;

        try
        {
            Result<OfflineTtsGeneratedAudio> resuAudio = await GenerateAudioAsync(text, cancellationToken);

            if (resuAudio.Success)
            {
                var audio = resuAudio.GetValue;
                cancellationToken.ThrowIfCancellationRequested();

                return Result<AudioSample>.Ok(new AudioSample(audio.SampleRate, audio.Samples));
            }
        }
        catch (OperationCanceledException)
        {

        }
        return Result<AudioSample>.Fail("");
    }

    private async Task<Result<OfflineTtsGeneratedAudio>> GenerateAudioAsync(string text, CancellationToken cancellationToken)
    {
        if (text == null)
        {
            return Result<OfflineTtsGeneratedAudio>.Fail("Text is null");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            return Result<OfflineTtsGeneratedAudio>.Fail("Text is empty");
        }

        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            var generationConfig = new OfflineTtsGenerationConfig
            {
                Sid = 0,
                Speed = Speed
            };

            var result = _tts.GenerateWithConfig(
                text,
                generationConfig,
                null);

            cancellationToken.ThrowIfCancellationRequested();


            return Result<OfflineTtsGeneratedAudio>.Ok(result);

        }, cancellationToken);
    }
}
