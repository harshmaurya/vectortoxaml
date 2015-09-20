using System;
using System.Windows;
using Common.Themes;

namespace VectorToXamlConvertor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeThemes();
        }

        private static void InitializeThemes()
        {
            ThemesManager.Instance.Init(ThemeMode.Application);
            ThemesManager.Instance.CurrentTheme = ThemeNames.MauryaDark;
            ThemesManager.Instance.RegisterApplication(Current);
            ThemesManager.Instance.AddDictionary(new ResourceDictionary { Source = new Uri("CustomStyles.xaml", UriKind.Relative) });

        }
    }
}
