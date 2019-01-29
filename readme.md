# RedCorners SDK for Vimeo and YouTube

RedCorners SDK is a wrapper for Vimeo and YouTube APIs, aiming to make the lives of C# developers accessing these platforms easier. The library is .NET Standard 2.0 based, which makes it theoretically compatible with Windows, Mono, .NET Core and Xamarin platforms.

## Vimeo

### Authentication
When you create an API App on the Vimeo Developer portal, you receive two values: `ClientId` and `ClientSecret`. You also have to specify one or more `RedirectUrl`s. These values are used for the OAuth 2.0 authentication process, which Vimeo uses for accessing account-related endpoints.

If you don't have a server that handles redirects for successful authentications, you can set your `RedirectUrl` to `http://ooze.redcorners.com`.

In order to access RedCorners SDK for Vimeo, you need to use the `RedCorners.Vimeo` namespace:

```c#
using RedCorners.Vimeo;
```

