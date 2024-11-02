using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// The level 2 enemy class, inherits game object
    /// </summary>
    public class Lvl2Enemy : Enemy
    {
        private const int SpeedXDirection = 4;
        private const int SpeedYDirection = 0;
        
        /// <summary>
        /// Creates an instance of a level 2 enemy
        /// PostCondition: Sprite == new Enemy2Sprite(), SpeedX == SpeedXDirection, SpeedY == SpeedYDirection
        /// </summary>
        public Lvl2Enemy()
        {
            Sprite = new Enemy2Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            Points = 2;
        }
    }
}
