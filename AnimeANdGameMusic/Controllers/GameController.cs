﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Net.Http;
using AnimeANdGameMusic.Models;
using AnimeANdGameMusic.Models.ViewModels;
using System.Web.Script.Serialization;

namespace AnimeANdGameMusic.Controllers
{
    public class GameController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static GameController()
        {
            // set up the base url address
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }

        // GET: Game/List
        public ActionResult List()
        {
            //communicate with BuyerData api to retrieve a list of Buyers
            //curl https://localhost:44340/api/GameData/ListGames


            string url = "GameData/ListGames";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(response.StatusCode);

            IEnumerable<GameDto> Games = response.Content.ReadAsAsync<IEnumerable<GameDto>>().Result;

            //Debug.WriteLine(games.Count());

            // return to the 'Games' view page
            return View(Games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            DetailsGame ViewModel = new DetailsGame();

            //communicate with Gamedata api to retrieve one specific game
            //curl https://localhost:44340/api/Gamedata/findGame/{id}

            string url = "Gamedata/findGame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            GameDto SelectedGame = response.Content.ReadAsAsync<GameDto>().Result;
            ViewModel.SelectedGame = SelectedGame;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            // error view page
            return View();
        }


        public ActionResult New()
        {
            //show a list of genres
            //GET api/Genredata/listGenre

            string url = "genredata/listgenres";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<GenreDto> GenreOptions = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;

            return View(GenreOptions);
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(Game Game)
        {
            //Debug.WriteLine("json payload:");
            //Debug.WriteLine(Game.GameName);
            //add a new game
            string url = "gamedata/addgame";

            // json
            string jsonpayload = jss.Serialize(Game);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateGame ViewModel = new UpdateGame();

            //the existing Game information
            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GameDto SelectedGame = response.Content.ReadAsAsync<GameDto>().Result;
            ViewModel.SelectedGame = SelectedGame;

            // all genres to choose from when updating this game
            // the existing game information
            url = "genredata/listgenres/";
            response = client.GetAsync(url).Result;
            IEnumerable<GenreDto> GenreOptions = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;

            ViewModel.GenreOptions = GenreOptions;

            return View(ViewModel);
        }

        // GET: Movie/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GameDto selectedGame = response.Content.ReadAsAsync<GameDto>().Result;
            return View(selectedGame);
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "gamedata/deletegame/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Game/Update/1
        /// <summary>
        /// Add upload game picture funtion
        /// </summary>
        /// Updated games information and redirect to the game List page
        /// User can update the game without uploading a picture 
        [HttpPost]
        public ActionResult Update(int id, Game Game, HttpPostedFileBase GamePic)
        {
            // upload Game pictures method
            // add a feature to uplaod image file to the server using POST request(in the Update function)

            string url = "gamedata/updategame/" + id;
            string jsonpayload = jss.Serialize(Game);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);


            //server response is OK, and we have Game picture data(file exists)
            if (response.IsSuccessStatusCode && GamePic != null)
            {
                //Seperate request for updating the Game picture (when user update Game without providing pictures) 
                Debug.WriteLine("Update picture");

                //set up picture url
                url = "GameData/UploadGamePic/" + id;

                MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                HttpContent imagecontent = new StreamContent(GamePic.InputStream);
                requestcontent.Add(imagecontent, "GamePic", GamePic.FileName);
                response = client.PostAsync(url, requestcontent).Result;

                return RedirectToAction("List");
            }
            else if (response.IsSuccessStatusCode)
            {
                //server response is OK, but no picture uploaded(upload picture is a seperate add-on feature)
                return RedirectToAction("List");
            }
            else
            {

                return RedirectToAction("Error");
            }
        }
    }
}