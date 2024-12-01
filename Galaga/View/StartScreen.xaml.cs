using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Galaga.Datatier;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Galaga.View
{
    /// <summary>
    /// The start screen page 
    /// </summary>
    public sealed partial class StartScreen : Page
    {
        /// <summary>
        ///  Creates an instance of a start screen
        /// </summary>
        public StartScreen()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Width = Width, Height = Height };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(Width, Height));
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(frame));
            frame.Navigate(typeof(GameCanvas));
        }

        private void scoreboardButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(frame));
            frame.Navigate(typeof(ScoreBoard));
        }

        private async void resetScoreboardButton_Click(object sender, RoutedEventArgs e)
        {
            ScoresFileManager scoresFileManager = new ScoresFileManager();
            await scoresFileManager.CreateFileManagement();
            scoresFileManager.ClearAllScores();
        }
    }
}
