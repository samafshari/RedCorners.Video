using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get a list of all Groups.
        /// </summary>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// videos
        /// followers</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <param name="filter">Filter to apply to the results.
        /// featured</param>
        /// <returns>200 OK</returns>
        public async Task<JSONNode> GetGroupsAsync(int? page = null, int? per_page = null, 
            string query = null, string sort = null,
            string direction = null, string filter = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            if (filter != null) payload["filter"] = filter;
            return await RequestAsync("/groups", payload, "GET", true);
        }

        /// <summary>
        /// Create a new Group.
        /// </summary>
        /// <param name="name" required="true">The name of the new Channel</param>
        /// <param name="description" required="true">The description of the new Channel</param>
        /// <returns>
        /// 403: If the authenticated user is not allowed to create Groups
        /// 400: If one of the parameters is invalid
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> CreateGroupsAsync(string name, string description)
        {
            var payload = new Dictionary<string, object>();
            payload["name"] = name;
            payload["description"] = description;
            return await RequestAsync("/groups", payload, "POST", true);
        }

        /// <summary>
        /// Get a Group.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>200 OK</returns>
        public async Task<JSONNode> GetGroupAsync(int groupId)
        {
            return await RequestAsync(string.Format("/groups/{0}", groupId), null, "GET", true);
        }

        /// <summary>
        /// Delete a Group.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>
        /// 403: If the authenticated user is not the Group owner
        /// 204 No Content
        /// </returns>
        public async Task<JSONNode> DeleteGroupAsync(int groupId)
        {
            return await RequestAsync(string.Format("/groups/{0}", groupId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of users that joined a Group.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <param name="filter">Filter to apply to the results.
        /// moderators</param>
        /// <returns>
        /// 404 Not Found: If the Group cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetGroupUsersAsync(int groupId, int? page = null, int? per_page = null,
            string query = null, string sort = null,
            string direction = null, string filter = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            if (filter != null) payload["filter"] = filter;
            return await RequestAsync(string.Format("/groups/{0}/users", groupId), null, "GET", true);
        }

        /// <summary>
        /// Get a list of videos in a Group.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="filter">Filter to apply to the results.
        /// embeddable</param>
        /// <param name="filter_embeddable">Required if filter=embeddable. Choose between only videos that are embeddable, and only videos that are not embeddable.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// plays
        /// likes
        /// comments
        /// duration</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns>
        /// 400 Not Found: If the Group cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetGroupVideosAsync(int groupId, int? page = null, int? per_page = null,
            string query = null, string filter = null,
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
            return await RequestAsync(string.Format("/groups/{0}/videos", groupId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a Group has a video.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 404 Not Found: If the Group cannot be found
        /// 404 Not Found: If the video cannot be found in the Group
        /// 204 No Content
        /// </returns>
        public async Task<JSONNode> GetGroupHasVideoAsync(int groupId, int videoId)
        {
            return await RequestAsync(string.Format("/groups/{0}/videos/{1}", groupId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to a Group.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 403: If the authenticated user is not allowed to add videos to the Group
        /// 403: If the video is not addable to Groups
        /// 403: If the video is already in the Group
        /// 202 Accepted: If the video is pending addition to the Group
        /// 204 No Content: If the video was successfully added to the Group
        /// </returns>
        public async Task<JSONNode> AddVideoToGroupAsync(int groupId, int videoId)
        {
            return await RequestAsync(string.Format("/groups/{0}/videos/{1}", groupId, videoId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a video from a Group.
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 403: If the authenticated user is not allowed to modify this Group's videos
        /// 403: If the video is not allowed to be added to Groups
        /// 204 No Content
        /// </returns>
        public async Task<JSONNode> DeleteVideoFromGroupAsync(int groupId, int videoId)
        {
            return await RequestAsync(string.Format("/groups/{0}/videos/{1}", groupId, videoId), null, "DELETE", true);
        }
    }
}