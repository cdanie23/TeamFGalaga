
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// Represents a Player in the game.
    /// </summary>
    public class Player : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        /// <summary>
        /// Gets the Bullet
        /// </summary>
        public Bullet Bullet; 
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Sprite = new PlayerSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);

            this.Bullet = new Bullet(BulletType.Player);
        }

        #endregion
    }
}
