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
        private ScoreEntries scoreEntries;

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
            this.LoadData();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Loads in all the data from the file containing all the scores
        /// </summary>
        public async void LoadData()
        {
            await this.scoresFileManager.CreateFileManagement();
            this.scoreEntries = this.scoresFileManager.ReadScoreEntries();
            this.SortByScore();
            this.scoresFileManager.WriteScores(this.scoreEntries);

            this.Names = this.scoreEntries.Names.ToObservableCollection();
            this.Scores = this.scoreEntries.Scores.ToObservableCollection();
            this.Levels = this.scoreEntries.Levels.ToObservableCollection();
        }

        /// <summary>
        ///     Sorts the entries by score
        /// </summary>
        public void SortByScore()
        {
            this.scoreEntries.SortByScoresDescending();
        }

        /// <summary>
        ///     Sorts the entries by name
        /// </summary>
        public void SortByNameDescending()
        {
            this.scoreEntries.SortByNameDescending();
        }

        /// <summary>
        ///     Sorts the entries by level
        /// </summary>
        public void SortByLevelDescending()
        {
            this.scoreEntries.SortByLevelDescending();
        }

        #endregion
    }
}