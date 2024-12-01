// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using System;
using System.Diagnostics;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScoreBoard
    {
        #region Constructors

        /// <summary>
        ///     The scoreboard of Galaga
        /// </summary>
        public ScoreBoard()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void sortByNameButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.SortByNameDescending();
        }

        private void sortByLevelButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.SortByLevelDescending();
        }

        private void sortByScoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.SortByScore();
        }

        #endregion

        private void playAgainButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(frame));
            frame.Navigate(typeof(GameCanvas));
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}