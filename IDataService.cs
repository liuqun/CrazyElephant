using System.Collections.Generic;

namespace MyApp
{
    public interface IDataService
    {
        List<Dish> FindAllDishes();
    }
}
