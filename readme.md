# RedCorners.Video SDK for Vimeo and YouTube

RedCorners.Video SDK is a wrapper for Vimeo and YouTube APIs, aiming to make the lives of C# developers accessing these platforms easier. The library is .NET Standard 2.0 based, which makes it theoretically compatible with Windows, Mono, .NET Core and Xamarin platforms.

## NuGet
[https://www.nuget.org/packages/RedCorners.Video/](https://www.nuget.org/packages/RedCorners.Video/)

## Vimeo

### Authentication
When you create an API App on the Vimeo Developer portal, you receive two values: `ClientId` and `ClientSecret`. You also have to specify one or more `RedirectUrl`s. These values are used for the OAuth 2.0 authentication process, which Vimeo uses for accessing account-related endpoints.

If you don't have a server that handles redirects for successful authentications, you can set your `RedirectUrl` to `http://ooze.redcorners.com`.

In order to access RedCorners SDK for Vimeo, you need to use the `RedCorners.Video.Vimeo` namespace:

```c#
using RedCorners.Video.Vimeo;
```

The OAuth authentication is a three-step process:

#### Step 1. Get Login URL

```c#
LoginUrl = VimeoHook.GetLoginURL(ClientId, redirect: RedirectUrl);
```

`LoginUrl` is a `string` containing a URL which users should open in order to authorize their accounts. When the link is opened, Vimeo asks users to give the app access to use their account. 

#### Step 2. Authorization Code

If the process is successful, they are redirected to the `RedirectUrl`, which will contain a `code` query parameter that contains the authorization code.

#### Step 3. Access Token

Once you have the authorization code, you can finalize the process by passing it to the `AuthorizeAsync` method, which returns a `VimeoHook` object. `VimeoHook` object contains various methods which you can use to access different Vimeo API endpoints.

```c#
var hook = await VimeoHook.AuthorizeAsync(AuthCode, ClientId, ClientSecret, RedirectUrl);
```

You can store the `Token` from the `hook` object and reuse it later instead of repeating the authorization process from scratch every time. In case you already have a valid token, you can use the `ReAuthorizeAsync` method to get a `VimeoHook` instead:

```c#
var hook = await VimeoHook.ReAuthorizeAsync(Token, ClientId, RedirectUrl);
```

### API Endpoints

Once you have a `VimeoHook`, you can start doing cool stuff with the API. For example `hook.User` returns a `JSONNode` that contains information about the current user.

### Quick Upload

To quickly upload a file, you can use the following one-liner:

```c#
await hook.UploadAsync(path);
```

That's it. :)

### Upload Progress

RedCorners.Video uploads files in chunks. After uploading each chunk, it tries to verify whether the chunk was uploaded successfully. If so, it goes to uploading the next chunks. After all chunks are uploaded, it signals the backend that the file was uploaded and the transcoding can begin.

The SDK provides a callback through which you can get notified after a chunk is uploaded. You can use it like this:

```c#
hook.UploadCallback = (f) =>
{
    // Calculate how much of the file (in percent) is uploaded (0.0f - 100.0f)
    var progress = (float)f.LastByte / (float)f.ContentSize * 100.0f;
    
    if (f.LastByte >= f.ContentSize)
    {
        // The video has finished uploading. Do finalizing stuff.
    }
    else
    {
        // More chunks have to be uploaded.
    }
};
```

Of course, since the `UploadAsync(...)` method is awaitable, after it's finished executing, you can see whether the video is uploaded or the process has failed.

When `UploadAsync(...)` returns, it either:
- Has a `null` value, meaning the upload did not finish.
- Has a string value, which is the video ID on the server, and indicates that the upload was completed successfully.

### SDK Logging and Debugging

The SDK provides a callback which can be used for debugging the upload process. To use it, simply assign your handler to the `VerboseCallback` action:

```c#
VimeoHook.VerboseCallback = (message) =>
{
    Console.WriteLine(message);
};
```

### Resumable Upload

To have resumable uploads, prior to calling `UploadAsync` you need to obtain an *Upload Ticket* and store it somewhere. The *Upload Ticket* can be thought of as the pointer to which you POST the chunks of a video file.

```c#
var ticket = await GetTicketAsync();
// Store ticket.CompleteUri, TicketId and UploadLink somewhere to reuse later.
```

To reuse a ticket:
```c#
var ticket = new Ticket {
    CompleteUri = ...,
    TicketId = ...,
    UploadLink = ...
};
```

Once you have a ticket, you can use it to upload a video:

```c#
string videoId = hook.Upload(@"C:\Videos\MyVideo.mp4", ticket);
```

### Advanced Upload

The `UploadAsync` method is defined as follows:

```c#
public async Task<string> UploadAsync(
    string path, 
    Ticket ticket = null, 
    int chunkSize = Core.DEFAULT_CHUNK_SIZE, 
    int maxAttempts = Core.DEFAULT_MAX_ATTEMPTS, 
    long startByte = 0, 
    bool step = false)
```

The only required parameter is `path`, and if no other parameters are assigned, you'll have the simple upload. As described above, if you pass a `ticket`, you can have resumable uploads.

`chunkSize` is the maximum size of a chunk in bytes, with a default value of `1MB`. The lower the number, the more reliable the upload, but also the slower. If you have a good connection, you can increase the value to reduce the amount of validation calls and make the upload faster.

`maxAttempts` specify the maximum number of retry attempts the method does before giving up when a failure in the API call occurs. The default is `5`, after which, the method returns `null`.

When `step` is `true`, the SDK goes to the **stepped upload** mode, in which it uploads only one chunk, and returns with a `""` result, meaning _the chunk was uploaded successfully but there's still more to upload_.

### Replacing Existing Videos on Vimeo
With RedCorners.Video, you can replace an existing video by passing its ID to the `GetTicketAsync` method:

```c#
var ticket = vimeoHook.GetTicket(videoId);
string videoId = vimeoHook.Upload(@"C:\Videos\MyVideo.mp4", ticket);
```

The `videoId` parameter of `GetTicketAsync` should be a string containing a number. For example if you want to replace `http://vimeo.com/1234`, you should call `GetTicketAsync("1234")`.

### Sending an API Request

You can use the `RequestAsync` method to send an API request. The following example displays your name on the console:

```c#
var me = await vimeoHook.RequestAsync("/me", null, "GET");
Console.WriteLine(me["name"].ToString());
```

