﻿using System;
using Windows.Foundation;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Represents a game object in the Galaga game.
    /// </summary>
    public abstract class GameObject
    {
        #region Data members

        private Point location;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the x location of the game object.
        /// </summary>
        /// <value>
        ///     The x.
        /// </value>
        public double X
        {
            get => this.location.X;
            set
            {
                this.location.X = value;
                this.render();
            }
        }

        /// <summary>
        ///     Gets or sets the y location of the game object.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public double Y
        {
            get => this.location.Y;
            set
            {
                this.location.Y = value;
                this.render();
            }
        }

        /// <summary>
        ///     Gets the x speed of the game object.
        /// </summary>
        /// <value>
        ///     The speed x.
        /// </value>
        public int SpeedX { get; protected set; }

        /// <summary>
        ///     Gets the y speed of the game object.
        /// </summary>
        /// <value>
        ///     The speed y.
        /// </value>
        public int SpeedY { get; private set; }

        /// <summary>
        ///     Gets the width of the game object.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public double Width => this.Sprite.Width;

        /// <summary>
        ///     Gets the height of the game object.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public double Height => this.Sprite.Height;

        /// <summary>
        ///     Gets or sets the sprite associated with the game object.
        /// </summary>
        /// <value>
        ///     The sprite.
        /// </value>
        public BaseSprite Sprite { get; protected set; }

        /// <summary>
        ///     Gets or sets the original placement of the game object
        /// </summary>
        /// <param name="value">the original location</param>
        public Point OriginalLocation { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the game object right
        ///     Precondition: the game object cannot move past the border of the game Canvas
        ///     Post-condition: X += SpeedX
        /// </summary>
        public void MoveRight()
        {
            var newLocation = this.X + this.SpeedX;
            this.X = newLocation;
        }

        /// <summary>
        ///     Moves the game object left.
        ///     Precondition: the game object cannot move past the border of the game Canvas
        ///     Post-condition: X == X@prev + SpeedX
        /// </summary>
        public void MoveLeft()
        {
            var newLocation = this.X - this.SpeedX;
            this.X = newLocation;
        }

        /// <summary>
        ///     Moves the game object up.
        ///     Precondition: None
        ///     Post-condition: Y == Y@prev - SpeedY
        /// </summary>
        public void MoveUp()
        {
            this.Y -= this.SpeedY;
        }

        /// <summary>
        ///     Moves the game object down.
        ///     Precondition: None
        ///     Post-condition: Y == Y@prev + SpeedY
        /// </summary>
        public void MoveDown()
        {
            this.Y += this.SpeedY;
        }

        private void render()
        {
            var render = this.Sprite as ISpriteRenderer;

            render?.RenderAt(this.X, this.Y);
        }

        /// <summary>
        ///     Checks if the game object was collided with by a bullet
        ///     Precondition: bullet != null
        /// </summary>
        /// <param name="bullet">the bullet to check for</param>
        /// <exception cref="ArgumentNullException">Thrown if the bullet is null</exception>
        /// <returns>true or false based on if the bullet collided with the game object</returns>
        public bool CollisionDetected(Bullet bullet)
        {
            if (bullet == null)
            {
                throw new ArgumentNullException(nameof(bullet));
            }

            return this.Sprite.Boundary.IntersectsWith(bullet.Sprite.Boundary);
        }

        /// <summary>
        ///     Sets the speed of the game object.
        ///     Precondition: speedX >= 0 AND speedY >=0
        ///     Post-condition: SpeedX == speedX AND SpeedY == speedY
        /// </summary>
        /// <param name="speedX">The speed x.</param>
        /// <param name="speedY">The speed y.</param>
        protected void SetSpeed(int speedX, int speedY)
        {
            if (speedX < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speedX));
            }

            if (speedY < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speedY));
            }

            this.SpeedX = speedX;
            this.SpeedY = speedY;
        }

        #endregion
    }
}