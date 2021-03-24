using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirLogicTT.Models;
using AirLogicTT.BusinessLayer;
using AirLogicTT.Interface;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;

namespace AirLogicTT.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> GetDataAsync(Artist model)
        {
            if (ModelState.IsValid)
            {
                var baseAddress = new Uri(ConfigurationManager.AppSettings["baseAddress"]);
                var baseAddress1 = new Uri(ConfigurationManager.AppSettings["baseAddress1"]);

                IBLService _blService;
                List<string> singles = new List<string>();
                List<int> noOfWords = new List<int>();
                List<Song> songs = new List<Song>();

                //Get all releases for selected group/artist
                _blService = BLServiceFactory.GetBLServiceObj();
                HttpClient httpClient1 = new HttpClient();
                httpClient1.BaseAddress = baseAddress1;
                httpClient1.DefaultRequestHeaders.Add("User-Agent", "C# App");
                httpClient1.Timeout = TimeSpan.FromSeconds(30);

                //Get songs for the selected artist
                singles = await GetSingles(model, _blService, singles, httpClient1);

                //call api to get the lyrics of each song
                await GetLyrics(model, baseAddress, _blService, singles, songs);

                noOfWords = songs.Select(x => x.WordCount).ToList();
                model.Avg = _blService.CalcAverage(noOfWords);
                model.MinWords = noOfWords.Min();
                model.MaxWords = noOfWords.Max();
                model.Range = model.MaxWords - model.MinWords;
                model.Var = _blService.Variance(noOfWords, model.Avg);
                model.StdDev = _blService.StdDev(model.Var);
                model.Songs = songs;
            }


            return View("GetData", model);
        }

        private static async Task GetLyrics(Artist model, Uri baseAddress, IBLService _blService, List<string> singles, List<Song> songs)
        {
            using (var httpClient = new HttpClient { BaseAddress = baseAddress, Timeout = TimeSpan.FromSeconds(20) })
            {
                foreach (var single in singles)
                {
                    //if statement is to avoid duplicates
                    if (songs.Where(x => x.Name == single).Count() == 0)
                    {
                        try
                        {
                            HttpResponseMessage response = await httpClient.GetAsync(model.ArtistName + "/" + single);
                            if (response.IsSuccessStatusCode)
                            {
                                int noWordsinSong;
                                string responseData = await response.Content.ReadAsStringAsync();
                                noWordsinSong = _blService.CountWords(responseData);
                                Song song = new Song();
                                song.Name = single;
                                song.WordCount = noWordsinSong;
                                songs.Add(song);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        private static async Task<List<string>> GetSingles(Artist model, IBLService _blService, List<string> singles, HttpClient httpClient1)
        {
            using (var response = await httpClient1.GetAsync("?query=artist:" + model.ArtistName))
            {
                var responseData1 = await response.Content.ReadAsStringAsync();
                //Add song titles to the list
                singles = _blService.GetTitles(responseData1);
            }

            return singles;
        }
    }
}