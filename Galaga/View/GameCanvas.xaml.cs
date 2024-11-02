using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Galaga.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameCanvas
    {

        #region Data members

        private readonly GameManager gameManager;
        
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameCanvas" /> class.
        /// </summary>
        public GameCanvas()
        {
            this.InitializeComponent();

            Width = this.canvas.Width;
            Height = this.canvas.Height;

            ApplicationView.PreferredLaunchViewSize = new Size { Width = Width, Height = Height };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(Width, Height));
            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;

            this.gameManager = new GameManager(this.canvas);
            this.gameManager.EnemyStruck += this.onScoreUpdate;
            this.gameManager.PlayerStruck += this.onPlayerDeath;
            this.gameManager.EnemyStruck += this.onAllEnemiesDead;
        }

        #endregion

        #region Methods

       

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Space:
                    this.gameManager.ShootPlayerBullet();
                    break;
            }
        }

        private void onScoreUpdate(Object sender, GameManager.EnemyDeathEventArgs args)
        {
            this.gameManager.Score += args.Enemy.Points;
            this.scoreTextBlock.Text = this.gameManager.GetFormattedScore();
        }

        private void onAllEnemiesDead(Object sender, GameManager.EnemyDeathEventArgs args)
        {
            if (this.gameManager.AreAllEnemiesDestroyed)
            {
                this.showWinDialog();
            }
        }
        private void onPlayerDeath(Object sender, EventArgs e)
        {
            this.showGameOverDialog();
        }

        private async void showWinDialog()
        {
            var dialog = new ContentDialog
            {
                Title = "Thanks For Playing",
                Content = "Game Over, You Win!"
            };
            await dialog.ShowAsync();
        }

        private async void showGameOverDialog()
        {
            var dialog = new ContentDialog
            {
                Title = "Thanks For Playing",
                Content = "Game Over, You lose"
            };
            await dialog.ShowAsync();
        }

     

        #endregion
    }
}