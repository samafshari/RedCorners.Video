using System.Collections.Generic;
using SimpleJSON;
namespace RedCorners.Vimeo
{
    public partial class VimeoHook
    {
        /// <summary>
        /// View an existing On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPage(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}", ondemandId), null, "GET", true);
        }

        /// <summary>
        /// Edit an existing On Demand page. Currently only supports enabling pre-orders, or publishing the page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="link">The custom string to use in this On Demand page's Vimeo URL.</param>
        /// <param name="publish_active">If set to true, you will publish the On Demand page</param>
        /// <param name="preorder_active">If set to true, you will enable pre-orders on the On Demand page</param>
        /// <param name="preorder_publish_time">Required if preorder.active is true. The time that the On Demand page will be published</param>
        /// <param name="publishWhenReady">This On Demand page will be automatically published when all videos are finished transcoding</param>
        /// <returns></returns>
        public JSONNode EditOnDemandPage(string ondemandId,
            string link = null,
            bool? publish_active = null,
            bool? preorder_active = null,
            bool? preorder_publish_time = null,
            bool? publishWhenReady = null)
        {
            var payload = new Dictionary<string, object>();
            if (link != null) payload["link"] = link;
            if (publish_active.HasValue) payload["publish.active"] = publish_active.Value.ToString().ToLower();
            if (preorder_active.HasValue) payload["preorder.active"] = preorder_active.Value.ToString().ToLower();
            if (preorder_publish_time.HasValue) payload["preorder.publish.time"] = preorder_publish_time.Value.ToString().ToLower();
            if (publishWhenReady.HasValue) payload["publish_when_ready"] = publishWhenReady.Value.ToString().ToLower();
            return Request(string.Format("/ondemand/pages/{0}", ondemandId), payload, "PATCH", true);
        }

        /// <summary>
        /// Delete an On Demand page. Currently only supports deleting On Demand drafts.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode DeleteOnDemandPage(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}", ondemandId), null, "DELETE", true);
        }

        /// <summary>
        /// View an On Demand page's genres.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageGenres(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}/genres", ondemandId), null, "GET", true);
        }

        /// <summary>
        /// Check whether an On Demand page has an On Demand Genre.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageHasGenre(string ondemandId, string genreId)
        {
            return Request(string.Format("/ondemand/pages/{0}/genres/{1}", ondemandId, genreId), null, "GET", true);
        }

        /// <summary>
        /// Add an On Demand genre to an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public JSONNode AddGenreToOnDemandPage(string ondemandId, string genreId)
        {
            return Request(string.Format("/ondemand/pages/{0}/genres/{1}", ondemandId, genreId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a genre from an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public JSONNode DeleteGenreFromOnDemandPage(string ondemandId, string genreId)
        {
            return Request(string.Format("/ondemand/pages/{0}/genres/{1}", ondemandId, genreId), null, "DELETE", true);
        }

        /// <summary>
        /// View an On Demand page's Regions.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageRegions(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}/regions", ondemandId), null, "GET", true);
        }

        /// <summary>
        /// Add a region to an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public JSONNode AddRegionToOnDemandPage(string ondemandId, string regionId)
        {
            return Request(string.Format("/ondemand/pages/{0}/regions/{1}", ondemandId, regionId), null, "PUT", true);
        }

        /// <summary>
        /// Remove a region from an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public JSONNode DeleteRegionFromOnDemandPage(string ondemandId, string regionId)
        {
            return Request(string.Format("/ondemand/pages/{0}/regions/{1}", ondemandId, regionId), null, "DELETE", true);
        }

        /// <summary>
        /// Check whether an On Demand page has a region.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageHasRegion(string ondemandId, string regionId)
        {
            return Request(string.Format("/ondemand/pages/{0}/regions/{1}", ondemandId, regionId), null, "GET", true);
        }

        /// <summary>
        /// Get an On Demand page's pictures.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPagePictures(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}/pictures", ondemandId), null, "GET", true);
        }

        /// <summary>
        /// Create a new picture resource for an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode CreatePictureForOnDemandPage(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}/pictures", ondemandId), null, "POST", true);
        }

        /// <summary>
        /// Get an existing picture on an On Demand.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="posterId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPagePicture(string ondemandId, string posterId)
        {
            return Request(string.Format("/ondemand/pages/{0}/pictures/{1}", ondemandId, posterId), null, "GET", true);
        }

        /// <summary>
        /// Modify an existing picture on a video.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="posterId"></param>
        /// <param name="active">Set this picture as the active picture</param>
        /// <returns></returns>
        public JSONNode EditOnDemandPagePicture(string ondemandId, string posterId, bool active)
        {
            var payload = new Dictionary<string, object>();
            payload["active"] = active.ToString().ToLower();
            return Request(string.Format("/ondemand/pages/{0}/pictures/{1}", ondemandId, posterId), payload, "PATCH", true);
        }

        /// <summary>
        /// Get an On Demand page's Backgrounds.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageBackgrounds(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}/backgrounds", ondemandId), null, "GET", true);
        }

        /// <summary>
        /// Create a new background resource for an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode CreateBackgroundForOnDemandPage(string ondemandId)
        {
            return Request(string.Format("/ondemand/pages/{0}/backgrounds", ondemandId), null, "POST", true);
        }

        /// <summary>
        /// Get a Single Background on an OnDemand Page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="backgroundId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageBackground(string ondemandId, string backgroundId)
        {
            return Request(string.Format("/ondemand/pages/{0}/backgrounds/{1}", ondemandId, backgroundId), null, "GET", true);
        }

        /// <summary>
        /// Modify an existing background on a On Demand.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="backgroundId"></param>
        /// <param name="active">Set this background as the active background</param>
        /// <returns></returns>
        public JSONNode EditOnDemandPageBackground(string ondemandId, string backgroundId, bool active)
        {
            var payload = new Dictionary<string, object>();
            payload["active"] = active.ToString().ToLower();
            return Request(string.Format("/ondemand/pages/{0}/backgrounds/{1}", ondemandId, backgroundId), payload, "PATCH", true);
        }

        /// <summary>
        /// Remove a background image from an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="backgroundId"></param>
        /// <returns></returns>
        public JSONNode DeleteOnDemandPageBackground(string ondemandId, string backgroundId)
        {
            return Request(string.Format("/ondemand/pages/{0}/backgrounds/{1}", ondemandId, backgroundId), null, "DELETE", true);
        }

        /// <summary>
        /// List all videos associated with an On Demand page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="filter">Filter to apply to the results.
        /// trailer
        /// extra
        /// main
        /// all</param>
        /// <param name="sort">Technique used to sort the results.
        /// date
        /// default
        /// manual</param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageVideos(string ondemandId, string filter = null, string sort = null)
        {
            var payload = new Dictionary<string, object>();
            if (filter != null) payload["filter"] = filter;
            if (sort != null) payload["sort"] = sort;
            return Request(string.Format("/ondemand/pages/{0}/videos", ondemandId), payload, "GET", true);
        }

        /// <summary>
        /// Check if an On Demand page contains a video.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandPageHasVideo(string ondemandId, string videoId)
        {
            return Request(string.Format("/ondemand/pages/{0}/videos/{1}", ondemandId, videoId), null, "GET", true);
        }

        /// <summary>
        /// Add a video to an On Demand Page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="videoId"></param>
        /// <param name="type">The type of video you are associating with the On Demand Page
        /// main
        /// trailer
        /// extra</param>
        /// <param name="rent_price_usd">The rental price of this video in USD. Required if rent.active is true.</param>
        /// <param name="rent_price_gbp">The rental price of this video in GBP.</param>
        /// <param name="rent_price_eur">The rental price of this video in EUR.</param>
        /// <param name="rent_price_cad">The rental price of this video in CAD.</param>
        /// <param name="rent_price_aud">The rental price of this video in AUD.</param>
        /// <param name="buy_price_usd">The purchase price of this video. Required if buy.active is true.</param>
        /// <param name="buy_price_gbp">The purchase price of this video in GBP.</param>
        /// <param name="buy_price_eur">The purchase price of this video in EUR.</param>
        /// <param name="buy_price_cad">The purchase price of this video in CAD.</param>
        /// <param name="buy_price_aud">The purchase price of this video in AUD.</param>
        /// <param name="position">The position this video will appear in this ondemand's video collection.</param>
        /// <param name="release_year">The video release year.</param>
        /// <returns></returns>
        public JSONNode AddVideoToOnDemandPage(string ondemandId, string videoId, string type,
            string rent_price_usd = null,
            string rent_price_gbp = null,
            string rent_price_eur = null,
            string rent_price_cad = null,
            string rent_price_aud = null,
            string buy_price_usd = null,
            string buy_price_gbp = null,
            string buy_price_eur = null,
            string buy_price_cad = null,
            string buy_price_aud = null,
            string position = null,
            int? release_year = null)
        {
            var payload = new Dictionary<string, object>();
            payload["type"] = type;
            if (rent_price_usd != null) payload["rent.price.usd"] = rent_price_usd;
            if (rent_price_gbp != null) payload["rent.price.gbp"] = rent_price_gbp;
            if (rent_price_eur != null) payload["rent.price.eur"] = rent_price_eur;
            if (rent_price_cad != null) payload["rent.price.cad"] = rent_price_cad;
            if (rent_price_aud != null) payload["rent.price.aud"] = rent_price_aud;
            if (buy_price_usd != null) payload["buy.price.usd"] = buy_price_usd;
            if (buy_price_gbp != null) payload["buy.price.gbp"] = buy_price_gbp;
            if (buy_price_eur != null) payload["buy.price.eur"] = buy_price_eur;
            if (buy_price_cad != null) payload["buy.price.cad"] = buy_price_cad;
            if (buy_price_aud != null) payload["buy.price.aud"] = buy_price_aud;
            if (position != null) payload["position"] = position;
            if (release_year.HasValue) payload["release_year"] = release_year.Value.ToString();
            return Request(string.Format("/ondemand/pages/{0}/videos/{1}", ondemandId, videoId), payload, "PUT", true);
        }

        /// <summary>
        /// Disconnect a Video from an On Demand Page.
        /// </summary>
        /// <param name="ondemandId"></param>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public JSONNode DeleteVideoFromOnDemandPage(string ondemandId, string videoId)
        {
            return Request(string.Format("/ondemand/pages/{0}/videos/{1}", ondemandId, videoId), null, "DELETE", true);
        }

        /// <summary>
        /// View all On Demand Genres.
        /// </summary>
        /// <returns></returns>
        public JSONNode GetOnDemandGenres()
        {
            return Request("/ondemand/genres", null, "GET", true);
        }

        /// <summary>
        /// View one Genre.
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandGenre(string genreId)
        {
            return Request(string.Format("/ondemand/genres/{0}", genreId), null, "GET", true);
        }

        /// <summary>
        /// View a genre's On Demand pages.
        /// </summary>
        /// <param name="genreId"></param>
        /// <param name="filter">Filter to apply to the results.
        /// country</param>
        /// <param name="sort">Technique used to sort the results.
        /// name
        /// video
        /// publish.time</param>
        /// <returns></returns>
        public JSONNode GetOnDemandGenrePages(string genreId, string filter = null, string sort = null)
        {
            var payload = new Dictionary<string, object>();
            if (filter != null) payload["filter"] = filter;
            if (sort != null) payload["sort"] = sort;
            return Request(string.Format("/ondemand/genres/{0}/pages", genreId), payload, "GET", true);
        }

        /// <summary>
        /// Check whether a genre contains a single On Demand Page.
        /// </summary>
        /// <param name="genreId"></param>
        /// <param name="ondemandId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandGenreHasPage(string genreId, string ondemandId)
        {
            return Request(string.Format("/ondemand/genres/{0}/pages/{1}", genreId, ondemandId), null, "GET", true);
        }

        /// <summary>
        /// Get all On Demand Regions.
        /// </summary>
        /// <returns></returns>
        public JSONNode GetOnDemandRegions()
        {
            return Request("/ondemand/regions", null, "GET", true);
        }

        /// <summary>
        /// Get a single On Demand Region.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public JSONNode GetOnDemandRegion(string countryId)
        {
            return Request(string.Format("/ondemand/regions/{0}", countryId), null, "GET", true);
        }
    }
}