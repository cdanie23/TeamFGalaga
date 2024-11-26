using System;
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
        private const int BonusRandomUpperLimit = 120;
        private readonly EnemyFactory enemyFactory;
        private readonly Canvas canvas;
        private readonly EnemiesManager enemiesManager;
        private readonly DispatcherTimer bonusTimer;
        private readonly DispatcherTimer shootingTimer;

        private ShootingEnemy bonusEnemy;

        private bool bonusPlaced;
        private bool bonusActive;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusEnemyManager" /> class.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="enemyManager"></param>
        public BonusEnemyManager(Canvas canvas, EnemiesManager enemyManager)
        {
            this.canvas = canvas;
            this.enemiesManager = enemyManager;
            this.enemyFactory = new EnemyFactory(new GameSettings());

            var random = new Random();
            var randomNumber = random.Next(BonusRandomLowerLimit, BonusRandomUpperLimit);

            this.bonusTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, randomNumber, 0) };
            this.shootingTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 20) };
            this.setUpTimers();
        }

        #endregion

        #region Methods

        private void setUpTimers()
        {
            this.bonusTimer.Tick += this.bonusTickEvent;
            this.bonusTimer.Start();

            this.shootingTimer.Tick += this.bonusActiveTickEvent;
        }

        private void bonusTickEvent(object sender, object e)
        {
            if (!this.bonusPlaced)
            {
                this.PlaceBonusEnemy();
                this.bonusPlaced = true;
                this.bonusActive = true;

                this.shootingTimer.Start();
            }
        }

        private void bonusActiveTickEvent(object sender, object e)
        {
            if (this.bonusActive)
            {
                this.Shoot();
                this.Move();
                SoundPlayer.playBonusActiveSound();
            }
            else
            {
                this.bonusTimer.Stop();
                this.shootingTimer.Stop();
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

        #endregion
    }
}