using EasyVoiceSynthesizer.Application.Interfaces;
using EasyVoiceSynthesizer.SherpaOnnx.Services;

using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.SherpaOnnx.DI;

internal static class EasyVoiceSynthesizerServicesModule
{
    public static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddTransient<IVoiceSynthesizer, SynthesizerSherpaOnnx>();

        return services;
    }
}
