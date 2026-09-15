using Microsoft.Extensions.DependencyInjection;

namespace EasyVoiceSynthesizer.SherpaOnnx.DI;

public static class EasyVoiceSynthesizerSherpaOnnxDI
{
    public static IServiceCollection AddSherpaOnnxModules(IServiceCollection services)
    {
        EasyVoiceSynthesizerServicesModule.AddServices(services);
        return services;
    }
}
