using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows.Input;

namespace MyApp
{
    public class CustomerOrderItemViewModel : ObservableRecipient
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

        /// <summary>
        ///   回调事件，当客户点餐订单内容发生变化时被触发
        /// </summary>
        public Action OnOrderChangedCallback { get; set; }

        private int _numOrdered;

        public int NumOrdered
        {
            get => _numOrdered;
            set
            {
                bool orderIsChanged = SetProperty(ref _numOrdered, value);
                if (!orderIsChanged)
                {
                    return;
                }
                string details = $"行号={RowSerialNum} 菜名={Dish.Name} 份数={_numOrdered}";
                _ = Messenger.Send(new CustomerOrderChangedMessage(details));
            }
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

        private string customerPreferedOpinions;
        public string CustomerPreferedOpinions
        {
            get => customerPreferedOpinions;
            set => SetProperty(ref customerPreferedOpinions, value);
        }
    }
}
