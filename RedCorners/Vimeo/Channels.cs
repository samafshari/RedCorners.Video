using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get a list of all Channels.
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
        /// <returns>
        /// 404: If relevant sort is provided without a search query
        /// 200: OK
        /// </returns>
        public async Task<JSONNode> GetChannelsAsync(int? page = null, int? per_page = null,
            string query = null, string sort = null, string direction = null, string filter = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            if (filter != null) payload["filter"] = filter;
            return await RequestAsync("/channels", payload, "GET", true);
        }

        /// <summary>
        /// Create a new Channel.
        /// </summary>
        /// <param name="name" required="true">The name of the new Channel</param>
        /// <param name="description" required="true">The description of the new Channel</param>
        /// <param name="privacy" required="true">The privacy level of the new Channel</param>
        /// <returns>
        /// 400: If an invalid parameter is supplied
        /// 403: If the authenticated user can not create a Channel
        /// 201: Created
        /// </returns>
        public async Task<JSONNode> CreateChannelAsync(string name, string description, string privacy)
        {
            var payload = new Dictionary<string, object>();
            payload["name"] = name;
            payload["description"] = description;
            payload["privacy"] = privacy;
            return await RequestAsync("/channels", payload, "POST", true);
        }

        /// <summary>
        /// Get a Channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>200 OK</returns>
        public async Task<JSONNode> GetChannelAsync(int channelId)
        {
            return await RequestAsync(string.Format("/channels/{0}", channelId), null, "GET", true);
        }

        /// <summary>
        /// Edit a Channel's information
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <param name="name">The Channel's new name</param>
        /// <param name="description">The Channel's new description</param>
        /// <param name="privacy">The Channel's new privacy level</param>
        /// <returns>
        /// 400: If an invalid parameter is supplied
        /// 204: No Content
        /// </returns>
        public async Task<JSONNode> EditChannelAsync(int channelId, string name, string description, string privacy)
        {
            var payload = new Dictionary<string, object>();
            if (name != null) payload["name"] = name;
            if (description != null) payload["description"] = description;
            if (privacy != null) payload["privacy"] = privacy;
            return await RequestAsync(string.Format("/channels/{0}", channelId), payload, "POST", true);
        }

        /// <summary>
        /// Delete a Channel
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <returns>
        /// 403: If this user is not Channel owner
        /// 204: No Content
        /// </returns>
        public async Task<JSONNode> DeleteChannelAsync(int channelId)
        {
            return await RequestAsync(string.Format("/channels/{0}", channelId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of users who follow a Channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <param name="filter">Filter to apply to the results.</param>
        /// <returns>
        /// 404 Not found: If the Channel cannot be found
        /// 400: If a search query is provided without the `moderators` filter. This feature is not yet supported.
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetChannelUsersAsync(int channelId, int? page = null, int? per_page = null,
            string query = null, string sort = null, string direction = null, string filter = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            if (filter != null) payload["filter"] = filter;
            return await RequestAsync(string.Format("/channels/{0}/users", channelId), payload, "GET", true);
        }

        /// <summary>
        /// Get a list of videos in a Channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
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
        /// duration
        /// added
        /// modified_time
        /// </param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns>
        /// 404 Not Found: If the Channel cannot be found
        /// 400: If a sort direction is provided along with the "default" sort
        /// 304 Not Modified: If no videos were added to this Channel since the provided If-Modified-Since header
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetChannelVideosAsync(int channelId, int? page = null, int? per_page = null, 
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
            return await RequestAsync(string.Format("/channels/{0}/videos", channelId), payload, "GET", true);
        }

        /// <summary>
        /// Check if this Channel contains a video.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 404 Not Found: If the Channel cannot be found
        /// 200 OK
        /// </returns>
        public async Task<JSONNode> GetChannelHasVideoAsync(int channelId, int videoId)
        {
            return await RequestAsync(string.Format("/channels/{0}/videos/{1}", channelId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to a Channel
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 404 Not Found: If the Channel cannot be found
        /// 404 Not Found: If the video cannot be found
        /// 403: If the video is restricted from being added to Channels
        /// 204 No Content
        /// </returns>
        public async Task<JSONNode> AddVideoToChannelAsync(int channelId, int videoId)
        {
            return await RequestAsync(string.Format("/channels/{0}/videos/{1}", channelId, videoId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a video from a Channel.
        /// </summary>
        /// <param name="channelId">Channel ID</param>
        /// <param name="videoId">Video ID</param>
        /// <returns>
        /// 404 Not Found: If the Channel cannot be found
        /// 404 Not Found: If the video cannot be found
        /// 403: If the authenticated user is not a moderator of the Channel
        /// 204 No Content
        /// </returns>
        public async Task<JSONNode> DeleteVideoFromChannelAsync(int channelId, int videoId)
        {
            return await RequestAsync(string.Format("/channels/{0}/videos/{1}", channelId, videoId), null, "DELETE", true);
        }
    }
}