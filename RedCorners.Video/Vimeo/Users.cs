using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;
namespace RedCorners.Video.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Search for Users
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="sort">Technique used to sort the results.
        /// relevant
        /// date
        /// alphabetical</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <returns></returns>
		public async Task<JSONNode> GetUsersAsync(string query, string sort, string direction, int? page = null, int? per_page = null)
		{
			var payload = new Dictionary<string, object>();
			payload["query"] = query;
			if (page != null) payload["page"] = page.Value.ToString();
			if (per_page != null) payload["per_page"] = per_page.Value.ToString();
			if (sort != null) payload["sort"] = sort;
			if (direction != null) payload["direction"] = direction;
			return await RequestAsync("/users", payload, "GET", true);
		}
			
        /// <summary>
        /// Get a User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
		public async Task<JSONNode> GetUserAsync(string userId)
		{
			return await RequestAsync(string.Format("/users/{0}", userId), null, "GET", true);
		}

        /// <summary>
        /// Edit a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videos_privacy_download">Sets the default download setting for all future videos uploaded by this user. If true, the video can be downloaded by any user.</param>
        /// <param name="videos_privacy_add">Sets the default add setting for all future videos uploaded by this user. If true, anyone can add the video to an album, channel, or group.</param>
        /// <param name="videos_privacy_comments">Sets the default comment setting for all future videos uploaded by this user. It specifies who can comment on the video.
        /// anybody
        /// nobody
        /// contacts</param>
        /// <param name="videos_privacy_view">Sets the default view setting for all future videos uploaded by this user. It specifies who can view the video.
        /// anybody
        /// nobody
        /// contacts
        /// password
        /// users
        /// disable</param>
        /// <param name="videos_privacy_embed">Sets the default embed setting for all future videos uploaded by this user. Whitelist allows you to define all valid embed domains. Check out our docs for adding and removing domains.
        /// public
        /// private
        /// whitelist</param>
        /// <param name="name">The user's display name</param>
        /// <param name="location">The user's location</param>
        /// <param name="bio">The user's bio</param>
        /// <returns></returns>
        public async Task<JSONNode> EditUserAsync(string userId,
			bool? videos_privacy_download = null,
			bool? videos_privacy_add = null,
			string videos_privacy_comments = null,
			string videos_privacy_view = null,
			string videos_privacy_embed = null,
			string name = null,
			string location = null,
			string bio = null)
		{
			var payload = new Dictionary<string, object>();
			if (videos_privacy_download.HasValue) payload["videos.privacy.download"] = videos_privacy_download.Value.ToString().ToLower();
			if (videos_privacy_add.HasValue) payload["videos.privacy.add"] = videos_privacy_add.Value.ToString().ToLower();
			if (videos_privacy_comments != null) payload["videos.privacy.comments"] = videos_privacy_comments;
			if (videos_privacy_view != null) payload["videos.privacy.view"] = videos_privacy_view;
			if (videos_privacy_embed != null) payload["videos.privacy.embed"] = videos_privacy_embed;
			if (name != null) payload["name"] = name;
			if (location != null) payload["location"] = location;
			if (bio != null) payload["bio"] = bio;
			return await RequestAsync(string.Format("/users/{0}", userId), payload, "PATCH", true);
		}

        /// <summary>
        /// Get a list of a user's Albums.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// videos
        /// duration</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserAlbumsAsync(string userId,
			int? page = null, int? per_page = null, string query = null, string sort = null, string direction = null)
			{
				var payload = new Dictionary<string, object>();
				if (page != null) payload["page"] = page.Value.ToString();
				if (per_page != null) payload["per_page"] = per_page.Value.ToString();
				if (query != null) payload["query"] = query;
				if (sort != null) payload["sort"] = sort;
				if (direction != null) payload["direction"] = direction;
				return await RequestAsync(string.Format("/users/{0}/albums", userId), payload, "GET", true);
			}

        /// <summary>
        /// Create an Album
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name">The Album title</param>
        /// <param name="description">The Album description</param>
        /// <param name="privacy">The Album's privacy level
        /// anybody
        /// password</param>
        /// <param name="password">Required if privacy=password. The Album's password</param>
        /// <param name="sort">The default sort order of an Album's videos
        /// arranged
        /// newest
        /// oldest
        /// plays
        /// comments
        /// likes
        /// added_first
        /// added_last
        /// alphabetical</param>
        /// <returns></returns>
        public async Task<JSONNode> CreateUserAlbumAsync(string userId, string name, string description,
			string privacy, string password, string sort)
			{
				var payload = new Dictionary<string, object>();
				payload["name"] = name;
				payload["description"] = description;
				if (privacy != null) payload["privacy"] = privacy;
				if (password != null) payload["password"] = password;
				if (sort != null) payload["sort"] = sort;
				return await RequestAsync(string.Format("/users/{0}/albums", userId), payload, "POST", true);
			}
			
        /// <summary>
        /// Get info on an Album
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <returns></returns>
		public async Task<JSONNode> GetUserAlbumAsync(string userId, string albumId)
		{
			return await RequestAsync(string.Format("/users/{0}/albums/{1}", userId, albumId), null, "GET", true);
		}

        /// <summary>
        /// Edit an Album
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="name">The Album's new title</param>
        /// <param name="description">The Album's new description</param>
        /// <param name="privacy">The Album's new privacy level
        /// anybody
        /// password</param>
        /// <param name="sort">The new default sort for the Album
        /// arranged
        /// newest
        /// oldest
        /// plays
        /// comments
        /// likes
        /// added_first
        /// added_last
        /// alphabetical</param>
        /// <returns></returns>
        public async Task<JSONNode> EditUserAlbumAsync(string userId, string albumId,
			string name = null, string description = null, string privacy = null, string sort = null)
			{
				var payload = new Dictionary<string, object>();
				if (name != null) payload["name"] = name;
				if (description != null) payload["description"] = description;
				if (privacy != null) payload["privacy"] = privacy;
				if (sort != null) payload["sort"] = sort;
				return await RequestAsync(string.Format("/users/{0}/albums/{1}", userId, albumId), payload, "PATCH", true);
			}

        /// <summary>
        /// Delete an Album. This method requires a token with the "delete" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteUserAlbumAsync(string userId, string albumId)
		{
			return await RequestAsync(string.Format("/users/{0}/albums/{1}", userId, albumId), null, "DELETE", true);
		}

        /// <summary>
        /// Get the list of videos in an Album.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="filter">Filter to apply to the results.
        /// embeddable</param>
        /// <param name="filter_embeddable">Required if filter=embeddable. Choose between only videos that are embeddable, and only videos that are not embeddable.</param>
        /// <param name="sort">Technique used to sort the results.
        /// manual
        /// date
        /// alphabetical
        /// plays
        /// likes
        /// comments
        /// duration
        /// modified_time</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserAlbumVideosAsync(string userId, string albumId, 
			int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/albums/{1}/videos", userId, albumId), payload, "GET", true);
			}

        /// <summary>
        /// Check if an Album contains a video.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserAlbumContainsVideoAsync(string userId, string albumId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/albums/{1}/videos/{2}", userId, albumId, videoId), null, "GET", true);
		}

        /// <summary>
        /// Add a video to an Album.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoToUserAlbumAsync(string userId, string albumId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/albums/{1}/videos/{2}", userId, albumId, videoId), null, "PUT", true);
		}

        /// <summary>
        /// Remove a video from an Album.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="albumId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoFromUserAlbumAsync(string userId, string albumId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/albums/{1}/videos/{2}", userId, albumId, videoId), null, "DELETE", true);
		}

        /// <summary>
        /// Get all videos that a user appears in.
        /// </summary>
        /// <param name="userId"></param>
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
        /// direction</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserAppearancesAsync(string userId, int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/appearances", userId), payload, "GET", true);
        }

        /// <summary>
        /// Get a list of the Channels a user follows.
        /// </summary>
        /// <param name="userId"></param>
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
        /// moderated</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserChannelsAsync(string userId, int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/channels", userId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a user follows a Channel.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserFollowingChannelAsync(string userId, string channelId)
        {
            return await RequestAsync(string.Format("/users/{0}/channels/{1}", userId, channelId), null, "GET", true);
        }

        /// <summary>
        /// Subscribe to a Channel.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<JSONNode> SubscribeChannelAsync(string userId, string channelId)
        {
            return await RequestAsync(string.Format("/users/{0}/channels/{1}", userId, channelId), null, "PUT", true);
        }

        /// <summary>
        /// Unsubscribe from a Channel.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<JSONNode> UnsubscribeChannelAsync(string userId, string channelId)
        {
            return await RequestAsync(string.Format("/users/{0}/channels/{1}", userId, channelId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of the Groups a user has joined.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// videos
        /// members</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <param name="filter">Filter to apply to the results.
        /// moderated</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserGroupsAsync(string userId, int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/groups", userId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a user has joined a Group?
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserMemberOfGroupAsync(string userId, string groupId)
        {
            return await RequestAsync(string.Format("/users/{0}/groups/{1}", userId, groupId), null, "GET", true);
        }

        /// <summary>
        /// Join a Group. This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<JSONNode> JoinGroupAsync(string userId, string groupId)
        {
            return await RequestAsync(string.Format("/users/{0}/groups/{1}", userId, groupId), null, "PUT", true);
        }

        /// <summary>
        /// Leave a Group. This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<JSONNode> LeaveGroupAsync(string userId, string groupId)
		{
			return await RequestAsync(string.Format("/users/{0}/groups/{1}", userId, groupId), null, "DELETE", true);
		}

        /// <summary>
        /// Get a list of the videos in your feed.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="offset">This is necessary for proper pagination. Do not provide this value yourself, just use the pagination links provided in the feed response</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserFeedsAsync(string userId, string offset, int? page = null, int? per_page = null)
        {
            var payload = new Dictionary<string, object>();
            payload["offset"] = offset;
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            return await RequestAsync(string.Format("/users/{0}/feed", userId), payload, "GET", true);
        }

        /// <summary>
        /// Get a list of the user's followers.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserFollowersAsync(string userId, int? page = null, int? per_page = null,
            string query = null, string sort = null,
            string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/users/{0}/followers", userId), payload, "GET", true);
        }

        /// <summary>
        /// Get a list of the users that a user is following.
        /// </summary>
        /// <param name="userId"></param>
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
        /// online</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserFollowingAsync(string userId, int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/following", userId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a user follows another user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        public async Task<JSONNode> CheckUserFollowAsync(string userId, string followUserId)
        {
            return await RequestAsync(string.Format("/users/{0}/following/{1}", userId, followUserId), null, "GET", true);
        }

        /// <summary>
        /// Follow a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        public async Task<JSONNode> UserFollowAsync(string userId, string followUserId)
        {
            return await RequestAsync(string.Format("/users/{0}/following/{1}", userId, followUserId), null, "PUT", true);
        }

        /// <summary>
        /// Unfollow a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        public async Task<JSONNode> UserUnfollowAsync(string userId, string followUserId)
        {
            return await RequestAsync(string.Format("/users/{0}/following/{1}", userId, followUserId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of videos that a user likes.
        /// </summary>
        /// <param name="userId"></param>
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
        /// <param name="direction">The direction that the results are sorted.</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserLikesAsync(string userId, int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/likes", userId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a user likes a video.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserLikeAsync(string userId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/likes/{1}", userId, videoId), null, "GET", true);
		}

        /// <summary>
        /// Like a video. This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> LikeAsync(string userId, string videoId)
        {
            return await RequestAsync(string.Format("/users/{0}/likes/{1}", userId, videoId), null, "PUT", true);
        }

        /// <summary>
        /// Unlike a video.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> UnlikeAsync(string userId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/likes/{1}", userId, videoId), null, "DELETE", true);
		}

        /// <summary>
        /// Get a list of this user's portrait images.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPicturesAsync(string userId)
        {
            return await RequestAsync(string.Format("/users/{0}/pictures", userId), null, "GET", true);
        }

        /// <summary>
        /// Create a new picture resource. This method requires a token with the "upload" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JSONNode> CreateUserPictureAsync(string userId)
        {
            return await RequestAsync(string.Format("/users/{0}/pictures", userId), null, "POST", true);
        }

        /// <summary>
        /// Check if user has a portrait
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portraitset_id"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserHasPictureAsync(string userId, string portraitset_id)
        {
            return await RequestAsync(string.Format("/users/{0}/pictures/{1}", userId, portraitset_id), null, "GET", true);
        }

        /// <summary>
        /// Edit a portrait.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portraitsetId"></param>
        /// <param name="active">Set a picture as your active portrait</param>
        /// <returns></returns>
        public async Task<JSONNode> EditUserPictureAsync(string userId, string portraitsetId, bool? active = null)
        {
            var payload = new Dictionary<string, object>();
            if (active.HasValue) payload["active"] = active.Value.ToString().ToLower();
            return await RequestAsync(string.Format("/users/{0}/pictures/{1}", userId, portraitsetId), payload, "PATCH", true);
        }

        /// <summary>
        /// Get a list of Portfolios created by a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPortfoliosAsync(string userId, int? page = null, int? per_page = null,
            string query = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/users/{0}/portfolios", userId), payload, "GET", true);
        }

        /// <summary>
        /// Get a Portfolio.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portfolioId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPortfolioAsync(string userId, string portfolioId)
        {
            return await RequestAsync(string.Format("/users/{0}/portfolios/{1}", userId, portfolioId), null, "GET", true);
        }

        /// <summary>
        /// Get the videos in this Portfolio.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portfolioId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// plays
        /// likes
        /// comments
        /// manual
        /// default</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPortfolioVideosAsync(string userId, string portfolioId,
            int? page = null, int? per_page = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/users/{0}/portfolios/{1}", userId, portfolioId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a Portfolio contains a video.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portfolioId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPortfolioContainsVideoAsync(string userId, string portfolioId, string videoId)
        {
            return await RequestAsync(string.Format("/users/{0}/portfolios/{1}/videos/{2}", userId, portfolioId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to the Portfolio.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portfolioId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoToUserPortfolioAsync(string userId, string portfolioId, string videoId)
        {
            return await RequestAsync(string.Format("/users/{0}/portfolios/{1}/videos/{2}", userId, portfolioId, videoId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a video from the Portfolio.  This method requires a token with the "edit" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portfolioId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoFromPortfolioAsync(string userId, string portfolioId, string videoId)
        {
            return await RequestAsync(string.Format("/users/{0}/portfolios/{1}/videos/{2}", userId, portfolioId, videoId), null, "DELETE", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPresetsAsync(string userId, int? page = null, int? per_page = null, string query = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            return await RequestAsync(string.Format("/users/{0}/presets", userId), payload, "GET", true);
        }

        /// <summary>
        /// Get a preset
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPresetAsync(string userId, string presetId)
        {
            return await RequestAsync(string.Format("/users/{0}/presets/{1}", userId, presetId), null, "PATCH", true);
        }

        /// <summary>
        /// Edit a preset.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="presetId"></param>
        /// <param name="outro">Disable the outro.
        /// nothing</param>
        /// <returns></returns>
        public async Task<JSONNode> EditUserPresetAsync(string userId, string presetId, string outro = null)
        {
            var payload = new Dictionary<string, object>();
            if (outro != null) payload["outro"] = outro;
            return await RequestAsync(string.Format("/users/{0}/presets/{1}", userId, presetId), payload, "PATCH", true);
        }

        /// <summary>
        /// Get videos that have the provided preset.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserPresetVideosAsync(string userId, string presetId)
		{
			return await RequestAsync(string.Format("/users/{0}/presets/{1}/videos", userId, presetId), null, "GET", true);
		}

        /// <summary>
        /// Get a list of videos uploaded by a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="filter">Filter to apply to the results.
        /// embeddable
        /// playable</param>
        /// <param name="filter_embeddable">Required if filter=embeddable. Choose between only videos that are embeddable, and only videos that are not embeddable.</param>
        /// <param name="filter_playable">Default true. Choose between only videos that are playable, and only videos that are not playable.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// plays
        /// likes
        /// comments
        /// duration
        /// default</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserVideosAsync(string userId, int? page = null, int? per_page = null,
            string query = null, string filter = null,
            bool? filter_embeddable = null, bool? filter_playable = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (filter != null) payload["filter"] = filter;
            if (filter_embeddable != null) payload["filter_embeddable"] = filter_embeddable.Value.ToString().ToLower();
            if (filter_playable != null) payload["filter_playable"] = filter_playable.Value.ToString().ToLower();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return await RequestAsync(string.Format("/users/{0}/videos", userId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a user owns a clip.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserHasVideoAsync(string userId, string videoId)
        {
            return await RequestAsync(string.Format("/users/{0}/videos/{1}", userId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Get the authenticated user's Watch Later queue.
        /// </summary>
        /// <param name="userId"></param>
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
        /// <returns></returns>
        public async Task<JSONNode> GetUserWatchLaterAsync(string userId, int? page = null, int? per_page = null,
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
            return await RequestAsync(string.Format("/users/{0}/watchlater", userId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a video is in the authenticated user's Watch Later queue.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserWatchLaterContainsVideoAsync(string userId, string videoId)
        {
            return await RequestAsync(string.Format("/users/{0}/watchlater/{1}", userId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to the authenticated user's watch later list. This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoToUserWatchlaterAsync(string userId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/watchlater/{1}", userId, videoId), null, "PUT", true);
		}

        /// <summary>
        /// Remove a video from your watch later list. This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoFromUserWatchlaterAsync(string userId, string videoId)
		{
			return await RequestAsync(string.Format("/users/{0}/watchlater/{1}", userId, videoId), null, "DELETE", true);
		}

        /// <summary>
        /// Get an upload ticket. This method requires a token with the "upload" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserUploadTicketAsync(string userId, string ticketId)
		{
			return await RequestAsync(string.Format("/users/{0}/tickets/{1}", userId, ticketId), null, "GET", true);
		}

        /// <summary>
        /// Complete a streaming upload. This method requires a token with the "upload" scope. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ticketId"></param>
        /// <param name="videoFileId">The ID of the uploaded file</param>
        /// <param name="signature">The crypto signature of the completed upload</param>
        /// <returns></returns>
        public async Task<JSONNode> CompleteUserUploadTicketAsync(string userId, string ticketId,
			string videoFileId, string signature)
		{
			var payload = new Dictionary<string, object>();
			payload["video_file_id"] = videoFileId;
			payload["signature"] = signature;
			return await RequestAsync(string.Format("/users/{0}/tickets/{1}", userId, ticketId), payload, "DELETE", true);
		}

        /// <summary>
        /// Get a user's On Demand pages.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type">of OnDemand pages to display
        /// film
        /// series</param>
        /// <param name="page">The page number to show. Page for paging</param>
        /// <param name="per_page">Number of items to show on each page. Max 50. How many OnDemand pages to display at a time</param>
        /// <param name="sort">Technique used to sort the results.
        /// name
        /// rating
        /// added
        /// publish.time
        /// modified_time</param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserOnDemandPagesAsync(string userId, string type, int? page = null, int? per_page = null,
            string sort = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            payload["type"] = type;
            if (sort != null) payload["sort"] = sort;
            return await RequestAsync(string.Format("/users/{0}/ondemand/pages", userId), payload, "GET", true);
        }

        /// <summary>
        /// Create an On Demand page.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name">The On Demand page name.</param>
        /// <param name="description">The On Demand page description.</param>
        /// <param name="type">The On Demand page type.
        /// film
        /// series</param>
        /// <param name="contentRating">One or more ratings, comma separated or JSON array depending on your await RequestAsync( format.
        /// language
        /// drugs
        /// violence
        /// nudity
        /// safe
        /// unrated</param>
        /// <param name="link">The custom string to use in this On Demand page's Vimeo URL.</param>
        /// <param name="domainLink">The On Demand page's custom domain.</param>
        /// <param name="rent_active">Required if buy.active is false.</param>
        /// <param name="rent_price_usd">Required if rent.active is true. If this On Demand page is a `film`, this is the price to rent the film. If `series`, this is the price to rent the entire collection.</param>
        /// <param name="rent_price_gbp">The rental price of this video in GBP.</param>
        /// <param name="rent_price_eur">The rental price of this video in EUR.</param>
        /// <param name="rent_price_cad">The rental price of this video in CAD.</param>
        /// <param name="rent_price_aud">The rental price of this video in AUD.</param>
        /// <param name="rent_period">Required if rent.active is true.</param>
        /// <param name="buy_active">Required if rent.active is false.</param>
        /// <param name="buy_download">If download is available to purchasers who buy the film.</param>
        /// <param name="buy_price_usd">Required if buy.active is true. If this On Demand page is a `film`, this is the price to purchase the film. If `series`, this is the price to purchase the entire collection.</param>
        /// <param name="buy_price_gbp">The purchase price of this video in GBP.</param>
        /// <param name="buy_price_eur">The purchase price of this video in EUR.</param>
        /// <param name="buy_price_cad">The purchase price of this video in CAD.</param>
        /// <param name="buy_price_aud">The purchase price of this video in AUD.</param>
        /// <param name="episodes_rent_active">Whether episodes can be rented</param>
        /// <param name="episodes_rent_price_usd">Required if episodes.rent.active is true. Only applicable if this On Demand page is a series. This is the default price for each individual episode.</param>
        /// <param name="episodes_rent_period">Required if episodes.rent.active is true.</param>
        /// <param name="episodes_buy_active">Whether episodes can be bought</param>
        /// <param name="episodes_buy_download">If download is available to purchasers who buy the episode.</param>
        /// <param name="episodes_buy_price_usd">Required if episodes.buy.active is true.</param>
        /// <param name="subscription_monthly_active">Required if rent.active and buy.active are false.</param>
        /// <param name="subscription_monthly_price_usd">Required if subscription.active is true.</param>
        /// <param name="accepted_currencies">List of accepted currencies.</param>
        /// <returns></returns>
        public async Task<JSONNode> CreateUserOnDemandPageAsync(string userId, string name,
            string description,
            string type,
            string contentRating,
            string link = null,
            string domainLink = null,
            string rent_active = null,
            string rent_price_usd = null,
            string rent_price_gbp = null,
            string rent_price_eur = null,
            string rent_price_cad = null,
            string rent_price_aud = null,
            string rent_period = null,
            bool? buy_active = null,
            bool? buy_download = null,
            string buy_price_usd = null,
            string buy_price_gbp = null,
            string buy_price_eur = null,
            string buy_price_cad = null,
            string buy_price_aud = null,
            bool? episodes_rent_active = null,
            string episodes_rent_price_usd = null,
            string episodes_rent_period = null,
            bool? episodes_buy_active = null,
            bool? episodes_buy_download = null,
            string episodes_buy_price_usd = null,
            bool? subscription_monthly_active = null,
            string subscription_monthly_price_usd = null,
            string accepted_currencies = null)
			{
				var payload = new Dictionary<string, object>();
            payload["name"] = name;
            payload["description"] = description;
            payload["type"] = type;
            payload["content_rating"] = contentRating;
            if (link != null) payload["link"] = link;
            if (domainLink != null) payload["domain_link"] = domainLink;
            if (rent_active != null) payload["rent.active"] = rent_active;
            if (rent_price_usd != null) payload["rent.price.usd"] = rent_price_usd;
            if (rent_price_gbp != null) payload["rent.price.gbp"] = rent_price_gbp;
            if (rent_price_eur != null) payload["rent.price.eur"] = rent_price_eur;
            if (rent_price_cad != null) payload["rent.price.cad"] = rent_price_cad;
            if (rent_price_aud != null) payload["rent.price.aud"] = rent_price_aud;
            if (rent_period != null) payload["rent.period"] = rent_period;
            if (buy_active.HasValue) payload["buy.active"] = buy_active.Value.ToString().ToLower();
            if (buy_download.HasValue) payload["buy.download"] = buy_download.Value.ToString().ToLower();
            if (buy_price_usd != null) payload["buy.price.usd"] = buy_price_usd;
            if (buy_price_gbp != null) payload["buy.price.gbp"] = buy_price_gbp;
            if (buy_price_eur != null) payload["buy.price.eur"] = buy_price_eur;
            if (buy_price_cad != null) payload["buy.price.cad"] = buy_price_cad;
            if (buy_price_aud != null) payload["buy.price.aud"] = buy_price_aud;
            if (episodes_rent_active.HasValue) payload["episodes.rent.active"] = episodes_rent_active.Value.ToString().ToLower();
            if (episodes_rent_price_usd != null) payload["episodes.rent.price.usd"] = episodes_buy_price_usd;
            if (episodes_rent_period != null) payload["episodes.rent.period"] = episodes_rent_period;
            if (episodes_buy_active.HasValue) payload["episodes.buy.active"] = episodes_buy_active.Value.ToString().ToLower();
            if (episodes_buy_download.HasValue) payload["episodes.buy.download"] = episodes_buy_download.Value.ToString().ToLower();
            if (episodes_buy_price_usd != null) payload["episodes.buy.price.usd"] = episodes_buy_price_usd;
            if (subscription_monthly_active.HasValue) payload["subscription.monthly.active"] = subscription_monthly_active.Value.ToString().ToLower();
            if (subscription_monthly_price_usd != null) payload["subscription.monthly.price.usd"] = subscription_monthly_price_usd;
            if (accepted_currencies != null) payload["accepted.currencies"] = accepted_currencies;
            return await RequestAsync(string.Format("/users/{0}/ondemand/pages", userId), payload, "POST", true);
			}

        /// <summary>
        /// Check if an On Demand page is in your library.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetUserOnDemandLibraryContainsPageAsync(string userId, string ondemandId)
        {
            return await RequestAsync(string.Format("/users/{0}/ondemand/library/{1}", userId, ondemandId), null, "GET", true);
        }
	}
}