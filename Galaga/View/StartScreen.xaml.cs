using Windows.UI.Xaml;
using Galaga.View.Sprites.PlayerSprites;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Galaga.View
{
    /// <summary>
    ///     The start screen page
    /// </summary>
    public sealed partial class StartScreen
    {
        #region Constructors

        /// <summary>
        ///     Creates an instance of a start screen
        /// </summary>
        public StartScreen()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.NavigateToGame();
        }

        private void scoreboardButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.NavigateToScoreboard();
        }

        private void resetScoreboardButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DeleteScores();
        }

        private void onSkin1Checked(object sender, RoutedEventArgs e)
        {
            this.viewModel.SetChosenSkin(new PlayerSprite());
        }

        private void onSkin2Checked(object sender, RoutedEventArgs e)
        {
            this.viewModel.SetChosenSkin(new PlayerSprite2());
        }

        private void onSkin3Checked(object sender, RoutedEventArgs e)
        {
            this.viewModel.SetChosenSkin(new PlayerSprite3());
        }

        #endregion
    }
}