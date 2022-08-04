using System.Collections.Generic;

namespace MyApp.Services
{
    public interface IOrderService
    {
        void PlaceOrder(List<string> ordered);
    }
}
