using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PictureWorker.Models
{
    public class Picture
    {   
        public string Name { get; set; }
        public Bitmap Bitmap { get; set; }
        public string Path { get; set; }


        public Picture(Bitmap bm, string name) 
        {
            Bitmap = bm;
            Name = name;
        }


        public bool Save(string savePath)
        {
            try
            {
                Bitmap.Save(savePath + Name, ImageFormat.Png);
                Path = savePath + Name;
                return true;
            }
            catch 
            { 
                return false;
            }
        }
    }
}