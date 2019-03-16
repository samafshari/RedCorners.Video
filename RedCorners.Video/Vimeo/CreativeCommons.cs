using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Video.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get all valid creative commons licenses
        /// </summary>
        /// <returns></returns>
        public async Task<JSONNode> GetCreativeCommonsAsync()
        {
            return await RequestAsync("/creativecommons", null, "GET", true);
        }
    }
}