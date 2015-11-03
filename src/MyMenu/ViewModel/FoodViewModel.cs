﻿//
// FoodViewModel.cs
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
using Xamarin.Forms;
using MyMenu.Helpers;

namespace MyMenu
{
	public enum RecordStatus
	{
		Inserted,
		Deleted
	}

	public class FoodViewModel : BaseViewModel
	{
		readonly Food foodItem;

		public Food FoodItem {
			get {
				return foodItem;
			}
		}

		public FoodViewModel (Food foodItem)
		{
			this.foodItem = foodItem;
		}

		public string Price {
			get {
				return string.Format ("{0:C}", foodItem.PricePerQty);
			}
		}

		async void AddFavoriteMethod ()
		{
			var dataService = DependencyService.Get<IDataService> ();

			var status = await dataService.SaveFavorite (new FavoriteItem {
				FoodItemId = foodItem.Id,
				UserId = Settings.CurrntUserId,
				IsRemoved = false
			});

			IsFavourite = (status == RecordStatus.Inserted);

			await dataService.SyncFavoriteItemsAysnc ();
		}

		void AddToBasketMethod ()
		{
			App.CheckoutItems.Add (foodItem);
		}

		public Command AddFavorite {
			get {
				return addFavorite ?? (addFavorite = new Command (AddFavoriteMethod));
			}
		}

		public Command AddToBasket {
			get {
				return addToBasket ?? (addToBasket = new Command (AddToBasketMethod));
			}
		}

		public float ImageWidth {
			get {
				return (float)App.ScreenSize.Width;	
			}
		}

		public float ImageHeight {
			get {
				return (float)(App.ScreenSize.Width / 1.333d);	
			}
		}

		public bool IsFavourite {
			get {
				return foodItem.IsFavorite;
			}
			set {
				foodItem.IsFavorite = value;
				RaisePropertyChanged ();
			}
		}

		Command addToBasket;
		Command addFavorite;
	}
}

