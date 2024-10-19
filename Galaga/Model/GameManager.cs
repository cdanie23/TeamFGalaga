using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Windows.UI.Xaml.Controls;
using Point = Windows.Foundation.Point;

namespace Galaga.Model
{
    /// <summary>
    /// Manages the Galaga game play.
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

        
        

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
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
            double spaceBetweenLvl1Enemies = this.canvasWidth / EnemiesManager.NumOfLvl1Enemies / 2.0;
            double startXLvl1 = spaceBetweenLvl1Enemies;
            
            double spaceBetweenLvl2Enemies = this.canvasWidth / EnemiesManager.NumOfLvl2Enemies / 2.0;
            double startXLvl2 = spaceBetweenLvl2Enemies;

            double spaceBetweenLvl3Enemies = this.canvasWidth / EnemiesManager.NumOfLvl3Enemies / 2.0;
            double startXLvl3 = spaceBetweenLvl3Enemies;
            

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

                enemy.OriginalLocation = new Point() { X = enemy.X, Y = enemy.Y };
            }
            
        }

        private void placePlayerNearBottomOfBackgroundCentered()
        {
            this.player.X = this.canvasWidth / 2 - this.player.Width / 2.0;
            this.player.Y = this.canvasHeight - this.player.Height - PlayerOffsetFromBottom;
        }

        /// <summary>
        /// Moves the player left.
        /// </summary>
        public void MovePlayerLeft()
        {
            this.player.MoveLeft();
        }

        /// <summary>
        /// Moves the player right.
        /// </summary>
        public void MovePlayerRight()
        {
            this.player.MoveRight();
        }
        /// <summary>
        /// Moves the enemies left
        /// </summary>
        public void MoveEnemiesLeft()
        {
            this.enemiesManager.MoveEnemiesLeft();
        }
        /// <summary>
        /// Moves the enemies right
        /// </summary>
        public void MoveEnemiesRight()
        {
            this.enemiesManager.MoveEnemiesRight();
            
        }
        /// <summary>
        /// Checks to see if the enemies have stepped the amount of times they should
        /// </summary>
        /// <returns>true or false</returns>
        public bool EnemiesDoneMovingInDirection()
        {
            return this.enemiesManager.StepsTaken == this.enemiesManager.NumOfStepsInEachDirection;
        }
        /// <summary>
        /// Resets the amount of steps taken
        /// </summary>
        public void ResetEnemyStepsTakenInEachDirection()
        {
            this.enemiesManager.StepsTaken = 0;
        }
        /// <summary>
        /// Doubles the amount of steps taken in each direction 
        /// </summary>
        public void DoubleNumOfStepsTaken()
        {
            this.enemiesManager.NumOfStepsInEachDirection *= 2;
        }
        #endregion

    }
}
