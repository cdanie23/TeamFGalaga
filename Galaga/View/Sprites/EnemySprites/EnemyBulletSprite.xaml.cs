// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.EnemySprites
{
    /// <summary>
    ///     The bullet sprite class
    /// </summary>
    public sealed partial class EnemyBulletSprite
    {
        #region Constructors

        /// <summary>
        ///     Instantiates an instance of a bullet sprite
        /// </summary>
        public EnemyBulletSprite()
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
            return new EnemyBulletSprite();
        }

        #endregion
    }
}