using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites.EnemySprites;

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
        private readonly BonusEnemyManager bonusEnemyManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerBulletManager"/> class.
        ///     Creates an instance of the player manager
        ///     PreCondition: enemyManager != null, playerManager != null
        ///     PostCondition: this.enemyManager == enemyManager, this.playerManager == playerManager
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="enemyManager">The enemy manager.</param>
        /// <param name="playerManager">The player manager.</param>
        /// <param name="bonusEnemyManager">The bonus enemy manager.</param>
        /// <exception cref="System.ArgumentNullException">
        /// enemyManager
        /// or
        /// playerManager
        /// or
        /// bonusEnemyManager
        /// </exception>
        public PlayerBulletManager(Canvas canvas, EnemiesManager enemyManager, PlayerManager playerManager, BonusEnemyManager bonusEnemyManager) :
            base(canvas)
        {
            this.enemyManager = enemyManager ?? throw new ArgumentNullException(nameof(enemyManager));
            this.playerManager = playerManager ?? throw new ArgumentNullException(nameof(playerManager));
            this.bonusEnemyManager = bonusEnemyManager ?? throw new ArgumentNullException(nameof(bonusEnemyManager));
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
                        if (enemy.Sprite is BonusEnemySprite)
                        {
                            SoundPlayer.playBonusGottenSound();
                            this.bonusEnemyManager.SetInactive();
                        }

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