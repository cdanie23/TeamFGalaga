using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The stars manager class
    /// </summary>
    public class StarsManager
    {
        #region Data members

        private const int NumOfStars = 60;
        private const int TimerInteralMilliseconds = 20;
        private const int SpeedOfStars = 6;

        private readonly Canvas canvas;
        private readonly DispatcherTimer starMoveTimer;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the star manager
        ///     PreConditions: this.canvas != null
        ///     PostConditions: this.canvas == canvas, this.startMoveTimer != null,
        /// </summary>
        /// <param name="canvas">the canvas</param>
        /// <exception cref="ArgumentNullException">thrown if the canvas is null</exception>
        public StarsManager(Canvas canvas)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.starMoveTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, TimerInteralMilliseconds) };

            this.setupStars();

            this.starMoveTimer.Tick += this.onTimerTick;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Makes all the stars and starts the movement of the stars
        ///     PostConditions: this.starMoveTimer.IsEnabled == true
        /// </summary>
        public void InitializeStars()
        {
            this.setupStars();
            this.starMoveTimer.Start();
        }

        /// <summary>
        ///     Stops all the stars from moving
        ///     PostConditions: this.starMoveTimer.IsEnabled == false;
        /// </summary>
        public void StopStars()
        {
            this.starMoveTimer.Stop();
        }

        private void setupStars()
        {
            for (var i = 0; i < NumOfStars; i++)
            {
                var star = new StarSprite();
                Canvas.SetLeft(star, this.getRandomXPosition());
                Canvas.SetTop(star, this.getRandomYPosition());
                Canvas.SetZIndex(star, -2);
                this.canvas.Children.Add(star);
            }
        }

        private double getRandomXPosition()
        {
            var random = new Random();
            return random.Next(0, (int)this.canvas.Width);
        }

        private double getRandomYPosition()
        {
            var random = new Random();
            return random.Next(0, (int)this.canvas.Height);
        }

        private void onTimerTick(object sender, object e)
        {
            foreach (var item in this.canvas.Children)
            {
                if (item is StarSprite star)
                {
                    var currentY = Canvas.GetTop(star);
                    var newY = currentY + SpeedOfStars;
                    if (newY >= this.canvas.Height)
                    {
                        newY = 0;
                    }

                    Canvas.SetTop(star, newY);
                }
            }
        }

        #endregion
    }
}