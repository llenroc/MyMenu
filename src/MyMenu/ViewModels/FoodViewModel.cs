﻿
using System;
using Xamarin.Forms;

namespace MyMenu
{
    
	public class FoodViewModel : BaseViewModel
	{
		readonly Food foodItem;

		public Food FoodItem => foodItem;

	    public FoodViewModel (Food foodItem)
	    {
	        this.foodItem = foodItem;
            QuantityChangedCommand = new Command((parameter) =>
	        {
	            var action = (AddRemoveQtyAction) parameter;
                if(action == AddRemoveQtyAction.Add)
                    AddToCart();
                else
                    RemoveFromCart();
	        });
	    }

	    public string Price => $"{foodItem.PricePerQty:C}";

	    private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; RaisePropertyChanged(); }
        }
        
        public FavoriteManager FavoriteManager => DependencyService.Get<IAzureDataManager<FavoriteItem>>() as FavoriteManager;
        
        async void AddFavoriteMethod ()
		{
			
			await FavoriteManager.SaveAsync(new FavoriteItem {
				FoodItemId = foodItem.Id,
				UserId = Settings.CurrentUserId,
				IsRemoved = false
			});

			IsFavourite = true;

			await FavoriteManager.SyncAsync();
		}

	    private void AddToCart ()
		{
            App.CheckoutItems.Add (foodItem);
		}
        private void RemoveFromCart()
        {
            if (App.CheckoutItems.Contains(foodItem))
                App.CheckoutItems.Remove(foodItem);
        }

        private Command quantityChangedCommand;

        public Command QuantityChangedCommand
        {
            get { return quantityChangedCommand; }
            set { quantityChangedCommand = value; RaisePropertyChanged(); }
        }
       
        public float ImageWidth => (float)App.ScreenSize.Width;

	    public float ImageHeight => (float)(App.ScreenSize.Width / (16d/9d));

	    public bool IsFavourite {
			get {
				return foodItem.IsFavorite;
			}
			set {
				foodItem.IsFavorite = value;
				RaisePropertyChanged ();
			}
		}

		//Command addToBasket;
		Command addFavorite;
	}
}

