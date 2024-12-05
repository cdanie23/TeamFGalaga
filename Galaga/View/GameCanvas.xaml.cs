using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Galaga.Datatier;
using Galaga.Model;
using Galaga.View.Sprites;

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
        private const double NameTextBoxWidth = 200;
        private const double NameTextBoxHeight = 30;
        private const int MaxNameLength = 10;
        private const string PromptForName = "Enter your name";
        private static readonly object PromptScoreboard = "Congratulations you made the scoreboard";
        private const string PrimaryButtonText = "Submit";
        private const int MillisecondsDelay = 1000;

        private GameManager gameManager;
        private readonly DispatcherTimer timer;
        private bool isLeft;
        private bool isRight;
        private bool isSpace;

        private readonly ScoresFileManager scoresFileManager;
        private ScoreEntries scoreEntries;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameCanvas" /> class.
        ///     PostConditions: this.scoresFileManager != null, this.timer.IsEnabled == true
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

            this.scoresFileManager = new ScoresFileManager();
            this.setupFileManagement();

            this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, MillisecondsForTimer) };
            this.timer.Tick += this.timerTickEvent;
            this.timer.Start();
        }

        #endregion

        #region Methods

        private event EventHandler<EventArgs> PlayerMadeScoreboard;

        /// <summary>
        ///     On navigation the gameManager gets initialized with the skin chosen by the player
        ///     PostCondition: this.gameManager != null, this.PlayerMadeScoreboard != null
        /// </summary>
        /// <param name="e">the event args passed when navigate to is provoked</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var chosenSkin = e.Parameter as BaseSprite;

            this.gameManager = new GameManager(this.canvas, chosenSkin);
            this.gameManager.LivesChanged += this.onLivesUpdate;
            this.gameManager.PlayerStruck += this.onPlayerDeath;
            this.gameManager.EnemyStruck += this.onScoreUpdate;
            this.gameManager.LevelOver += this.onLevelOver;
            this.PlayerMadeScoreboard += this.onPlayerMadeScoreboard;
        }

        private async void setupFileManagement()
        {
            await this.scoresFileManager.CreateFileManagement();
            this.scoreEntries = this.scoresFileManager.ReadScoreEntries();
        }

        private async void onPlayerMadeScoreboard(object sender, EventArgs e)
        {
            await this.promptUserToAddScoreAndNavigateToScoreboard();
        }

        private async Task promptUserForName()
        {
            var textBox = new TextBox
            {
                PlaceholderText = PromptForName,
                Width = NameTextBoxWidth,
                Height = NameTextBoxHeight,
                MaxLength = MaxNameLength
            };
            var contentDialog = new ContentDialog
            {
                Title = PromptScoreboard,
                Content = textBox,
                PrimaryButtonText = PrimaryButtonText
            };

            await contentDialog.ShowAsync();
            this.gameManager.PlayerName = textBox.Text;
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
                this.timer.Stop();
                this.gameManager.StopGame();
                Window.Current.CoreWindow.KeyDown -= this.coreWindowOnKeyDown;
                if (this.scoreEntries.IsScoreInTop10(this.gameManager.PlayerScore))
                {
                    this.PlayerMadeScoreboard?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.navigateToScoreboard();
                }
            }
            else
            {
                this.gameOverTextBlock.Visibility = Visibility.Visible;
                this.gameOverTextBlock.Text = $"Round: {this.gameManager.GameLevel}";

                await Task.Delay(MillisecondsDelay);

                this.gameOverTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private async Task promptUserToAddScoreAndNavigateToScoreboard()
        {
            await this.promptUserForName();
            var scoreboardEntry =
                new ScoreboardEntry(this.gameManager.PlayerName, this.gameManager.GameLevel,
                    this.gameManager.PlayerScore);
            if (this.scoreEntries.Count == ScoreEntries.MaxNumberOfScores)
            {
                this.scoreEntries.ReplaceLowestScoreEntry(scoreboardEntry);
            }
            else
            {
                this.scoreEntries.Add(scoreboardEntry);
            }

            this.scoresFileManager.WriteScores(this.scoreEntries);
            this.navigateToScoreboard();
        }

        private void navigateToScoreboard()
        {
            Frame rootFrame = Window.Current.Content as Frame ?? throw new ArgumentNullException(nameof(rootFrame));
            rootFrame.Navigate(typeof(ScoreBoard));
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

                this.navigateToScoreboard();
            }
        }

        #endregion
    }
}