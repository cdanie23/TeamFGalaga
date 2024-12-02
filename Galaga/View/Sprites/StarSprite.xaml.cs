using System;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Galaga.View.Sprites
{
    /// <summary>
    ///     The star sprite class
    /// </summary>
    public sealed partial class StarSprite
    {
        #region Data members

        private const int MaxValueOfRgbColor = 256;
        private const double StrokeThicknessOfLine = 1;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a randomly sized star sprite
        ///     PostCondition: this.canvas.Children.Count == 4
        /// </summary>
        public StarSprite()
        {
            this.InitializeComponent();
            var random = new Random();
            var starLength = random.Next(0, (int)Height) / 2;

            var randomColor = Color.FromArgb(
                (byte)random.Next(MaxValueOfRgbColor),
                (byte)random.Next(MaxValueOfRgbColor),
                (byte)random.Next(MaxValueOfRgbColor),
                (byte)random.Next(MaxValueOfRgbColor)
            );
            var origin = (int)this.canvas.Height / 2;
            var randomBrush = new SolidColorBrush(randomColor);
            var line1 = new Line
            {
                X1 = origin,
                X2 = origin,
                Y1 = origin - starLength,
                Y2 = origin + starLength,
                Stroke = randomBrush,
                StrokeThickness = StrokeThicknessOfLine
            };

            var line2 = new Line
            {
                X1 = origin + starLength,
                X2 = origin - starLength,
                Y1 = origin,
                Y2 = origin,
                Stroke = randomBrush,
                StrokeThickness = StrokeThicknessOfLine
            };

            var line3 = new Line
            {
                X1 = origin - starLength,
                X2 = origin + starLength,
                Y1 = origin - starLength,
                Y2 = origin + starLength,
                Stroke = randomBrush,
                StrokeThickness = StrokeThicknessOfLine
            };

            var line4 = new Line
            {
                X1 = origin - starLength,
                X2 = origin + starLength,
                Y1 = origin + starLength,
                Y2 = origin - starLength,
                Stroke = randomBrush,
                StrokeThickness = StrokeThicknessOfLine
            };

            this.canvas.Children.Add(line1);
            this.canvas.Children.Add(line2);
            this.canvas.Children.Add(line3);
            this.canvas.Children.Add(line4);
        }

        #endregion
    }
}