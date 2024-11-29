using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.Datatier;
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

        private readonly ScoresFileManager scoresFileManager;
        private readonly ScoreEntries scoreEntries;
        private string playerName;

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

            this.gameManager.LivesChanged += this.onLivesUpdate;
            this.gameManager.PlayerStruck += this.onPlayerDeath;
            this.gameManager.EnemyStruck += this.onScoreUpdate;
            this.gameManager.LevelOver += this.onLevelOver;
            this.PlayerMadeScoreboard += this.onPlayerMadeScoreboard;

            this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, MillisecondsForTimer) };
            this.timer.Tick += this.timerTickEvent;
            this.timer.Start();

            this.scoresFileManager = new ScoresFileManager();
            this.scoreEntries = this.scoresFileManager.ReadScoreEntries();
        }

        #endregion

        #region Methods

        private event EventHandler<EventArgs> PlayerMadeScoreboard;

        private async void onPlayerMadeScoreboard(object sender, EventArgs e)
        {
            //TODO figure out a way to show the scoreboard only after the user has put in their name, as soon as you navigate to the frame of the scoreboard it intializes it. 
            await this.promptUserForName();
            var scoreboardEntry =
                new ScoreboardEntry(this.playerName, this.gameManager.GameLevel, this.gameManager.PlayerScore);
            this.scoreEntries.Add(scoreboardEntry);
            this.scoresFileManager.WriteScores(this.scoreEntries);
        }

        private async Task promptUserForName()
        {
            var textBox = new TextBox
            {
                PlaceholderText = "Enter your name",
                Width = 200,
                Height = 30
            };
            var contentDialog = new ContentDialog
            {
                Title = "Scoreboard Entry",
                Content = textBox,
                PrimaryButtonText = "Submit"
            };

            await contentDialog.ShowAsync();
            this.playerName = textBox.Text;
        }

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

        private void onLivesUpdate(object sender, int lives)
        {
            var numOfLives = this.gameManager.PlayerLives;
            this.playerLivesTextBlock.Text = $"Lives : {numOfLives}";
        }

        private async void onLevelOver(object sender, int level)
        {
            if (this.gameManager.AreAllEnemiesDestroyed && level == GameManager.LastLevel)
            {
                this.gameManager.StopGame();
                Window.Current.CoreWindow.KeyDown -= this.coreWindowOnKeyDown;
                if (this.scoreEntries.IsScoreInTop10(this.gameManager.PlayerScore))
                {
                    this.PlayerMadeScoreboard?.Invoke(this, EventArgs.Empty);
                }

                Frame rootFrame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(rootFrame));
                rootFrame.Navigate(typeof(ScoreBoard));
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
                this.timer.Stop();
                this.gameManager.StopGame();

                if (this.scoreEntries.IsScoreInTop10(this.gameManager.PlayerScore))
                {
                    this.PlayerMadeScoreboard?.Invoke(this, EventArgs.Empty);
                }

                Frame rootFrame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(rootFrame));
                rootFrame.Navigate(typeof(ScoreBoard));
            }
        }

        #endregion

        //private async void showWinDialog()
        //{
        //    var dialog = new ContentDialog
        //    {
        //        Title = "Thanks For Playing",
        //        Content = "Game Over, You Win!"
        //    };
        //    await dialog.ShowAsync();
        //}

        //private async void showGameOverDialog()
        //{
        //    var dialog = new ContentDialog
        //    {
        //        Title = "Thanks For Playing",
        //        Content = "Game Over, You lose"
        //    };
        //    await dialog.ShowAsync();
        //}
    }
}