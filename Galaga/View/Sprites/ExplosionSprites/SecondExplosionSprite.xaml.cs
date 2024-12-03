// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.ExplosionSprites
{
    /// <summary>
    ///     The Second Explosion Sprite
    /// </summary>
    /// <seealso cref="Galaga.View.Sprites.BaseSprite" />
    public sealed partial class SecondExplosionSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SecondExplosionSprite" /> class.
        /// </summary>
        public SecondExplosionSprite()
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
            return new SecondExplosionSprite();
        }

        #endregion
    }
}