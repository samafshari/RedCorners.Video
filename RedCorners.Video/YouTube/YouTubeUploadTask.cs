using System;
using RedCorners.Video.YouTube;

namespace RedCorners.Video.YouTube
{
    [Serializable]
	public class YouTubeUploadTask : UploadTask
    {
		public override SupportedProviders Provider {
			get {
				return SupportedProviders.YouTube;
			}
		}

		public string Url = null;
        public YouTubeMetadata Meta = new YouTubeMetadata();

		public override string ToString ()
		{
			return base.ToString () + 
				"Url: " + (Url ?? "null") + "\n" +
				Meta.ToJson ();
		}
    }
}