using System.Collections.ObjectModel;
using Galaga.Datatier;
using Galaga.Extensions;
using Galaga.Model;

namespace Galaga.ViewModel
{
    /// <summary>
    ///     The score board view model
    /// </summary>
    public class ScoreboardViewModel
    {
        #region Data members

        private readonly ScoresFileManager scoresFileManager;
        private readonly ScoreEntries scoreEntries;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the names
        /// </summary>
        public ObservableCollection<string> Names { get; set; }

        /// <summary>
        ///     Gets or sets the scores
        /// </summary>
        public ObservableCollection<string> Scores { get; set; }

        /// <summary>
        ///     Gets or sets the levels
        /// </summary>
        public ObservableCollection<string> Levels { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Makes an instance of the scoreboard view model
        ///     PostConditions: this.scoreFileManager != null, this.scoreEntries != @prev, this.Names != null,
        ///     this.Scores != null, this.Levels != null
        /// </summary>
        public ScoreboardViewModel()
        {
            this.scoresFileManager = new ScoresFileManager();

            this.scoreEntries = this.scoresFileManager.ReadScoreEntries();

            this.scoresFileManager.WriteScores(this.scoreEntries);

            this.Names = this.scoreEntries.Names.ToObservableCollection();
            this.Scores = this.scoreEntries.Scores.ToObservableCollection();
            this.Levels = this.scoreEntries.Levels.ToObservableCollection();
        }

        #endregion
    }
}