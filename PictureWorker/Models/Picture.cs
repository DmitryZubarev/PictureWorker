using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace PictureWorker.Models
{
    public class Picture
    {
        private readonly string savePath = "C://EditedPics//";


        public string Name { get; set; }
        public Bitmap Bitmap { get; set; }


        public Picture(Bitmap bm, int number) 
        {
            Name = "IMG_" + DateTime.Now.ToString();
            Bitmap = bm;
        }


        public bool Save()
        {
            try
            {
                Bitmap.Save(savePath + this.Name, ImageFormat.Png);
                return true;
            }
            catch 
            { 
                return false;
            }
        }
    }
}