using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SampleCode.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SampleCode.Main;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LoginWindow : Window
{
    public LoginWindow()
    {
        this.InitializeComponent();
        ContentFrame.Navigate(typeof(LoginPage));
    }

    private void openTestPage_Click(object sender, RoutedEventArgs e)
    {
        //Window b = new Window();
        //TestPage2 t=new TestPage2();
        //ContentFrame.Navigate(t);
        //Window b= new Window();
        //b.Content = TestPage2.ContentProperty;
        //ContentFrame.Navigate(typeof(TestPage2), null);
        //ContentFrame.Navigate(typeof(AboutPage));

    }
    private void openLoginPage_Click(object sender, RoutedEventArgs e)
    {
        //Window b = new Window();
        ContentFrame.Navigate(typeof(LoginPage));

    }
}
