using System;
using RedCorners.YouTube;

namespace RedCorners.YouTube
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