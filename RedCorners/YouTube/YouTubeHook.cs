using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SimpleJSON;

//Good tutorials:
//https://developers.google.com/youtube/v3/guides/using_resumable_upload_protocol
//https://developers.google.com/accounts/docs/OAuth2WebServer#offline
//https://developers.google.com/+/api/oauth#login-scopes
//https://developers.google.com/youtube/v3/sample_requests

namespace RedCorners.YouTube
{
    public class YouTubeHook
    {
        string clientId;
        string secret;
        string apiRoot;
        string redirect;
        int unauthorizedAttempts = 0;
        public static Action<string> VerboseCallback = null;
        public Dictionary<int, string> Categories { get; protected set; }

        public YouTubeHook()
        {
            Categories = new Dictionary<int, string>();
        }
        const string AUTH_ENDPOINT = "https://accounts.google.com/o/oauth2/auth";
        static List<string> DEFAULT_SCOPES = new List<string> {
            "profile",
            "https://www.googleapis.com/auth/plus.me",
            "https://www.googleapis.com/auth/userinfo.profile",
            "https://www.googleapis.com/auth/youtube",
            "https://www.googleapis.com/auth/youtube.readonly",
            "https://www.googleapis.com/auth/youtube.upload",
            "https://www.googleapis.com/auth/youtubepartner",
            "https://www.googleapis.com/auth/youtubepartner-channel-audit"};

        static string AUTH_STATE = "purpletentacle";

        JSONNode AccessJson { get; set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; set; }
        public string DisplayName { get; private set; }
        public string Me { get; private set; }

        public static string GetLoginURL(
            string clientId,
            string redirect = "urn:ietf:wg:oauth:2.0:oob"
            )
        {
            string scopes = DEFAULT_SCOPES[0];
            for (int i = 1; i < DEFAULT_SCOPES.Count; i++)
                scopes += "+" + DEFAULT_SCOPES[i];

            string authUrl = string.Format("{0}?scope={1}&state={2}&redirect_uri={3}&response_type=code&client_id={4}&access_type=offline&approval_prompt=auto",
                AUTH_ENDPOINT,
                scopes,
                AUTH_STATE,
                redirect,
                clientId);

            return authUrl;
        }

        static JSONNode GetAccessToken(
            string authCode, string clientId, string secret, string redirect)
        {
            var payload = new Dictionary<string, object>
            {
                {"code", authCode },
                {"client_id", clientId },
                {"client_secret", secret },
                {"redirect_uri", redirect },
                { "grant_type", "authorization_code" }
            };
            var response = Core.HTTPFetch(
                "https://www.googleapis.com/oauth2/v3/token",
                "POST", null, payload);
            if (VerboseCallback != null) VerboseCallback(response);
            return JSON.Parse(response);
        }

        static JSONNode RefreshAccessToken(
            string refreshToken, string clientId, string secret)
        {
            var payload = new Dictionary<string, object>
            {
                {"client_secret", secret },
                {"grant_type", "refresh_token" },
                {"refresh_token", refreshToken },
                {"client_id", clientId }
            };
            var response = Core.HTTPFetch(
                "https://www.googleapis.com/oauth2/v3/token",
                "POST", null, payload);
            if (VerboseCallback != null) VerboseCallback(response);
            return JSON.Parse(response);
        }
        void loadAccessData()
        {
			try
			{
            	AccessToken = AccessJson["access_token"].Value;
            	if (AccessJson["refresh_token"] != null)
                	RefreshToken = AccessJson["refresh_token"].Value;
			}
			catch {
				throw new Exception("Error loading AccessData from " + AccessJson.ToString());
			}
        }

        void loadDisplayName()
        {
            Me = RequestRaw(
                url: "https://www.googleapis.com/plus/v1/people/me",
                method: "GET",
                jsonBody: false,
                body: null,
                overrideApiRoot: "",
                host: "www.googleapis.com");
            
            DisplayName = JSON.Parse(Me)["displayName"].Value;
        }

        public static YouTubeHook Authorize(
            string authCode,
            string clientId,
            string secret,
            string redirect)
        {
            YouTubeHook yc = new YouTubeHook();
            yc.clientId = clientId;
            yc.secret = secret;
            yc.apiRoot = "https://www.googleapis.com/youtube/v3";
            yc.redirect = redirect;
            yc.AccessJson = GetAccessToken(authCode, yc.clientId, yc.secret, yc.redirect);
            yc.loadAccessData();
            yc.loadDisplayName();
            return yc;
        }

        public static YouTubeHook ReAuthorize(
            string refreshToken,
            string clientId,
            string secret,
            string redirect=""
            )
        {
            YouTubeHook yc = new YouTubeHook();
            yc.clientId = clientId;
            yc.redirect = redirect;
            yc.secret = secret;
            yc.RefreshToken = refreshToken;
            yc.apiRoot = "https://www.googleapis.com/youtube/v3";
            yc.RefreshAuthorization();
            yc.loadDisplayName();
            return yc;
        }

        public void RefreshAuthorization()
        {
            AccessJson = RefreshAccessToken(RefreshToken, clientId, secret);
            if (AccessJson.Count < 4) throw new Exception(AccessJson["error"]);
            loadAccessData();
        }

        public JSONNode Request(
            string url,
            Dictionary<string, object> parameters,
            string method,
            bool jsonBody = true,
            string overrideApiRoot = "",
            string host = null)
        {
            string body = "";

            if (parameters != null && parameters.Count > 0)
            {
                if (method == "GET")
                {
                    url += "?" + Core.KeyValueToString(parameters);
                }
                else if (method == "POST" || method == "PATCH" || method == "PUT" || method == "DELETE")
                {
                    if (jsonBody)
                    {
                        body = Core.JsonEncode(parameters);
                    }
                    else
                    {
                        body = Core.KeyValueToString(parameters);
                    }
                }
            }

            return Request(url, method, jsonBody, body, overrideApiRoot, host: host);
        }

        public JSONNode Request(
            string url,
            string method,
            bool jsonBody,
            string body,
            string overrideApiRoot = "",
            string host = null)
        {
            var fetch = RequestRaw(url, method, jsonBody, body, overrideApiRoot, host);

            try
            {
                return JSON.Parse(fetch);
            }
            catch
            {
                return null;
            }
        }

        public string RequestRaw(
            string url,
            string method,
            bool jsonBody,
            string body,
            string overrideApiRoot = "",
            string host = null)
        {
            string contentType = "application/x-www-form-urlencoded";
            if (jsonBody) contentType = "application/json; charset=utf-8";
            if (string.IsNullOrEmpty(body)) contentType = null;
            var headers = new WebHeaderCollection()
            {
                { "Authorization", String.Format("Bearer {0}", AccessToken) }
            };
            method = method.ToUpper();
            if (overrideApiRoot == null) overrideApiRoot = apiRoot;
            url = overrideApiRoot + url;
            string[] responseHeaders;
            string fetch = Core.HTTPFetch(url, method, headers, body, out responseHeaders, contentType,
                requestAccept: null, host: host);
            if (VerboseCallback != null) VerboseCallback(fetch);

            if (responseHeaders.Length == 0 || responseHeaders[0].Contains(" 40"))
            {
                if (unauthorizedAttempts == 0)
                {
                    unauthorizedAttempts++;
                    RefreshAuthorization();
                    fetch = Core.HTTPFetch(url, method, headers, body, contentType,
                    requestAccept: null, host: host);
                    if (VerboseCallback != null) VerboseCallback(fetch);
                    unauthorizedAttempts = 0;
                }
                else throw new Exception("Got 40x multiple times.");
            }

            return fetch;
        }

        public void ApplyVideoMetadata(string id, YouTubeMetadata meta)
        {
            Request(
                "https://www.googleapis.com/youtube/v3/videos?part=snippet%2Cstatus&fields=id%2Csnippet%2Cstatus&key=" + clientId,
                "PUT",
                true,
                meta.ToJsonWithId(id),
                "",
                null);
        }

        public void FillCategories(string regionCode = "us")
        {
            JSONNode response = Request(
                "https://www.googleapis.com/youtube/v3/videoCategories?part=snippet&regionCode=" + regionCode + "&key=" + clientId, 
                "GET", 
                false, 
                null, 
                "", 
                "");
            var list = response["items"];
            Categories.Clear();
            foreach (var item in list.Children)
            {
                string id = item["id"].Value;
                string name = item["snippet"]["title"].Value;
                Categories[int.Parse(id)] = name;
            }
        }

        public string GetUploadSessionUrl(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Debug.WriteLine("File {0} does not exist.", filepath);
                return null;
            }
            FileInfo info = new FileInfo(filepath);
            return GetUploadSessionUrl(info);
        }

        public string GetUploadSessionUrl(FileInfo file)
        {
            var url = "https://www.googleapis.com/upload/youtube/v3/videos?uploadType=resumable&part=id&key=" + clientId;
            var headers = new WebHeaderCollection()
            {
                { "Authorization", String.Format("Bearer {0}", AccessToken) },
                { "X-Upload-Content-Length", file.Length.ToString() },
                { "X-Upload-Content-Type", "video/*"}
            };

            string[] responseHeaders = null;
            Core.HTTPFetch(url, "POST", headers, "", out responseHeaders, "application/json; charset=utf-8");

            if (responseHeaders.Length == 0 || responseHeaders[0].Contains(" 40"))
            {
                if (unauthorizedAttempts == 0)
                {
                    unauthorizedAttempts++;
                    RefreshAuthorization();
                    var r = GetUploadSessionUrl(file);
                    unauthorizedAttempts = 0;
                    return r;
                }
                else throw new Exception("Error getting the upload session.");
            }

            string resumeUrl = Core.GetHeaderValue(responseHeaders, "Location");

            Debug.WriteLine(String.Format("Response from URL {0}:", url), "StartResumableSession");
            Debug.WriteLine(resumeUrl, "StartResumableSession");
            return resumeUrl;
        }

        public string Upload(
            string url,
            long totalLength,
            long startByte,
            byte[] payload)
        {
            var headers = new WebHeaderCollection()
            {
                { "Authorization", String.Format("Bearer {0}", AccessToken) },
                { "Content-Range", String.Format("bytes {0}-{1}/{2}",
                startByte, startByte + payload.Length - 1, totalLength) }
            };

            if (VerboseCallback != null) VerboseCallback("SENDING PAYLOAD TO SERVER");
            string[] responseHeaders = null;
            var responseFromServer = Core.HTTPFetch(url, "PUT", headers, payload, payload.Length, out responseHeaders, "video/*");
            if (VerboseCallback != null) VerboseCallback("FINISHED SENDING PAYLOAD TO SERVER");

            Debug.WriteLine(String.Format("Response from URL {0}:", url), "Upload");
            return responseFromServer;
        }

        public VerifyFeedback Verify(string url, FileInfo info)
        {
            if (VerboseCallback != null) VerboseCallback("VERIFYING...");
            var headers = new WebHeaderCollection()
            {
                { "Authorization", String.Format("Bearer {0}", AccessToken) },
                { "Content-Range", String.Format("bytes */{0}",info.Length.ToString()) }
            };

            string[] responseHeaders = null;
            Core.HTTPFetch(url, "PUT", headers, "", out responseHeaders, contentType: null);
            bool responseOK = responseHeaders.Length > 0 && responseHeaders[0].Contains(" 20");

            VerifyFeedback v = new VerifyFeedback();
            if (VerboseCallback != null) VerboseCallback("VERIFY FEEDBACK: " + responseOK);
            if (responseOK)
            {
                v.LastByte = v.ContentSize; //Meaning upload is successful
            }
            else
            {
                try
                {
                    //Range: bytes=0-999999
                    foreach (var item in responseHeaders)
                    {
                        if (item.StartsWith("Range:"))
                        {
                            var s = item.Split(':')[1].Trim();
                            var rangestr = s.Split('=')[1].Split('-');
                            v.FirstByte = long.Parse(rangestr[0]);
                            v.LastByte = long.Parse(rangestr[1]);
                        }
                    }
                }
                catch
                {
                    //Range header not found.
                    v.FirstByte = 0;
                    v.LastByte = 0;
                }
            }
            
            foreach (string item in responseHeaders)
                Debug.WriteLine(item);

            return v;
        }

        public Action<VerifyFeedback> UploadCallback = null;
        public string Upload(string path, string url = null,
            int chunkSize = Core.DEFAULT_CHUNK_SIZE,
            int maxAttempts = Core.DEFAULT_MAX_ATTEMPTS,
            long startByte = 0,
            bool step = false)
        {
            int attempts = 0;
            FileInfo info = new FileInfo(path);
            if (!info.Exists) throw new FileNotFoundException("File not found", path);

            if (url == null)
            {
                Debug.WriteLine("No upload URL specified. Making a new one...");
                url = GetUploadSessionUrl(info);
            }
            long contentLength = info.Length;
            string result = null;
            while (true)
            {
                if (VerboseCallback != null) VerboseCallback("Begin Upload Loop");
                try
                {
                    var feedback = Verify(url, info);
                    if (startByte + 1024 < feedback.LastByte) attempts = 0;
                    startByte = feedback.LastByte;
                    feedback.ContentSize = contentLength;
                    Debug.WriteLine(String.Format("{0}/{1} Uploaded.", feedback.LastByte, contentLength), "Upload");
                    if (UploadCallback != null) UploadCallback(feedback);
                    if (feedback.LastByte >= contentLength)
                    {
                        Debug.WriteLine("Done!");
                        break;
                    }

                    var payload = Core.GetPayload(path, startByte, chunkSize);
                    result = Upload(url, contentLength, startByte, payload);
                    if (result != null)
                    {
                        if (VerboseCallback != null) VerboseCallback(result);
                        if (!Core.IsNullOrWhiteSpace(result))
                        {
                            var d = JSON.Parse(result);
                            Debug.WriteLine(result);
                            return d["id"].Value;
                        }
                    }
                    unauthorizedAttempts = 0;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(string.Format("Error in Upload (Attempts {0})", attempts), "Upload");
                    if (VerboseCallback != null) VerboseCallback(string.Format("Error in Upload (Attempts {0})", attempts));
                    if (VerboseCallback != null) VerboseCallback(e.Message);
                    attempts++;
                    if (attempts > maxAttempts)
                    {
                        Debug.WriteLine(e.Message, "Upload");
                        return null;
                    }
                    throw e;
                }
                if (VerboseCallback != null) VerboseCallback("End Upload Loop");
                if (step) return "";
            }

            var dr = JSON.Parse(result);
            return dr["id"].Value;
        }
    }
}
