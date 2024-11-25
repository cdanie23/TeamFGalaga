using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The Bullet game object
    /// </summary>
    public class Bullet : GameObject
    {
        
        #region Properties

        /// <summary>
        ///     Gets the bullet type
        /// </summary>
        public BulletType BulletType { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the Bullet
        ///     PostCondition: this.BulletType == BulletType, this.Sprite == EnemyBulletSprite, this.SpeedX == 0, this.SpeedY ==
        ///     this.BulletYSpeed
        /// </summary>
        public Bullet(BulletType bulletType, int bulletSpeed)
        {
            this.BulletType = bulletType;
            SetSpeed(0, bulletSpeed);
            if (bulletType == BulletType.Player)
            {
                Sprite = new PlayerBulletSprite();
            }
            else
            {
                Sprite = new EnemyBulletSprite();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the bullet of the player
        /// </summary>
        /// <param name="canvas">the canvas of the bullet</param>
        /// <returns>true or false based on if the bullet can move</returns>
        public bool Move(Canvas canvas)
        {
            if (Y + SpeedY > canvas.Height)
            {
                canvas.Children.Remove(Sprite);
                return false;
            }

            if (Y - SpeedY < 0)
            {
                canvas.Children.Remove(Sprite);
                return false;
            }

            switch (this.BulletType)
            {
                case BulletType.Enemy:
                    Y += SpeedY;
                    break;
                case BulletType.Player:
                    Y -= SpeedY;
                    break;
            }

            return true;
        }

        #endregion
    }
}