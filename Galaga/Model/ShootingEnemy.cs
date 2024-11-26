using System;
using Windows.UI.Xaml;
using Galaga.View.Sprites;
using Galaga.View.Sprites.EnemySprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The shooting enemy class
    /// </summary>
    public class ShootingEnemy : Enemy
    {
        #region Properties

        /// <summary>
        ///     Get or set the bullet
        /// </summary>
        /// <param name="value">the bullet</param>
        public Bullet Bullet { get; set; }

        /// <summary>
        ///     Gets or sets the array of sprite variants
        /// </summary>
        public BaseSprite[] SpriteAnimations { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of a shooting enemy
        ///     PreConditions: baseSprite != null, spriteAnimations != null
        ///     PostConditions: Points == points, Level == level, SpeedX == speedX, SpeedY == speedY,
        ///     Sprite == baseSprite, this.SpriteAnimations == spriteAnimations,
        ///     this.Bullet.BulletType == BulletType.Enemy
        /// </summary>
        /// <param name="points">the points</param>
        /// <param name="level">the level</param>
        /// <param name="speedX">the speed in the x plane</param>
        /// <param name="speedY">the speed in the y plane</param>
        /// <param name="baseSprite">the sprite of the enemy</param>
        /// <param name="spriteAnimations">the variation sprites of the enemy</param>
        /// <param name="bulletSpeed">the speed of the bullet</param>
        /// <exception cref="ArgumentNullException">thrown if the base sprite or spriteAnimations are null</exception>
        public ShootingEnemy(int points, int level, int speedX, int speedY, BaseSprite baseSprite,
            BaseSprite[] spriteAnimations, int bulletSpeed) : base(points, level, speedX, speedY, baseSprite)
        {
            if (baseSprite == null)
            {
                throw new ArgumentNullException(nameof(baseSprite));
            }

            this.SpriteAnimations = spriteAnimations ?? throw new ArgumentNullException(nameof(spriteAnimations));
            this.Bullet = new Bullet(BulletType.Enemy, bulletSpeed);
            Sprite = spriteAnimations[0];
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Flips the sprite back and forth
        ///     PostCondition: Sprite != @prev
        /// </summary>
        public void UpdateSprite()
        {
            if (Sprite is BonusEnemySprite)
            {
                return;
            }

            if (Sprite == this.SpriteAnimations[0])
            {
                this.SpriteAnimations[0].Visibility = Visibility.Collapsed;
                this.SpriteAnimations[1].Visibility = Visibility.Visible;
                Sprite = this.SpriteAnimations[1];
            }
            else
            {
                this.SpriteAnimations[1].Visibility = Visibility.Collapsed;
                this.SpriteAnimations[0].Visibility = Visibility.Visible;
                Sprite = this.SpriteAnimations[0];
            }
        }

        #endregion
    }
}