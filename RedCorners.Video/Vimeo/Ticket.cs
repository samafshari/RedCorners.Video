using System;
using System.Collections.Generic;
using SimpleJSON;

namespace RedCorners.Video.Vimeo
{
    [Serializable]
    public class Ticket
    {
        public string Uri;
        public string CompleteUri;
        public string TicketId;
        public string UploadLinkSecure;

        public static Ticket FromJson(JSONNode json)
        {
            Ticket ticket = new Ticket();
            ticket.Uri = json["uri"].Value;
            ticket.CompleteUri = json["complete_uri"].Value;
            ticket.TicketId = json["ticket_id"].Value;
            ticket.UploadLinkSecure = json["upload_link_secure"].Value;
            return ticket;
        }

        public override string ToString()
        {
            JSONClass json = new JSONClass();
            json["uri"] = Uri;
            json["complete_uri"] = CompleteUri;
            json["ticket_id"] = TicketId;
            json["upload_link_secure"] = UploadLinkSecure;
            return json;
        }
    }
}
