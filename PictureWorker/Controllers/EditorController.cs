using Microsoft.Ajax.Utilities;
using PictureWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading;
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
        public ActionResult CutPicture(Uri uri, int height, int width)
        {
            WebClient client = new WebClient();
            string imgName =  "new_pic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string savedImg = editor.pathToLoad + imgName;
            client.DownloadFile(uri, savedImg);
            List<Picture> pics = editor.CutPicture(savedImg, height, width);
            var src = from picture in pics select picture.Path;
            List<string> images = src.ToList();
            return PartialView(images);
        }
    }
}