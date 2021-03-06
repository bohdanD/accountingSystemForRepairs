﻿using DALayer;
using Entities;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using UILayer.Commands;
using UILayer.Properties;

namespace UILayer.ViewModels
{
    public class StatisticViewModel : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;

        private Spending spending = new Spending() { Date = DateTime.Today};
        private DateTime fromDate;
        private DateTime toDate;
        private ICommand addSpendinfCommand;

        public StatisticViewModel()
        {
            unitOfWork = new UnitOfWork();
            addSpendinfCommand = new AddCommand(() => true, AddSpending);
            toDate = DateTime.Today.Date;
            fromDate = toDate.AddDays(-30);
            AssignStatistic();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Add spending
        private void AddSpending()
        {
            try
            {
                unitOfWork.SpendingRepository.Add(spending);
                MessageBox.Show(Resources.MsgAddSpendingSuccess, Resources.MsgSuccess, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MsgAddSpendingFail, Resources.MsgError
                    , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand AddSpendingCommand
        {
            get { return addSpendinfCommand; }
        }

        public string Descriprtion
        {
            get { return spending.Description; }
            set { spending.Description = value; }
        }

        public decimal Cost
        {
            get { return spending.Cost; }
            set { spending.Cost = value; }
        }

        public DateTime Date
        {
            get { return spending.Date; }
            set { spending.Date = value; }
        }
        #endregion

        #region statistic
        private void AssignStatistic()
        {
            OrdersCost = unitOfWork.OrderRepository
                .GetDoneOrdersCost(fromDate, toDate);
            OrdersIncome = unitOfWork.OrderRepository
                .GetDoneOrdersIncome(fromDate, toDate);
            SpendingsCost = unitOfWork.SpendingRepository
                .GetSpendingsCost(fromDate, toDate);
            OrdersCount = unitOfWork.OrderRepository
                .GetOrdersCount(fromDate, toDate);
            DoneOrdersCount = unitOfWork.OrderRepository
                .GetDoneOrdersCount(fromDate, toDate);
            OnPropertyChanged("OrdersCost");
            OnPropertyChanged("OrdersIncome");
            OnPropertyChanged("SpendingsCost");
            OnPropertyChanged("OrdersCount");
            OnPropertyChanged("DoneOrdersCount");
        }

        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                fromDate = value;
                AssignStatistic();
            }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                toDate = value;
                AssignStatistic();
            }
        }

        public decimal OrdersCost { get; set; }

        public decimal OrdersIncome { get; set; }

        public decimal SpendingsCost { get; set; }

        public int OrdersCount { get; set; }

        public int DoneOrdersCount { get; set; }
        #endregion
    }
}
