using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Point = Windows.Foundation.Point;

namespace Galaga.Model
{
    /// <summary>
    ///     Manages the Galaga game play.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const double PlayerOffsetFromBottom = 30;
        private readonly Canvas canvas;
        private readonly double canvasHeight;
        private readonly double canvasWidth;

        private Player player;

        private readonly EnemiesManager enemiesManager;

        #endregion

        #region Properties

        /// <summary>
        ///     The score of the game
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        ///     The score formatted for the view
        /// </summary>
        public string ScoreText => "Score : " + this.Score;

        /// <summary>
        ///     Check if all enemies are destroyed
        /// </summary>
        public bool AreAllEnemiesDestroyed => this.enemiesManager.AreAllEnemiesDestroyed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        public GameManager(Canvas canvas)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));

            this.canvas = canvas;
            this.canvasHeight = canvas.Height;
            this.canvasWidth = canvas.Width;

            this.enemiesManager = new EnemiesManager();

            this.initializeGame();
        }

        #endregion

        #region Methods

        private void initializeGame()
        {
            this.createAndPlacePlayer();
            this.placeCenteredEnemies();
        }

        private void createAndPlacePlayer()
        {
            this.player = new Player();
            this.canvas.Children.Add(this.player.Sprite);

            this.placePlayerNearBottomOfBackgroundCentered();
        }

        private void placeCenteredEnemies()
        {
            foreach (var enemy in this.enemiesManager)
            {
                this.canvas.Children.Add(enemy.Sprite);
            }

            this.centerEnemiesNearTopOfCanvas();
        }

        private void centerEnemiesNearTopOfCanvas()
        {
            var spaceBetweenLvl1Enemies = this.canvasWidth / EnemiesManager.NumOfLvl1Enemies / 2.0;
            var startXLvl1 = spaceBetweenLvl1Enemies;

            var spaceBetweenLvl2Enemies = this.canvasWidth / EnemiesManager.NumOfLvl2Enemies / 2.0;
            var startXLvl2 = spaceBetweenLvl2Enemies;

            var spaceBetweenLvl3Enemies = this.canvasWidth / EnemiesManager.NumOfLvl3Enemies / 2.0;
            var startXLvl3 = spaceBetweenLvl3Enemies;

            foreach (var enemy in this.enemiesManager)
            {
                switch (enemy)
                {
                    case Lvl1Enemy lvl1:
                        enemy.X = startXLvl1;
                        enemy.Y = 200;
                        startXLvl1 += enemy.Width + spaceBetweenLvl1Enemies;
                        break;
                    case Lvl2Enemy lvl2:
                        enemy.X = startXLvl2;
                        enemy.Y = 100;
                        startXLvl2 += enemy.Width + spaceBetweenLvl2Enemies;
                        break;
                    case Lvl3Enemy lvl3Enemy:
                        enemy.X = startXLvl3;
                        enemy.Y = 0;
                        startXLvl3 += enemy.Width + spaceBetweenLvl3Enemies;
                        break;
                }

                enemy.OriginalLocation = new Point { X = enemy.X, Y = enemy.Y };
            }
        }

        private void placePlayerNearBottomOfBackgroundCentered()
        {
            this.player.X = this.canvasWidth / 2 - this.player.Width / 2.0;
            this.player.Y = this.canvasHeight - this.player.Height - PlayerOffsetFromBottom;
        }

        /// <summary>
        ///     Moves the player left.
        /// </summary>
        public void MovePlayerLeft()
        {
            var newLocation = this.player.X - this.player.SpeedX;
            if (newLocation > 0)
            {
                this.player.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player right.
        /// </summary>
        public void MovePlayerRight()
        {
            var newLocation = this.player.X + this.player.SpeedX;
            if (newLocation + this.player.Width < this.canvasWidth)
            {
                this.player.MoveRight();
            }
        }

        /// <summary>
        ///     Moves the enemies left
        /// </summary>
        public void MoveEnemiesLeft()
        {
            this.enemiesManager.MoveEnemiesLeft();
        }

        /// <summary>
        ///     Moves the enemies right
        /// </summary>
        public void MoveEnemiesRight()
        {
            this.enemiesManager.MoveEnemiesRight();
        }

        /// <summary>
        ///     Checks to see if the enemies have stepped the amount of times they should
        /// </summary>
        /// <returns>true or false</returns>
        public bool EnemiesDoneMovingInDirection()
        {
            return this.enemiesManager.StepsTaken == this.enemiesManager.NumOfStepsInEachDirection;
        }

        /// <summary>
        ///     Resets the amount of steps taken
        /// </summary>
        public void ResetEnemyStepsTakenInEachDirection()
        {
            this.enemiesManager.StepsTaken = 0;
        }

        /// <summary>
        ///     Doubles the amount of steps taken in each direction
        /// </summary>
        public void IncreaseStepsTaken()
        {
            this.enemiesManager.NumOfStepsInEachDirection = 10;
        }

        /// <summary>
        ///     Shoots the Bullet of the player
        ///     Precondition: Player must not already have a Bullet on the canvas
        /// </summary>
        public void ShootPlayerBullet()
        {
            if (!this.canvas.Children.Contains(this.player.Bullet.Sprite))
            {
                this.canvas.Children.Add(this.player.Bullet.Sprite);
                //Place bullet in the middle and in front of the sprite 
                this.player.Bullet.X = this.player.X + this.player.Width / 2;
                this.player.Bullet.Y = this.player.Y - 10;
            }
        }

        /// <summary>
        ///     Moves the Bullet of the player
        ///     Precondition: The player must have a bullet on the canvas and the bullet cannot exceed the height boundary of the
        ///     canvas
        /// </summary>
        public void MovePlayerBullet()
        {
            if (this.canvas.Children.Contains(this.player.Bullet.Sprite) &&
                this.player.Bullet.Y - this.player.Bullet.SpeedY > 0)
            {
                this.player.Bullet.MoveUp();
            }
            else
            {
                this.canvas.Children.Remove(this.player.Bullet.Sprite);
            }
        }

        /// <summary>
        ///     Removes the enemy that was struck by the Bullet and the Bullet that struck the enemy
        /// </summary>
        /// <returns>true if the Bullet struck an enemy and was removed, false otherwise</returns>
        public bool RemoveStruckEnemyAndBullet()
        {
            foreach (var enemy in this.enemiesManager)
            {
                if (this.player.Bullet.Sprite.Boundary.IntersectsWith(enemy.Sprite.Boundary))
                {
                    this.enemiesManager.Remove(enemy);
                    this.canvas.Children.Remove(enemy.Sprite);
                    this.canvas.Children.Remove(this.player.Bullet.Sprite);
                    switch (enemy)
                    {
                        case Lvl1Enemy _:
                            this.Score += Lvl1Enemy.Points;
                            break;
                        case Lvl2Enemy _:
                            this.Score += Lvl2Enemy.Points;
                            break;
                        case Lvl3Enemy lvl3Enemy:
                            this.Score += Lvl3Enemy.Points;
                            this.canvas.Children.Remove(lvl3Enemy.Bullet.Sprite);
                            break;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Checks to see if the player is struck and removes the player
        /// </summary>
        /// <returns>True if the player is struck, false otherwise</returns>
        public bool RemovePlayerIfStruck()
        {
            foreach (var enemy in this.enemiesManager)
            {
                if (enemy is Lvl3Enemy lvl3Enemy)
                {
                    if (lvl3Enemy.Bullet.Sprite.Boundary.IntersectsWith(this.player.Sprite.Boundary))
                    {
                        this.canvas.Children.Remove(this.player.Sprite);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Shoots all level 3 enemies weapons
        /// </summary>
        public void ShootRandomLevel3EnemyWeapon()
        {
            var lvl3Enemies = new Collection<Lvl3Enemy>();
            foreach (var enemy in this.enemiesManager)
            {
                if (enemy is Lvl3Enemy lvl3Enemy)
                {
                    lvl3Enemies.Add(lvl3Enemy);
                }
            }

            Lvl3Enemy randomLvl3Enemy = null;
            if (lvl3Enemies.Count > 0)
            {
                var randomIndex = new Random().Next(0, lvl3Enemies.Count - 1);
                randomLvl3Enemy = lvl3Enemies[randomIndex];
            }

            if (randomLvl3Enemy != null && !this.canvas.Children.Contains(randomLvl3Enemy.Bullet.Sprite))
            {
                this.canvas.Children.Add(randomLvl3Enemy.Bullet.Sprite);
                randomLvl3Enemy.Bullet.X = randomLvl3Enemy.X + randomLvl3Enemy.Width / 2;
                randomLvl3Enemy.Bullet.Y = randomLvl3Enemy.Y + randomLvl3Enemy.Height;
            }
        }

        /// <summary>
        ///     Moves the level 3 enemies bullets and removes it if it exceeds the height of the canvas
        /// </summary>
        public void MoveLevel3EnemyBullets()
        {
            foreach (var enemy in this.enemiesManager)
            {
                if (enemy is Lvl3Enemy lvl3Enemy)
                {
                    if (lvl3Enemy.Bullet.Y + lvl3Enemy.Bullet.SpeedY + lvl3Enemy.Bullet.Height < this.canvasHeight)
                    {
                        lvl3Enemy.Bullet.Y += lvl3Enemy.Bullet.SpeedY;
                    }
                    else
                    {
                        this.canvas.Children.Remove(lvl3Enemy.Bullet.Sprite);
                    }
                }
            }
        }

        #endregion
    }
}