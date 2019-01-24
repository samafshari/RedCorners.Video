using System.Collections.Generic;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get a Tag.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public JSONNode GetTag(string word)
        {
            return Request(string.Format("/tags/{0}", word), null, "GET", true);
        }

        /// <summary>
        /// Get a list of videos associated with a tag.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// created_time
        /// name
        /// duration</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public JSONNode GetVideosForTag(string word,
            int? page = null, int? per_page = null,
            string query = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return Request(string.Format("/tags/{0}/videos", word), payload, "GET", true);
        }
    }
}