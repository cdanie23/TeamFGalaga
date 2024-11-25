using Galaga.View.Sprites;
using Galaga.View.Sprites.EnemySprites;
using Galaga.View.Sprites.EnemySprites.EnemySpriteVariants;
using System;

namespace Galaga.Model
{
    /// <summary>
    /// The enemy factory class
    /// </summary>
    public class EnemyFactory
    {
        
        private const int Level1EnemyPoints = 5;
        private const int Level2EnemyPoints = 10;
        private const int Level3EnemyPoints = 15;
        private const int Level4EnemyPoints = 20;

        private  int level1EnemyXSpeed;
        private  int level2EnemyXSpeed;
        private  int level3EnemyXSpeed;
        private  int level4EnemyXSpeed;

        private  int shootingEnemyBulletSpeed;

        private const int EnemyLevelOne = 1;
        private const int EnemyLevelTwo = 2;
        private const int EnemyLevelThree = 3;
        private const int EnemyLevelFour = 4;

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
                    return new Enemy(Level1EnemyPoints, EnemyLevelOne, this.level1EnemyXSpeed, 0, new Enemy1Sprite());
                case EnemyLevelTwo:
                    return new Enemy(Level2EnemyPoints, EnemyLevelTwo, this.level2EnemyXSpeed, 0, new Enemy2Sprite());
                case EnemyLevelThree:
                    BaseSprite[] level3EnemySprites = { new Enemy3Sprite(), new Enemy3SpriteVariant() };
                    return new ShootingEnemy(Level3EnemyPoints, EnemyLevelThree, this.level3EnemyXSpeed, 0, new Enemy3Sprite(), level3EnemySprites, this.shootingEnemyBulletSpeed);
                case EnemyLevelFour:
                    BaseSprite[] level4EnemySprites = { new Enemy4Sprite(), new Enemy4SpriteVariant() };
                    return new ShootingEnemy(Level4EnemyPoints, EnemyLevelFour, this.level4EnemyXSpeed, 0, new Enemy4Sprite(), level4EnemySprites, this.shootingEnemyBulletSpeed);
            }

            return null;
        }

        public void ChangeEnemyAttributes(int gamelevel)
        {
            switch (gamelevel)
            {
                case 1:
                    this.level1EnemyXSpeed = 2;
                    this.level2EnemyXSpeed = 2;
                    this.level3EnemyXSpeed = 2;
                    this.level4EnemyXSpeed = 2;

                    this.shootingEnemyBulletSpeed = 8;
                    break;
                case 2:
                    this.level1EnemyXSpeed = 3;
                    this.level2EnemyXSpeed = 3;
                    this.level3EnemyXSpeed = 4;
                    this.level4EnemyXSpeed = 4;

                    this.shootingEnemyBulletSpeed = 9;
                    break;
                case 3:
                    this.level1EnemyXSpeed = 5;
                    this.level2EnemyXSpeed = 5;
                    this.level3EnemyXSpeed = 6;
                    this.level4EnemyXSpeed = 7;

                    this.shootingEnemyBulletSpeed = 10;
                    break;
            }
        }
    }
}
