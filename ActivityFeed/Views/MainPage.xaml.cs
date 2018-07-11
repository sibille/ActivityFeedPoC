﻿using ActivityFeed.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ActivityFeed.Views
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
