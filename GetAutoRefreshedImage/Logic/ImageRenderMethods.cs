using ImageMagick;
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
    public class ImageRenderMethods
    {
        DateTime _eventDate, _dateTimeNow;
        //MagickImageCollection imageCollection = null;

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
        /// <param name="eventDate"></param>
        /// <returns>Gif Image</returns>
        //TODO: Return a MagickImageCollection object 
        public MagickImageCollection CreateGIF(DateTime eventDate)
        {
            _dateTimeNow = DateTime.Now;
            _eventDate = eventDate;
            MagickImageCollection imageCollection = null;

            var hoursleft = (_eventDate - _dateTimeNow);
            var text = hoursleft.ToString();
            if (hoursleft < TimeSpan.Zero)
            {
                //imageCollection = new MagickImageCollection();
                text = "Your event is already outdated";
                //imageProperties.Text = text;

                imageCollection = FramesGeneration(imageCollection, text, 0);
            }
            else
            {
                int counter = 0;

                while (counter != 295)
                {
                    //Task timeTask = Task.Run(() =>
                    //{
                    //await Task.Delay(2000);
                    //do stuff here
                    //    GifTimer();
                    //});
                    //timeTask.RunSynchronously();
                    //timeTask.Wait();
                    imageCollection = FramesGeneration(imageCollection, hoursleft.ToString(), counter);
                    eventDate = eventDate.AddSeconds(-1);
                    hoursleft = (eventDate - _dateTimeNow);
                    counter++;
                }
            }
            
            QuantizeSettings settings = new QuantizeSettings();
            settings.Colors = 256;
            imageCollection.Quantize(settings);

            //imageCollection.Optimize();

            return imageCollection;
        }

        

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

        private void GifTimer()
        {
            //await Task.Delay(100);
            Timer aTimer = new Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += new ElapsedEventHandler(T_Tick);
            aTimer.Enabled = true;

            //return imageCollection;
        }

        public static MemoryStream GetMemoryStreamResult(MagickImageCollection imageGif)
        {
            MemoryStream ms = new MemoryStream();
            imageGif.Write(ms, MagickFormat.Gif);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        void T_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = _eventDate.Subtract(_dateTimeNow);
            //imageCollection = FramesGeneration(imageCollection, ts.ToString(), 0);
        }
    }
}