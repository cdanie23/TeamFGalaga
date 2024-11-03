using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Model
{
    /// <summary>
    /// The shooting enemy class
    /// </summary>
    public class ShootingEnemy : Enemy
    {
        /// <summary>
        /// Get or set the bullet
        /// </summary>
        public Bullet Bullet { get; set; }
    }
}
