
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// The Bullet game object
    /// </summary>
    public class Bullet : GameObject
    {
        private const int BulletYSpeed = 20;

        /// <summary>
        /// Gets the bullet type
        /// </summary>
        public BulletType BulletType { get; private set; }
        /// <summary>
        /// Creates an instance of the Bullet
        /// PostCondition: this.BulletType == BulletType, this.Sprite == BulletSprite, this.SpeedX == 0, this.SpeedY == this.BulletYSpeed
        /// </summary>
        public Bullet(BulletType bulletType)
        {
            this.BulletType = bulletType;
            Sprite = new BulletSprite();
            SetSpeed(0, BulletYSpeed);
        }

       
    }
}
