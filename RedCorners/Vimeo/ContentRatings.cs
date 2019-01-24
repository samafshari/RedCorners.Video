using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get all valid content ratings
        /// </summary>
        /// <returns></returns>
        public async Task<JSONNode> GetContentRatingsAsync()
        {
            return await RequestAsync("/contentratings", null, "GET", true);
        }
    }
}