using System.Collections.Generic;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get all valid content ratings
        /// </summary>
        /// <returns></returns>
        public JSONNode GetContentRatings()
        {
            return Request("/contentratings", null, "GET", true);
        }
    }
}