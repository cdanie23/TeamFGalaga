using System;
using Galaga.View.Sprites;
using Galaga.View.Sprites.EnemySprites;
using Galaga.View.Sprites.EnemySprites.EnemySpriteVariants;

namespace Galaga.Model
{
    /// <summary>
    ///     The enemy factory class
    /// </summary>
    public class EnemyFactory
    {
        #region Data members

        private const int EnemyLevelOne = 1;
        private const int EnemyLevelTwo = 2;
        private const int EnemyLevelThree = 3;
        private const int EnemyLevelFour = 4;

        private readonly GameSettings gameSettings;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the enemy factory
        ///     PreCondition: gameSettings != null
        ///     PostCondition: this.gameSettings == gameSettings
        /// </summary>
        /// <param name="gameSettings">the game settings of the enemies</param>
        /// <exception cref="ArgumentNullException">thrown if game settings is null</exception>
        public EnemyFactory(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings ?? throw new ArgumentNullException(nameof(gameSettings));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates an enemy
        ///     Precondition: LevelEnemy > 0, LevelEnemy less than or equal to 4
        /// </summary>
        /// <param name="levelEnemy">The level of the enemy you want created</param>
        /// <exception cref="ArgumentException">thrown if the level of the enemy is less than 1 or greater than 4</exception>
        /// <returns>the enemy object or null if no object was created</returns>
        public Enemy CreateNewEnemy(int levelEnemy)
        {
            if (levelEnemy > EnemyLevelFour)
            {
                throw new ArgumentException(nameof(levelEnemy));
            }

            if (levelEnemy < EnemyLevelOne)
            {
                throw new ArgumentException(nameof(levelEnemy));
            }

            switch (levelEnemy)
            {
                case EnemyLevelOne:
                    return new Enemy(EnemiesManager.Level1EnemyPoints, EnemyLevelOne,
                        this.gameSettings.Level1EnemyXSpeed, 0, new Enemy1Sprite());
                case EnemyLevelTwo:
                    return new Enemy(EnemiesManager.Level2EnemyPoints, EnemyLevelTwo,
                        this.gameSettings.Level2EnemyXSpeed, 0, new Enemy2Sprite());
                case EnemyLevelThree:
                    BaseSprite[] level3EnemySprites = { new Enemy3Sprite(), new Enemy3SpriteVariant() };
                    return new ShootingEnemy(EnemiesManager.Level3EnemyPoints, EnemyLevelThree,
                        this.gameSettings.Level3EnemyXSpeed, 0, new Enemy3Sprite(), level3EnemySprites,
                        this.gameSettings.ShootingEnemyBulletSpeed);
                case EnemyLevelFour:
                    BaseSprite[] level4EnemySprites = { new Enemy4Sprite(), new Enemy4SpriteVariant() };
                    return new ShootingEnemy(EnemiesManager.Level4EnemyPoints, EnemyLevelFour,
                        this.gameSettings.Level4EnemyXSpeed, 0, new Enemy4Sprite(), level4EnemySprites,
                        this.gameSettings.ShootingEnemyBulletSpeed);
            }

            return null;
        }

        #endregion
    }
}