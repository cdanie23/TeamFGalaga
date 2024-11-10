using System;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     Manages the Galaga game play.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private readonly PlayerManager playerManager;
        private readonly EnemiesManager enemyManager;
        private readonly BulletManager bulletManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Pass through property to get the formatted score from the player manager
        /// </summary>
        public string Score => this.playerManager.FormattedScore;

        /// <summary>
        ///     Checks if all enemies are destroyed in the enemy manager
        /// </summary>
        public bool AreAllEnemiesDestroyed => this.enemyManager.AreAllEnemiesDestroyed;

        /// <summary>
        ///     Pass through property to get the formatted player lives in the player manager
        /// </summary>
        public string PlayerLives => this.playerManager.FormattedLives;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: canvas != null
        ///     PostCondition: this.playerManager != null, this.enemyManager != null, this.bulletManager != null
        /// </summary>
        /// <param name="canvas">the canvas of the game</param>
        /// <exception cref="ArgumentNullException">thrown if the canvas is null</exception>
        public GameManager(Canvas canvas)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            this.playerManager = new PlayerManager(canvas);
            this.enemyManager = new EnemiesManager(canvas);
            this.bulletManager = new BulletManager(canvas, this.playerManager);

            this.initializeGame();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The event of an enemy being killed
        /// </summary>
        public event EventHandler<EnemyDeathEventArgs> EnemyStruck;

        /// <summary>
        ///     The event of the player being struck
        /// </summary>
        public event EventHandler<EventArgs> PlayerStruck;

        private void initializeGame()
        {
            this.playerManager.SetupPlayer();
            this.PlayerStruck += this.playerManager.OnPlayerStruck;

            this.enemyManager.SetupEnemies();

            this.bulletManager.SetupBulletTimers();
            this.bulletManager.BulletMoveTimer.Tick += this.checkForStruckEnemyTickEvent;
            this.bulletManager.BulletMoveTimer.Tick += this.checkForStruckPlayerTickEvent;
            this.bulletManager.EnemyRandomShootTimer.Tick += this.shootRandomLevel3EnemyWeaponTickEvent;
            this.bulletManager.EnemyRandomShootTimer.Start();
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
                this.bulletManager.AddBullet(bullet);
            }
        }

        private void checkForStruckPlayerTickEvent(object sender, object e)
        {
            foreach (var bullet in this.bulletManager)
            {
                if (bullet.BulletType == BulletType.Enemy && this.playerManager.IsPlayerStruck(bullet))
                {
                    this.bulletManager.RemoveBullet(bullet);
                    this.PlayerStruck?.Invoke(this, EventArgs.Empty);
                    break;
                }
            }
        }

        private void checkForStruckEnemyTickEvent(object sender, object e)
        {
            var enemy = this.enemyManager.RemoveStruckEnemy(this.playerManager, this.bulletManager);

            if (enemy != null)
            {
                this.playerManager.Score += enemy.Points;
                this.EnemyStruck?.Invoke(this, new EnemyDeathEventArgs(enemy));
            }
        }

        private void shootRandomLevel3EnemyWeaponTickEvent(object sender, object e)
        {
            this.enemyManager.ShootRandomEnemyWeapon(this.bulletManager);
            this.bulletManager.EnemyRandomShootTimer.Interval = this.bulletManager.GetRandomInterval();
        }

        /// <summary>
        ///     Event args for an enemy death even
        /// </summary>
        public class EnemyDeathEventArgs : EventArgs
        {
            #region Properties

            /// <summary>
            ///     Gets the enemy
            /// </summary>
            public Enemy Enemy { get; }

            #endregion

            #region Constructors

            /// <summary>
            ///     Creates an instance of the enemy death event args
            /// </summary>
            /// <param name="enemy">the enemy that was killed</param>
            public EnemyDeathEventArgs(Enemy enemy)
            {
                this.Enemy = enemy;
            }

            #endregion
        }

        #endregion
    }
}