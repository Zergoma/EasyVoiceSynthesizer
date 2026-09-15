using NAudio.Wave;

namespace EasyVoiceSynthesizer.NAudio.Services;

public static class BufferWavePcmGenerator
{
    public static IWaveProvider GenerateAudioBuffer(int audioSampleRate, byte[] pcmData)
    {
        var waveFormat = new WaveFormat(
            rate: audioSampleRate,
            bits: 16,
            channels: 1);

        var buffer = new BufferedWaveProvider(waveFormat);
        buffer.AddSamples(
            buffer: pcmData,
            offset: 0,
            count: pcmData.Length);
        return buffer;
    }
}