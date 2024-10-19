using Galaga.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Model
{
    /// <summary>
    /// The level 3 enemy class, inherits game object
    /// </summary>
    public class Lvl3Enemy : GameObject
    {
        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;
        /// <summary>
        /// Creates an instance of a level 3 enemy
        /// PostCondition: Sprite == new Enemy1Sprite(), SpeedX == SpeedXDirection, SpeedY == SpeedYDirection
        /// </summary>
        public Lvl3Enemy()
        {
            this.Sprite = new Enemy3Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        public void ShootWeapon()
        {
            //TODO implement and document
        }
    }
}
