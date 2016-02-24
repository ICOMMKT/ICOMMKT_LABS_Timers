using GetAutoRefreshedImage.Logic;
using GetAutoRefreshedImage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GetAutoRefreshedImage
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SaveDateTime(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txt_Date.Text))
            {
                var idUrl = Guid.NewGuid().ToString();
                idUrl = Regex.Replace(idUrl, "-", string.Empty, RegexOptions.IgnoreCase);

                var urlDateMapping = new UrlsDatesMapping
                {
                    IdUrl = idUrl,
                    EventDateTime = DateTime.Parse(txt_Date.Text)
                };

                DBActions dbAction = new DBActions();
                dbAction.AddDateTimeToDb(urlDateMapping);

                var currentUrl = HttpContext.Current.Request.Url;
                string urlGenerated = currentUrl.Scheme + "://"+ currentUrl.Authority + "/" + idUrl;
                UrlGenerated.Text = urlGenerated;
                ModalPanel.Visible = true;

            }
        }
    }
}