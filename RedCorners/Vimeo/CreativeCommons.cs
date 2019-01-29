using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Vimeo
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