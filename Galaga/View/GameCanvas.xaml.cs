using System;
using System.Threading.Tasks;
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

        private const int MillisecondsForTimer = 50;

        private readonly GameManager gameManager;
        private readonly DispatcherTimer timer;
        private bool isLeft;
        private bool isRight;
        private bool isSpace;

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
            Window.Current.CoreWindow.KeyUp += this.onKeyUpEvent;
            this.gameManager = new GameManager(this.canvas);

            this.gameManager.PlayerStruck += this.onLivesUpdate;
            this.gameManager.PlayerStruck += this.onPlayerDeath;
            this.gameManager.EnemyStruck += this.onScoreUpdate;
            this.gameManager.LevelOver += this.onLevelOver;

            this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, MillisecondsForTimer) };
            this.timer.Tick += this.timerTickEvent;
            this.timer.Start();
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.isLeft = true;
                    break;
                case VirtualKey.Right:
                    this.isRight = true;
                    break;
                case VirtualKey.Space:
                    this.isSpace = true;
                    break;
            }
        }

        private void onKeyUpEvent(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.isLeft = false;
                    break;
                case VirtualKey.Right:
                    this.isRight = false;
                    break;
                case VirtualKey.Space:
                    this.isSpace = false;
                    break;
            }
        }

        private void timerTickEvent(object sender, object e)
        {
            if (this.isLeft)
            {
                this.gameManager.MovePlayerLeft();
            }

            if (this.isRight)
            {
                this.gameManager.MovePlayerRight();
            }

            if (this.isSpace)
            {
                this.gameManager.ShootPlayerBullet();
            }
        }

        private void onScoreUpdate(object sender, Enemy enemy)
        {
            var score = this.gameManager.PlayerScore;
            this.scoreTextBlock.Text = $"Score : {score}";
        }

        private void onLivesUpdate(object sender, object e)
        {
            var lives = this.gameManager.PlayerLives;
            this.playerLivesTextBlock.Text = $"Lives : {lives}";
        }

        private async void onLevelOver(object sender, int level)
        {
            if (this.gameManager.AreAllEnemiesDestroyed && level == GameManager.LastLevel)
            {
                this.showWinDialog();
                this.gameManager.StopGame();
                Window.Current.CoreWindow.KeyDown -= this.coreWindowOnKeyDown;
            }
            else
            {
                this.gameOverTextBlock.Visibility = Visibility.Visible;
                this.gameOverTextBlock.Text = $"Round: {this.gameManager.GameLevel}";

                await Task.Delay(1000);

                this.gameOverTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void onPlayerDeath(object sender, EventArgs e)
        {
            if (this.gameManager.PlayerLives == 0)
            {
                this.showGameOverDialog();
                this.timer.Stop();
                this.gameManager.StopGame();
            }
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