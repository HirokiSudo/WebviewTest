using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace App1
{
    /// <summary>
    /// Frame 内へナビゲートするために利用する空欄ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.webView1.NavigationCompleted += webView1_NavigationCompleted;
            this.webView1.NavigationStarting += webView1_NavigationStarting;
        }

        // ［GO］ボタンがタップされた
        private async void buttonGo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Uri newUri;
            if (Uri.TryCreate(this.textBox1.Text, UriKind.Absolute, out newUri)
                && newUri.Scheme.StartsWith("http"))
            {
                this.webView1.Navigate(newUri);
            }
            else
            {
                // エラー処理
                string errMsg = @"入力された文字列がURIとして不正か、""http""で始まっていません";
                await new Windows.UI.Popups.MessageDialog(errMsg).ShowAsync();
            }
        }

        // ［戻る］ボタンがタップされた
        private void buttonGoBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.webView1.GoBack();
        }

        // ［進む］ボタンがタップされた
        private void buttonGoForward_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.webView1.GoForward();
        }

        // ［リフレッシュ］ボタンがタップされた
        private void buttonRefresh_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.webView1.Refresh();
        }

        async void webView1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            this.textBox1.Text = args.Uri.ToString();
        }

        async void webView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                // エラー発生
                string errMsg = args.WebErrorStatus.ToString();
                int errCode = (int)args.WebErrorStatus;
                string msg = string.Format("サーバ側エラー：{0}（{1}）", errMsg, errCode);
                await new Windows.UI.Popups.MessageDialog(msg).ShowAsync();
            }
        }

    }
}
