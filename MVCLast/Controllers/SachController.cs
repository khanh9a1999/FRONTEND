using MVCLast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCLast.Controllers
{
    public class SachController : Controller
    {
        private readonly string  link = "https://localhost:44374/";
        // GET: Sach
        [Route("index")]
        public ActionResult Index()
        {
            List<Sach> product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                var respondtask = client.GetAsync("api/Sach/all");
                respondtask.Wait();
                var rs = respondtask.Result;
                if (rs.IsSuccessStatusCode)
                {
                    var readtask = rs.Content.ReadAsAsync<List<Sach>>();
                    readtask.Wait();
                    product = readtask.Result;
                }
                else product = null;
            }
            return View(product);
        }

        // GET: Sach/Details/5
        [Route("detail/{id}")]
        public ActionResult Details(int id)
        {
            Sach product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);
                var respondtask = client.GetAsync("api/Sach/detail/" + id);
                respondtask.Wait();
                var rs = respondtask.Result;
                if (rs.IsSuccessStatusCode)
                {
                    var readtask = rs.Content.ReadAsAsync<Sach>();
                    readtask.Wait();
                    product = readtask.Result;
                }
                else product = null;
            }
            return View(product);
        }

        // GET: Sach/Create
        [Route("add")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sach/Create
        [Route("create")]
        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);

                //HTTP POST
                var postTask = client.PostAsJsonAsync("api/Sach/add", sach);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Có lỗi gì đó rồi bạn iu ơi ovO.");

            return View(sach);
        }

        // GET: Sach/Edit/5
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            var sach = Details(id);
            return View(sach);
        }

        // POST: Sach/Edit/5
        [Route("put")]
        [HttpPut]
        public ActionResult Edit( Sach sach)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Sach>("api/Sach/put/", sach);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Có lỗi gì đó rồi bạn iu ơi ovO.");
            return View(sach);
        }

       
        // POST: Sach/Delete/5
        [Route("delete/{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(link);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Sach/delete/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
