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

        public string AccessCode { get; set; }

        public string Token { get; set; }

        Visibility _authenticatingVisibility = Visibility.Collapsed;
        public Visibility AuthenticatingVisibility
        {
            get => _authenticatingVisibility;
            set
            {
                _authenticatingVisibility = value;
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

        public Command AuthenticateCommand => new Command(async () =>
        {
            if (AuthenticatingVisibility == Visibility.Visible)
                return;

            if (string.IsNullOrWhiteSpace(AccessCode))
            {
                MessageBox.Show("Error: Access Code is empty.");
                return;
            }

            AuthenticatingVisibility = Visibility.Visible;
            try
            {
                hook = await VimeoHook.AuthorizeAsync(AccessCode, ClientId, ClientSecret, RedirectUrl);
                Token = hook.AccessToken;
            }
            finally
            {
                AuthenticatingVisibility = Visibility.Collapsed;
            }
        });
    }
}
