using System.Collections.Generic;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get all valid creative commons licenses
        /// </summary>
        /// <returns></returns>
        public JSONNode GetCreativeCommons()
        {
            return Request("/creativecommons", null, "GET", true);
        }
    }
}