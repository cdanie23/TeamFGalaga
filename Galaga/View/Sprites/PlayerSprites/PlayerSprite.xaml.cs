// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.PlayerSprites
{
    /// <summary>
    ///     The Player sprite.
    /// </summary>
    /// <seealso cref="Galaga.View.Sprites.BaseSprite" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class PlayerSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerSprite" /> class.
        /// </summary>
        public PlayerSprite()
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
            return new PlayerSprite();
        }

        #endregion
    }
}