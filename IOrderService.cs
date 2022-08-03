using System.Collections.Generic;

namespace MyApp
{
    interface IOrderService
    {
        void PlaceOrder(List<string> ordered);
    }
}
