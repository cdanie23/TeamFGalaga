using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Galaga.Datatier;
using Galaga.Extensions;
using Galaga.Model;

namespace Galaga.ViewModel
{
    /// <summary>
    ///     The score board view model
    /// </summary>
    public class ScoreboardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private readonly ScoresFileManager scoresFileManager;
        private ScoreEntries scoreEntries;

        private ObservableCollection<string> names;

        private ObservableCollection<string> scores;

        private ObservableCollection<string> levels;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the names
        /// </summary>
        public ObservableCollection<string> Names
        {
            get => this.names;
            set
            {
                this.names = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the scores
        /// </summary>
        public ObservableCollection<string> Scores
        {
            get => this.scores;
            set
            {
                this.scores = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the levels
        /// </summary>
        public ObservableCollection<string> Levels
        {
            get => this.levels;
            set
            {
                this.levels = value;
                this.OnPropertyChanged();
            }
        }

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
        ///     The property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Loads in all the data from the file containing all the scores
        /// </summary>
        public async void LoadData()
        {
            await this.scoresFileManager.CreateFileManagement();
            this.scoreEntries = this.scoresFileManager.ReadScoreEntries();
            this.SortByScore();
            this.scoresFileManager.WriteScores(this.scoreEntries);
        }

        /// <summary>
        ///     Sorts the entries by score
        /// </summary>
        public void SortByScore()
        {
            this.scoreEntries.SortByScoresDescending();
            this.updateEntries();
        }

        private void updateEntries()
        {
            this.Levels = this.scoreEntries.Levels.ToObservableCollection();
            this.Names = this.scoreEntries.Names.ToObservableCollection();
            this.Scores = this.scoreEntries.Scores.ToObservableCollection();
        }

        /// <summary>
        ///     Sorts the entries by name
        /// </summary>
        public void SortByNameDescending()
        {
            this.scoreEntries.SortByNameDescending();
            this.updateEntries();
        }

        /// <summary>
        ///     Sorts the entries by level
        /// </summary>
        public void SortByLevelDescending()
        {
            this.scoreEntries.SortByLevelDescending();
            this.updateEntries();
        }

        /// <summary>
        ///     Invokes the property changed event when the property is changed
        /// </summary>
        /// <param name="propertyName">the name of the property</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}