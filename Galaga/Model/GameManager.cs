using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        private readonly PlayerManager playerManager;
        private readonly EnemiesManager enemyManager;
        private readonly PlayerBulletManager playerBulletManager;
        private readonly DispatcherTimer gameTimer;
        private readonly EnemyBulletManager enemyBulletManager;

        #endregion

        #region Properties

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
        /// <exception cref="ArgumentNullException">thrown if the Canvas is null</exception>
        public GameManager(Canvas canvas)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            this.playerManager = new PlayerManager(canvas);
            this.enemyManager = new EnemiesManager(canvas);
            this.playerBulletManager = new PlayerBulletManager(canvas, this.enemyManager, this.playerManager);
            this.enemyBulletManager = new EnemyBulletManager(canvas, this.playerManager);
            this.gameTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 20) };

            this.GameLevel = 1;

            this.initializeGame();
        }

        #endregion

        #region Methods

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

        private void initializeGame()
        {
            this.playerManager.SetupPlayer();
            this.enemyManager.SetupEnemies();
            this.setupTimers();

            this.playerBulletManager.EnemyStruck += this.onEnemyStruck;
            this.enemyBulletManager.PlayerStruck += this.playerManager.OnPlayerStruck;

            this.LevelOver += this.onLevelOver;
            this.LevelOver += this.enemyManager.OnLevelOver;
        }

        private void setupTimers()
        {
            this.enemyBulletManager.EnemyRandomShootTimer.Tick += this.shootEnemyBulletTickEvent;
            this.enemyBulletManager.EnemyRandomShootTimer.Start();

            this.gameTimer.Tick += this.moveBulletTickEvent;
            this.gameTimer.Start();
        }

        private void moveBulletTickEvent(object sender, object e)
        {
            this.playerBulletManager.MoveBullet();
            this.enemyBulletManager.MoveBullet();
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

            if (this.enemyManager.Count == 0)
            {
                this.LevelOver?.Invoke(this, this.GameLevel);
            }
        }

        /// <summary>
        ///     Stops the game
        ///     PostCondition: this.gameTimer.IsEnabled == false, this.enemyBulletManager.EnemyRandomShootTimer.IsEnabled == false
        /// </summary>
        public void StopGame()
        {
            this.gameTimer.Stop();
            this.enemyBulletManager.EnemyRandomShootTimer.Stop();
            this.enemyManager.StopEnemyMoveTimer();
        }

        private void onLevelOver(object sender, int level)
        {
            this.GameLevel++;
            this.enemyManager.SetEnemySettings(this.GameLevel);
            this.enemyBulletManager.Clear();
            this.playerBulletManager.Clear();
        }

        #endregion
    }
}