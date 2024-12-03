// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.ExplosionSprites
{
    /// <summary>
    ///     The Third Explosion Sprite
    /// </summary>
    /// <seealso cref="Galaga.View.Sprites.BaseSprite" />
    public sealed partial class ThirdExplosionSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThirdExplosionSprite" /> class.
        /// </summary>
        public ThirdExplosionSprite()
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
            return new ThirdExplosionSprite();
        }

        #endregion
    }
}