using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using saintify.Business;

namespace saintify.Data
{
    [TestClass]
    public class UnitTestJsonConnector
    {
        #region private attributes
        private JsonConnector testJsonConnector = null;
        private List<Artist> listOfArtists = null;
        private String testJsonFileName = "testJson.json";
        private StreamWriter strWriter = null;
        #endregion private attributes

        /// <summary>
        /// This test method initializes variables and objects needed for the next test session.
        /// Run before each test method.
        /// </summary>
        [TestInitialize]
        public void Init()
        {

        }

        /// <summary>
        /// This test method is designed to test the json connector when the json file
        /// to read is correctly filled.
        /// </summary>
        [TestMethod]
        public void TestMethodListOfArtistsSucessAmountOfArtistsObject()
        {
            int expectedAmountOfArtists = 2;
            int actualAmountOfArtists = -1;

            //given
            this.strWriter = new StreamWriter(this.testJsonFileName);
            strWriter.Write("[{\"pictureName\":\"Pic1.png\",\"name\":\"Artiste1\",\"listOfSongs\":[{\"title\":\"SongA1\",\"duration\":1},{\"title\":\"SongA2\",\"duration\":2}]},{\"pictureName\":\"Pic2.png\",\"name\":\"Artiste1\",\"listOfSongs\":[{\"title\":\"SongB1\",\"duration\":3},{\"title\":\"SongB2\",\"duration\":4}]}]");
            strWriter.Close();
            this.testJsonConnector = new JsonConnector(this.testJsonFileName);

            //then
            this.listOfArtists = this.testJsonConnector.ListOfArtists();
            actualAmountOfArtists = this.listOfArtists.Count;

            //when
            Assert.AreEqual(expectedAmountOfArtists, actualAmountOfArtists);
        }

        /// <summary>
        /// This test method is designed to test the json connector when the json file
        /// to read is correctly filled.
        /// </summary>
        [TestMethod]
        public void TestMethodListOfArtistsSucessAmountOfSongsObject()
        {
            int expectedAmountOfSongs = 3;
            int actualAmountOfSongs = -1;

            //given
            this.strWriter = new StreamWriter(this.testJsonFileName);
            strWriter.Write("[{\"pictureName\":\"Pic1.png\",\"name\":\"Artiste1\",\"listOfSongs\":[{\"title\":\"SongA1\",\"duration\":1},{\"title\":\"SongA2\",\"duration\":2}]},{\"pictureName\":\"Pic2.png\",\"name\":\"Artiste1\",\"listOfSongs\":[{\"title\":\"SongB1\",\"duration\":3},{\"title\":\"SongB2\",\"duration\":4}]}]");
            strWriter.Close();
            this.testJsonConnector = new JsonConnector(this.testJsonFileName);
            this.listOfArtists = this.testJsonConnector.ListOfArtists();

            //then
            foreach (Artist artist in this.listOfArtists)
            {
                foreach (Song listOfSongs in artist.ListOfSongs())
                {
                    actualAmountOfSongs++;
                }
            }

            //when
            Assert.AreEqual(expectedAmountOfSongs, actualAmountOfSongs);
        }

        /// <summary>
        /// This test method is designed to test the json connector when the json file
        /// to read is UNcorrectly filled.
        /// </summary>
        [TestMethod]
        public void TestMethodListOfArtistsFailedDueToFileErrorContaint()
        {
            //given
            this.strWriter = new StreamWriter(this.testJsonFileName);
            strWriter.Write("This is not json content");
            strWriter.Close();
            this.testJsonConnector = new JsonConnector(this.testJsonFileName);

            //then
            this.listOfArtists = this.testJsonConnector.ListOfArtists();

            //when
            Assert.IsNull(this.listOfArtists);
        }

        /// <summary>
        /// This test method cleanup variables and objects used for the last test session.
        /// Run after each test method.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            this.testJsonConnector = null;
            if (File.Exists(this.testJsonFileName))
            {
                File.Delete(this.testJsonFileName);
            }
        }
    }
}
