using RedCorners.Vimeo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RedCorners.WPF.ViewModels
{
    public class VimeoViewModel : INotifyPropertyChanged
    {
        VimeoHook hook = null;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string _clientId;
        public string ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                RegenerateLoginUrl();
            }
        }

        string _clientSecret;
        public string ClientSecret
        {
            get => _clientSecret;
            set
            {
                _clientSecret = value;
                RegenerateLoginUrl();
            }
        }

        string _redirectUrl;
        public string RedirectUrl
        {
            get => _redirectUrl;
            set
            {
                _redirectUrl = value;
                RegenerateLoginUrl();
            }
        }

        string _loginUrl;
        public string LoginUrl
        {
            get => _loginUrl;
            private set
            {
                _loginUrl = value;
                OnPropertyChanged();
            }
        }

        public string AuthCode { get; set; }

        public string Token { get; set; }

        string _userInfo = "Not Logged In";
        public string UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;
                OnPropertyChanged();
            }
        }

        Visibility _authorizingVisibility = Visibility.Collapsed;
        public Visibility AuthorizingVisibility
        {
            get => _authorizingVisibility;
            set
            {
                _authorizingVisibility = value;
                OnPropertyChanged();
            }
        }

        public VimeoViewModel()
        {
            ClientId = AppSettings.Default.VimeoClientId;
            ClientSecret = AppSettings.Default.VimeoClientSecret;
            RedirectUrl = AppSettings.Default.VimeoRedirectUrl;
            Token = AppSettings.Default.VimeoToken;
            RegenerateLoginUrl();
        }

        void RegenerateLoginUrl()
        {
            if (string.IsNullOrWhiteSpace(ClientId)) return;
            if (string.IsNullOrWhiteSpace(ClientSecret)) return;
            if (string.IsNullOrWhiteSpace(RedirectUrl)) return;

            LoginUrl = Vimeo.VimeoHook.GetLoginURL(ClientId, redirect: RedirectUrl);
            AppSettings.Default.Save();
        }

        public Command OpenLoginUrlCommand => new Command(() =>
        {
            var psi = new ProcessStartInfo("iexplore.exe");
            psi.Arguments = LoginUrl;
            Process.Start(psi);
        });

        public Command AuthorizeCommand => new Command(async () =>
        {
            if (AuthorizingVisibility == Visibility.Visible)
                return;

            if (string.IsNullOrWhiteSpace(AuthCode))
            {
                MessageBox.Show("Error: Access Code is empty.");
                return;
            }

            AuthorizingVisibility = Visibility.Visible;
            try
            {
                hook = await VimeoHook.AuthorizeAsync(AuthCode, ClientId, ClientSecret, RedirectUrl);
                Token = hook.AccessToken;
                AppSettings.Default.VimeoToken = Token;
                AppSettings.Default.Save();
                AfterAuthorize();
            }
            finally
            {
                AuthorizingVisibility = Visibility.Collapsed;
            }
        });

        public Command ReauthorizeCommand => new Command(async () =>
        {
            if (AuthorizingVisibility == Visibility.Visible)
                return;

            if (string.IsNullOrWhiteSpace(Token))
            {
                MessageBox.Show("Error: Access Token is empty.");
                return;
            }

            AuthorizingVisibility = Visibility.Visible;
            try
            {
                hook = await VimeoHook.ReAuthorizeAsync(Token, ClientId, RedirectUrl);
                AfterAuthorize();
            }
            finally
            {
                AuthorizingVisibility = Visibility.Collapsed;
            }
        });

        void AfterAuthorize()
        {
            UserInfo = hook.User.ToString();
        }
    }
}
