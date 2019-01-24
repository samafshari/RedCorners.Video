using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;

namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// Search for videos.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="sort">Technique used to sort the results.</param>
        /// <param name="direction">The direction that the results are sorted.
        /// asc
        /// desc</param>
        /// <param name="filter">Filter to apply to the results. The CC filters will show only those videos with the applicable creative commons licenses. See our Creative Commons page for more.
        /// CC
        /// CC-BY
        /// CC-BY-SA
        /// CC-BY-ND
        /// CC-BY-NC-SA
        /// CC-BY-NC-ND
        /// in-progress</param>
        /// <returns></returns>
		public async Task<JSONNode> GetVideosAsync(string query,
			int? page = null, int? per_page = null, string sort = null,
			string direction = null, string filter = null
		)
		{
			var payload = new Dictionary<string, object>();
			payload["query"] = query;
			if (page != null) payload["page"] = page.Value.ToString();
            if (per_page != null) payload["per_page"] = per_page.Value.ToString();
            if (sort != null) payload["sort"] = sort;
            if (direction != null) payload["direction"] = direction;
            if (filter != null) payload["filter"] = filter;
			return await RequestAsync("/videos", payload, "GET", true);
		}
		
        /// <summary>
        /// Get a Video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
		public async Task<JSONNode> GetVideoAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}", videoId), null, "GET", true);
		}

        /// <summary>
        /// Edit video metadata. This method requires a token with the "edit" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="name">The new name for the video</param>
        /// <param name="description">The new description for the video</param>
        /// <param name="license">Set the Creative Commons license</param>
        /// <param name="privacy_view">The new privacy setting for the video
        /// anybody
        /// nobody
        /// contacts
        /// password
        /// users
        /// disable</param>
        /// <param name="privacy_download">Enable or disable the ability for anyone to download video.</param>
        /// <param name="privacy_add">Enable or disable the ability for anyone to add the video to an album, channel, or group.</param>
        /// <param name="privacy_comments">The privacy for who can comment on the video.</param>
        /// <param name="password">When you set privacy.view to password, you must provide the password as an additional parameter</param>
        /// <param name="privacy_embed">The videos new embed settings. Whitelist allows you to define all valid embed domains. Check out our docs for adding and removing domains.
        /// public
        /// private
        /// whitelist</param>
        /// <param name="reviewLink">Enable or disable the review page</param>
        /// <param name="locale">Set the default language for this video. For a full list of valid languages use the "/languages?filter=texttracks" endpoint</param>
        /// <param name="contentRating">A list of values describing the content in this video. You can find the full list in the /contentrating endpoint. You must provide a list representation appropriate for your request body (comma separated for querystring, or array for JSON)</param>
        /// <param name="embed_buttons_like">Show or hide the like button</param>
        /// <param name="embed_buttons_watchlater">Show or hide the watch later button</param>
        /// <param name="embed_buttons_share">Show or hide the share button</param>
        /// <param name="embed_buttons_embed">Show or hide the embed button</param>
        /// <param name="embed_buttons_hd">Show or hide the hd button</param>
        /// <param name="embed_buttons_fullscreen">Show or hide the fullscreen button</param>
        /// <param name="embed_buttons_scaling">Show or hide the scaling button (shown only in fullscreen mode)</param>
        /// <param name="embed_logos_vimeo">Show or hide the vimeo logo</param>
        /// <param name="embed_logos_custom_active">Show or hide your custom logo</param>
        /// <param name="embed_logos_custom_sticky">Always show the custom logo, or hide it after time with the rest of the UI</param>
        /// <param name="embed_logos_custom_link">A url that your user will navigate to if they click your custom logo</param>
        /// <param name="embed_playbar">Show or hide the playbar</param>
        /// <param name="embed_volume">Show or hide the volume selector</param>
        /// <param name="embed_color">A primary color used by the embed player</param>
        /// <param name="embed_title_owner">Show, hide, or let the user decide if the owners information shows on the video
        /// user
        /// show
        /// hide</param>
        /// <param name="embed_title_portrait">Show, hide, or let the user decide if the owners portrait shows on the video
        /// user
        /// show
        /// hide</param>
        /// <param name="embed_title_name">Show, hide, or let the user decide if the video title shows on the video
        /// user
        /// show
        /// hide</param>
        /// <returns></returns>
        public async Task<JSONNode> EditVideoAsync(string videoId, 
			string name = null,
			string description = null,
			string license = null,
			string privacy_view = null,
			bool? privacy_download = null,
			bool? privacy_add = null,
			string privacy_comments = null,
			string password = null,
			string privacy_embed = null,
			bool? reviewLink = null,
			string locale = null,
			string contentRating = null,
			bool? embed_buttons_like = null,
			bool? embed_buttons_watchlater = null,
			bool? embed_buttons_share = null,
			bool? embed_buttons_embed = null,
			bool? embed_buttons_hd = null,
			bool? embed_buttons_fullscreen = null,
			bool? embed_buttons_scaling = null,
			bool? embed_logos_vimeo = null,
			bool? embed_logos_custom_active = null,
			bool? embed_logos_custom_sticky = null,
			string embed_logos_custom_link = null,
			bool? embed_playbar = null,
			bool? embed_volume = null,
			string embed_color = null,
			string embed_title_owner = null,
			string embed_title_portrait = null,
			string embed_title_name = null)
		{
            if (videoId.StartsWith("/videos/")) videoId = videoId.Remove(0, "/videos/".Length);
            var payload = new Dictionary<string, object>();
            if (name != null) payload["name"] = name;
            if (description != null) payload["description"] = description;
            if (license != null) payload["license"] = license;
            if (privacy_view != null) payload["privacy.view"] = privacy_view;
            if (privacy_download.HasValue) payload["privacy.download"] = privacy_download.Value.ToString().ToLower();
            if (privacy_add.HasValue) payload["privacy.add"] = privacy_add.Value.ToString().ToLower();
            if (privacy_comments != null) payload["privacy.comments"] = privacy_comments;
            if (password != null && password != string.Empty) payload["password"] = password;
            if (privacy_embed != null) payload["privacy.embed"] = privacy_embed;
            if (reviewLink.HasValue) payload["review_link"] = reviewLink.Value.ToString().ToLower();
            if (locale != null) payload["locale"] = locale;
            if (contentRating != null) payload["content_rating"] = contentRating;
            if (embed_buttons_like.HasValue) payload["embed.buttons.like"] = embed_buttons_like.Value.ToString().ToLower();
            if (embed_buttons_watchlater.HasValue) payload["embed.buttons.watchlater"] = embed_buttons_watchlater.Value.ToString().ToLower();
            if (embed_buttons_share.HasValue) payload["embed.buttons.share"] = embed_buttons_share.Value.ToString().ToLower();
            if (embed_buttons_embed.HasValue) payload["embed.buttons.embed"] = embed_buttons_embed.Value.ToString().ToLower();
            if (embed_buttons_hd.HasValue) payload["embed.buttons.hd"] = embed_buttons_hd.Value.ToString().ToLower();
            if (embed_buttons_fullscreen.HasValue) payload["embed.buttons.fullscreen"] = embed_buttons_fullscreen.Value.ToString().ToLower();
            if (embed_buttons_scaling.HasValue) payload["embed.buttons.scaling"] = embed_buttons_scaling.Value.ToString().ToLower();
            if (embed_logos_vimeo.HasValue) payload["embed.logos.vimeo"] = embed_logos_vimeo.Value.ToString().ToLower();
            if (embed_logos_custom_active.HasValue) payload["embed.logos.custom.active"] = embed_logos_custom_active.Value.ToString().ToLower();
            if (embed_logos_custom_sticky.HasValue) payload["embed.logos.custom.sticky"] = embed_logos_custom_sticky.Value.ToString().ToLower();
            if (embed_logos_custom_link != null) payload["embed.logos.custom.link"] = embed_logos_custom_link;
            if (embed_playbar.HasValue) payload["embed.playbar"] = embed_playbar.Value.ToString().ToLower();
            if (embed_volume.HasValue) payload["embed.volume"] = embed_volume.Value.ToString().ToLower();
            if (embed_color != null) payload["embed.color"] = embed_color;
            if (embed_title_owner != null) payload["embed.title.owner"] = embed_title_owner;
            if (embed_title_portrait != null) payload["embed.title.portrait"] = embed_title_portrait;
            if (embed_title_name != null) payload["embed.title.name"] = embed_title_name;
            return await RequestAsync(string.Format("/videos/{0}", videoId), payload, "PATCH", true);
        }

        /// <summary>
        /// Delete a video.  This method requires a token with the "delete" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}", videoId), null, "DELETE", true);
		}

        /// <summary>
        /// Get an upload ticket to replace this video file.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="type">Upload type
        /// POST
        /// streamin</param>
        /// <param name="redirect_url">The app redirect URL</param>
        /// <param name="upgrade_to_1080">Immediately upgrade to 1080p on upload complete. For more information about 1080p check out the FAQ.</param>
        /// <returns></returns>
        public async Task<JSONNode> ReplaceVideoAsync(string videoId, string type, string redirect_url, bool upgrade_to_1080)
		{
			var payload = new Dictionary<string, object>();
			payload["type"] = type;
			payload["redirect_url"] = redirect_url;
			payload["upgrade_to_1080"] = upgrade_to_1080.ToString().ToLower();
			return await RequestAsync(string.Format("/videos/{0}/files", videoId), payload, "PUT", true);
		}

        /// <summary>
        /// Get a list of users credited on a video.
        /// </summary>
        /// <param name="videoId"></param>
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
        public async Task<JSONNode> GetVideoCreditsAsync(string videoId, 
			int? page=null, int? per_page=null, string query=null, string sort=null, string direction=null)
		{
			var payload = new Dictionary<string, object>();
			if (page.HasValue) payload["page"] = page.Value.ToString();
			if (per_page.HasValue) payload["per_page"] = per_page.Value.ToString();
			if (query != null) payload["query"] = query;
			if (sort != null) payload["sort"] = sort;
			if (direction != null) payload["direction"] = direction;
			return await RequestAsync(string.Format("/videos/{0}/credits", videoId), payload, "GET", true);
		}

        /// <summary>
        /// Add a credit to a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="role">The role of the person being credited</param>
        /// <param name="name">The name of the person being credited</param>
        /// <param name="email">The email address of the person being credited</param>
        /// <param name="userUri">The URI of the Vimeo user who should be given credit in this video</param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoCreditAsync(string videoId, string role, string name, string email, string userUri)
		{
			var payload = new Dictionary<string, object>();
			payload["role"] = role;
			payload["name"] = name;
			payload["email"] = email;
			payload["user_uri"] = userUri;
			return await RequestAsync(string.Format("/videos/{0}/credits", videoId), payload, "POST", true);
		}

        /// <summary>
        /// Get all the text tracks for a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoTextTracksAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}/texttracks", videoId), null, "GET", true);
		}

        /// <summary>
        /// Add a text track to a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="active">Text tracks marked active will be visible to other users, and will show up in the player. Only one text track per language can be active.</param>
        /// <param name="type">Text track type</param>
        /// <param name="language">Text track language</param>
        /// <param name="name">Text track name</param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoTextTracksAsync(string videoId, bool active, string type, string language, string name)
		{
			var payload = new Dictionary<string, object>();
			payload["active"] = active.ToString().ToLower();
			payload["type"] = type;
			payload["language"] = language;
			payload["name"] = name;
			return await RequestAsync(string.Format("/videos/{0}/texttracks", videoId), payload, "POST", true);
		}

        /// <summary>
        /// Get a single text track for a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="texttrackId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoTextTrackAsync(string videoId, string texttrackId)
		{
			return await RequestAsync(string.Format("/videos/{0}/texttracks/{1}", videoId, texttrackId), null, "GET", true);
		}

        /// <summary>
        /// Edit text track metadata.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="texttrackId"></param>
        /// <param name="active">Text tracks marked active will be visible to other users, and will show up in the player. Only one text track per language can be active.</param>
        /// <param name="type">Text track type</param>
        /// <param name="language">Text track language</param>
        /// <param name="name">Text track name</param>
        /// <returns></returns>
        public async Task<JSONNode> EditVideoTextTrackAsync(string videoId, string texttrackId, bool active, string type, string language, string name)
		{
			var payload = new Dictionary<string, object>();
			payload["active"] = active.ToString().ToLower();
			payload["type"] = type;
			payload["language"] = language;
			payload["name"] = name;
			return await RequestAsync(string.Format("/videos/{0}/texttracks/{1}", videoId, texttrackId), payload, "PATCH", true);
		}

        /// <summary>
        /// Delete a text track.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="texttrackId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoTextTrackAsync(string videoId, string texttrackId)
		{
			return await RequestAsync(string.Format("/videos/{0}/texttracks/{1}", videoId, texttrackId), null, "DELETE", true);
		}

        /// <summary>
        /// Get related videos.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <param name="filter">Filter to apply to the results.
        /// related</param>
        /// <returns></returns>
        public async Task<JSONNode> GetRelatedVideosAsync(string videoId, int? page = null, int? per_page = null, string filter = "related")
		{
			var payload = new Dictionary<string, object>();
			if (page.HasValue) payload["page"] = page.Value.ToString();
			if (per_page.HasValue) payload["per_page"] = per_page.Value.ToString();
			payload["filter"] = filter;
			return await RequestAsync(string.Format("/videos/{0}/videos", videoId), payload, "GET", true);
		}

        /// <summary>
        /// Get a list of all categories this video is in.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoCategoriesAsync(string videoId, int? page = null, int? per_page = null)
		{
			var payload = new Dictionary<string, object>();
			if (page.HasValue) payload["page"] = page.Value.ToString();
			if (per_page.HasValue) payload["per_page"] = per_page.Value.ToString();
			return await RequestAsync(string.Format("/videos/{0}/categories", videoId), payload, "GET", true);
		}

        /// <summary>
        /// Get a single credit
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoCreditAsync(string videoId, string creditId)
		{
			return await RequestAsync(string.Format("/videos/{0}/credits/{1}", videoId, creditId), null, "GET", true);
		}

        /// <summary>
        /// Edit information about a single credit
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="creditId"></param>
        /// <param name="role">The role of the person being credited</param>
        /// <param name="name">The name of the person being credited</param>
        /// <param name="email">The email address of the person being credited</param>
        /// <returns></returns>
        public async Task<JSONNode> EditVideoCreditAsync(string videoId, string creditId, 
			string role = null, string name = null, string email = null)
		{
			var payload = new Dictionary<string, object>();
			if (role != null) payload["role"] = role;
			if (name != null) payload["name"] = name;
			if (email != null) payload["email"] = email;
			return await RequestAsync(string.Format("/videos/{0}/credits/{1}", videoId, creditId), payload, "PATCH", true);
		}

        /// <summary>
        /// Delete a credit
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoCreditAsync(string videoId, string creditId)
		{
			return await RequestAsync(string.Format("/videos/{0}/credits/{1}", videoId, creditId), null, "DELETE", true);
		}

        /// <summary>
        /// Get comments on this video.
        /// </summary>
        /// <param name="videoId"></param>
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
        public async Task<JSONNode> GetVideoCommentsAsync(string videoId,
			int? page=null, int? per_page=null, string query=null, string sort=null, string direction=null)
		{
			var payload = new Dictionary<string, object>();
			if (page.HasValue) payload["page"] = page.Value.ToString();
			if (per_page.HasValue) payload["per_page"] = per_page.Value.ToString();
			if (query != null) payload["query"] = query;
			if (sort != null) payload["sort"] = sort;
			if (direction != null) payload["direction"] = direction;
			return await RequestAsync(string.Format("/videos/{0}/comments", videoId), payload, "GET", true);
		}

        /// <summary>
        /// Post a comment on the video. This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoCommentAsync(string videoId, string text)
		{
			var payload = new Dictionary<string, object>();
			payload["text"] = text;
			return await RequestAsync(string.Format("/videos/{0}/comments", videoId), payload, "POST", true);
		}

        /// <summary>
        /// Check if a video has a specific comment
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoContainsCommentAsync(string videoId, string commentId)
		{
			return await RequestAsync(string.Format("/videos/{0}/comments/{1}", videoId, commentId), null, "GET", true);
		}

        /// <summary>
        /// Edit an existing comment on a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commentId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<JSONNode> EditVideoCommentAsync(string videoId, string commentId, string text)
		{
			var payload = new Dictionary<string, object>();
			payload["text"] = text;
			return await RequestAsync(string.Format("/videos/{0}/comments/{1}", videoId, commentId), payload, "PATCH", true);
		}

        /// <summary>
        /// Delete a comment from a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoCommentAsync(string videoId, string commentId)
		{
			return await RequestAsync(string.Format("/videos/{0}/comments/{1}", videoId, commentId), null, "DELETE", true);
		}

        /// <summary>
        /// Get comments on this video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commentId"></param>
        /// <param name="page">The page number to show.</param>
        /// <param name="per_page">Number of items to show on each page. Max 50.</param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoCommentRepliesAsync(string videoId, string commentId, int? page=null, int? per_page=null)
		{
			var payload = new Dictionary<string, object>();
			if (page.HasValue) payload["page"] = page.Value.ToString();
			if (per_page.HasValue) payload["per_page"] = per_page.Value.ToString();
			return await RequestAsync(string.Format("/videos/{0}/comments/{1}/replies", videoId, commentId), payload, "GET", true);
		}

        /// <summary>
        /// Post a reply to a comment on the video.  This method requires a token with the "interact" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commentId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoCommentReplyAsync(string videoId, string commentId, string text)
		{
			var payload = new Dictionary<string, object>();
			payload["text"] = text;
			return await RequestAsync(string.Format("/videos/{0}/comments/{1}/replies", videoId, commentId), payload, "POST", true);
		}

        /// <summary>
        /// Get a list of this video's past and present pictures.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoPicturesAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}/pictures", videoId), null, "GET", true);
		}

        /// <summary>
        /// Add a picture resource to a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="time">If set will create a picture resource from video at given time offset.</param>
        /// <param name="active">Make this picture the default picture if you have created a picture response from video at given time offset.</param>
        /// <returns></returns>
        public async Task<JSONNode> AddVideoPictureAsync(string videoId, string time = null, bool? active = null)
		{
			var payload = new Dictionary<string, object>();
			if (time != null) payload["time"] = time;
			if (active.HasValue) payload["active"] = active.Value.ToString().ToLower();
			return await RequestAsync(string.Format("/videos/{0}/pictures", videoId), payload, "POST", true);
		}

        /// <summary>
        /// Get a single picture resource for a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoPictureAsync(string videoId, string pictureId)
		{
			return await RequestAsync(string.Format("/videos/{0}/pictures/{1}", videoId, pictureId), null, "GET", true);
		}

        /// <summary>
        /// Modify an existing picture on a video. This method requires a token with the "edit" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="pictureId"></param>
        /// <param name="active"> 	Make this picture the default picture</param>
        /// <returns></returns>
        public async Task<JSONNode> EditVideoPictureAsync(string videoId, string pictureId, bool? active = null)
		{
			var payload = new Dictionary<string, object>();
			if (active.HasValue) payload["active"] = active.Value.ToString().ToLower();
			return await RequestAsync(string.Format("/videos/{0}/pictures/{1}", videoId, pictureId), payload, "PATCH", true);
		}

        /// <summary>
        /// Remove an existing picture from a video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteVideoPictureAsync(string videoId, string pictureId)
        {
            return await RequestAsync(string.Format("/videos/{0}/pictures/{1}", videoId, pictureId), null, "DELETE", true);
        }
        /// <summary>
        /// Get a list of the users who liked this video.
        /// </summary>
        /// <param name="videoId"></param>
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
		public async Task<JSONNode> GetVideoLikesAsync(string videoId,
			int? page=null, int? per_page=null, string query=null, string sort=null, string direction=null)
		{
			var payload = new Dictionary<string, object>();
			if (page.HasValue) payload["page"] = page.Value.ToString();
			if (per_page.HasValue) payload["per_page"] = per_page.Value.ToString();
			if (query != null) payload["query"] = query;
			if (sort != null) payload["sort"] = sort;
			if (direction != null) payload["direction"] = direction;
			return await RequestAsync(string.Format("/videos/{0}/likes", videoId), payload, "GET", true);
		}

        /// <summary>
        /// Check if a video has a specific embed preset
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoContainsPresetAsync(string videoId, string presetId)
		{
			return await RequestAsync(string.Format("/videos/{0}/presets/{1}", videoId, presetId), null, "GET", true);
		}

        /// <summary>
        /// Apply an embed preset to a video. This method requires a token with the "edit" scope.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddPresetToVideoAsync(string videoId, string presetId)
		{
			return await RequestAsync(string.Format("/videos/{0}/presets/{1}", videoId, presetId), null, "PUT", true);
		}

        /// <summary>
        /// Remove an embed preset from a video. This method requires a token with the "edit" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="presetId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeletePresetFromVideoAsync(string videoId, string presetId)
		{
			return await RequestAsync(string.Format("/videos/{0}/presets/{1}", videoId, presetId), null, "DELETE", true);
		}

        /// <summary>
        /// List all of the tags on the video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoTagsAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}/tags", videoId), null, "GET", true);
		}

        /// <summary>
        /// Check if a tag has been applied to a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoContainsTagAsync(string videoId, string word)
		{
			return await RequestAsync(string.Format("/videos/{0}/tags/{1}", videoId, word), null, "GET", true);
		}

        /// <summary>
        /// Tag a video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddTagToVideoAsync(string videoId, string word)
		{
			return await RequestAsync(string.Format("/videos/{0}/tags/{1}", videoId, word), null, "PUT", true);
		}

        /// <summary>
        /// Remove the tag from this video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteTagFromVideoAsync(string videoId, string word)
		{
			return await RequestAsync(string.Format("/videos/{0}/tags/{1}", videoId, word), null, "DELETE", true);
		}

        /// <summary>
        /// Get all users that are allowed to see this video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoAllowedUsersAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}/privacy/users", videoId), null, "GET", true);
		}

        /// <summary>
        /// Add a user to the allowed users list.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddAllowedUserToVideoAsync(string videoId, string userId)
		{
			return await RequestAsync(string.Format("/videos/{0}/privacy/users/{1}", videoId, userId), null, "PUT", true);
		}

        /// <summary>
        /// Remove a user from the allowed users list.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteAllowedUserFromVideoAsync(string videoId, string userId)
		{
			return await RequestAsync(string.Format("/videos/{0}/privacy/users/{1}", videoId, userId), null, "DELETE", true);
		}

        /// <summary>
        /// Retrieve the domains that are allowed to embed this video.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public async Task<JSONNode> GetVideoDomainsAsync(string videoId)
		{
			return await RequestAsync(string.Format("/videos/{0}/privacy/domains", videoId), null, "GET", true);
		}

        /// <summary>
        /// If this video has domain privacy enabled, this call will enable this video to be embedded on the provided domain. This method requires a token with the "edit" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<JSONNode> AddDomainToVideoAsync(string videoId, string domain)
		{
			return await RequestAsync(string.Format("/videos/{0}/privacy/domains/{1}", videoId, domain), null, "PUT", true);
		}

        /// <summary>
        /// Remove a domain from the allowed domains list. This method requires a token with the "edit" scope. 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<JSONNode> DeleteDomainFromVideoAsync(string videoId, string domain)
		{
			return await RequestAsync(string.Format("/videos/{0}/privacy/domains/{1}", videoId, domain), null, "DELETE", true);
		}
	}
}