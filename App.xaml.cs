using System;
using Microsoft.UI.Xaml;

namespace ImageToWebpConverter
{
    public partial class App : Application
    {
        private Window? _window;

        public App()
        {
            this.InitializeComponent();

            this.UnhandledException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Unhandled exception: {e.Message}");
                e.Handled = true;
            };
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
