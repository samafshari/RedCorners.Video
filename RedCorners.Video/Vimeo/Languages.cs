using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Video.Vimeo
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
        public async Task<JSONNode> GetLanguagesAsync(string filter = null)
        {
            var payload = new Dictionary<string, object>();
            if (filter != null) payload["filter"] = filter;
            return await RequestAsync("/languages", payload, "GET", true);
        }
    }
}