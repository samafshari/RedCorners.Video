using System.Collections.Generic;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get all valid video languages
        /// </summary>
        /// <param name="filter">Filter to apply to the results.
        /// texttracks
        /// </param>
        /// <returns></returns>
        public JSONNode GetLanguages(string filter = null)
        {
            var payload = new Dictionary<string, object>();
            if (filter != null) payload["filter"] = filter;
            return Request("/languages", payload, "GET", true);
        }
    }
}