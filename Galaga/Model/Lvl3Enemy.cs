using Galaga.View.Sprites;


namespace Galaga.Model
{
    /// <summary>
    /// The level 3 enemy class, inherits game object
    /// </summary>
    public class Lvl3Enemy :Enemy
    {
        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;
       
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
            Sprite = new Enemy3Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.Bullet = new Bullet(BulletType.Enemy);

            Points = 3;
        }

    }
}
