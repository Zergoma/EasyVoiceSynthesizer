using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.Application.DI;

public static class EasyVoiceApplicationDI
{
    public static IServiceCollection AddModules(IServiceCollection services)
    {
        EasyVoiceSynthesizerOrchestratorsModule.AddServices(services);
        return services;
    }
}
