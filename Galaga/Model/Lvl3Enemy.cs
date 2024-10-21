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
        /// The number of points gained for killing this enemy
        /// </summary>
        public const int Points = 3;
        /// <summary>
        /// Get or set the bullet
        /// </summary>
        public Bullet Bullet { get; private set; }
        /// <summary>
        /// Creates an instance of a level 3 enemy
        /// PostCondition: Sprite == new Enemy1Sprite(), SpeedX == SpeedXDirection, SpeedY == SpeedYDirection
        /// </summary>
        public Lvl3Enemy()
        {
            this.Sprite = new Enemy3Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.Bullet = new Bullet();
        }

    }
}
