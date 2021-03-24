using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using AirLogicTT.Interface;

namespace AirLogicTT.BusinessLayer
{
    public class BLService : IBLService
    {
        public BLService()
        {

        }

        //Count number of words in lyrics
        public int CountWords(string text)
        {
            try
            {
                int wordCount = 0, index = 0;
                // skip whitespace until first word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;

                while (index < text.Length)
                {
                    // check if current char is part of a word
                    while (index < text.Length && !char.IsWhiteSpace(text[index]))
                        index++;

                    wordCount++;

                    // skip whitespace until next word
                    while (index < text.Length && char.IsWhiteSpace(text[index]))
                        index++;
                }
                return wordCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get a list of titles of songs
        public List<string> GetTitles(string strDetails)
        {
            try
            {
                List<string> songs = new List<string>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strDetails);
                XmlElement root = xmlDoc.DocumentElement;
                XmlNode nodes = root["release-list"];
                //loop through xml file
                foreach (XmlNode node in nodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name == "title")
                        {
                            songs.Add(childNode.InnerText);
                        }
                    }
                }
                return songs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Calculate average time from list of words per song
        public double CalcAverage(List<int> noOfWords)
        {
            try
            {
                double total = 0;
                double avg = 0;
                foreach (int count in noOfWords)
                {
                    total += count;
                }
                avg = Math.Round(total / noOfWords.Count, 2);
                return avg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //calculate the variance
        public double Variance(List<int> noOfWords, double avg)
        {
            try
            {
                if (noOfWords.Count > 1)
                {
                    double variance = 0.0;
                    foreach (int value in noOfWords)
                    {
                        // Math.Pow to calculate variance 
                        variance += Math.Pow(value - avg, 2.0);
                    }
                    return Math.Round(variance, 2);
                }
                else
                {
                    return 0.0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //calculate standard deviation
        public double StdDev(double var)
        {
            try
            {
                return Math.Round(Math.Sqrt(var), 2);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}