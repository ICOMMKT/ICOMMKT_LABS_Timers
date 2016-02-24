using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GetAutoRefreshedImage.Logic
{
    public static class ImageRenderMethods
    {
        public static Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        //public static MagickImageCollection CreateGif(MagickImageCollection collection)
        //{
        //    //TODO: Dispose this Object Later
        //    //MagickImageCollection images = new MagickImageCollection();

        //    // Add first image and set the animation delay to 100ms
        //    collection.Add("Snakeware.png");
        //    collection[0].AnimationDelay = 100;

        //    // Add second image, set the animation delay to 100ms and flip the image
        //    collection.Add("Snakeware.png");
        //    collection[1].AnimationDelay = 100;
        //    collection[1].Flip();

        //    // Optionally reduce colors
        //    QuantizeSettings settings = new QuantizeSettings();
        //    settings.Colors = 256;
        //    collection.Quantize(settings);

        //    // Optionally optimize the images (images should have the same size).
        //    collection.Optimize();

        //    // Save gif
        //    collection.Write("Snakeware.Animated.gif");

        //    return collection;
        //}

        /// <summary>
        /// Create .gif Image
        /// </summary>
        /// <param name="eventDate"></param>
        /// <returns>Gif Image</returns>
        //TODO: Return a MagickImageCollection object 
        public static Image CreateGIF(DateTime eventDate)
        {
            //MagickImageCollection imageCollection = null;
            Image image = null;
            Font font = new Font("Arial", 12);
            Color txtColor = Color.Black;
            Color bckColor = Color.White;

            var hoursleft = ((DateTime)eventDate - DateTime.Now);
            var text = hoursleft.ToString();
            if(hoursleft < TimeSpan.Zero)
            {
                text = "Your event is already outdated";
                image = DrawText(text, font, txtColor, bckColor);
            }
            else
            {
                while(eventDate != DateTime.Now)
                {
                    image = DrawText(text, font, txtColor, bckColor);
                    var imageMagick = new MagickImage(image,);
                    //imageCollection.Add();
                }
                
            }

            return image;
        }
    }
}