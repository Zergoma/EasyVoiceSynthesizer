using System.Windows;

using EasyVoiceSynthesizer.WpfUI.Pages;

namespace EasyVoiceSynthesizer.WpfUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(SynthesizerPage pageSynth)
    {
        InitializeComponent();

        MyFrame.Content = pageSynth;
    }
}