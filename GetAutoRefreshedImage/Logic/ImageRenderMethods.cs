﻿using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace GetAutoRefreshedImage.Logic
{
    public static class ImageRenderMethods
    {
        /// <summary>
        /// Draw image
        /// </summary>
        /// <param name="text">Text to Draw</param>
        /// <returns>Image Object</returns>
        private static Image DrawText(string text)
        {
            Font font = new Font("Arial", 12);
            Color txtColor = Color.Black;
            Color bckColor = Color.White;

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
            drawing.Clear(bckColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(txtColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        /// <summary>
        /// Create .gif Image
        /// </summary>
        /// <param name="eventDate">DateTime to be rendered</param>
        /// <returns>Gif Image</returns>
        //TODO: Return a MagickImageCollection object 
        public static MemoryStream CreateGIF(DateTime eventDate)
        {
            var _dateTimeNow = DateTime.Now;
            MagickImageCollection imageCollection = null;

            var hoursleft = (eventDate - _dateTimeNow);
            var text = hoursleft.ToString();
            if (hoursleft < TimeSpan.Zero)
            {
                text = "Your event is already outdated";
                imageCollection = FramesGeneration(imageCollection, text, 0);
            }
            else
            {
                int counter = 0;
                //set for 10 minutes
                var seconds = hoursleft.Seconds;
                if(seconds > 590)
                {
                    seconds = 590;
                }
                while (counter != seconds)
                {
                    imageCollection = FramesGeneration(imageCollection, hoursleft.ToString(@"hh\:mm\:ss"), counter);
                    eventDate = eventDate.AddSeconds(-1);
                    hoursleft = (eventDate - _dateTimeNow);
                    counter++;
                }
            }
            
            QuantizeSettings settings = new QuantizeSettings();
            settings.Colors = 256;
            imageCollection.Quantize(settings);

            return GetMemoryStreamResult(imageCollection);
        }


        /// <summary>
        /// Generates frames for .gif image
        /// </summary>
        /// <param name="imageCollection">Object for Stack Images</param>
        /// <param name="text">Text to be Draw</param>
        /// <param name="counter">Stack number</param>
        /// <returns></returns>
        public static MagickImageCollection FramesGeneration(MagickImageCollection imageCollection, string text, int counter)
        {
            Image image = null;
            ImageConverter converter = new ImageConverter();

            if (imageCollection == null)
            {
                imageCollection = new MagickImageCollection();
            }

            image = DrawText(text);
            byte[] buffer = (byte[])converter.ConvertTo(image, typeof(byte[]));
            var imageMagick = new MagickImage(buffer);

            imageCollection.Add(imageMagick);
            imageCollection[counter].AnimationDelay = 100;

            converter = null;
            
            return imageCollection;
        }

        [Obsolete]
        private static void GifTimer()
        {
            //await Task.Delay(100);
            Timer aTimer = new Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += new ElapsedEventHandler(T_Tick);
            aTimer.Enabled = true;

            //return imageCollection;
        }

        /// <summary>
        /// transform into a Memory Stream object
        /// </summary>
        /// <param name="imageGif">Object to be transformed</param>
        /// <returns>Memory Stream object</returns>
        public static MemoryStream GetMemoryStreamResult(MagickImageCollection imageGif)
        {
            MemoryStream ms = new MemoryStream();
            imageGif.Write(ms, MagickFormat.Gif);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        [Obsolete]
        static void T_Tick(object sender, EventArgs e)
        {
            //TimeSpan ts = _eventDate.Subtract(_dateTimeNow);
            //imageCollection = FramesGeneration(imageCollection, ts.ToString(), 0);
        }
    }
}