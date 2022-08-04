using System.Collections.Generic;

namespace MyApp
{
    public interface IOrderService
    {
        void PlaceOrder(List<string> ordered);
    }
}
