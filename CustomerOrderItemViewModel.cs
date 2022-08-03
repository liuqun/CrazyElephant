using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace MyApp
{
    public class CustomerOrderItemViewModel : ObservableObject
    {
        public Dish Dish { get; set; }

        private bool selected;

        public bool Selected
        {
            get => selected;
            set => SetProperty(ref selected, value);
        }

        private int numOrdered;

        public int NumOrdered
        {
            get => numOrdered;
            set => SetProperty(ref numOrdered, value);
        }

        public int RowSerialNum { get; set; }

        public ICommand OrderAddCommand { get; private set; }

        public ICommand OrderDecCommand { get; private set; }

        private void OrderAdd() => NumOrdered++;

        private void OrderDecrease()
        {
            if (--NumOrdered < 0)
            {
                NumOrdered = 0;
            }
            
        }

        public CustomerOrderItemViewModel()
        {
            OrderAddCommand = new RelayCommand(new Action(OrderAdd));
            OrderDecCommand = new RelayCommand(new Action(OrderDecrease));
        }
    }
}
