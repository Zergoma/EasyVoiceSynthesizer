using EasyVoiceSynthesizer.Application.Interfaces;
using EasyVoiceSynthesizer.Application.Orchestrators;

using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.Application.DI;

internal static class EasyVoiceSynthesizerOrchestratorsModule
{
    public static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddTransient<ISynthetizerOrchestrator, TextToSpeechOrchestrator>();
        return services;
    }
}
