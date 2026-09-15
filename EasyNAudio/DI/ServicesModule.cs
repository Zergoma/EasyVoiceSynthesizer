using EasyVoiceSynthesizer.NAudio.Services;

using Microsoft.Extensions.DependencyInjection;

using EasyVoiceSynthesizer.Application.Interfaces;

namespace EasyVoiceSynthesizer.NAudio.DI;

internal static class ServicesModule
{
    public static IServiceCollection AddServicesNAudio(IServiceCollection services)
    {
        services.AddTransient<IAudioPlayer, NAudioPlayer>();
        return services;
    }
}
