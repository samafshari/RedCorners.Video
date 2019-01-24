using System;
using System.Collections.Generic;
using RedCorners.Vimeo;

namespace RedCorners.Vimeo
{
    [Serializable]
    public class VimeoUploadTask : UploadTask
    {
		public override SupportedProviders Provider {
			get {
				return SupportedProviders.Vimeo;
			}
		}

		public Ticket Ticket;
        public string VideoId = null;
        public VimeoMetadata Meta = new VimeoMetadata();

		public override string ToString ()
		{
			return base.ToString () + 
				"Ticket: " + (Ticket != null ? Ticket.ToString() : "null") + "\n" +
				Meta.ToString ();
		}
    }
}