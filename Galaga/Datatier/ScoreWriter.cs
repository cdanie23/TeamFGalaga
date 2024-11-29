using System.IO;
using System.Xml.Serialization;
using Galaga.Model;

namespace Galaga.Datatier
{
    /// <summary>
    ///     The score writer class
    /// </summary>
    public class ScoreWriter
    {
        #region Data members

        private readonly string fileName;

        #endregion

        #region Constructors

        /// <summary>
        ///     Makes an instance
        ///     PostConditions: this.fileName == fileName
        /// </summary>
        /// <param name="fileName">the name of the file</param>
        public ScoreWriter(string fileName)
        {
            this.fileName = fileName;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Writes the scores out to  this.Filename
        ///     PostCondition: this.FileName.Content != @prev
        /// </summary>
        /// <param name="scoreEntries">the list of scores you want to write out</param>
        public void WriteScore(ScoreEntries scoreEntries)
        {
            var serializer = new XmlSerializer(typeof(ScoreEntries));
            var fileOutStreamWriter = File.OpenWrite(this.fileName);
            using (fileOutStreamWriter)
            {
                serializer.Serialize(fileOutStreamWriter, scoreEntries);
            }

            fileOutStreamWriter.Dispose();
        }

        #endregion
    }
}