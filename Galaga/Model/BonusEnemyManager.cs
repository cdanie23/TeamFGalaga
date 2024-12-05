using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     The Enemy Manager for the Bonus Enemy
    /// </summary>
    public class BonusEnemyManager
    {
        #region Data members

        private const int BonusRandomLowerLimit = 5;
        private const int BonusRandomUpperLimit = 80;
        private const int NumOfBonusBullets = 15;
        private const int ShootingIntervalInMilliseconds = 750;
        private const int MoveIntervalInMilliseconds = 20;

        private readonly EnemyFactory enemyFactory;
        private readonly Canvas canvas;
        private readonly EnemiesManager enemiesManager;
        private readonly DispatcherTimer bonusTimer;
        private readonly DispatcherTimer shootingTimer;
        private readonly DispatcherTimer moveTimer;
        private readonly EnemyBulletManager bulletManager;
        private readonly GameSettings gameSettings;

        private ShootingEnemy bonusEnemy;
        private readonly Stack<Bullet> bonusBullets;

        private bool bonusPlaced;
        private bool bonusActive;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusEnemyManager" /> class.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="enemyManager">the enemy manager</param>
        /// <param name="bulletManager">the bullet manager of the enemy</param>
        /// <param name="gameSettings">the game settings to assign to the bonus enemy</param>
        public BonusEnemyManager(Canvas canvas, EnemiesManager enemyManager, EnemyBulletManager bulletManager,
            GameSettings gameSettings)
        {
            this.canvas = canvas;
            this.enemiesManager = enemyManager;
            this.enemyFactory = new EnemyFactory(gameSettings);
            this.bulletManager = bulletManager;
            this.bonusBullets = new Stack<Bullet>();
            this.gameSettings = gameSettings;

            var random = new Random();
            var randomNumber = random.Next(BonusRandomLowerLimit, BonusRandomUpperLimit);

            this.bonusTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, randomNumber, 0) };
            this.shootingTimer = new DispatcherTimer
                { Interval = new TimeSpan(0, 0, 0, 0, ShootingIntervalInMilliseconds) };
            this.moveTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, MoveIntervalInMilliseconds) };
            this.SetUpTimers();

            this.setupBonusBullets();
        }

        #endregion

        #region Methods

        private void setupBonusBullets()
        {
            for (var i = 0; i < NumOfBonusBullets; i++)
            {
                this.bonusBullets.Push(new Bullet(BulletType.Enemy, this.gameSettings.ShootingEnemyBulletSpeed));
            }
        }

        /// <summary>
        ///     Starts the bonus enemy timers and adds all the tick event
        ///     PostConditions: this.bonusTimer.IsEnabled == true, this.bonusTimer.Tick != null, this.moveTimer.Tick != null,
        ///     this.shootingTimer.Tick != null
        /// </summary>
        public void SetUpTimers()
        {
            this.bonusTimer.Tick += this.bonusTickEvent;
            this.bonusTimer.Start();

            this.moveTimer.Tick += this.bonusMoveTickEvent;
            this.shootingTimer.Tick += this.bonusShootTickEvent;
        }

        private void bonusTickEvent(object sender, object e)
        {
            if (!this.bonusPlaced)
            {
                this.PlaceBonusEnemy();
                this.bonusPlaced = true;
                this.bonusActive = true;

                this.shootingTimer.Start();
                this.moveTimer.Start();

                SoundPlayer.playBonusActiveSound();
            }
        }

        private void bonusMoveTickEvent(object sender, object e)
        {
            if (this.bonusActive)
            {
                this.Move();
                SoundPlayer.playBonusActiveSound();
            }
            else
            {
                this.bonusTimer.Stop();
                this.shootingTimer.Stop();
            }
        }

        private void bonusShootTickEvent(object sender, object e)
        {
            if (this.bonusActive)
            {
                this.Shoot();
            }
        }

        /// <summary>
        ///     Places the bonus enemy.
        /// </summary>
        public void PlaceBonusEnemy()
        {
            this.bonusEnemy = (ShootingEnemy)this.enemyFactory.CreateNewEnemy(0);
            this.bonusEnemy.Y = (this.canvas.Height - this.bonusEnemy.Height) / 2.0;
            this.canvas.Children.Add(this.bonusEnemy.Sprite);
            this.enemiesManager.AddBonusEnemy(this.bonusEnemy);
        }

        /// <summary>
        ///     Shoots this instance.
        /// </summary>
        public void Shoot()
        {
            if (this.bonusBullets.Count > 1)
            {
                var bullet = this.bonusBullets.Pop();
                this.bulletManager.AddBullet(bullet);
                this.canvas.Children.Add(bullet.Sprite);
                bullet.X = this.bonusEnemy.X + this.bonusEnemy.Width / 2;
                bullet.Y = this.bonusEnemy.Y + BulletManager.SpaceInBetweenBulletAndShip;
            }
        }

        /// <summary>
        ///     Moves this instance.
        /// </summary>
        public void Move()
        {
            var newLocation = this.bonusEnemy.X + this.bonusEnemy.SpeedX;
            if (newLocation < this.canvas.Width - this.bonusEnemy.Width)
            {
                this.bonusEnemy.MoveRight();
            }
            else
            {
                this.enemiesManager.RemoveEnemy(this.bonusEnemy);
                this.bonusActive = false;
            }
        }

        /// <summary>
        ///     Stops all behaviours of the bonus enemy
        ///     PostConditions: this.bonusTimer.IsEnabled == false, this.moveTimer.IsEnabled == false,
        ///     this.shootingTimer.IsEnabled == false
        /// </summary>
        public void StopBonusEnemyTimers()
        {
            this.bonusTimer.Stop();
            this.moveTimer.Stop();
            this.shootingTimer.Stop();
        }

        /// <summary>
        ///     Sets the instance to inactive.
        /// </summary>
        public void SetInactive()
        {
            this.bonusActive = false;
            this.StopBonusEnemyTimers();
        }

        #endregion
    }
}