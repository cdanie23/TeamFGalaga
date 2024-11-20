using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     A bullet manager which encapsulates all the bullets involved in the game
    /// </summary>
    public abstract class BulletManager : IEnumerable<Bullet>
    {
        #region Data members

        /// <summary>
        ///     The space in between the bullet and the ship
        /// </summary>
        public const double SpaceInBetweenBulletAndShip = 10;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the canvas
        /// </summary>
        public Canvas Canvas { get; }

        /// <summary>
        ///     Gets the collection of bullets
        /// </summary>
        public ICollection<Bullet> Bullets { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Makes an instance of the bullet manager
        ///     Post-condition: this.Canvas == Canvas
        ///     Precondition: Canvas != null
        /// </summary>
        /// <param name="canvas">The Canvas which the game is played</param>
        /// <exception cref="ArgumentNullException">Thrown when the Canvas passed in is null</exception>
        protected BulletManager(Canvas canvas)
        {
            this.Canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.Bullets = new List<Bullet>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the IEnumerator to iterate over the collection
        /// </summary>
        /// <returns>The enumerator over the collection</returns>
        public IEnumerator<Bullet> GetEnumerator()
        {
            var enumerator = this.Bullets.GetEnumerator();
            enumerator.Dispose();
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var enumerator = this.Bullets.GetEnumerator();
            enumerator.Dispose();
            return enumerator;
        }

        /// <summary>
        ///     Adds the bullet
        ///     Precondition: bullet != null
        ///     PostCondition: this.bullets.Count++
        /// </summary>
        /// <exception cref="ArgumentNullException">thrown if bullet is null</exception>
        /// <param name="bullet">the bullet to add</param>
        public void AddBullet(Bullet bullet)
        {
            if (bullet == null)
            {
                throw new ArgumentNullException(nameof(bullet));
            }

            this.Bullets.Add(bullet);
        }
        /// <summary>
        ///     Removes the bullets which were flagged
        ///     PostCondition: this.Bullets.Count != @prev, this.Canvas.Children.Count != @prev
        /// </summary>
        /// <param name="flaggedBullets">the flagged bullets</param>
        public void RemoveFlaggedBullets(ICollection<Bullet> flaggedBullets)
        {
            foreach (var bullet in flaggedBullets)
            {
                this.Bullets.Remove(bullet);
                this.Canvas.Children.Remove(bullet.Sprite);
            }
        }

        #endregion
    }
}