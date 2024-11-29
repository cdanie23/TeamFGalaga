using System;

namespace Galaga.Model
{
    /// <summary>
    ///     The scoreboard entry class
    /// </summary>
    public class ScoreboardEntry : IComparable<ScoreboardEntry>
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///     Gets or sets the Score
        /// </summary>
        public int Score { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Makes an instance of the scoreboard entry
        ///     PreCondition: this.Name != null
        ///     PostCondition: this.Name == name, this.Level == level, this.Score == score
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="level">The level</param>
        /// <param name="score">The score</param>
        /// <exception cref="ArgumentNullException">thrown if the name is null</exception>
        public ScoreboardEntry(string name, int level, int score)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Level = level;
            this.Score = score;
        }

        /// <summary>
        ///     Parameterless constructor used for serialization
        /// </summary>
        public ScoreboardEntry()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Compares the entries by their natural score
        /// </summary>
        /// <param name="other">the object to compare to</param>
        /// <returns>the int corresponding to which entry comes first</returns>
        public int CompareTo(ScoreboardEntry other)
        {
            return other.Score.CompareTo(this.Score);
        }

        /// <summary>
        ///     Gets a string representation of this
        /// </summary>
        /// <returns>the string representation</returns>
        public override string ToString()
        {
            return $"Name : {this.Name} Level: {this.Level} Score: {this.Score}";
        }

        #endregion
    }
}