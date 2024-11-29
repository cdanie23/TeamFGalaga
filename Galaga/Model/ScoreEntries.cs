using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Galaga.Model
{
    /// <summary>
    ///     The score entries class
    /// </summary>
    public class ScoreEntries : IList<ScoreboardEntry>
    {
        #region Data members

        private readonly IList<ScoreboardEntry> scoreEntries;

        #endregion

        #region Properties

        /// <summary>
        ///     Collection of all the names in the entries
        /// </summary>
        public IEnumerable<string> Names => this.scoreEntries.Select(scoreEntry => scoreEntry.Name);

        /// <summary>
        ///     Collection of all the scores in the entries
        /// </summary>
        public IEnumerable<string> Scores => this.scoreEntries.Select(scoreEntry => scoreEntry.Score.ToString());

        /// <summary>
        ///     Collection of the levels in the entries
        /// </summary>
        public IEnumerable<string> Levels => this.scoreEntries.Select(scoreEntry => scoreEntry.Level.ToString());

        /// <summary>
        ///     Gets the count of the score entries
        /// </summary>
        public int Count => this.scoreEntries.Count;

        /// <summary>
        ///     Checks if the score entries are read only
        /// </summary>
        public bool IsReadOnly => this.scoreEntries.IsReadOnly;

        /// <summary>
        ///     Gets or sets the item at the specified index
        ///     PostConditions: this.scoreEntries[index] == value
        /// </summary>
        /// <param name="index">the index of the item</param>
        /// <returns>the item at the index</returns>
        public ScoreboardEntry this[int index]
        {
            get => this.scoreEntries[index];
            set => this.scoreEntries[index] = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the score entries
        ///     PostCondition: this.scoreEntries != null
        /// </summary>
        public ScoreEntries()
        {
            this.scoreEntries = new List<ScoreboardEntry>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the enumerator
        /// </summary>
        /// <returns>the enumerator</returns>
        public IEnumerator<ScoreboardEntry> GetEnumerator()
        {
            var enumerator = this.scoreEntries.GetEnumerator();
            enumerator.Dispose();
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Adds the item
        ///     PostCondition: this.scoreEntries.Count != @prev
        /// </summary>
        /// <param name="item">the item to add</param>
        public void Add(ScoreboardEntry item)
        {
            this.scoreEntries.Add(item);
        }

        /// <summary>
        ///     Clears all score entries
        ///     @PostCondition: this.scoreEntries.Count == 0
        /// </summary>
        public void Clear()
        {
            this.scoreEntries.Clear();
        }

        /// <summary>
        ///     Checks if the item is contained in the score entries
        /// </summary>
        /// <param name="item">the item to check for</param>
        /// <returns>true or false based on if the item is contained in the score entries</returns>
        public bool Contains(ScoreboardEntry item)
        {
            return this.scoreEntries.Contains(item);
        }

        /// <summary>
        ///     Copies the data of the score entries into an array
        /// </summary>
        /// <param name="array">the array to dump the data in</param>
        /// <param name="arrayIndex">the index to start dumping data at</param>
        public void CopyTo(ScoreboardEntry[] array, int arrayIndex)
        {
            this.scoreEntries.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the item from the score entries
        ///     @PostCondition: this.scoreEntries.Count != prev
        /// </summary>
        /// <param name="item">the item to remove</param>
        /// <returns>true or false based on if the item was removed</returns>
        public bool Remove(ScoreboardEntry item)
        {
            return this.scoreEntries.Remove(item);
        }

        /// <summary>
        ///     Gets the index of a certain item
        /// </summary>
        /// <param name="item">the item to get the index of</param>
        /// <returns>the index of the item in the score entries</returns>
        public int IndexOf(ScoreboardEntry item)
        {
            return this.scoreEntries.IndexOf(item);
        }

        /// <summary>
        ///     Inserts the item at the specified index
        ///     @PreCondition: this.scoreEntries.Count != @prev
        /// </summary>
        /// <param name="index">the index to put the item at</param>
        /// <param name="item">the item to insert</param>
        public void Insert(int index, ScoreboardEntry item)
        {
            this.scoreEntries.Insert(index, item);
        }

        /// <summary>
        ///     Removes the item at the specified index
        ///     PostCondition: this.scoreEntries.Count != @prev
        /// </summary>
        /// <param name="index">the index of the item you want to remove</param>
        public void RemoveAt(int index)
        {
            this.scoreEntries.RemoveAt(index);
        }

        /// <summary>
        ///     Checks if the score is in the top 10 of all scores
        /// </summary>
        /// <param name="score">the score to compare to</param>
        /// <returns>true or false based on if the score makes the top 10</returns>
        public bool IsScoreInTop10(int score)
        {
            return this.scoreEntries.Count < 10 || this.scoreEntries.Min(scoreEntry => scoreEntry.Score) < score;
        }

        #endregion
    }
}