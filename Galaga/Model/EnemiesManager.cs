using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     Encapsulates all the enemies in the game of Galaga
    /// </summary>
    public class EnemiesManager : IEnumerable<Enemy>
    {
        #region Data members

        private const int NumOfLvl1Enemies = 3;
        private const int NumOfLvl2Enemies = 4;
        private const int NumOfLvl3Enemies = 4;
        private const int NumOfLvl4Enemies = 5;

        private const int EnemyRow1 = 300;
        private const int EnemyRow2 = 200;
        private const int EnemyRow3 = 100;
        private const int EnemyRow4 = 0;

        private const int MoveTimerMilliseconds = 200;
        private const int NumOfStepsInEachDirectionOfOrigin = 10;

        private readonly Collection<Enemy> enemies;
        private readonly Canvas canvas;
        private readonly double canvasWidth;

        private readonly DispatcherTimer moveTimer;

        private int stepsTaken;
        private int numOfStepsInEachDirection;

        #endregion

        #region Properties

        /// <summary>
        ///     Checks if all enemy ships are destroyed
        /// </summary>
        public bool AreAllEnemiesDestroyed => this.Count == 0;

        /// <summary>
        ///     Gets the count
        /// </summary>
        public int Count => this.enemies.Count;

        private bool EnemiesDoneMovingInDirection => this.stepsTaken == this.numOfStepsInEachDirection;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the enemy manager class
        ///     Precondition: canvas != null
        ///     Post-conditions: this.enemies != null , this.stepsTaken == 0, this.numOfStepsInEachDirection == 5, this.canvas ==
        ///     canvas, this.canvasHeight == canvas.Height, this.canvasWidth == canvas.Width
        ///     , this.bulletManager != null, this.moveTimer != null
        ///     <param name="canvas">the canvas of the game</param>
        /// </summary>
        public EnemiesManager(Canvas canvas)
        {
            this.enemies = new Collection<Enemy>();
            this.createEnemies();

            this.stepsTaken = 0;
            this.numOfStepsInEachDirection = 5;

            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.canvasWidth = canvas.Width;

            this.moveTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, MoveTimerMilliseconds) };
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the enumerator for the collection
        /// </summary>
        /// <returns>the enumerator for the collection</returns>
        public IEnumerator<Enemy> GetEnumerator()
        {
            var enumerator = ((IEnumerable<Enemy>)this.enemies).GetEnumerator();
            enumerator.Dispose();
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var enumerator = ((IEnumerable<Enemy>)this.enemies).GetEnumerator();
            enumerator.Dispose();
            return enumerator;
        }

        private void createEnemies()
        {
            for (var i = 0; i < NumOfLvl1Enemies; i++)
            {
                this.enemies.Add(new Lvl1Enemy());
            }

            for (var i = 0; i < NumOfLvl2Enemies; i++)
            {
                this.enemies.Add(new Lvl2Enemy());
            }

            for (var i = 0; i < NumOfLvl3Enemies; i++)
            {
                this.enemies.Add(new Lvl3Enemy());
            }

            for (var i = 0; i < NumOfLvl4Enemies; i++)
            {
                this.enemies.Add(new Lvl4Enemy());
            }
        }

        /// <summary>
        ///     Shoots a randomly selected enemy weapon
        ///     Precondition: bulletManager != null
        ///     Post-condition: this.canvas.Children++
        /// </summary>
        /// <exception cref="ArgumentNullException">thrown if the bullet manager is null</exception>
        /// <param name="bulletManager">the bullet manager to add the bullet to</param>
        public void ShootRandomEnemyWeapon(BulletManager bulletManager)
        {
            if (bulletManager == null)
            {
                throw new ArgumentNullException(nameof(bulletManager));
            }

            var enemy = this.getRandomShootingEnemy();
            if (enemy != null && !this.canvas.Children.Contains(enemy.Bullet.Sprite))
            {
                this.canvas.Children.Add(enemy.Bullet.Sprite);
                enemy.Bullet.X = enemy.X + enemy.Width / 2;
                enemy.Bullet.Y = enemy.Y + enemy.Height;
                bulletManager.AddBullet(enemy.Bullet);
            }
        }

        private void timerTickMoveLeft(object sender, object e)
        {
            if (this.EnemiesDoneMovingInDirection)
            {
                this.moveTimer.Tick -= this.timerTickMoveLeft;
                this.moveTimer.Tick += this.timerTickMoveRight;

                this.stepsTaken = 0;
                this.numOfStepsInEachDirection = NumOfStepsInEachDirectionOfOrigin;
            }
            else
            {
                this.MoveEnemiesLeft();
            }
        }

        private void timerTickMoveRight(object sender, object e)
        {
            if (this.EnemiesDoneMovingInDirection)
            {
                this.moveTimer.Tick -= this.timerTickMoveRight;
                this.moveTimer.Tick += this.timerTickMoveLeft;

                this.stepsTaken = 0;
            }
            else
            {
                this.MoveEnemiesRight();
            }
        }

        /// <summary>
        ///     Sets the enemies up for the game
        /// </summary>
        public void SetupEnemies()
        {
            this.placeCenteredEnemies();
            this.setupFirstEnemyAnimation();

            this.moveTimer.Tick += this.timerTickMoveLeft;
            this.moveTimer.Start();
        }

        private void centerEnemiesNearTopOfCanvas()
        {
            var spaceBetweenLvl1Enemies = this.canvasWidth / NumOfLvl1Enemies / 2.0;
            var startXLvl1 = spaceBetweenLvl1Enemies;

            var spaceBetweenLvl2Enemies = this.canvasWidth / NumOfLvl2Enemies / 2.0;
            var startXLvl2 = spaceBetweenLvl2Enemies;

            var spaceBetweenLvl3Enemies = this.canvasWidth / NumOfLvl3Enemies / 2.0;
            var startXLvl3 = spaceBetweenLvl3Enemies;

            var spaceBetweenLvl4Enemies = this.canvasWidth / NumOfLvl4Enemies / 2.0;
            var startXLvl4 = spaceBetweenLvl4Enemies;

            foreach (var enemy in this.enemies)
            {
                switch (enemy)
                {
                    case Lvl1Enemy _:
                        enemy.X = startXLvl1;
                        enemy.Y = EnemyRow1;
                        startXLvl1 += enemy.Width + spaceBetweenLvl1Enemies;
                        break;
                    case Lvl2Enemy _:
                        enemy.X = startXLvl2;
                        enemy.Y = EnemyRow2;
                        startXLvl2 += enemy.Width + spaceBetweenLvl2Enemies;
                        break;
                    case Lvl3Enemy _:
                        enemy.X = startXLvl3;
                        enemy.Y = EnemyRow3;
                        startXLvl3 += enemy.Width + spaceBetweenLvl3Enemies;
                        break;
                    case Lvl4Enemy _:
                        enemy.X = startXLvl4;
                        enemy.Y = EnemyRow4;
                        startXLvl4 += enemy.Width + spaceBetweenLvl4Enemies;
                        break;
                }

                enemy.OriginalLocation = new Point { X = enemy.X, Y = enemy.Y };
            }
        }

        private void placeCenteredEnemies()
        {
            foreach (var enemy in this.enemies)
            {
                this.canvas.Children.Add(enemy.Sprite);
            }

            this.centerEnemiesNearTopOfCanvas();
        }

        private void setupFirstEnemyAnimation()
        {
            foreach (var enemy in this.enemies)
            {
                if (enemy is ShootingEnemy shootingEnemy)
                {
                    var secondSprite = shootingEnemy.SpriteAnimations[1];
                    this.canvas.Children.Add(secondSprite);
                    secondSprite.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        ///     Removes the struck enemy by the player
        ///     Precondition: playerManager != null, bulletManager != null
        /// </summary>
        /// <param name="playerManager">the player manager of the game</param>
        /// <param name="bulletManager">the bullet manager to remove the bullet from</param>
        /// <exception cref="ArgumentNullException">thrown if player manager or bullet manager is null</exception>
        /// <returns>the enemy which was struck or null if no one was struck</returns>
        public Enemy RemoveStruckEnemy(PlayerManager playerManager, BulletManager bulletManager)
        {
            if (playerManager == null)
            {
                throw new ArgumentNullException(nameof(playerManager));
            }

            if (bulletManager == null)
            {
                throw new ArgumentNullException(nameof(bulletManager));
            }

            foreach (var enemy in this.enemies)
            {
                foreach (var bullet in bulletManager)
                {
                    if (bullet.BulletType == BulletType.Player && enemy.CollisionDetected(bullet))
                    {
                        this.canvas.Children.Remove(enemy.Sprite);
                        this.enemies.Remove(enemy);
                        this.updateBullets(playerManager, bulletManager, bullet);

                        return enemy;
                    }
                }
            }

            return null;
        }

        private void updateBullets(PlayerManager playerManager, BulletManager bulletManager, Bullet bullet)
        {
            bulletManager.RemoveBullet(bullet);
            playerManager.Player.BulletsAvailable.Push(new Bullet(BulletType.Player));
        }

        private ShootingEnemy getRandomShootingEnemy()
        {
            var shootingEnemies = new List<ShootingEnemy>();
            foreach (var enemy in this.enemies)
            {
                if (enemy is ShootingEnemy shootingEnemy)
                {
                    shootingEnemies.Add(shootingEnemy);
                }
            }

            ShootingEnemy randomLvl3Enemy = null;
            if (shootingEnemies.Count > 0)
            {
                var randomIndex = new Random().Next(0, shootingEnemies.Count - 1);
                randomLvl3Enemy = shootingEnemies[randomIndex];
            }

            return randomLvl3Enemy;
        }

        /// <summary>
        ///     Moves all enemies to the left one step
        ///     PostCondition: stepsTaken == @prev + 1
        /// </summary>
        public void MoveEnemiesLeft()
        {
            foreach (var enemy in this.enemies)
            {
                if (enemy is ShootingEnemy shootingEnemy)
                {
                    shootingEnemy.UpdateSprite();
                }

                enemy.MoveLeft();
            }

            this.stepsTaken++;
        }

        /// <summary>
        ///     Moves all enemies to the right one step
        ///     PostCondition: stepsTaken == @prev + 1
        /// </summary>
        public void MoveEnemiesRight()
        {
            foreach (var enemy in this.enemies)
            {
                if (enemy is ShootingEnemy shootingEnemy)
                {
                    shootingEnemy.UpdateSprite();
                }

                enemy.MoveRight();
            }

            this.stepsTaken++;
        }

        #endregion
    }
}