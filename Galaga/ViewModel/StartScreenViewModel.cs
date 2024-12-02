using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.Datatier;
using Galaga.View;
using Galaga.View.Sprites;
using Galaga.View.Sprites.PlayerSprites;

namespace Galaga.ViewModel
{
    /// <summary>
    ///     The view model for the start screen
    /// </summary>
    public class StartScreenViewModel
    {
        #region Data members

        private BaseSprite chosenSkin;
        private readonly ScoresFileManager scoresFileManager;
        private Frame frame;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the view model
        ///     PreCondition: Window.Current.Content != null
        ///     PostCondition: this.scoresFileManager != null, this.chosenSkin != null, this.frame == Window.Current.Content
        /// </summary>
        /// <exception cref="ArgumentNullException">thrown if current window's content is null</exception>
        public StartScreenViewModel()
        {
            this.scoresFileManager = new ScoresFileManager();
            this.chosenSkin = new PlayerSprite();
            this.frame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(this.frame));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the chosen skin
        ///     PreCondition: skin != null
        ///     PostCondition: this.chosenSkin == skin
        /// </summary>
        /// <param name="skin">the chosen skin</param>
        /// <exception cref="ArgumentNullException">thrown if skin is null</exception>
        public void SetChosenSkin(BaseSprite skin)
        {
            this.chosenSkin = skin ?? throw new ArgumentNullException(nameof(skin));
        }

        /// <summary>
        ///     Navigates to the scoreboard
        ///     PostCondition: this.frame != @prev
        /// </summary>
        public void NavigateToScoreboard()
        {
            this.frame.Navigate(typeof(ScoreBoard));
        }

        /// <summary>
        ///     Navigates to the game
        ///     PostCondition: this.frame != @prev
        /// </summary>
        public void NavigateToGame()
        {
            this.frame.Navigate(typeof(GameCanvas), this.chosenSkin);
        }

        /// <summary>
        ///     Resets the scoreboard
        ///     PostCondition: the scores file is wiped and the scoreboard is reset
        /// </summary>
        public async void DeleteScores()
        {
            await this.scoresFileManager.CreateFileManagement();
            this.scoresFileManager.ClearAllScores();
        }

        #endregion
    }
}