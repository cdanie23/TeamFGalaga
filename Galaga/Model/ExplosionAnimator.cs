using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.View.Sprites;
using Galaga.View.Sprites.ExplosionSprites;

namespace Galaga.Model
{
    /// <summary>
    ///     The Explosion Animator
    /// </summary>
    public class ExplosionAnimator
    {
        #region Data members

        private const double AnimationIntervalInMilliseconds = 50;

        #endregion

        #region Methods

        /// <summary>
        ///     Animates three sprites in consecutive order at the specified coordinates.
        /// </summary>
        /// <param name="canvas">The Canvas to draw the sprites on.</param>
        /// <param name="x">The X coordinate for the sprite.</param>
        /// <param name="y">The Y coordinate for the sprite.</param>
        public static void PlayExplosion(Canvas canvas, double x, double y)
        {
            const int currentSpriteIndex = 0;

            BaseSprite[] spriteAnimations =
            {
                new FirstExplosionSprite(), new SecondExplosionSprite(), new ThirdExplosionSprite()
            };

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(AnimationIntervalInMilliseconds)
            };

            setTimerTick(canvas, x, y, timer, currentSpriteIndex, spriteAnimations);
            timer.Start();
        }

        private static void setTimerTick(Canvas canvas, double x, double y, DispatcherTimer timer,
            int currentSpriteIndex, BaseSprite[] spriteAnimations)
        {
            timer.Tick += (sender, args) =>
            {
                if (currentSpriteIndex > spriteAnimations.Length)
                {
                    timer.Stop();

                    removeAllAnimations(canvas, spriteAnimations);
                }

                if (currentSpriteIndex < spriteAnimations.Length)
                {
                    removePreviousAnimation(canvas, currentSpriteIndex, spriteAnimations);
                    addSpriteAnimation(canvas, x, y, spriteAnimations, currentSpriteIndex);
                }

                currentSpriteIndex++;
            };
        }

        /// <summary>
        ///     Removes the previous animation.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="currentSpriteIndex">Index of the current sprite.</param>
        /// <param name="spriteAnimations">The sprite animations.</param>
        private static void removePreviousAnimation(Canvas canvas, int currentSpriteIndex,
            BaseSprite[] spriteAnimations)
        {
            if (currentSpriteIndex > 0)
            {
                canvas.Children.Remove(spriteAnimations[currentSpriteIndex - 1]);
            }
        }

        /// <summary>
        ///     Removes all animations.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="spriteAnimations">The sprite animations.</param>
        private static void removeAllAnimations(Canvas canvas, BaseSprite[] spriteAnimations)
        {
            foreach (var sprite in spriteAnimations)
            {
                canvas.Children.Remove(sprite);
            }
        }

        /// <summary>
        ///     Adds the sprite animation.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="spriteAnimations">The sprite animations.</param>
        /// <param name="currentSpriteIndex">Index of the current sprite.</param>
        private static void addSpriteAnimation(Canvas canvas, double x, double y, BaseSprite[] spriteAnimations,
            int currentSpriteIndex)
        {
            var spriteElement = spriteAnimations[currentSpriteIndex];
            Canvas.SetLeft(spriteElement, x);
            Canvas.SetTop(spriteElement, y);
            canvas.Children.Add(spriteElement);
        }

        #endregion
    }
}