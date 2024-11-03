

using System.Collections.Generic;

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
       
        private const int MaxNumOfBullets = 3;

        /// <summary>
        /// The available bullets of the player
        /// </summary>
        public Stack<Bullet> BulletsAvailable { get;}

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Sprite = new PlayerSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);

            this.BulletsAvailable = new Stack<Bullet>();
            this.setupActiveBullets();

        }

        private void setupActiveBullets()
        {
            for (var i = 0; i < MaxNumOfBullets; i++)
            {
                this.BulletsAvailable.Push(new Bullet(BulletType.Player));
            }
        } 

        #endregion
    }
}
