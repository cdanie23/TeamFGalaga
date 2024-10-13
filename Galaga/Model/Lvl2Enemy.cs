using Galaga.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Model
{
    /// <summary>
    /// The level 2 enemy class, inherits game object
    /// </summary>
    public class Lvl2Enemy : GameObject
    {
        private const int SpeedXDirection = 4;
        private const int SpeedYDirection = 0;
        /// <summary>
        /// Creates an instance of a level 1 enemy
        /// PostCondition: Sprite == new Enemy1Sprite() && SpeedX == SpeedXDirection && SpeedY == SpeedYDirection
        /// 
        /// </summary>
        public Lvl2Enemy()
        {
            Sprite = new Enemy2Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
