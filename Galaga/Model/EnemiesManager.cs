﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites.EnemySprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Encapsulates all the enemies in the game of Galaga
    /// </summary>
    public class EnemiesManager : IEnumerable<Enemy>
    {
        #region Data members

        /// <summary>
        ///     The points of the level one enemy
        /// </summary>
        public const int Level1EnemyPoints = 5;

        /// <summary>
        ///     The points of the level two enemy
        /// </summary>
        public const int Level2EnemyPoints = 10;

        /// <summary>
        ///     The points of the level three enemy
        /// </summary>
        public const int Level3EnemyPoints = 15;

        /// <summary>
        ///     The points of the level four enemy
        /// </summary>
        public const int Level4EnemyPoints = 20;

        /// <summary>
        ///     The points of the bonus enemy
        /// </summary>
        public const int BonusEnemyPoints = 50;

        private const int EnemyRow1 = 300;
        private const int EnemyRow2 = 200;
        private const int EnemyRow3 = 100;
        private const int EnemyRow4 = 0;

        private readonly Collection<Enemy> enemies;
        private readonly Canvas canvas;
        private readonly double canvasWidth;

        private readonly EnemyFactory enemyFactory;

        private readonly GameSettings gameSettings;

        #endregion

        #region Properties

        /// <summary>
        ///     Checks if all enemy ships are destroyed
        /// </summary>
        public bool AreAllEnemiesDestroyed => this.enemies.Count == 0;

        /// <summary>
        ///     The count of the enemies in the enemy manager
        /// </summary>
        public int Count => this.enemies.Count;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the enemy manager class
        ///     Precondition: Canvas != null
        ///     Post-conditions: this.enemies != null , this.stepsTaken == 0, this.numOfStepsInEachDirection == 5, this.Canvas ==
        ///     Canvas, this.canvasHeight == Canvas.Height, this.canvasWidth == Canvas.Width
        ///     , this.bulletManager != null, this.moveTimer != null
        /// </summary>
        /// <param name="canvas">the Canvas of the game</param>
        /// <param name="gameSettings">the game settings to assign to the enemy</param>
        public EnemiesManager(Canvas canvas, GameSettings gameSettings)
        {
            this.enemies = new Collection<Enemy>();

            this.gameSettings = gameSettings;
            this.enemyFactory = new EnemyFactory(this.gameSettings);

            this.createEnemies();

            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.canvasWidth = canvas.Width;
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

        /// <summary>
        ///     updates the current game settings
        ///     PostConditions: the game settings properties != @prev
        /// </summary>
        /// <param name="gameLevel"></param>
        public void SetEnemySettings(int gameLevel)
        {
            this.gameSettings.SetGameSettings(gameLevel);
        }

        /// <summary>
        ///     Creates and places centered enemies when the level is over
        /// </summary>
        /// <param name="sender">the sender of the event provoked</param>
        /// <param name="level">the current level when the event was provoked</param>
        public async void OnLevelOver(object sender, int level)
        {
            await Task.Delay(1500);
            this.createEnemies();
            this.SetupEnemies();
        }

        private void createEnemies()
        {
            for (var iteration = 1; iteration <= this.gameSettings.NumberOfEachEnemy.Length; iteration++)
            {
                for (var i = 0; i < this.gameSettings.NumberOfEachEnemy[iteration - 1]; i++)
                {
                    this.enemies.Add(this.enemyFactory.CreateNewEnemy(iteration));
                }
            }
        }

        /// <summary>
        ///     Shoots a randomly selected enemy weapon
        ///     Precondition: bulletManager != null
        ///     Post-condition: this.Canvas.Children++
        /// </summary>
        /// <exception cref="ArgumentNullException">thrown if the bullet manager is null</exception>
        /// <param name="bulletManager">the bullet manager to add the bullet to</param>
        public void ShootRandomEnemyWeapon(EnemyBulletManager bulletManager)
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

                SoundPlayer.playShootSound();
            }
        }

        /// <summary>
        ///     Sets the enemies up for the game
        /// </summary>
        public void SetupEnemies()
        {
            this.placeCenteredEnemies();
            this.setupFirstEnemyAnimation();
        }

        private void centerEnemiesNearTopOfCanvas()
        {
            var spaceBetweenLvl1Enemies = this.canvasWidth / this.gameSettings.NumberOfEachEnemy[0] / 2.0;
            var startXLvl1 = spaceBetweenLvl1Enemies;

            var spaceBetweenLvl2Enemies = this.canvasWidth / this.gameSettings.NumberOfEachEnemy[1] / 2.0;
            var startXLvl2 = spaceBetweenLvl2Enemies;

            var spaceBetweenLvl3Enemies = this.canvasWidth / this.gameSettings.NumberOfEachEnemy[2] / 2.0;
            var startXLvl3 = spaceBetweenLvl3Enemies;

            var spaceBetweenLvl4Enemies = this.canvasWidth / this.gameSettings.NumberOfEachEnemy[3] / 2.0;
            var startXLvl4 = spaceBetweenLvl4Enemies;

            foreach (var enemy in this.enemies)
            {
                switch (enemy.Level)
                {
                    case 1:
                        enemy.X = startXLvl1;
                        enemy.Y = EnemyRow1;
                        startXLvl1 += enemy.Width + spaceBetweenLvl1Enemies;
                        break;
                    case 2:
                        enemy.X = startXLvl2;
                        enemy.Y = EnemyRow2;
                        startXLvl2 += enemy.Width + spaceBetweenLvl2Enemies;
                        break;
                    case 3:
                        enemy.X = startXLvl3;
                        enemy.Y = EnemyRow3;
                        startXLvl3 += enemy.Width + spaceBetweenLvl3Enemies;
                        break;
                    case 4:
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
                if (!(enemy.Sprite is BonusEnemySprite))
                {
                    this.canvas.Children.Add(enemy.Sprite);
                }
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
        ///     Removes the enemy from the canvas and the collection
        /// </summary>
        /// Precondition: enemy != null
        /// Post-condition: this.enemies.Count == @prev - 1, this.canvas.Children.Count == @prev -1
        /// <param name="enemy">the enemy to remove</param>
        /// <exception cref="ArgumentNullException">thrown if the enemy is null</exception>
        public void RemoveEnemy(Enemy enemy)
        {
            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            this.enemies.Remove(enemy);
            this.canvas.Children.Remove(enemy.Sprite);
            ExplosionAnimator.PlayExplosion(this.canvas, enemy.X, enemy.Y);
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
        ///     Adds the bonus enemy to the enemies collection
        /// </summary>
        /// <param name="enemy"></param>
        /// <exception cref="ArgumentNullException">thrown if the enemy is null</exception>
        public void AddBonusEnemy(Enemy enemy)
        {
            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            this.enemies.Add(enemy);
        }

        /// <summary>
        ///     Moves the enemy tick event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void MoveEnemyTickEvent(object sender, object e)
        {
            foreach (var enemy in this.enemies)
            {
                if (enemy is ShootingEnemy shootingEnemy)
                {
                    shootingEnemy.UpdateSprite();
                }

                if (!(enemy.Sprite is BonusEnemySprite))
                {
                    enemy.Move(this.gameSettings.Level);
                }
            }
        }

        #endregion
    }
}