﻿using DALayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UILayer.Commands;
using UILayer.Properties;

namespace UILayer.ViewModels
{   
    public class AddOrderViewModel : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;
        private Order order;
        private ICommand addOrderCommand;
        private IEnumerable<Worker> workers;
        private IEnumerable<Worker> selectedWorkers;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddOrderViewModel()
        {
            unitOfWork = new UnitOfWork();
            order = new Order()
            {
                ReceptionDate = DateTime.Today.Date
            };
            workers = unitOfWork.WorkerRepository.GetWorkers();
            addOrderCommand = new AddCommand(() => true, AddOrder);
        }

        private void AddOrder()
        {
            try
            {
                order.Worker = selectedWorkers.ToList();
                unitOfWork.OrderRepository.AddOrder(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.MsgAddFail + ex.Message + ex.Source, Resources.MsgFail,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(Resources.MsgAddOrderSuccess, Resources.MsgSuccess, 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddOrderCommand
        {
            get => addOrderCommand;
        }

        #region UI props
        public string CustomerName
        {
            get => order.CustomerName;
            set => order.CustomerName = value;
        }

        public string PhoneNumber
        {
            get => order.PhoneNumber;
            set => order.PhoneNumber = value;
        }

        public string Email
        {
            get => order.Email;
            set => order.Email = value;
        }

        public string Device
        {
            get => order.Device;
            set => order.Device = value;
        }

        public string Problem
        {
            get => order.Problem;
            set => order.Problem = value;
        }

        public DateTime ReceptionDate
        {
            get => order.ReceptionDate;
            set => order.ReceptionDate = value;
        }

        public decimal Cost
        {
            get => order.Cost;
            set => order.Cost = value;
        }

        public decimal Income
        {
            get => order.Income;
            set => order.Income = value;
        }

        public string Note
        {
            get => order.Note;
            set => order.Note = value;
        }

        public IEnumerable<Worker> Workers
        {
            get => workers;
        }

        public IEnumerable<Worker> SelectedWorkers
        {
            get => workers;
            set => selectedWorkers = value;
        }
        #endregion
    }
}
