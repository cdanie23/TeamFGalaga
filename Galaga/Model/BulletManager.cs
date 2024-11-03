using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    /// A bullet manager which encapsulates all the bullets involved in the game
    /// </summary>
    public class BulletManager : IEnumerable<Bullet>
    {
        private const int Milliseconds = 200;
        /// <summary>
        /// The space in between the bullet and the ship
        /// </summary>
        public const double SpaceInBetweenBulletAndShip = 10;
        private const int RandomSecMaxValue = 4;
        private const int RandomSecMinValue = 2;
        /// <summary>
        /// The dispatch timer that control the timing of the bullets moving
        /// </summary>
        public DispatcherTimer BulletMoveTimer { get; private set; }
        /// <summary>
        /// The dispatch timer that controls the timing of when the enemies shoot
        /// </summary>
        public DispatcherTimer EnemyRandomShootTimer { get; private set; }
        private readonly ICollection<Bullet> bullets;
        private readonly Canvas canvas;
        
        /// <summary>
        /// Makes an instant of the bullet manager 
        /// </summary>
        /// <param name="canvas">The canvas which the game is played</param>
        /// <exception cref="ArgumentNullException">Thrown when the canvas passed in is null</exception>
        public BulletManager(Canvas canvas)
        {
            this.bullets = new List<Bullet>();

            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));

            this.canvas = canvas;
           
        }

        

        public void SetupBulletTimers()
        {
            this.BulletMoveTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, Milliseconds) };
            this.BulletMoveTimer.Tick += this.moveAllBulletsTickEvent;
            this.BulletMoveTimer.Start();

            this.EnemyRandomShootTimer = new DispatcherTimer() { Interval = this.GetRandomInterval() };
        }
        
        /// <summary>
        /// Removes the bullet
        /// </summary>
        /// <param name="bullet">the bullet to remove</param>
        /// <returns>Returns true if the bullet was removed, false if not</returns>
        public Boolean RemoveBullet(Bullet bullet)
        {
            this.bullets.Remove(bullet);
            return this.canvas.Children.Remove(bullet.Sprite);
        }
        /// <summary>
        /// Gets a random time span between this.RandomSecMinvalue & this.RandomSecMaxvalue
        /// </summary>
        /// <returns>A random time span</returns>
        public TimeSpan GetRandomInterval()
        {
            var random = new Random();

            var randomSecond = random.Next(RandomSecMinValue, RandomSecMaxValue);
            return new TimeSpan(0, 0, 0, randomSecond);
        }
        /// <summary>
        /// Gets the IEnumerator to iterate over the collection
        /// </summary>
        /// <returns>The enumerator over the collection</returns>
        public IEnumerator<Bullet> GetEnumerator()
        {
            var enumerator = this.bullets.GetEnumerator();
            enumerator.Dispose();
            return enumerator;
            
        }

        /// <summary>
        /// Adds the bullet
        /// Precondition: bullet != null
        /// PostCondition: this.bullets.Count++
        /// </summary>
        /// <exception cref="ArgumentNullException">thrown if bullet is null</exception>
        /// <param name="bullet">the bullet to add</param>
        public void AddBullet(Bullet bullet)
        {
            if (bullet == null)
            {
                throw new ArgumentNullException(nameof(bullet));
            }
            this.bullets.Add(bullet);
        }

        private void moveAllBulletsTickEvent(Object sender, object e)
        {
            Collection<Bullet> flaggedBullets = new Collection<Bullet>();
            foreach (Bullet bullet in this.bullets)
            {
                switch (bullet.BulletType)
                {
                    case BulletType.Player:
                        if (bullet.Y - bullet.SpeedY > 0)
                        {
                            bullet.MoveUp();
                        }
                        else
                        {
                            flaggedBullets.Add(bullet);
                        }
                        
                        break;
                    case BulletType.Enemy:
                        if (bullet.Y + bullet.SpeedY < this.canvas.Height)
                        {
                            bullet.MoveDown();
                        }
                        else
                        {
                            flaggedBullets.Add(bullet);
                        }
                        break;
                }
            }

            this.removeFlaggedBullets(flaggedBullets);
        }

        private void removeFlaggedBullets(Collection<Bullet> flaggedBullets)
        {
            foreach (Bullet bullet in flaggedBullets)
            {
                this.RemoveBullet(bullet);
            }
        }
        /// <summary>
        /// Clears the collection of all bullets
        /// PostCondition: this.bullets.Count == 0
        /// </summary>
        public void Clear()
        {
            this.bullets.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            var enumerator = this.bullets.GetEnumerator();
            enumerator.Dispose();
            return enumerator;
        }
    }
}
