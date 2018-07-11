using System;

using ActivityFeed.Switch.ViewModels;

using Windows.UI.Xaml.Controls;

namespace ActivityFeed.Switch.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
