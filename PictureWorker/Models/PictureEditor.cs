using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace PictureWorker.Models
{
    public class PictureEditor
    {
        private Bitmap CropPicture(Bitmap baseBitmap, Rectangle cropArea)
        {
            using (Bitmap target = new Bitmap(cropArea.Width, cropArea.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(baseBitmap, new Rectangle(0, 0, target.Width, target.Height),
                        cropArea,
                        GraphicsUnit.Pixel);
                }
                return target;
            }
        }

        public List<string> CutPicture(string file, int horizontalPartsNumber, int verticalPartsNumber)
        {
            //Image baseImage = Image.FromFile(file);
            //Bitmap baseBitmap = new Bitmap(baseImage, 1600, 1200);
            List<string> pictures = new List<string>();
            int hStep = 1600 / horizontalPartsNumber;
            int vStep = 1200 / verticalPartsNumber;

            for (int i = 0; i < verticalPartsNumber; i++)
            {
                for (int j = 0; j < horizontalPartsNumber; j++)
                {
                    Image baseImage = Image.FromFile(file);
                    Bitmap baseBitmap = new Bitmap(baseImage, 1600, 1200);
                    Rectangle cropArea = new Rectangle(hStep*j, vStep*i, hStep, vStep);
                    //Bitmap target = CropPicture(baseBitmap, cropArea);
                    using (Bitmap target = new Bitmap(cropArea.Width, cropArea.Height))
                    {
                        using (Graphics g = Graphics.FromImage(target))
                        {
                            g.DrawImage(baseBitmap, new Rectangle(0, 0, target.Width, target.Height),
                                cropArea,
                                GraphicsUnit.Pixel);
                        }
                        string toSave = @"C:\EditedPics\new_pic_" + i.ToString() + j.ToString() + ".png"; // + ".png"
                        target.Save(toSave, ImageFormat.Png); //, ImageFormat.Png
                        pictures.Add(toSave);
                        //string toSave = @"C:\EditedPics\new_pic_" + i.ToString() + j.ToString() + ".png"; // + ".png"
                        //target.Save(toSave, ImageFormat.Png); //, ImageFormat.Png
                        //pictures.Add(toSave);
                    }
                }
            }
            return pictures;
        }
    }
}