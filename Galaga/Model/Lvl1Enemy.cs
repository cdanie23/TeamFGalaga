using Galaga.View.Sprites.EnemySprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The level one enemy class inherits from Game Objects
    /// </summary>
    public class Lvl1Enemy : Enemy
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of a level 1 enemy
        ///     PostCondition: Sprite == new Enemy1Sprite(), SpeedX == SpeedXDirection, SpeedY == SpeedYDirection, Points == 1
        /// </summary>
        public Lvl1Enemy()
        {
            Sprite = new Enemy1Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            Points = 1;
        }

        #endregion
    }
}