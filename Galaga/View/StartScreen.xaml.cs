using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.Datatier;
using Galaga.View.Sprites;
using Galaga.View.Sprites.PlayerSprites;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Galaga.View
{
    /// <summary>
    ///     The start screen page
    /// </summary>
    public sealed partial class StartScreen
    {
        #region Data members

        private BaseSprite chosenSkin;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of a start screen
        /// </summary>
        public StartScreen()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Width = Width, Height = Height };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(Width, Height));

            this.chosenSkin = new PlayerSprite();
        }

        #endregion

        #region Methods

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(frame));
            frame.Navigate(typeof(GameCanvas), this.chosenSkin);
        }

        private void scoreboardButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(frame));
            frame.Navigate(typeof(ScoreBoard));
        }

        private async void resetScoreboardButton_Click(object sender, RoutedEventArgs e)
        {
            var scoresFileManager = new ScoresFileManager();
            await scoresFileManager.CreateFileManagement();
            scoresFileManager.ClearAllScores();
        }

        private void onSkin1Checked(object sender, RoutedEventArgs e)
        {
            this.chosenSkin = new PlayerSprite();
        }

        private void onSkin2Checked(object sender, RoutedEventArgs e)
        {
            this.chosenSkin = new PlayerSprite2();
        }

        private void onSkin3Checked(object sender, RoutedEventArgs e)
        {
            this.chosenSkin = new PlayerSprite3();
        }

        #endregion
    }
}