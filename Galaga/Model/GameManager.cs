using System;
using Windows.UI.Xaml.Controls;


namespace Galaga.Model
{
    /// <summary>
    ///     Manages the Galaga game play.
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Event args for an enemy death even
        /// </summary>
        public class EnemyDeathEventArgs : EventArgs
        {
            /// <summary>
            /// Gets the enemy
            /// </summary>
            public Enemy Enemy { get; }
            /// <summary>
            /// Creates an instance of the enemy death event args
            /// </summary>
            /// <param name="enemy">the enemy that was killed</param>
            public EnemyDeathEventArgs(Enemy enemy)
            {
                this.Enemy = enemy;
            }
        }

        #region Data members

        /// <summary>
        /// The event of an enemy being killed
        /// </summary>
        public event EventHandler<EnemyDeathEventArgs> EnemyStruck;

        /// <summary>
        /// The event of the player being struck
        /// </summary>
        public event EventHandler<EventArgs> PlayerStruck;

        private readonly PlayerManager playerManager;
        private readonly EnemiesManager enemyManager;
        private readonly BulletManager bulletManager;



        #endregion

        #region Properties


        /// <summary>
        ///     The score of the game
        /// </summary>
        public int Score { get; set; }


        /// <summary>
        ///     Check if all enemies are destroyed
        /// </summary>
        public bool AreAllEnemiesDestroyed => this.enemyManager.AreAllEnemiesDestroyed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     PostCondition: this.playerManager != null, this.enemyManager != null, this.bulletManager != null
        ///     <param name="canvas">the canvas of the game</param>
        /// </summary>
        public GameManager(Canvas canvas)
        {
            this.playerManager = new PlayerManager(canvas);
            this.enemyManager = new EnemiesManager(canvas);
            this.bulletManager = new BulletManager(canvas, this.playerManager);
            this.initializeGame();
        }

        #endregion

        #region Methods

        private void initializeGame()
        {
            this.bulletManager.SetupBulletTimers();
            this.bulletManager.BulletMoveTimer.Tick += this.checkForStruckEnemyTickEvent;
            this.bulletManager.BulletMoveTimer.Tick += this.checkForStruckPlayerTickEvent;

            this.playerManager.SetupPlayer();
            this.enemyManager.SetupEnemies();

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
        /// Shoots the players bullet and adds it to the bullet manager if it was shot
        /// </summary>
        public void ShootPlayerBullet()
        {
            //TODO update documentation and figure out how to maintain the same instance of a bullet across objects 
            Bullet bullet = this.playerManager.ShootPlayerBullet();
            if (bullet != null)
            {
                this.bulletManager.AddBullet(bullet);
            }
        }


        /// <summary>
        /// Gets the score formatted for the game
        /// </summary>
        /// <returns>the formatted score</returns>
        public String GetFormattedScore()
        {
            return "Score : " + this.Score.ToString();
        }

        private void checkForStruckPlayerTickEvent(Object sender, object e)
        {
            foreach (Enemy enemy in this.enemyManager)
            {
                if (enemy is Lvl3Enemy lvl3Enemy && this.playerManager.IsPlayerStruck(lvl3Enemy.Bullet))
                {
                    this.bulletManager.Clear();
                    this.bulletManager.BulletMoveTimer.Stop();
                    this.PlayerStruck?.Invoke(this, EventArgs.Empty);
                    break;
                }
            }
        }

        private void checkForStruckEnemyTickEvent(Object sender, object e)
        {
            Enemy enemy = this.enemyManager.RemoveStruckEnemy(this.playerManager, this.bulletManager);

            if (enemy != null)
            {
                this.EnemyStruck?.Invoke(this, new EnemyDeathEventArgs(enemy));
            }
        }

        private void shootRandomLevel3EnemyWeaponTickEvent(object sender, object e)
        {
            this.enemyManager.ShootRandomLevel3EnemyWeapon(this.bulletManager);
            this.bulletManager.EnemyRandomShootTimer.Interval = this.bulletManager.GetRandomInterval();
        }
        #endregion
    }
}