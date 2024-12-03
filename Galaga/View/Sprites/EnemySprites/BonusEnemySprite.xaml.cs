// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites.EnemySprites
{
    /// <summary>
    ///     Bonus Enemy Sprite
    /// </summary>
    public sealed partial class BonusEnemySprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusEnemySprite" /> class.
        /// </summary>
        public BonusEnemySprite()
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
            return new BonusEnemySprite();
        }

        #endregion
    }
}