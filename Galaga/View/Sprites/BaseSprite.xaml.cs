using System.Drawing;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites
{
    /// <summary>
    /// Defines BaseSprite from which all sprites inherit.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// <seealso cref="Galaga.View.Sprites.ISpriteRenderer" />
    public abstract partial class BaseSprite : ISpriteRenderer
    {
        /// <summary>
        /// Gets the boundary rectangle for the sprite
        /// </summary>
        public Rectangle Boundary { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSprite"/> class.
        /// </summary>
        protected BaseSprite()
        {
            this.InitializeComponent();
            this.Boundary = new Rectangle(0, 0, 0, 0);


        }

        


        
        /// <summary>
        /// Renders sprite at the specified (x,y) location in relation
        /// to the top, left part of the canvas.
        /// </summary>
        /// <param name="x">x location</param>
        /// <param name="y">y location</param>
        public void RenderAt(double x, double y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            this.Boundary = new Rectangle((int)x, (int)y, (int)this.ActualWidth, (int)this.ActualHeight);
        }

    }


}
