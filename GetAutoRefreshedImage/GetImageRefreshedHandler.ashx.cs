using GetAutoRefreshedImage.Logic;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace GetAutoRefreshedImage
{
    /// <summary>
    /// Summary description for GetImageRefreshedHandler
    /// </summary>
    public class GetImageRefreshedHandler : IHttpHandler
    {
        public RequestContext RequestContext { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            MagickImageCollection image = null;//TODO: Create method to retrieve image

            var imageId = (string)RequestContext.RouteData.Values["imageID"];
            var hasContent = imageId == null ? false : true;

            if (hasContent)
            {
                var dbAction = new DBActions();
                var eventDate = dbAction.GetDate(imageId);
                //ImageConverter converter = new ImageConverter();
                if (eventDate != null)
                {
                    //DateTime TestDateTime = new DateTime(2016, 03, 10, 11, 55, 00);
                    var imageRender = new ImageRenderMethods();
                    image = imageRender.CreateGIF(TestDateTime);
                }
                //var path = RequestContext.HttpContext.Server.MapPath("/Content/Images");
                //path += "\\test.gif";
                //image.Write(path);
                var imageGif = ImageRenderMethods.GetMemoryStreamResult(image);
                byte[] buffer = imageGif.ToArray();
                context.Response.ContentType = "image/gif";
                context.Response.BinaryWrite(buffer);
                context.Response.Flush();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }        
    }
    public class RouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new GetImageRefreshedHandler() { RequestContext = requestContext };
        }
    }
}