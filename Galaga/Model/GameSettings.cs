namespace Galaga.Model
{
    /// <summary>
    ///     The game settings class
    /// </summary>
    public class GameSettings
    {
        #region Properties

        /// <summary>
        ///     Get the setting of the speed of the level one enemy
        /// </summary>
        public int Level1EnemyXSpeed { get; private set; }

        /// <summary>
        ///     Get the setting of the speed of the level two enemy
        /// </summary>
        public int Level2EnemyXSpeed { get; private set; }

        /// <summary>
        ///     Get the setting of the speed of the level three enemy
        /// </summary>
        public int Level3EnemyXSpeed { get; private set; }

        /// <summary>
        ///     Get the setting of the speed of the level four enemy
        /// </summary>
        public int Level4EnemyXSpeed { get; private set; }

        /// <summary>
        ///     Get the setting of the speed of the bonus enemy
        /// </summary>
        public int BonusEnemyXSpeed { get; private set; }

        /// <summary>
        ///     Get the setting of the bullet speed of the shooting enemies
        /// </summary>
        public int ShootingEnemyBulletSpeed { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of game settings
        ///     PostConditions: this.Level1EnemyXSpeed == 2, this.Level2EnemyXSpeed == 2, this.Level3EnemyXSpeed == 2,
        ///     this.Level4EnemyXSpeed == 2
        /// </summary>
        public GameSettings()
        {
            this.Level1EnemyXSpeed = 2;
            this.Level2EnemyXSpeed = 2;
            this.Level3EnemyXSpeed = 2;
            this.Level4EnemyXSpeed = 2;
            this.BonusEnemyXSpeed = 5;

            this.ShootingEnemyBulletSpeed = 8;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the game settings given the game level
        ///     PostConditions: all properties != @prev
        /// </summary>
        /// <param name="gameLevel">the level of the game to set the settings to</param>
        public void SetGameSettings(int gameLevel)
        {
            switch (gameLevel)
            {
                case 1:
                    this.Level1EnemyXSpeed = 2;
                    this.Level2EnemyXSpeed = 2;
                    this.Level3EnemyXSpeed = 2;
                    this.Level4EnemyXSpeed = 2;

                    this.ShootingEnemyBulletSpeed = 8;
                    break;
                case 2:
                    this.Level1EnemyXSpeed = 3;
                    this.Level2EnemyXSpeed = 3;
                    this.Level3EnemyXSpeed = 4;
                    this.Level4EnemyXSpeed = 4;

                    this.ShootingEnemyBulletSpeed = 9;
                    break;
                case 3:
                    this.Level1EnemyXSpeed = 5;
                    this.Level2EnemyXSpeed = 5;
                    this.Level3EnemyXSpeed = 6;
                    this.Level4EnemyXSpeed = 7;

                    this.ShootingEnemyBulletSpeed = 10;
                    break;
            }
        }

        #endregion
    }
}