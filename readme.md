# RedCorners SDK for Vimeo and YouTube

RedCorners SDK is a wrapper for Vimeo and YouTube APIs, aiming to make the lives of C# developers accessing these platforms easier. The library is .NET Standard 2.0 based, which makes it theoretically compatible with Windows, Mono, .NET Core and Xamarin platforms.

## NuGet
[https://www.nuget.org/packages/RedCorners/](https://www.nuget.org/packages/RedCorners/)

## Vimeo

### Authentication
When you create an API App on the Vimeo Developer portal, you receive two values: `ClientId` and `ClientSecret`. You also have to specify one or more `RedirectUrl`s. These values are used for the OAuth 2.0 authentication process, which Vimeo uses for accessing account-related endpoints.

If you don't have a server that handles redirects for successful authentications, you can set your `RedirectUrl` to `http://ooze.redcorners.com`.

In order to access RedCorners SDK for Vimeo, you need to use the `RedCorners.Vimeo` namespace:

```c#
using RedCorners.Vimeo;
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
