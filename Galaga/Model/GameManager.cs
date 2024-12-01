using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Manages the Galaga game play.
    /// </summary>
    public class GameManager
    {
        #region Data members

        /// <summary>
        ///     The last level of the game
        /// </summary>
        public const int LastLevel = 3;

        private const int BonusEnemyLevel = 0;

        private readonly PlayerManager playerManager;
        private readonly EnemiesManager enemyManager;
        private readonly PlayerBulletManager playerBulletManager;
        private readonly DispatcherTimer gameTimer;
        private readonly EnemyBulletManager enemyBulletManager;
        private readonly BonusEnemyManager bonusEnemyManager;
        private readonly Canvas canvas;

        #endregion

        #region Properties

        /// <summary>
        ///     Get or set the players name
        /// </summary>
        public string PlayerName
        {
            get => this.playerManager.Player.Name;
            set => this.playerManager.Player.Name = value;
        }

        /// <summary>
        ///     Checks if all enemies are destroyed in the enemy manager
        /// </summary>
        public bool AreAllEnemiesDestroyed => this.enemyManager.AreAllEnemiesDestroyed;

        /// <summary>
        ///     Pass thru property to access the number of lives of the player
        /// </summary>
        public int PlayerLives => this.playerManager.NumOfLives;

        /// <summary>
        ///     Pass thru property to access the player's score
        /// </summary>
        public int PlayerScore => this.playerManager.Score;

        /// <summary>
        ///     Gets or sets the current game level
        /// </summary>
        /// <param name="value">the value to set</param>
        public int GameLevel { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: Canvas != null
        ///     PostCondition: this.playerManager != null, this.enemyManager != null, this.bulletManager != null
        /// </summary>
        /// <param name="canvas">the Canvas of the game</param>
        /// <param name="playerSkin">the player skin chosen by the user</param>
        /// <exception cref="ArgumentNullException">thrown if the Canvas is null</exception>
        public GameManager(Canvas canvas, BaseSprite playerSkin)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            var gameSettings = new GameSettings();
            this.playerManager = new PlayerManager(canvas);
            this.enemyManager = new EnemiesManager(canvas, gameSettings);
            this.enemyBulletManager = new EnemyBulletManager(canvas, this.playerManager);
            this.bonusEnemyManager =
                new BonusEnemyManager(canvas, this.enemyManager, this.enemyBulletManager, gameSettings);

            this.playerBulletManager =
                new PlayerBulletManager(canvas, this.enemyManager, this.playerManager, this.bonusEnemyManager);
            this.gameTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 20) };

            this.GameLevel = 1;

            this.initializeGame(playerSkin);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The players lives changed event
        /// </summary>
        public event EventHandler<int> LivesChanged;

        /// <summary>
        ///     Pass thru property used to access the player struck event in the enemy bullet manager
        /// </summary>
        public event EventHandler<EventArgs> PlayerStruck
        {
            add => this.enemyBulletManager.PlayerStruck += value;
            remove => this.enemyBulletManager.PlayerStruck -= value;
        }

        /// <summary>
        ///     Pass thru property used to access the enemy struck event in the player bullet manager
        /// </summary>
        public event EventHandler<Enemy> EnemyStruck
        {
            add => this.playerBulletManager.EnemyStruck += value;
            remove => this.playerBulletManager.EnemyStruck -= value;
        }

        /// <summary>
        ///     The level over event
        /// </summary>
        public event EventHandler<int> LevelOver;

        private void initializeGame(BaseSprite playerSkin)
        {
            this.playerManager.SetupPlayer(playerSkin);
            this.enemyManager.SetupEnemies();
            this.setupTimers();
            this.bonusEnemyManager.SetUpTimers();

            this.playerBulletManager.EnemyStruck += this.onEnemyStruck;
            this.enemyBulletManager.PlayerStruck += this.onPlayerStruck;

            this.LevelOver += this.onLevelOver;
            this.LevelOver += this.enemyManager.OnLevelOver;

            this.LivesChanged += this.onLivesChanged;
        }

        private void setupTimers()
        {
            this.enemyBulletManager.EnemyRandomShootTimer.Tick += this.shootEnemyBulletTickEvent;
            this.enemyBulletManager.EnemyRandomShootTimer.Start();

            this.gameTimer.Tick += this.moveBulletTickEvent;
            this.gameTimer.Tick += this.enemyManager.MoveEnemyTickEvent;
            this.gameTimer.Start();
        }

        private void moveBulletTickEvent(object sender, object e)
        {
            this.playerBulletManager.MoveBullet();
            this.enemyBulletManager.MoveBullet();
        }

        private void onPlayerStruck(object sender, object e)
        {
            this.LivesChanged?.Invoke(this, -1);
            if (this.playerManager.NumOfLives == 0)
            {
                this.canvas.Children.Remove(this.playerManager.Player.Sprite);
            }

            this.playerManager.IsInvulnerable = true;
            this.playerManager.InvulnerabilityTimer.Start();

            ExplosionAnimator.PlayExplosion(this.canvas, this.playerManager.Player.X, this.playerManager.Player.Y);

            SoundPlayer.playExplodeSound();
        }

        /// <summary>
        ///     Moves the Player left.
        /// </summary>
        public void MovePlayerLeft()
        {
            this.playerManager.MovePlayerLeft();
        }

        /// <summary>
        ///     Moves the Player right.
        /// </summary>
        public void MovePlayerRight()
        {
            this.playerManager.MovePlayerRight();
        }

        /// <summary>
        ///     Shoots the players bullet and adds it to the bullet manager if it was shot
        /// </summary>
        public void ShootPlayerBullet()
        {
            var bullet = this.playerManager.ShootPlayerBullet();
            if (bullet != null)
            {
                this.playerBulletManager.AddBullet(bullet);
                SoundPlayer.playShootSound();
            }
        }

        private void shootEnemyBulletTickEvent(object sender, object e)
        {
            this.enemyManager.ShootRandomEnemyWeapon(this.enemyBulletManager);
        }

        private void onEnemyStruck(object sender, Enemy enemy)
        {
            this.playerManager.Player.BulletsAvailable.Push(new Bullet(BulletType.Player, Player.PlayerBulletSpeed));
            this.playerManager.Score += enemy.Points;
            this.enemyManager.RemoveEnemy(enemy);
            SoundPlayer.playExplodeSound();

            if (enemy.Level == BonusEnemyLevel)
            {
                this.LivesChanged?.Invoke(this, 1);
                this.playerManager.TemporaryPowerUp();
                SoundPlayer.playBonusGottenSound();
            }

            if (this.enemyManager.Count == 0)
            {
                this.LevelOver?.Invoke(this, this.GameLevel);
            }
        }

        private void onLivesChanged(object sender, int lives)
        {
            this.playerManager.NumOfLives += lives;
        }

        /// <summary>
        ///     Stops the game
        ///     PostCondition: this.gameTimer.IsEnabled == false, this.enemyBulletManager.EnemyRandomShootTimer.IsEnabled == false
        /// </summary>
        public void StopGame()
        {
            if (this.GameLevel != 4)
            {
                SoundPlayer.playLoseSound();
            }

            this.gameTimer.Stop();
            this.enemyBulletManager.EnemyRandomShootTimer.Stop();
            this.bonusEnemyManager.StopBonusEnemyTimers();
        }

        private void onLevelOver(object sender, int level)
        {
            this.GameLevel++;
            this.enemyManager.SetEnemySettings(this.GameLevel);
            this.enemyBulletManager.Clear();
            this.playerBulletManager.Clear();

            SoundPlayer.playWinSound();
        }

        #endregion
    }
}