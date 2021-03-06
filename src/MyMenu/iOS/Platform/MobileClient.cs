﻿//
// MobileClient.cs
//
// Author:
//       Prashant Cholachagudda <prashant@xamarin.com>
//
// Copyright (c) 2015 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Threading.Tasks;

using UIKit;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using MyMenu.iOS;

[assembly:Dependency (typeof(MobileClient))]
namespace MyMenu.iOS
{
	public class MobileClient : IMobileClient
	{
		public async Task<MobileServiceUser> LoginAsync (MobileServiceAuthenticationProvider provider)
		{
			var view = UIApplication.SharedApplication.KeyWindow.RootViewController;
			return await App.Client.LoginAsync (view, provider);
		}

		public async void LogoutAsync ()
		{
			foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies) {
				NSHttpCookieStorage.SharedStorage.DeleteCookie (cookie);
			}

			await App.Client.LogoutAsync();
		}
	}
}

