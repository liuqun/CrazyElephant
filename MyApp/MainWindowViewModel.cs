using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MyApp
{
    public class MainWindowViewModel : ObservableObject
    {
        private string totalCost = "-.--";

        public string TotalCost
        {
            get => totalCost;
            set => SetProperty(ref totalCost, value);
        }

        private Restaurant restaurant;
        public Restaurant Restaurant
        {
            get => restaurant;
            set => SetProperty(ref restaurant, value);
        }

        private List<CustomerOrderItemViewModel> dishMenu;
        public List<CustomerOrderItemViewModel> DishMenu
        {
            get => dishMenu;
            set => SetProperty(ref dishMenu, value);
        }

        private void LoadDishMenu()
        {
            void OnOrderChanged()
            {
                long money = CalculateTotalCost();
                TotalCost = $"{money / 100}.{money % 100:D2}";
                PlaceOrderCommand.NotifyCanExecuteChanged();
            }
            List<Dish> dishes = dataService.FindAllDishes();
            DishMenu = new List<CustomerOrderItemViewModel>();
            int i = 1;
            foreach (Dish dish in dishes)
            {
                CustomerOrderItemViewModel item = new CustomerOrderItemViewModel
                {
                    Dish = dish,
                    OnOrderChangedCallback = OnOrderChanged,
                    RowSerialNum = i
                };
                DishMenu.Add(item);
                i += 1;
            }
        }

        private void LoadRestaurant()
        {
            Restaurant = new Restaurant
            {
                Name = "Crazy大象旗舰店",
                Address = "北京市朝阳区xxx路xxx号",
                PhoneNumber = "010-88886666"
            };
        }

        public RelayCommand PlaceOrderCommand { get; private set; }

        private void PlaceOrder()
        {
            IEnumerable<CustomerOrderItemViewModel> ordered = DishMenu.Where(menuItem => menuItem.Selected);
            List<string> orderedDishNameList = ordered.Select(
                menuItem => $"{menuItem.Dish.Name}\t￥{menuItem.Dish.Price}/份×{menuItem.NumOrdered}份"
                ).ToList();
            orderService.PlaceOrder(orderedDishNameList);
            _ = System.Windows.MessageBox.Show("提示：下单成功！");
        }

        private long CalculateTotalCost()
        {
            long sum = 0;
            List<CustomerOrderItemViewModel> ordered = dishMenu.FindAll(item => item.Selected);
            foreach (CustomerOrderItemViewModel item in ordered)
            {
                int n = item.NumOrdered;
                if (!double.TryParse(item.Dish.Price, out double price))
                {
                    continue;
                }
                if (price <= 0)
                {
                    continue;
                }
                int priceInt32 = Convert.ToInt32(price * 100);
                sum += Convert.ToInt64(priceInt32) * n;
            }
            return sum;
        }

        protected readonly IOrderService orderService;

        protected readonly IDataService dataService;

        public MainWindowViewModel(IDataService dataService, IOrderService orderService)
        {
            this.dataService = dataService;
            this.orderService = orderService;
            LoadRestaurant();
            LoadDishMenu();
            PlaceOrderCommand = new RelayCommand(PlaceOrder, () => CalculateTotalCost() > 0);
        }
    }
}
