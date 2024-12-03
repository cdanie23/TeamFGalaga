using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites;

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
        private const int NumOfFlashes = 5;
        private const int PowerUpTimeInSeconds = 20;
        private const int NormalPlayerSpeed = 8;
        private const int IncreasedPlayerSpeed = 15;

        /// <summary>
        ///     Gets if the player double is active
        /// </summary>
        public bool PlayerDoubleActive;

        private readonly Canvas canvas;

        private readonly double canvasWidth;
        private readonly double canvasHeight;

        private DateTime dateTimeOfLastPlayerBullet;
        private readonly TimeSpan delayBetweenBullets;
        private int invulnerabilityTimerTickCount;

        private DispatcherTimer powerUpTimer;
        private int powerUpTimerTickCount;

        private BaseSprite playerSkin;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the player
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        ///     Gets the player
        /// </summary>
        public Player PlayerDouble { get; private set; }

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
        ///     Gets or sets if the player is invulnerable
        /// </summary>
        /// <param name="value">the value to set</param>
        public bool IsInvulnerable { get; set; }

        /// <summary>
        ///     Get the timer for the invulnerability of the player
        /// </summary>
        public DispatcherTimer InvulnerabilityTimer { get; }

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
            this.InvulnerabilityTimer = new DispatcherTimer
                { Interval = new TimeSpan(0, 0, 0, 0, TimeBetweenSpriteFlash) };
            this.InvulnerabilityTimer.Tick += this.invulnerabilityTimerTick;

            this.NumOfLives = Player.NumOfLives;
            this.IsInvulnerable = false;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the player in the game
        ///     <param name="playerskin">the skin of the player ship</param>
        /// </summary>
        public void SetupPlayer(BaseSprite playerskin)
        {
            this.playerSkin = playerskin ?? throw new ArgumentNullException(nameof(playerskin));
            this.createAndPlacePlayer();
        }

        private void invulnerabilityTimerTick(object sender, object e)
        {
            this.flashPlayerSprite();

            if (this.invulnerabilityTimerTickCount == NumOfFlashes)
            {
                this.InvulnerabilityTimer.Stop();
                this.invulnerabilityTimerTickCount = 0;
                this.IsInvulnerable = false;
            }

            this.invulnerabilityTimerTickCount++;
        }

        /// <summary>
        ///     Shoots the Bullet of the Player of they are permitted to shoot
        ///     Pre:Condition: this.canPlayerShoot()
        ///     PostCondition: this.Canvas.Children == @prev + 1, this.dateTimeOfLastPlayerBullet == DateTime.Now
        /// </summary>
        /// <returns>The bullet that was shot or null otherwise</returns>
        public ICollection<Bullet> ShootPlayerBullet()
        {
            IList<Bullet> bullets = new List<Bullet>();
            if (this.CanPlayerShoot)
            {
                if (this.PlayerDoubleActive && this.PlayerDouble.BulletsAvailable.Count > 0)
                {
                    var doubleBullet = this.PlayerDouble.BulletsAvailable.Pop();
                    this.canvas.Children.Add(doubleBullet.Sprite);
                    doubleBullet.X = this.PlayerDouble.X + this.PlayerDouble.Width / 2;
                    doubleBullet.Y = this.PlayerDouble.Y - BulletManager.SpaceInBetweenBulletAndShip;
                    bullets.Add(doubleBullet);
                }

                if (this.Player.BulletsAvailable.Count > 0)
                {
                    var bullet = this.Player.BulletsAvailable.Pop();
                    this.canvas.Children.Add(bullet.Sprite);
                    bullet.X = this.Player.X + this.Player.Width / 2;
                    bullet.Y = this.Player.Y - BulletManager.SpaceInBetweenBulletAndShip;
                    this.dateTimeOfLastPlayerBullet = DateTime.Now;
                    bullets.Add(bullet);
                }
            }

            return bullets;
        }

        private void createAndPlacePlayer()
        {
            this.Player = new Player(this.playerSkin);
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
            if (this.PlayerDoubleActive)
            {
                var newLocation = this.Player.X - this.Player.SpeedX;
                if (newLocation > 0)
                {
                    this.Player.MoveLeft();
                    this.PlayerDouble.MoveLeft();
                }
            }
            else
            {
                var newLocation = this.Player.X - this.Player.SpeedX;
                if (newLocation > 0)
                {
                    this.Player.MoveLeft();
                }
            }
        }

        /// <summary>
        ///     Moves the Player right.
        ///     Pre-Condition: newLocation is less than this.canvasWidth + this.playerWidth
        /// </summary>
        public void MovePlayerRight()
        {
            if (this.PlayerDoubleActive)
            {
                var newLocation = this.PlayerDouble.X + this.PlayerDouble.SpeedX;
                if (newLocation + this.PlayerDouble.Width < this.canvasWidth)
                {
                    this.Player.MoveRight();
                    this.PlayerDouble.MoveRight();
                }
            }
            else
            {
                var newLocation = this.Player.X + this.Player.SpeedX;
                if (newLocation + this.Player.Width < this.canvasWidth)
                {
                    this.Player.MoveRight();
                }
            }
        }

        private void flashPlayerSprite()
        {
            this.Player.Sprite.Visibility =
                this.invulnerabilityTimerTickCount % 2 == 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        ///     Powers up the player
        /// </summary>
        public void TemporaryPowerUp()
        {
            this.powerUpTimer = new DispatcherTimer
                { Interval = new TimeSpan(0, 0, 0, 1, 0) };
            this.powerUpTimer.Tick += this.PowerUpTimer_Tick;
            this.powerUpTimerTickCount = 0;
            this.powerUpTimer.Start();
            this.Player.SetPlayerSpeed(IncreasedPlayerSpeed);
            this.PlayerDouble?.SetPlayerSpeed(IncreasedPlayerSpeed);
        }

        /// <summary>
        ///     Doubles up player.
        /// </summary>
        public void DoubleUpPlayer()
        {
            this.createPlayerDouble();
            this.placePlayerAndDouble();
            this.PlayerDoubleActive = true;
        }

        private void placePlayerAndDouble()
        {
            this.Player.X = this.canvasWidth / 2 - this.Player.Width / 2.0 - this.Player.Width;
            this.Player.Y = this.canvasHeight - this.Player.Height - PlayerOffsetFromBottom;

            this.PlayerDouble.X = this.canvasWidth / 2 - this.Player.Width / 2.0 + this.Player.Width;
            this.PlayerDouble.Y = this.canvasHeight - this.Player.Height - PlayerOffsetFromBottom;
        }

        private void createPlayerDouble()
        {
            this.PlayerDouble = new Player(this.playerSkin.Clone());
            this.canvas.Children.Add(this.PlayerDouble.Sprite);
        }

        private void PowerUpTimer_Tick(object sender, object e)
        {
            SoundPlayer.playPowerUpSound();
            this.powerUpTimerTickCount++;
            if (this.powerUpTimerTickCount > PowerUpTimeInSeconds)
            {
                this.powerUpTimer.Stop();
                this.Player.SetPlayerSpeed(NormalPlayerSpeed);
                this.PlayerDouble?.SetPlayerSpeed(NormalPlayerSpeed);
            }
        }

        /// <summary>
        ///     Removes the player double.
        /// </summary>
        /// <param name="playerHit">The player hit.</param>
        public void removePlayerDouble(Player playerHit)
        {
            this.PlayerDoubleActive = false;
            if (playerHit == this.PlayerDouble)
            {
                this.canvas.Children.Remove(this.PlayerDouble.Sprite);
                ExplosionAnimator.PlayExplosion(this.canvas, this.PlayerDouble.X, this.PlayerDouble.Y);
            }
            else
            {
                var playerToRemove = this.Player;
                this.canvas.Children.Remove(playerToRemove.Sprite);
                this.Player = this.PlayerDouble;
                ExplosionAnimator.PlayExplosion(this.canvas, playerToRemove.X, playerToRemove.Y);
            }
        }

        #endregion
    }
}