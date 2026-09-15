namespace EasyVoiceSynthesizer.Domain.ValueObjects;

public record AudioSample(int SampleRate, float[] Samples);