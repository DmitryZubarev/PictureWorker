using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Reflection;

namespace PictureWorker.Models
{
    public class PictureEditor
    {
        public readonly string pathToSave;
        public readonly string pathToLoad;


        public PictureEditor()
        {
            pathToLoad = "C:\\projects\\test_tasks\\PictureWorker\\PictureWorker\\Content\\Pictures\\Downloads\\";
            if (!Directory.Exists(pathToLoad))
            {
                Directory.CreateDirectory(pathToLoad);
            }

            pathToSave = "C:\\projects\\test_tasks\\PictureWorker\\PictureWorker\\Content\\Pictures\\Edited\\";
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
        }
        

        private void AddPointToImg(Picture picture, Point point)
        {
            string coordinates = $"{point.X},{point.Y}";

            Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            SizeF size = Graphics.FromImage(picture.Bitmap).MeasureString(coordinates, font);
            Point location = new Point(0, 0);

            // Рисуем текст на картинке
            using (Graphics g = Graphics.FromImage(picture.Bitmap))
            {
                g.DrawString(coordinates, font, Brushes.Red, location);
            }
        }


        public List<Picture> CutPicture(string file, int horizontalPartsNumber, int verticalPartsNumber)
        {
            List<Picture> pictures = new List<Picture>();
            int hStep = 1600 / horizontalPartsNumber;
            int vStep = 1200 / verticalPartsNumber;


            string date = DateTime.Now.ToString(); //дата для добавления в обрезаное изображение
            Image baseImage = Image.FromFile(file); //основное изображение
            Bitmap baseBitmap = new Bitmap(baseImage, 1600, 1200); //основное изображение в Bitmap

            for (int i = 0; i < verticalPartsNumber; i++)
            {
                for (int j = 0; j < horizontalPartsNumber; j++)
                {
                    
                    Rectangle cropArea = new Rectangle(hStep*j, vStep*i, hStep, vStep); //выбираем часть основнрой картинки для обрезки
                    using (Bitmap target = new Bitmap(cropArea.Width, cropArea.Height))
                    {
                        //перерисовка части основного изображения
                        using (Graphics g = Graphics.FromImage(target))
                        {
                            g.DrawImage(baseBitmap, new Rectangle(0, 0, target.Width, target.Height),
                                cropArea,
                                GraphicsUnit.Pixel);
                        }


                        string name = "new_pic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                        Picture newPic = new Picture(target, name);
                        AddPointToImg(newPic, new Point(j * hStep, i * vStep));
                        newPic.Save(pathToSave);
                        pictures.Add(newPic);
                    }
                }
            }
            return pictures;
        }
    }
}