using System;

using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    /// The player manager class
    /// </summary>
    public class PlayerManager
    {
        /// <summary>
        /// Gets or sets the player
        /// </summary>
        public Player Player { get; private set; }
        
        private readonly Canvas canvas;

        private readonly double canvasWidth;
        private readonly double canvasHeight;

        private const double PlayerOffsetFromBottom = 30;
        /// <summary>
        /// Creates an instance of the player manager
        /// PreCondition: canvas != null
        /// PostCondition: this.canvas == canvas & this.canvasHeight == canvasHeight & this.canvasWidth == canvasWidth & this.bulletManager != null
        /// </summary>
        /// <param name="canvas">the canvas of the game</param>
        /// <exception cref="ArgumentNullException">thrown if the canvas is null</exception>
        public PlayerManager(Canvas canvas)
        {
            
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));

            this.canvas = canvas;
            this.canvasHeight = canvas.Height;
            this.canvasWidth = canvas.Width;

        }
        /// <summary>
        /// Sets the player in the game
        /// </summary>
        public void SetupPlayer()
        {
            this.createAndPlacePlayer();
        }
        /// <summary>
        ///     Shoots the Bullet of the Player
        ///     Precondition: Player must not already have a Bullet on the canvas
        ///     <returns>True or false based on if the bullet was shot</returns>
        /// </summary>
        public Boolean ShootPlayerBullet()
        {
            if (!this.canvas.Children.Contains(this.Player.Bullet.Sprite))
            {
                this.canvas.Children.Add(this.Player.Bullet.Sprite);

                this.Player.Bullet.X = this.Player.X + this.Player.Width / 2;
                this.Player.Bullet.Y = this.Player.Y - BulletManager.SpaceInBetweenBulletAndShip;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check is the player is struck by a bullet
        /// </summary>
        /// <param name="bullet">the bullet to check for</param>
        /// <returns></returns>
        public Boolean IsPlayerStruck(Bullet bullet)
        {
            return this.Player.Sprite.Boundary.IntersectsWith(bullet.Sprite.Boundary);
        }



        private void createAndPlacePlayer()
        {
            this.Player = new Player();
            this.canvas.Children.Add(this.Player.Sprite);

            this.placePlayerNearBottomOfBackgroundCentered();
        }

        private void placePlayerNearBottomOfBackgroundCentered()
        {
            this.Player.X = this.canvasWidth / 2 - this.Player.Width / 2.0;
            this.Player.Y = this.canvasHeight - this.Player.Height - PlayerOffsetFromBottom;
        }


        /// <summary>
        ///     Moves the Player left.
        /// </summary>
        public void MovePlayerLeft()
        {
            var newLocation = this.Player.X - this.Player.SpeedX;
            if (newLocation > 0)
            {
                this.Player.MoveLeft();
            }
        }
        /// <summary>
        ///     Moves the Player right.
        /// </summary>
        public void MovePlayerRight()
        {
            var newLocation = this.Player.X + this.Player.SpeedX;
            if (newLocation + this.Player.Width < this.canvasWidth)
            {
                this.Player.MoveRight();
            }
        }

        
    }
}
