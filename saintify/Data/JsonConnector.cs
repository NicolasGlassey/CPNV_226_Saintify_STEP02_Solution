using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using saintify.Business;//Business Namespace to get easily access to business classes
using Newtonsoft.Json;//To manage JSON Content
using System.IO;//To manager files

namespace saintify.Data
{
    /// <summary>
    /// This class is designed to manage JSON File
    /// </summary>
    public class JsonConnector
    {
        #region private attributes
        private String jsonFileName = "";
        private List<Artist> listOfArtists;
        #endregion private attributes

        #region constructors
        /// <summary>
        /// This constructor initializes a new instance of the artist class.
        /// </summary>
        /// <param name="jsonFileName">File Name containing the json string</param>
        public JsonConnector (String jsonFileName)
        {
            this.jsonFileName = jsonFileName;
            this.listOfArtists = ReadJsonFile();
        }
        #endregion constructors

        #region private methods
        /// <summary>
        /// This method is designed to extrat from file json content and convert it in artist object
        /// </summary>
        /// <returns>A list of artists extracted from Json File</returns>
        private List<Artist> ReadJsonFile()
        {
            StreamReader strReader = new StreamReader(jsonFileName);
            String jsonFileContent = "";
            List<Artist> listOfArtists = null;
            if (File.Exists(jsonFileName))
            {
                try
                {
                    jsonFileContent = strReader.ReadToEnd();
                    if (jsonFileContent != null)
                    {
                        try
                        {
                            listOfArtists = new List<Artist>();
                            listOfArtists = JsonConvert.DeserializeObject<List<Artist>>(jsonFileContent);
                        }
                        catch (JsonReaderException ex)
                        {
                            listOfArtists = null;
                        }
                    }
                }
                finally
                {
                    strReader.Close();
                }
            }
            return listOfArtists;
        }
        #endregion private methods

        #region public methods
        #region getters and setters
        /// <summary>
        /// This 
        /// </summary>
        /// <returns></returns>
        public List<Artist> ListOfArtists()
        {
            return this.listOfArtists; 
        }
        #endregion getters and setters
        #endregion public methods
    }
}
