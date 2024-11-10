using Galaga.View.Sprites.EnemySprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The level 2 enemy class, inherits game object
    /// </summary>
    public class Lvl2Enemy : Enemy
    {
        #region Data members

        private const int SpeedXDirection = 4;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of a level 2 enemy
        ///     PostCondition: Sprite == new Enemy2Sprite(), SpeedX == SpeedXDirection, SpeedY == SpeedYDirection, Points == 2
        /// </summary>
        public Lvl2Enemy()
        {
            Sprite = new Enemy2Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            Points = 2;
        }

        #endregion
    }
}