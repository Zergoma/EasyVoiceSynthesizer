namespace EasyVoiceSynthesizer.Application.PcmConvertor;

public static class PcmConvertor
{
    public static async Task<byte[]> ConvertToPcmAsync(float[] samples)
    {
        return await Task.Run(() =>
        {
            byte[] pcmData = new byte[samples.Length * 2];

            for (var i = 0; i < samples.Length; i++)
            {
                var sample = Math.Clamp(
                    samples[i],
                    -1.0f,
                    1.0f);

                var value = (short)(sample * short.MaxValue);

                pcmData[i * 2] = (byte)(value & 0xFF);
                pcmData[i * 2 + 1] = (byte)((value >> 8) & 0xFF);
            }

            return pcmData;
        });
    }
}
