using System.Collections.Generic;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Get a user
        /// </summary>
        /// <returns></returns>
        public JSONNode GetMe()
        {
            return Request("/me", null, "GET", true);
        }

        /// <summary>
        /// Edit a single user
        /// </summary>
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
        public JSONNode EditMe(
            string videos_privacy_download = null,
            string videos_privacy_add = null,
            string videos_privacy_comments = null,
            string videos_privacy_view = null,
            string videos_privacy_embed = null,
            string name = null,
            string location = null,
            string bio = null
            )
        {
            var payload = new Dictionary<string, object>();
            if (videos_privacy_download != null) payload["videos.privacy.download"] = videos_privacy_download;
            if (videos_privacy_add != null) payload["videos.privacy.add"] = videos_privacy_add;
            if (videos_privacy_comments != null) payload["videos.privacy.comments"] = videos_privacy_comments;
            if (videos_privacy_view != null) payload["videos.privacy.view"] = videos_privacy_view;
            if (videos_privacy_embed != null) payload["videos.privacy.embed"] = videos_privacy_embed;
            if (name != null) payload["name"] = name;
            if (location != null) payload["location"] = location;
            if (bio != null) payload["bio"] = bio;
            return Request("/me", payload, "PATCH", true);
        }

        /// <summary>
        /// Get a list of a user's Albums.
        /// </summary>
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
        public JSONNode GetMyAlbums(int? page = null, int? per_page = null,
            string query = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return Request("/me/albums", payload, "GET", true);
        }

        /// <summary>
        /// Create an Album
        /// </summary>
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
        public JSONNode CreateAlbum(
            string name, string description,
            string privacy = null, string password = null, string sort = null)
        {
            var payload = new Dictionary<string, object>();
            payload["name"] = name;
            payload["description"] = description;
            if (privacy != null) payload["privacy"] = privacy;
            if (password != null) payload["password"] = password;
            if (sort != null) payload["sort"] = sort;
            return Request("/me/albums", payload, "POST", true);
        }

        /// <summary>
        /// Get info on an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public JSONNode GetAlbum(string albumId)
        {
            return Request(string.Format("/me/albums/{0}", albumId), null, "GET", true);
        }

        /// <summary>
        /// Edit an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="name">The Album's new title</param>
        /// <param name="description">The Album's new description</param>
        /// <param name="privacy">The Album's new privacy level</param>
        /// <param name="sort">The new default sort for the Album</param>
        /// <returns></returns>
        public JSONNode EditAlbum(string albumId, string name = null,
            string description = null, string privacy = null, string sort = null)
        {
            var payload = new Dictionary<string, object>();
            if (name != null) payload["name"] = name;
            if (description != null) payload["description"] = description;
            if (privacy != null) payload["privacy"] = privacy;
            if (sort != null) payload["sort"] = sort;
            return Request(string.Format("/me/albums/{0}", albumId), null, "PATCH", true);
        }

        /// <summary>
        /// Delete an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public JSONNode DeleteAlbum(string albumId)
        {
            return Request(string.Format("/me/albums/{0}", albumId), null, "DELETE", true);
        }

        /// <summary>
        /// Get the list of videos in an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="filter">Filter to apply to the results.</param>
        /// <param name="filter_embeddable">Required if filter=embeddable. Choose between only videos that are embeddable, and only videos that are not embeddable.</param>
        /// <param name="sort">Technique used to sort the results.</param>
        /// <param name="direction">The direction that the results are sorted.</param>
        /// <returns></returns>
        public JSONNode GetAlbumVideos(string albumId,
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
            return Request(string.Format("/me/albums/{0}/videos", albumId), payload, "GET", true);
        }

        /// <summary>
        /// Check if an Album contains a video.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode GetAlbumHasVideo(string albumId, string videoId)
        {
            return Request(string.Format("/me/albums/{0}/videos/{1}", albumId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode AddVideoToAlbum(string albumId, string videoId)
        {
            return Request(string.Format("/me/albums/{0}/videos/{1}", albumId, videoId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a video from an Album.
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode DeleteVideoFromAlbum(string albumId, string videoId)
        {
            return Request(string.Format("/me/albums/{0}/videos/{1}", albumId, videoId), null, "DELETE", true);
        }

        /// <summary>
        /// Get all videos that a user appears in.
        /// </summary>
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
        public JSONNode GetMyAppearances(int? page = null, int? per_page = null,
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
            return Request("/me/appearances", payload, "GET", true);
        }

        /// <summary>
        /// Get a list of the Channels a user follows.
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
        /// moderated</param>
        /// <returns></returns>
        public JSONNode GetMyChannels(int? page = null, int? per_page = null,
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
            return Request("/me/channels", payload, "GET", true);
        }

        /// <summary>
        /// Check if a user follows a Channel.
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public JSONNode AmIFollowingChannel(string channelId)
        {
            return Request(string.Format("/me/channels/{0}", channelId), null, "GET", true);
        }

        /// <summary>
        /// Subscribe to a Channel.
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public JSONNode SubscribeChannel(string channelId)
        {
            return Request(string.Format("/me/channels/{0}", channelId), null, "PUT", true);
        }

        /// <summary>
        /// Unsubscribe from a Channel.
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public JSONNode UnsubscribeChannel(string channelId)
        {
            return Request(string.Format("/me/channels/{0}", channelId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of the Groups a user has joined.
        /// </summary>
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
        public JSONNode GetMyGroups(int? page = null, int? per_page = null,
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
            return Request("/me/groups", payload, "GET", true);
        }

        /// <summary>
        /// Check if a user has joined a Group?
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public JSONNode AmIMemberOfGroup(string groupId)
        {
            return Request(string.Format("/me/groups/{0}", groupId), null, "GET", true);
        }

        /// <summary>
        /// Join a Group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public JSONNode JoinGroup(string groupId)
        {
            return Request(string.Format("/me/groups/{0}", groupId), null, "PUT", true);
        }

        /// <summary>
        /// Leave a Group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public JSONNode LeaveGroup(string groupId)
        {
            return Request(string.Format("/me/groups/{0}", groupId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of the videos in your feed.
        /// </summary>
        /// <param name="offset">This is necessary for proper pagination. Do not provide this value yourself, just use the pagination links provided in the feed response</param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <returns></returns>
        public JSONNode GetMyFeeds(string offset, int? page = null, int? per_page = null)
        {
            var payload = new Dictionary<string, object>();
            payload["offset"] = offset;
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            return Request("/me/feed", payload, "GET", true);
        }

        /// <summary>
        /// Get a list of the user's followers.
        /// </summary>
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
        public JSONNode GetMyFollowers(int? page = null, int? per_page = null,
            string query = null, string sort = null,
            string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return Request("/me/followers", payload, "GET", true);
        }

        /// <summary>
        /// Get a list of the users that a user is following.
        /// </summary>
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
        public JSONNode GetMyFollowing(int? page = null, int? per_page = null,
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
            return Request("/me/following", payload, "GET", true);
        }

        /// <summary>
        /// Check if a user follows another user.
        /// </summary>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        public JSONNode CheckFollow(string followUserId)
        {
            return Request(string.Format("/me/following/{0}", followUserId), null, "GET", true);
        }

        /// <summary>
        /// Follow a user.
        /// </summary>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        public JSONNode Follow(string followUserId)
        {
            return Request(string.Format("/me/following/{0}", followUserId), null, "PUT", true);
        }

        /// <summary>
        /// Unfollow a user.
        /// </summary>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        public JSONNode Unfollow(string followUserId)
        {
            return Request(string.Format("/me/following/{0}", followUserId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of videos that a user likes.
        /// </summary>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// alphabetical
        /// plays
        /// likes
        /// comments
        /// duration</param>
        /// <param name="filter">Filter to apply to the results.
        /// embeddable</param>
        /// <param name="filter_embeddable">Required if filter=embeddable. Choose between only videos that are embeddable, and only videos that are not embeddable.</param>
        /// <param name="direction">The direction that the results are sorted.</param>
        /// <returns></returns>
        public JSONNode GetMyLikes(int? page = null, int? per_page = null,
            string query = null, string sort = null,
            string filter = null, bool? filter_embeddable = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (filter != null) payload["filter"] = filter;
            if (filter_embeddable != null) payload["filter_embeddable"] = filter_embeddable.Value.ToString().ToLower();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return Request("/me/likes", payload, "GET", true);
        }

        /// <summary>
        /// Check if a user likes a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode DoILike(string videoId)
        {
            return Request(string.Format("/me/likes/{0}", videoId), null, "GET", true);
        }

        /// <summary>
        /// Like a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode Like(string videoId)
        {
            return Request(string.Format("/me/likes/{0}", videoId), null, "PUT", true);
        }

        /// <summary>
        /// Unlike a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode Unlike(string videoId)
        {
            return Request(string.Format("/me/likes/{0}", videoId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a list of this user's portrait images.
        /// </summary>
        /// <returns></returns>
        public JSONNode GetMyPictures()
        {
            return Request("/me/pictures", null, "GET", true);
        }

        /// <summary>
        /// Create a new picture resource.
        /// </summary>
        /// <returns></returns>
        public JSONNode CreatePicture()
        {
            return Request("/me/pictures", null, "POST", true);
        }

        /// <summary>
        /// Check if a user has a portrait.
        /// </summary>
        /// <param name="portraitsetId"></param>
        /// <returns></returns>
        public JSONNode GetMyPictureExists(string portraitsetId)
        {
            return Request(string.Format("/me/pictures/{0}", portraitsetId), null, "GET", true);
        }

        /// <summary>
        /// Edit a portrait.
        /// </summary>
        /// <param name="portraitsetId"></param>
        /// <param name="active">Set a picture as your active portrait</param>
        /// <returns></returns>
        public JSONNode EditPicture(string portraitsetId, bool? active = null)
        {
            var payload = new Dictionary<string, object>();
            if (active.HasValue) payload["active"] = active.Value.ToString().ToLower();
            return Request(string.Format("/me/pictures/{0}", portraitsetId), payload, "PATCH", true);
        }

        /// <summary>
        /// Get a list of Portfolios created by a user.
        /// </summary>
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
        public JSONNode GetMyPortfolios(int? page = null, int? per_page = null,
            string query = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return Request("/me/portfolios", payload, "GET", true);
        }

        /// <summary>
        /// Get a Portfolio.
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <returns></returns>
        public JSONNode GetPortfolio(string portfolioId)
        {
            return Request(string.Format("/me/portfolios/{0}", portfolioId), null, "GET", true);
        }

        /// <summary>
        /// Get the videos in this Portfolio.
        /// </summary>
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
        public JSONNode GetPortfolioVideos(string portfolioId,
            int? page = null, int? per_page = null, string sort = null, string direction = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            return Request(string.Format("/me/portfolios/{0}", portfolioId), payload, "GET", true);
        }

        /// <summary>
        /// Check if a Portfolio contains a video.
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode GetPortfolioHasVideo(string portfolioId, string videoId)
        {
            return Request(string.Format("/me/portfolios/{0}/videos/{1}", portfolioId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to the Portfolio.
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode AddVideoToPortfolio(string portfolioId, string videoId)
        {
            return Request(string.Format("/me/portfolios/{0}/videos/{1}", portfolioId, videoId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a video from the Portfolio.
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode DeleteVideoFromPortfolio(string portfolioId, string videoId)
        {
            return Request(string.Format("/me/portfolios/{0}/videos/{1}", portfolioId, videoId), null, "DELETE", true);
        }

        /// <summary>
        /// Get all presets created by the authenticated user.
        /// </summary>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <returns></returns>
        public JSONNode GetMyPresets(int? page = null, int? per_page = null, string query = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (query != null) payload["query"] = query;
            return Request("/me/presets", payload, "GET", true);
        }

        /// <summary>
        /// Get a preset.
        /// </summary>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public JSONNode GetPreset(string presetId)
        {
            return Request(string.Format("/me/presets/{0}", presetId), null, "GET", true);
        }

        /// <summary>
        /// Edit a preset.
        /// </summary>
        /// <param name="presetId"></param>
        /// <param name="outro">Disable the outro.
        /// nothing</param>
        /// <returns></returns>
        public JSONNode EditPreset(string presetId, string outro = null)
        {
            var payload = new Dictionary<string, object>();
            if (outro != null) payload["outro"] = outro;
            return Request(string.Format("/me/presets/{0}", presetId), payload, "PATCH", true);
        }

        /// <summary>
        /// Get videos that have the provided preset.
        /// </summary>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public JSONNode GetPresetVideos(string presetId)
        {
            return Request(string.Format("/me/presets/{0}/videos", presetId), null, "GET", true);
        }

        /// <summary>
        /// Get a list of videos uploaded by a user.
        /// </summary>
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
        /// default
        /// modified_time</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <returns></returns>
        public JSONNode GetMyVideos(int? page = null, int? per_page = null,
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
            return Request("/me/videos", payload, "GET", true);
        }

        /// <summary>
        /// Check if a user owns a clip.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode DoIHaveVideo(string videoId)
        {
            return Request(string.Format("/me/videos/{0}", videoId), null, "GET", true);
        }

        /// <summary>
        /// Get the authenticated user's Watch Later queue.
        /// </summary>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="query">Search query.</param>
        /// <param name="filter">Filter to apply to the results.</param>
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
        public JSONNode GetMyWatchLater(int? page = null, int? per_page = null,
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
            return Request("/me/watchlater", payload, "GET", true);
        }

        /// <summary>
        /// Check if a video is in the authenticated user's Watch Later queue.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode WatchLaterHasVideo(string videoId)
        {
            return Request(string.Format("/me/watchlater/{0}", videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to the authenticated user's watch later list.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode AddVideoToWatchLater(string videoId)
        {
            return Request(string.Format("/me/watchlater/{0}", videoId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a video from your watch later list.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode DeleteVideoFromWatchLater(string videoId)
        {
            return Request(string.Format("/me/watchlater/{0}", videoId), null, "DELETE", true);
        }

        /// <summary>
        /// Get a user's On Demand pages.
        /// </summary>
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
        public JSONNode GetMyOnDemandPage(string type, int? page = null, int? per_page = null,
            string sort = null)
        {
            var payload = new Dictionary<string, object>();
            if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            payload["type"] = type;
            if (sort != null) payload["sort"] = sort;
            return Request("/me/ondemand/pages", payload, "GET", true);
        }

        /// <summary>
        /// Create an On Demand page.
        /// </summary>
        /// <param name="name">The On Demand page name.</param>
        /// <param name="description">The On Demand page description.</param>
        /// <param name="type">The On Demand page type.
        /// film
        /// series</param>
        /// <param name="contentRating">One or more ratings, comma separated or JSON array depending on your request format.
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
        public JSONNode CreateOnDemandPage(
            string name,
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
            return Request("/me/ondemand/pages", payload, "POST", true);
        }

        /// <summary>
        /// Get a users On Demand purchases and rentals.
        /// </summary>
        /// <param name="filter">Which type of On Demand videos to show. `important` will show all pages which are about to expire
        /// rented
        /// important
        /// purchased</param>
        /// <param name="sort">Technique used to sort the results.
        /// alphabetical
        /// rating
        /// added
        /// date</param>
        /// <returns></returns>
        public JSONNode GetMyOnDemandLibrary(string filter, string sort = null)
        {
            var payload = new Dictionary<string, object>();
            payload["filter"] = filter;
            if (sort != null) payload["sort"] = sort;
            return Request("/me/ondemand/library", payload, "GET", true);
        }

        /// <summary>
        /// Check if an On Demand page is in your library.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetMyOnDemandLibraryHasPage(string ondemandId)
        {
            return Request(string.Format("/me/ondemand/library/{0}", ondemandId), null, "GET", true);
        }
    }
}