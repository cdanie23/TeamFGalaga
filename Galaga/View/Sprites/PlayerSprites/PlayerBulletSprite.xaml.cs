// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.PlayerSprites
{
    /// <summary>
    ///     The player bullet sprite class
    /// </summary>
    public sealed partial class PlayerBulletSprite
    {
        #region Constructors

        /// <summary>
        ///     Creates an instance of the player bullet sprite
        /// </summary>
        public PlayerBulletSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Clones the sprite
        /// </summary>
        /// <returns>
        ///     the clone
        /// </returns>
        public override BaseSprite Clone()
        {
            return new PlayerBulletSprite();
        }

        #endregion
    }
}