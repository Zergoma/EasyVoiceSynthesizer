using System.Windows;

using EasyVoiceSynthesizer.Application.DI;
using EasyVoiceSynthesizer.NAudio.DI;
using EasyVoiceSynthesizer.SherpaOnnx.DI;
using EasyVoiceSynthesizer.ViewModels.DI;
using EasyVoiceSynthesizer.WpfUI.Pages;
using EasyVoiceSynthesizer.WpfUI.Presenters;


using Microsoft.Extensions.DependencyInjection;

using VmPresenters = EasyVoiceSynthesizer.ViewModels.Presenters;

using SysWin = System.Windows;

namespace EasyVoiceSynthesizer.WpfUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : SysWin.Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();
        
        // Audio player implementation
        EasyVoiceSynthesizerNAudioDI.AddModules(services);
        
        // wpf specifics UI needed throught presenter
        services.AddTransient<VmPresenters.IDialogShow, DialogShowWpf>();

        // Viewmodels (need UI specific presenters) 
        EasyVoiceViewModelDI.AddDIModules(services);

        // sherpa Onnx model + implementation
        EasyVoiceSynthesizerSherpaOnnxDI.AddSherpaOnnxModules(services);

        // application's services and orchestrators
        EasyVoiceApplicationDI.AddModules(services);

        //EasyVoiceSynthesizerSherpaOnnxDI.AddModules(services);
        
        // UI page and window
        services.AddTransient<SynthesizerPage>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }

        base.OnExit(e);
    }
}
