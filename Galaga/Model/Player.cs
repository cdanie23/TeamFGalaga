using System.Collections.Generic;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Represents a Player in the game.
    /// </summary>
    public class Player : GameObject
    {
        #region Data members

        /// <summary>
        ///     The number of lives a player can have
        /// </summary>
        public const int NumOfLives = 3;

        /// <summary>
        ///     The players bullet speed
        /// </summary>
        public const int PlayerBulletSpeed = 15;

        private const int SpeedXDirection = 8;
        private const int SpeedYDirection = 0;

        /// <summary>
        ///     Maximum Number of bullets a player can have
        /// </summary>
        public static readonly int MaxNumOfBullets = 3;

        #endregion

        #region Properties

        /// <summary>
        ///     The available bullets of the player
        /// </summary>
        public Stack<Bullet> BulletsAvailable { get; }

        /// <summary>
        ///     Get or set the name of the player
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Player" /> class.
        ///     Post-condition: Sprite == PlayerSprite, this.BulletsAvailable != null,
        /// </summary>
        public Player(BaseSprite playerSkin)
        {
            Sprite = playerSkin;
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.BulletsAvailable = new Stack<Bullet>();
            this.SetupActiveBullets();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets up the specified number of bullets a player can have
        ///     PostCondition: this.BulletsAvailable.Count == MaxNumOfBullets
        /// </summary>
        public void SetupActiveBullets()
        {
            for (var i = 0; i < MaxNumOfBullets; i++)
            {
                this.BulletsAvailable.Push(new Bullet(BulletType.Player, PlayerBulletSpeed));
            }
        }

        /// <summary>
        ///     Sets the player speed.
        /// </summary>
        public void SetPlayerSpeed(int speed)
        {
            SpeedX = speed;
        }

        #endregion
    }
}