using Galaga.View.Sprites;
using Galaga.View.Sprites.EnemySprites;
using Galaga.View.Sprites.EnemySprites.EnemySpriteVariants;

namespace Galaga.Model
{
    /// <summary>
    ///     The level 3 enemy class, inherits game object
    /// </summary>
    public class Lvl3Enemy : ShootingEnemy
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of a level 3 enemy
        ///     PostCondition: Sprite == new Enemy1Sprite(), SpeedX == SpeedXDirection, SpeedY == SpeedYDirection, Points == 3,
        ///     Bullet != null, SpriteAnimations != null
        /// </summary>
        public Lvl3Enemy()
        {
            SpriteAnimations = new BaseSprite[] { new Enemy3Sprite(), new Enemy3SpriteVariant() };
            Sprite = SpriteAnimations[0];
            SetSpeed(SpeedXDirection, SpeedYDirection);
            Bullet = new Bullet(BulletType.Enemy);

            Points = 3;
        }

        #endregion
    }
}