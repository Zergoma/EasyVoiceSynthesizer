using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.ViewModels.DI;

internal static class EasyVoiceViewModelModules
{
    public static IServiceCollection AddViewModules(IServiceCollection services)
    {
        services.AddTransient<SynthesizerViewModel>();

        return services;
    }
}