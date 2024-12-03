// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.ExplosionSprites
{
    /// <summary>
    ///     First Explosion Sprite
    /// </summary>
    /// <seealso cref="Galaga.View.Sprites.BaseSprite" />
    public sealed partial class FirstExplosionSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FirstExplosionSprite" /> class.
        /// </summary>
        public FirstExplosionSprite()
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
        /// <exception cref="System.NotImplementedException"></exception>
        public override BaseSprite Clone()
        {
            return new FirstExplosionSprite();
        }

        #endregion
    }
}