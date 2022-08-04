using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace MyApp
{
    public class CustomerOrderItemViewModel : ObservableObject
    {
        public Dish Dish { get; set; }

        private bool _selected;

        public bool Selected
        {
            get => _numOrdered > 0;
            set
            {
                if (!value)
                {
                    NumOrdered = 0;
                }
                else if (0 == NumOrdered)
                {
                    NumOrdered = 1;
                }
                SetProperty(ref _selected, value);
            }
        }

        private int _numOrdered;

        public int NumOrdered
        {
            get => _numOrdered;
            set => SetProperty(ref _numOrdered, value);
        }

        public int RowSerialNum { get; set; }

        public ICommand OrderAddCommand { get; private set; }

        public ICommand OrderDecCommand { get; private set; }

        private void OrderAdd()
        {
            NumOrdered++;
            Selected = NumOrdered > 0;
        }

        private void OrderDecrease()
        {
            NumOrdered -= 1;
            if (NumOrdered < 0)
            {
                NumOrdered = 0;
            }
            Selected = NumOrdered > 0;
        }

        public CustomerOrderItemViewModel()
        {
            OrderAddCommand = new RelayCommand(new Action(OrderAdd));
            OrderDecCommand = new RelayCommand(new Action(OrderDecrease));
        }
    }
}
