// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.PlayerSprites
{
    /// <summary>
    ///     The third player sprite option
    /// </summary>
    public sealed partial class PlayerSprite3
    {
        #region Constructors

        /// <summary>
        ///     Creates an instance of the player sprite
        /// </summary>
        public PlayerSprite3()
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
            return new PlayerSprite3();
        }

        #endregion
    }
}