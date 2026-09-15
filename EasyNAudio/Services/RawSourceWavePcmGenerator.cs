using NAudio.Wave;

namespace EasyVoiceSynthesizer.NAudio.Services;

public static class RawSourceWavePcmGenerator
{
    public static IWaveProvider GenerateAudioSource(int audioSampleRate, byte[] pcmData)
    {
        var waveFormat = new WaveFormat(
            rate: audioSampleRate,
            bits: 16,
            channels: 1);

        var stream = new MemoryStream(pcmData);

        return new RawSourceWaveStream(
            stream,
            waveFormat);
    }
}