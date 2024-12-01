using System;
using System.Threading.Tasks;
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

        private ScoreReader scoreReader;
        private ScoreWriter scoreWriter;
        private StorageFile scoreFile;

        #endregion

        #region Methods

        private async Task createScoresFileAsync()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            this.scoreFile = await storageFolder.CreateFileAsync("Scores", CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        ///     Creates the file and reader and writes to the file
        ///     PostCondition: this.scoreFile != null, this.scoreReader != null, this.scoreWriter != null
        /// </summary>
        /// <returns></returns>
        public async Task CreateFileManagement()
        {
            await this.createScoresFileAsync();
            this.scoreReader = new ScoreReader(this.scoreFile.Path);
            this.scoreWriter = new ScoreWriter(this.scoreFile.Path);
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
        /// <summary>
        ///     Clears all scores in the score file
        ///     PostConditions: this.scoreFile == null
        /// </summary>
        public async void ClearAllScores()
        {
            await this.scoreFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }
        #endregion
    }
}