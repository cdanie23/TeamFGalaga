using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.View.Sprites.EnemySprites;

namespace Galaga.Model
{
    /// <summary>
    /// The level 4 enemy class
    /// </summary>
    public class Lvl4Enemy : ShootingEnemy
    {
        /// <summary>
        /// Creates an instance of a level 4 enemy 
        /// </summary>
        public Lvl4Enemy()
        {
            Sprite = new Enemy4Sprite();
            SetSpeed(3, 0);
            Points = 4;
            Bullet = new Bullet(BulletType.Enemy);
        }
    }
}
