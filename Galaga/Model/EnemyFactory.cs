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
        /// <summary>
        ///     Creates an enemy
        ///     Precondition: LevelEnemy > 0, LevelEnemy less than or equal to 4
        /// </summary>
        /// <param name="levelEnemy">The level of the enemy you want created</param>
        /// <exception cref="ArgumentException">thrown if the level of the enemy is less than 1 or greater than 4</exception>
        /// <returns>the enemy object or null if no object was created</returns>
        public Enemy CreateNewEnemy(int levelEnemy)
        {
            if (levelEnemy > 4)
            {
                throw new ArgumentException(nameof(levelEnemy));
            }

            if (levelEnemy < 1)
            {
                throw new ArgumentException(nameof(levelEnemy));
            }

            switch (levelEnemy)
            {
                case 1:
                    return new Enemy(1, 1, 4, 0, new Enemy1Sprite());
                case 2:
                    return new Enemy(2, 2, 4, 0, new Enemy2Sprite());
                case 3:
                    BaseSprite[] level3EnemySprites = { new Enemy3Sprite(), new Enemy3SpriteVariant() };
                    return new ShootingEnemy(3, 3, 5, 0, new Enemy3Sprite(), level3EnemySprites);
                case 4:
                    BaseSprite[] level4EnemySprites = { new Enemy4Sprite(), new Enemy4SpriteVariant() };
                    return new ShootingEnemy(4, 4, 5, 0, new Enemy4Sprite(), level4EnemySprites);
            }

            return null;
        }
    }
}
