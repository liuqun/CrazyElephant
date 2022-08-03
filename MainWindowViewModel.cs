using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MyApp
{
    public class MainWindowViewModel : ObservableObject
    {
        private int total = 0;
        public int Total
        {
            get => total;
            set => SetProperty(ref total, value);
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
            IDataService service = new XmlDataService();
            List<Dish> dishes = service.FindAllDishes();
            DishMenu = new List<CustomerOrderItemViewModel>();
            int i = 1;
            foreach (Dish dish in dishes)
            {
                CustomerOrderItemViewModel item = new CustomerOrderItemViewModel
                {
                    Dish = dish,
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

        public ICommand PlaceOrderCommand { get; private set; }

        public ICommand SelectMenuItemCommand { get; private set; }

        private void PlaceOrder()
        {
            IEnumerable<CustomerOrderItemViewModel> ordered = DishMenu.Where(menuItem => menuItem.Selected);
            List<string> orderedDishNameList = ordered.Select(menuItem => menuItem.Dish.Name).ToList();
            IOrderService orderService = new MockOrderService();
            orderService.PlaceOrder(orderedDishNameList);
            _ = System.Windows.MessageBox.Show("提示：下单成功！");
        }

        private void SelectMenuItem()
        {
            int cnt = dishMenu.Count(item => item.Selected);
            Total = cnt;
        }

        //private void OrderAdd()
        //{
        //    int cnt = dishMenu.Count(item => item.Selected);
        //    Total = cnt;
        //}

        public MainWindowViewModel()
        {
            LoadRestaurant();
            LoadDishMenu();
            PlaceOrderCommand = new RelayCommand(new Action(PlaceOrder));
            SelectMenuItemCommand = new RelayCommand(new Action(SelectMenuItem));
            
        }
    }
}
