using System;
using System.Linq;
using GetAutoRefreshedImage.Models;

namespace GetAutoRefreshedImage.Logic
{
    /// <summary>
    /// Actions to Execute into DB
    /// </summary>
    public class DBActions : IDisposable
    {
        private ContextDB _db = new ContextDB();

        public void AddDateTimeToDb(UrlsDatesMapping urlDatemapping)
        {
            if(urlDatemapping != null)
            {
                _db.UrlsDatesMapping.Add(urlDatemapping);
                _db.SaveChanges();
            }
        }
        public DateTime? GetDate(string idUrl)
        {
            DateTime? date = null;
            if(!string.IsNullOrEmpty(idUrl))
            {
                date = _db.UrlsDatesMapping.Where(x => x.IdUrl == idUrl).Select(x => x.EventDateTime).SingleOrDefault();
            }
            return date;
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }
}