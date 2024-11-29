using System;
using Windows.Storage;
using Galaga.Model;

namespace Galaga.Datatier
{
    /// <summary>
    ///     Manages the file which contains the scores
    /// </summary>
    public class ScoresFileManager
    {
        #region Data members

        private readonly ScoreReader scoreReader;
        private readonly ScoreWriter scoreWriter;
        private StorageFile scoreFile;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the scores file manager class
        ///     PostCondition: this.scoreFile != null, this.scoreReader != null, this.scoreWriter != null
        /// </summary>
        public ScoresFileManager()
        {
            this.createScoresFile();
            this.scoreReader = new ScoreReader(this.scoreFile.Path);
            this.scoreWriter = new ScoreWriter(this.scoreFile.Path);
        }

        #endregion

        #region Methods

        private async void createScoresFile()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            this.scoreFile = await storageFolder.CreateFileAsync("Scores", CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        ///     Reads all the score entries from this.scoreFile
        /// </summary>
        /// <returns>The score entries</returns>
        public ScoreEntries ReadScoreEntries()
        {
            return this.scoreReader.ReadScoreEntries();
        }

        /// <summary>
        ///     Writes all the scores into this.scoreFile
        ///     @PostCondition: this.scoreFile != @prev
        /// </summary>
        /// <param name="scoreEntries">the score entries to write into the file</param>
        public void WriteScores(ScoreEntries scoreEntries)
        {
            this.scoreWriter.WriteScore(scoreEntries);
        }

        #endregion
    }
}