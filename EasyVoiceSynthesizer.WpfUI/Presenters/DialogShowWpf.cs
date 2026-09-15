using System.Windows;

using EasyVoiceSynthesizer.ViewModels.Presenters;

namespace EasyVoiceSynthesizer.WpfUI.Presenters;

public class DialogShowWpf : IDialogShow
{
    public async Task ShowMe(string val)
    {
        MessageBox.Show(val);
    }
}
