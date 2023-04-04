using PictureWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PictureWorker.Controllers
{
    public class EditorController : Controller
    {
        private PictureEditor editor = new PictureEditor();

        // GET: Editor
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetParams()
        {
            return View();
        }

        [HttpPost]
        public int CutPicture(Uri uri, int height, int width)
        {
            
            //try
            //{
            WebClient client = new WebClient();
            client.DownloadFile(uri, "C://DownloadedPics//new_pic.jpg");
            List<string> pics = editor.CutPicture("C://DownloadedPics//new_pic.jpg", height, width);
            return pics.Count;

        }
    }
}