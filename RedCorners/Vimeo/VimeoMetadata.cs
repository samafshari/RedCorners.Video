using System;
using System.Collections.Generic;

namespace RedCorners.Vimeo
{
    [Serializable]
    public class VimeoMetadata
    {
        public string Title = "Untitled Video";
        public string Description = "Uploaded with RedCorners.";
        public string Tags = "";
        public string License = "";
        public string PrivacyView = "anybody";
        public string PrivacyEmbed = "public";
        public string Password = "";
        public string ReviewLink = "false";
        public string Album = "";

        public void SetMetadata(string videoUri, VimeoHook vc)
        {
            Action<string> SetStatus = (o) =>
            {
                if (VimeoHook.VerboseCallback != null)
                    VimeoHook.VerboseCallback(o);
            };
            try
            {
                SetStatus("Applying Metadata for " + Title);

                var parameters =
                    new Dictionary<string, object>{
                        {"name",Title},
                        {"description",Description.Replace("\\n","\n")},
                        {"privacy.view",PrivacyView},
                        {"privacy.embed",PrivacyEmbed},
                        {"review_link",ReviewLink}
                    };

                if (License == "-") License = "";
                parameters.Add("license", License.Trim());

                if (PrivacyView == "password")
                    parameters.Add("password", Password);

                vc.Request(videoUri, parameters, "PATCH", false);

                SetStatus("Adding Tags for " + Title);
                foreach (string tag in Tags.Split(','))
                    vc.Request(String.Format("{0}/tags/{1}", videoUri, tag), "PUT", false, "");

                if (!Core.IsNullOrWhiteSpace(Album))
                {
                    SetStatus("Adding video to album " + Album);
                    string videoRelUri = videoUri.Contains("/video") ? videoUri : "/videos/" + videoUri;
                    vc.Request(
                        string.Format("/me/albums/{0}{1}", Album, videoRelUri),
                        null, "PUT");
                }

                SetStatus("Metadata assignment completed for " + Title);
            }
            catch (Exception e)
            {
                SetStatus(string.Format("Error applying metadata for {0}: {1}",
                    Title, e.Message));
            }
        }

        public override string ToString()
        {
            return "Title: " + Title + "\n" +
                "Description: " + Description + "\n" +
                "Tags: " + Tags + "\n" +
                "License: " + License + "\n" +
                "PrivacyView: " + PrivacyView + "\n" +
                "PrivacyEmbed: " + PrivacyEmbed + "\n" +
                "Password: " + Password + "\n" +
                "ReviewLink: " + ReviewLink + "\n" +
                "Album: " + Album;
        }
    }
}
