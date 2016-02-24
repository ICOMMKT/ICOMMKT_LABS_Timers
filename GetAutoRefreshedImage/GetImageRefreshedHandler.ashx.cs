using GetAutoRefreshedImage.Logic;
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
            Image image = null;//TODO: Create method to retrieve image

            var imageId = (string)RequestContext.RouteData.Values["imageID"];
            var hasContent = imageId == null ? false : true;
            if (hasContent)
            {
                var dbAction = new DBActions();
                var eventDate = dbAction.GetDate(imageId);
                if (eventDate != null)
                {
                    image = ImageRenderMethods.CreateGIF((DateTime)eventDate);
                }
                ImageConverter converter = new ImageConverter();
                byte[] buffer = (byte[])converter.ConvertTo(image, typeof(byte[]));
                context.Response.ContentType = "image/jpg";
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