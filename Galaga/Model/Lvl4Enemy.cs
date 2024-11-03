
using Galaga.View.Sprites;
using Galaga.View.Sprites.EnemySprites;
using Galaga.View.Sprites.EnemySprites.EnemySpriteVariants;

namespace Galaga.Model
{
    /// <summary>
    /// The level 4 enemy class
    /// </summary>
    public class Lvl4Enemy : ShootingEnemy
    {
        /// <summary>
        /// Creates an instance of a level 4 enemy 
        /// </summary>
        public Lvl4Enemy()
        {
            SpriteAnimations = new BaseSprite[] { new Enemy4Sprite(), new Enemy4SpriteVariant() };
            Sprite = SpriteAnimations[0];
            SetSpeed(3, 0);
            Points = 4;
            Bullet = new Bullet(BulletType.Enemy);
            
        }
    }
}
