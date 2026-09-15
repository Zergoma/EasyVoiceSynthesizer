using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.ViewModels.DI;

public static class EasyVoiceViewModelDI
{
    public static IServiceCollection AddDIModules(IServiceCollection services)
    {
        EasyVoiceViewModelModules.AddViewModules(services);

        return services;
    }
}
