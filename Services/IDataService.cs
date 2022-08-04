using System.Collections.Generic;

namespace MyApp.Services
{
    public interface IDataService
    {
        List<Dish> FindAllDishes();
    }
}
