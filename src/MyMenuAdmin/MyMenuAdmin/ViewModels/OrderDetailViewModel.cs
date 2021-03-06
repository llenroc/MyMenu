﻿using System.Collections.Generic;
using MyMenu;
using Xamarin.Forms;

namespace MyMenuAdmin.ViewModel
{
    public class OrderDetailViewModel : BaseViewModel
    {
        private OrderItemViewModel selectedOrderItemViewModel;

        public OrderItemViewModel SelectedOrderItemViewModel
        {
            get { return selectedOrderItemViewModel; }
            set { selectedOrderItemViewModel = value; RaisePropertyChanged(); }
        }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; RaisePropertyChanged(); }
        }

        private List<OrderDetail> selectedOrderDetails;

        public List<OrderDetail> SelectedOrderDetails
        {
            get { return selectedOrderDetails; }
            set { selectedOrderDetails = value; RaisePropertyChanged();}
        }

        public async void UpdateOrder(string updateStatus)
        {
            SelectedOrder.Status = updateStatus;
            SelectedOrder.SendSms = true;
            await DependencyService.Get<IAzureDataManager<Order>>().SaveAsync(SelectedOrder);
        }

        public OrderDetailViewModel()
        {
            Title = "Order Detail";
        }
    }
}
