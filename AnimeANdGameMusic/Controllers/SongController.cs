using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AnimeANdGameMusic.Models;

namespace AnimeANdGameMusic.Controllers
{
    public class SongController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static SongController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/songData/");
        }
        /// <summary>
        /// returns the view of data fetch by anima data controller
        /// </summary>
        /// <returns></returns>
        // GET: Song
        public ActionResult List()
        {
            //communicate with Songdata api and fetch the list of songs
            //curl: https://localhost:44306/api/songData/listsongs

            string url = "listsongs";
            HttpResponseMessage resp = client.GetAsync(url).Result;
            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(resp.StatusCode);

            IEnumerable<SongDto> songs = resp.Content.ReadAsAsync<IEnumerable<SongDto>>().Result;
            return View(songs);
        }

        // GET: Song/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null) // Handle the case where 'id' is null and throw an exception
            {
                return View("Error");
            }
            //Get a single song using the id 
            //curl: https://localhost:44353/api/songData/findsong/{}
            string url = "findsong/" + id;
            HttpResponseMessage resp = client.GetAsync(url).Result;
            if (resp.IsSuccessStatusCode)
            {
                SongDto selectedsong = resp.Content.ReadAsAsync<SongDto>().Result;

                //reruen the select song + id
                return View(selectedsong);
            }
            // Handle the case where the API response was not successful
            return View("Error");

        }

        //GET:

        // GET: Song/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Song/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Song/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Song/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Song/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Song/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
