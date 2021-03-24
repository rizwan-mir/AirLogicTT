using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AirLogicTT.Interface;
using AirLogicTT.BusinessLayer;
using System.Collections.Generic;

namespace AriLogicTTTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountWords_Test()
        {
            //Arrange
            string text = "hello my name is test";
            int expected = 5;
            int actual;
            IBLService bLService = BLServiceFactory.GetBLServiceObj();

            //Act
            actual = bLService.CountWords(text);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTitle_Test()
        {
            //Arrange
            string strDetails = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><metadata><release-list><release><title>My Song</title></release></release-list></metadata>";
            List<string> expected = new List<string>();
            List<string> actual = new List<string>();
            IBLService bLService = BLServiceFactory.GetBLServiceObj();

            //Act
            expected.Add("My Song");
            actual = bLService.GetTitles(strDetails);

            //Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void CalcAverage_Test()
        {
            //Arrange
            List<int> noOfWords = new List<int>();
            double expected = 15;
            double actual;
            IBLService bLService = BLServiceFactory.GetBLServiceObj();

            //Act
            noOfWords.Add(10);
            noOfWords.Add(20);
            noOfWords.Add(15);
            actual = bLService.CalcAverage(noOfWords);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Variance_Test()
        {
            //Arrange
            List<int> noOfWords = new List<int>();
            double avg = 15;
            double expected = 50;
            double actual;
            IBLService bLService = BLServiceFactory.GetBLServiceObj();

            //Act
            noOfWords.Add(10);
            noOfWords.Add(15);
            noOfWords.Add(20);
            actual = bLService.Variance(noOfWords, avg);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StdDev_Test()
        {
            //Arrange
            double var = 50;
            double expected = 7.07;
            double actual;
            IBLService bLService = BLServiceFactory.GetBLServiceObj();

            //Act
            actual = bLService.StdDev(var);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
