using Windows.UI.Xaml;
using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// The shooting enemy class
    /// </summary>
    public class ShootingEnemy : Enemy
    {
        /// <summary>
        /// Get or set the bullet
        /// </summary>
        public Bullet Bullet { get; set; }
        /// <summary>
        /// Gets or sets the array of sprite variants 
        /// </summary>
        public BaseSprite[] SpriteAnimations { get; set; }
        /// <summary>
        /// Flips the sprite back and forth 
        /// </summary>
        public void UpdateSprite()
        {
            if (Sprite == this.SpriteAnimations[0])
            {
                this.SpriteAnimations[0].Visibility = Visibility.Collapsed;
                this.SpriteAnimations[1].Visibility = Visibility.Visible;
                Sprite = this.SpriteAnimations[1];
            }
            else
            {
                this.SpriteAnimations[1].Visibility = Visibility.Collapsed;
                this.SpriteAnimations[0].Visibility = Visibility.Visible;
                Sprite = this.SpriteAnimations[0];
            }
        }
    }
}
