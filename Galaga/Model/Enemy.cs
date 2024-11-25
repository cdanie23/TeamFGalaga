using System;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The enemy class which inherits game object
    /// </summary>
    public class Enemy : GameObject
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the points associated with the enemy
        ///     <param name="value">the value to set points to</param>
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        ///     Gets the level associated with the enemy
        /// </summary>
        public int Level { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of an enemy
        ///     PreCondition: baseSprite != null
        ///     PostCondition: this.Points == points, this.Level == level, SpeedX == speedX, SpeedY == speedY,
        ///     Sprite == baseSprite
        /// </summary>
        /// <param name="points">the points</param>
        /// <param name="level">the level</param>
        /// <param name="speedX">the speed in the x plane</param>
        /// <param name="speedY">the speed in y plane</param>
        /// <param name="baseSprite">the sprite</param>
        /// <exception cref="ArgumentNullException">thrown if the baseSprite is null</exception>
        public Enemy(int points, int level, int speedX, int speedY, BaseSprite baseSprite)
        {
            this.Points = points;
            this.Level = level;
            SetSpeed(speedX, speedY);
            Sprite = baseSprite ?? throw new ArgumentNullException(nameof(baseSprite));
        }

        #endregion
    }
}