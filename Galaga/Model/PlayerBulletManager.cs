using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     The player bullet manager class
    /// </summary>
    public class PlayerBulletManager : BulletManager
    {
        #region Data members

        private readonly EnemiesManager enemyManager;
        private readonly PlayerManager playerManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the player manager
        ///     PreCondition: enemyManager != null, playerManager != null
        ///     PostCondition: this.enemyManager == enemyManager, this.playerManager == playerManager
        /// </summary>
        /// <param name="canvas">the canvas</param>
        /// <param name="enemyManager">the enemy manager</param>
        /// <param name="playerManager">the player manager</param>
        /// <exception cref="ArgumentNullException">thrown if the enemy manager or player manager is null</exception>
        public PlayerBulletManager(Canvas canvas, EnemiesManager enemyManager, PlayerManager playerManager) :
            base(canvas)
        {
            this.enemyManager = enemyManager ?? throw new ArgumentNullException(nameof(enemyManager));
            this.playerManager = playerManager ?? throw new ArgumentNullException(nameof(playerManager));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The enemy struck event
        /// </summary>
        public event EventHandler<Enemy> EnemyStruck;

        /// <summary>
        ///     Moves the players bullet, if collision is detected then the enemy struck event is invoked and the player gets its
        ///     bullet back.
        /// </summary>
        public void MoveBullet()
        {
            var flaggedBullets = new Collection<Bullet>();
            var invokeEvent = false;
            Enemy hitEnemy = null;
            foreach (var bullet in Bullets)
            {
                if (!bullet.Move(Canvas))
                {
                    flaggedBullets.Add(bullet);
                    this.playerManager.Player.BulletsAvailable.Push(new Bullet(BulletType.Player,
                        Player.PlayerBulletSpeed));
                }

                foreach (var enemy in this.enemyManager)
                {
                    if (enemy.CollisionDetected(bullet))
                    {
                        flaggedBullets.Add(bullet);
                        invokeEvent = true;
                        hitEnemy = enemy;
                    }
                }
            }

            if (invokeEvent)
            {
                this.EnemyStruck?.Invoke(this, hitEnemy);
            }

            RemoveFlaggedBullets(flaggedBullets);
        }

        /// <summary>
        ///     Clears all the bullets and resets the players number of bullets available
        ///     PostConditions: this.Bullets.Count == 0, this.playerManager.Player.BulletsAvailable.Count == 3
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            this.playerManager.Player.BulletsAvailable.Clear();
            this.playerManager.Player.SetupActiveBullets();
        }

        #endregion
    }
}