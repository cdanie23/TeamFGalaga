using System;
using System.IO;
using System.Xml.Serialization;
using Galaga.Model;

namespace Galaga.Datatier
{
    /// <summary>
    ///     The score reader class
    /// </summary>
    public class ScoreReader
    {
        #region Data members

        private readonly string fileName;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates an instance of the score reader
        ///     @PostCondition: this.fileName == fileName
        /// </summary>
        /// <param name="fileName">the name of the file</param>
        public ScoreReader(string fileName)
        {
            this.fileName = fileName;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Reads all the score entries of this.fileName;
        /// </summary>
        /// <returns>the score entries</returns>
        public ScoreEntries ReadScoreEntries()
        {
            ScoreEntries scoreEntries;
            var serializer = new XmlSerializer(typeof(ScoreEntries));
            var fileStream = File.OpenRead(this.fileName);
            using (fileStream)
            {
                try
                {
                    scoreEntries = (ScoreEntries)serializer.Deserialize(fileStream);
                }
                catch (Exception)
                {
                    scoreEntries = new ScoreEntries();
                }
            }

            fileStream.Dispose();
            return scoreEntries;
        }

        #endregion
    }
}