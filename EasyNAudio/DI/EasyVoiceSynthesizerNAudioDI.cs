using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.NAudio.DI;

public static class EasyVoiceSynthesizerNAudioDI
{
    public static IServiceCollection AddModules(IServiceCollection services)
    {
        ServicesModule.AddServicesNAudio(services);
        return services;
    }
}