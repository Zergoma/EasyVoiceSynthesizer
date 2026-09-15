using System.Windows.Controls;

using EasyVoiceSynthesizer.ViewModels;

namespace EasyVoiceSynthesizer.WpfUI.Pages;

/// <summary>
/// Interaction logic for SynthesizerPage.xaml
/// </summary>
public partial class SynthesizerPage : Page
{
    public SynthesizerPage(SynthesizerViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
