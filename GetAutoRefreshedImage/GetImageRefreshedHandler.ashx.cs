using GetAutoRefreshedImage.Logic;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace GetAutoRefreshedImage
{
    /// <summary>
    /// Create a .gif image counter 
    /// </summary>
    public class GetImageRefreshedHandler : IHttpHandler
    {
        public RequestContext RequestContext { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            MemoryStream image = null;

            var imageId = (string)RequestContext.RouteData.Values["imageID"];
            var hasContent = imageId == null ? false : true;

            if (hasContent)
            {
                var dbAction = new DBActions();
                var eventDate = dbAction.GetDate(imageId);
                if (eventDate != null)
                {
                    //DateTime TestDateTime = new DateTime(2016, 03, 15, 11, 55, 00);
                    image = ImageRenderMethods.CreateGIF((DateTime)eventDate);
                }

                byte[] buffer = image.ToArray();
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