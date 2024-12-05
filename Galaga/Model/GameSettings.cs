namespace Galaga.Model
{
    /// <summary>
    ///     The game settings class
    /// </summary>
    public class GameSettings
    {
        #region Data members

        private const int StartLevel = 1;

        private static readonly int[] EnemyNumbersLevel2 = { 3, 4, 4, 5 };
        private static readonly int[] EnemyNumbersLevel1 = { 2, 3, 3, 4 };
        private static readonly int[] EnemyNumbersLevel3 = { 3, 4, 5, 6 };

        private const int Level1EnemySpeed = 2;
        private const int Level2Enemies1And2Speed = 3;
        private const int Level2Enemies3And4Speed = 4;
        private const int Level3Enemies1And2Speed = 5;
        private const int Level3Enemies3Speed = 6;
        private const int Level3Enemies4Speed = 7;
        private const int BonusEnemySpeed = 5;
        private const int Level1BulletSpeed = 8;
        private const int Level2BulletSpeed = 9;
        private const int Level3BulletSpeed = 10;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the level.
        /// </summary>
        public int Level { get; private set; }

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

        /// <summary>
        ///     Gets the number of each enemy.
        /// </summary>
        public int[] NumberOfEachEnemy { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of game settings
        ///     PostConditions: this.Level1EnemyXSpeed == 2, this.Level2EnemyXSpeed == 2, this.Level3EnemyXSpeed == 2,
        ///     this.Level4EnemyXSpeed == 2
        /// </summary>
        public GameSettings()
        {
            this.Level = StartLevel;

            this.Level1EnemyXSpeed = Level1EnemySpeed;
            this.Level2EnemyXSpeed = Level1EnemySpeed;
            this.Level3EnemyXSpeed = Level1EnemySpeed;
            this.Level4EnemyXSpeed = Level1EnemySpeed;
            this.BonusEnemyXSpeed = BonusEnemySpeed;

            this.ShootingEnemyBulletSpeed = Level1BulletSpeed;

            this.NumberOfEachEnemy = EnemyNumbersLevel1;
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
            this.Level = gameLevel;
            switch (gameLevel)
            {
                case 1:

                    this.Level1EnemyXSpeed = Level1EnemySpeed;
                    this.Level2EnemyXSpeed = Level1EnemySpeed;
                    this.Level3EnemyXSpeed = Level1EnemySpeed;
                    this.Level4EnemyXSpeed = Level1EnemySpeed;

                    this.ShootingEnemyBulletSpeed = Level1BulletSpeed;

                    this.NumberOfEachEnemy = EnemyNumbersLevel1;
                    break;
                case 2:

                    this.Level1EnemyXSpeed = Level2Enemies1And2Speed;
                    this.Level2EnemyXSpeed = Level2Enemies1And2Speed;
                    this.Level3EnemyXSpeed = Level2Enemies3And4Speed;
                    this.Level4EnemyXSpeed = Level2Enemies3And4Speed;

                    this.ShootingEnemyBulletSpeed = Level2BulletSpeed;

                    this.NumberOfEachEnemy = EnemyNumbersLevel2;
                    break;
                case 3:

                    this.Level1EnemyXSpeed = Level3Enemies1And2Speed;
                    this.Level2EnemyXSpeed = Level3Enemies1And2Speed;
                    this.Level3EnemyXSpeed = Level3Enemies3Speed;
                    this.Level4EnemyXSpeed = Level3Enemies4Speed;

                    this.ShootingEnemyBulletSpeed = Level3BulletSpeed;

                    this.NumberOfEachEnemy = EnemyNumbersLevel3;
                    break;
            }
        }

        #endregion
    }
}