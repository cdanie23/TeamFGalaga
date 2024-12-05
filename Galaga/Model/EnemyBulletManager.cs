using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     The enemy bullet manager class
    /// </summary>
    public class EnemyBulletManager : BulletManager
    {
        #region Data members

        private const int MaxTimeForNextEnemyShot = 1;
        private const int MinTimeForNextEnemyShot = 0;
        private readonly PlayerManager playerManager;

        #endregion

        #region Properties

        /// <summary>
        ///     The dispatch timer that controls the timing of when the enemies shoot
        /// </summary>
        public DispatcherTimer EnemyRandomShootTimer { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the enemy bullet manager
        ///     PreCondition: playerManager != null
        ///     PostCondition: this.playerManager == playerManager, this.EnemyRandomShootTimer.Interval == random interval
        /// </summary>
        /// <param name="canvas">the canvas</param>
        /// <param name="playerManager">the player manager</param>
        /// <exception cref="ArgumentNullException">thrown if the player manager is null</exception>
        public EnemyBulletManager(Canvas canvas, PlayerManager playerManager) : base(canvas)
        {
            this.playerManager = playerManager ?? throw new ArgumentNullException(nameof(playerManager));
            this.EnemyRandomShootTimer = new DispatcherTimer { Interval = this.GetRandomInterval() };
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The player struck event
        /// </summary>
        public event EventHandler<EventArgs> PlayerStruck;

        /// <summary>
        ///     Moves the bullet, if collision is detected then removes the used bullet and invoked the player struck event
        /// </summary>
        public void MoveBullet()
        {
            var flaggedBullets = new Collection<Bullet>();
            foreach (var bullet in Bullets)
            {
                if (!bullet.Move(Canvas))
                {
                    flaggedBullets.Add(bullet);
                }

                this.checkCollisionWithPlayer(bullet, flaggedBullets);
            }

            RemoveFlaggedBullets(flaggedBullets);
        }

        private void checkCollisionWithPlayer(Bullet bullet, Collection<Bullet> flaggedBullets)
        {
            if (this.playerManager.PlayerDoubleActive)
            {
                this.checkCollisionWhenPlayerDoubleActive(bullet, flaggedBullets);
            }
            else if (this.playerManager.Player.CollisionDetected(bullet) && !this.playerManager.IsInvulnerable)
            {
                this.PlayerStruck?.Invoke(this, EventArgs.Empty);
                flaggedBullets.Add(bullet);
            }
        }

        private void checkCollisionWhenPlayerDoubleActive(Bullet bullet, Collection<Bullet> flaggedBullets)
        {
            if (this.playerManager.PlayerDouble.CollisionDetected(bullet) && !this.playerManager.IsInvulnerable)
            {
                this.playerManager.removePlayerDouble(this.playerManager.PlayerDouble);
                flaggedBullets.Add(bullet);
            }
            else if (this.playerManager.Player.CollisionDetected(bullet) && !this.playerManager.IsInvulnerable)
            {
                this.playerManager.removePlayerDouble(this.playerManager.Player);
                flaggedBullets.Add(bullet);
            }
        }

        /// <summary>
        ///     Gets a random time span between this.RandomSecMinvalue and this.RandomSecMaxvalue
        /// </summary>
        /// <returns>A random time span</returns>
        public TimeSpan GetRandomInterval()
        {
            var random = new Random();

            var randomSecond = random.Next(MinTimeForNextEnemyShot, MaxTimeForNextEnemyShot);
            return new TimeSpan(0, 0, 0, randomSecond);
        }

        #endregion
    }
}