using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     The player manager class
    /// </summary>
    public class PlayerManager
    {
        #region Data members

        private const double PlayerOffsetFromBottom = 30;

        private const int TimeBetweenSpriteFlash = 200;
        private const int MillisecondDelayBetweenBullets = 500;

        private readonly Canvas canvas;

        private readonly double canvasWidth;
        private readonly double canvasHeight;

        private DateTime dateTimeOfLastPlayerBullet;
        private readonly TimeSpan delayBetweenBullets;
        private readonly DispatcherTimer invulnerabilityTimer;
        private int invulnerabilityTimerTickCount;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the player
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        ///     Gets or sets the number of lives
        /// </summary>
        /// <param name="value">the value to the set the number of lives to</param>
        public int NumOfLives { get; set; }

        /// <summary>
        ///     Get or set the score of the player
        ///     <param name="value">the value to set it to</param>
        /// </summary>
        public int Score { get; set; }

        private bool CanPlayerShoot => this.Player.BulletsAvailable.Count > 0 &&
                                       DateTime.Now - this.dateTimeOfLastPlayerBullet > this.delayBetweenBullets;

        /// <summary>
        ///     Gets or sets if the player is invulernable
        /// </summary>
        /// <param name="value">the value to set</param>
        public bool IsInvulnerable { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the player manager
        ///     PreCondition: Canvas != null
        ///     PostCondition: this.Canvas == Canvas, this.canvasHeight == canvasHeight, this.canvasWidth == canvasWidth,
        ///     this.delayBetweenBullets != null, this.NumOfLives == Player.NumOfLives;
        /// </summary>
        /// <param name="canvas">the Canvas of the game</param>
        /// <exception cref="ArgumentNullException">thrown if the Canvas is null</exception>
        public PlayerManager(Canvas canvas)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.canvasHeight = canvas.Height;
            this.canvasWidth = canvas.Width;

            this.delayBetweenBullets = new TimeSpan(0, 0, 0, 0, MillisecondDelayBetweenBullets);
            this.invulnerabilityTimer = new DispatcherTimer
                { Interval = new TimeSpan(0, 0, 0, 0, TimeBetweenSpriteFlash) };
            this.invulnerabilityTimer.Tick += this.invulnerabilityTimerTick;
            this.NumOfLives = Player.NumOfLives;
            this.IsInvulnerable = false;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the player in the game
        /// </summary>
        public void SetupPlayer()
        {
            this.createAndPlacePlayer();
        }

        private void invulnerabilityTimerTick(object sender, object e)
        {
            this.flashPlayerSprite();

            if (this.invulnerabilityTimerTickCount == 5)
            {
                this.invulnerabilityTimer.Stop();
                this.invulnerabilityTimerTickCount = 0;
                this.IsInvulnerable = false;
            }

            this.invulnerabilityTimerTickCount++;
        }

        /// <summary>
        ///     Updates the number of lives and removes the player if dead
        ///     Post-Condition: this.NumOfLives @prev - 1
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the event arguments</param>
        public void OnPlayerStruck(object sender, object e)
        {
            this.NumOfLives--;
            if (this.NumOfLives == 0)
            {
                this.canvas.Children.Remove(this.Player.Sprite);
            }

            this.IsInvulnerable = true;
            this.invulnerabilityTimer.Start();
        }

        /// <summary>
        ///     Shoots the Bullet of the Player of they are permitted to shoot
        ///     Pre:Condition: this.canPlayerShoot()
        ///     PostCondition: this.Canvas.Children == @prev + 1, this.dateTimeOfLastPlayerBullet == DateTime.Now
        /// </summary>
        /// <returns>The bullet that was shot or null otherwise</returns>
        public Bullet ShootPlayerBullet()
        {
            if (this.CanPlayerShoot)
            {
                var bullet = this.Player.BulletsAvailable.Pop();
                this.canvas.Children.Add(bullet.Sprite);
                bullet.X = this.Player.X + this.Player.Width / 2;
                bullet.Y = this.Player.Y - BulletManager.SpaceInBetweenBulletAndShip;
                this.dateTimeOfLastPlayerBullet = DateTime.Now;
                return bullet;
            }

            return null;
        }

        private void createAndPlacePlayer()
        {
            this.Player = new Player();
            this.canvas.Children.Add(this.Player.Sprite);

            this.placePlayerNearBottomOfBackgroundCentered();
        }

        private void placePlayerNearBottomOfBackgroundCentered()
        {
            this.Player.X = this.canvasWidth / 2 - this.Player.Width / 2.0;
            this.Player.Y = this.canvasHeight - this.Player.Height - PlayerOffsetFromBottom;
        }

        /// <summary>
        ///     Moves the Player left
        ///     Precondition: newLocation > 0
        /// </summary>
        public void MovePlayerLeft()
        {
            var newLocation = this.Player.X - this.Player.SpeedX;
            if (newLocation > 0)
            {
                this.Player.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the Player right.
        ///     Pre-Condition: newLocation is less than this.canvasWidth + this.playerWidth
        /// </summary>
        public void MovePlayerRight()
        {
            var newLocation = this.Player.X + this.Player.SpeedX;
            if (newLocation + this.Player.Width < this.canvasWidth)
            {
                this.Player.MoveRight();
            }
        }

        private void flashPlayerSprite()
        {
            this.Player.Sprite.Visibility =
                this.invulnerabilityTimerTickCount % 2 == 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        #endregion
    }
}