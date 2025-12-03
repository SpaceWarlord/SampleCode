using Microsoft.UI.Xaml;
using SampleCode.ViewModels.Data;
using SampleCode.Interfaces;
using SampleCode.Extensions;

namespace SampleCode.Main
{
    
    public sealed partial class Shell : Window, INavigation
    {
        public string pagePath = "SampleCode.Views.";
        public Shell()
        {            
            InitializeComponent();
            var appWindow = this.GetAppWindow();
            appWindow.SetIcon("Assets/Beer.ico");
            Root.RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Light : ElementTheme.Dark;            
            currentUserTextBlock.Text = (Application.Current.Resources["currentUser"] as UserViewModel).Username;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Settings.Containers[App.SettingsContainer].Values[KeyWord.IS_FIRST_TIME] = true;           
        }
    }
}
