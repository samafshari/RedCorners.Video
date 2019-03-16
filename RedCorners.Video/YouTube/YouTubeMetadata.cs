using System;
using SimpleJSON;

//Good resource: https://developers.google.com/youtube/v3/docs/videos#resource

namespace RedCorners.Video.YouTube
{
    [Serializable]
    public class YouTubeMetadata
    {
        //Todo: Make Getters/Setters to remove {}'"
        public string Title = "Untitled";
        public string Description = "Uploaded with RedCorners";
        public string Tags = "";
        public int CategoryId = 22;
        public string PrivacyStatus = "public";
        public bool Embeddable = true;
        public string License = "youtube";

        public string ToJson()
        {
            string title = Title.Replace('\'', ' ').Replace("\n", "\\n");
            if (Core.IsNullOrWhiteSpace(title)) title = "Untitled";
            string description = Description.Replace('\'', ' ').Replace('\n', ' ');
            string tags = "";
            var tagsList = Tags.Split(',');
            if (tagsList.Length > 0)
            {
                tags = "'" + tagsList[0].Replace("'", "").Trim() + "'";
                for (int i = 1; i < tagsList.Length; i++) tags += ",'" + tagsList[i].Replace("'", "").Trim() + "'";
            }
            string categoryId = CategoryId.ToString();
            string privacyStatus = PrivacyStatus;
            string embeddable = Embeddable ? "true" : "false";
            string license = License;
            
            string input = "~((~\n" +
                "'snippet': ~((~\n" +
                "'title': '{0}',\n" +
                "'description': '{1}',\n" +
                "'tags': [{2}],\n" +
                "'categoryId': {3}\n" +
                "~))~,\n" +
                "'status': ~((~\n" +
                "'privacyStatus': '{4}',\n" +
                "'embeddable': {5},\n" +
                "'license': '{6}'\n" +
                "~))~\n" +
                "~))~";

            input = string.Format(
                input,
                title,
                description,
                tags,
                categoryId,
                privacyStatus,
                embeddable,
                license
                );
            input = input.Replace("~((~", "{").Replace("~))~", "}").Replace("'", "\"");
            return input;
        }

        public string ToJsonWithId(string id)
        {
            var json = ToJson();
            json = json.Substring(1, json.Length - 2);
            json = "{" +
                string.Format("'id': '{0}',", id) +
                json +
                "}";
            return json.Replace('\'', '"');
        }
    }
}
