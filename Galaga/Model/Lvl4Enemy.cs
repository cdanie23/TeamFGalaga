using Galaga.View.Sprites;
using Galaga.View.Sprites.EnemySprites;
using Galaga.View.Sprites.EnemySprites.EnemySpriteVariants;

namespace Galaga.Model
{
    /// <summary>
    ///     The level 4 enemy class which inherits shooting enemy
    /// </summary>
    public class Lvl4Enemy : ShootingEnemy
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of a level 4 enemy
        ///     Preconditions: SpriteAnimations != null, Sprite == Level4Sprite, Points == 4, Bullet != null
        /// </summary>
        public Lvl4Enemy()
        {
            SpriteAnimations = new BaseSprite[] { new Enemy4Sprite(), new Enemy4SpriteVariant() };
            Sprite = SpriteAnimations[0];
            SetSpeed(SpeedXDirection, SpeedYDirection);
            Points = 4;
            Bullet = new Bullet(BulletType.Enemy);
        }

        #endregion
    }
}