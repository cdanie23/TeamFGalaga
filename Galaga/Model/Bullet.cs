using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// The Bullet game object
    /// </summary>
    public class Bullet : GameObject
    {
        private const int BulletYSpeed = 20;
        /// <summary>
        /// Creates an instance of the Bullet 
        /// </summary>
        public Bullet()
        {
            this.Sprite = new BulletSprite();
            this.SetSpeed(0, BulletYSpeed);
        }
        
    }
}
