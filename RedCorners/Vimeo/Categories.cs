using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get a list of the top level categories.
        /// </summary>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <returns></returns>
        public async Task<JSONNode> GetCategoriesAsync(int? page = null, int? per_page = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            return await RequestAsync("/categories", payload, "GET", true);
        }

        /// <summary>
        /// Get a category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>
        /// 404 Not Found: If the category cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetCategoryAsync(int categoryId)
        {
            return await RequestAsync(string.Format("/categories/{0}", categoryId), null, "GET", true);
        }

        /// <summary>
        /// Get a list of Channels related to a category.
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// videos
        /// followers
        /// </param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc
        /// </param>
        /// <returns>
        /// 404 Not Found: If the category cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetCategoryChannelsAsync(int categoryId,
            int? page = null, int? per_page = null, string query = null, string sort = null,
            string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/categories/{0}/channels", categoryId), payload, "GET", true);
        }

        /// <summary>
        /// Get a list of Groups related to a category.
        /// </summary>
        /// <param name="categoryId">The page number to show.</param>
        /// <param name="page">Number of items to show on each page. Max 50.</param>
        /// <param name="per_page">Search query.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// videos
        /// members
        /// </param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc
        /// </param>
        /// <returns>
        /// 404 Not Found: If the category cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetCategoryGroupsAsync(int categoryId,
            int? page = null, int? per_page = null, string query = null, string sort = null,
            string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/categories/{0}/groups", categoryId), payload, "GET", true);
        }

        /// <summary>
        /// Get a list of videos related to a category.
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="filter">Filter to apply to the results.</param>
        /// <param name="filter_embeddable">Required if filter=embeddable. Choose between only videos that are embeddable, and only videos that are not embeddable.</param>
        /// <param name="sort">Technique used to sort the results.
        /// relevant
        /// date
        /// alphabetical
        /// plays
        /// likes
        /// comments
        /// duration
        /// </param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc
        /// </param>
        /// <returns>
        /// 404 Not Found: If the category cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetCategoryVideosAsync(int categoryId,
            int? page = null, int? per_page = null, string query = null, string filter = null,
            bool? filter_embeddable = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (filter != null) payload["filter"] = filter;
            if (filter_embeddable != null) payload["filter_embeddable"] = filter_embeddable.Value.ToString().ToLower();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/categories/{0}/videos", categoryId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a category contains a video
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 404 Not Found: If the category cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetCategoryHasVideoAsync(int categoryId, int videoId)
        {
            return await RequestAsync(string.Format("/categories/{0}/videos/{1}", categoryId, videoId), null, "GET", true);
        }
    }
}