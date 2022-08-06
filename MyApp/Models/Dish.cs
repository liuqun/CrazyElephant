namespace MyApp
{
    public class Dish
    {
        // 菜名
        public string Name { get; set; }

        // 菜品分类
        public string Category { get; set; }

        // 菜品描述
        public string Description { get; set; }

        // 售价
        public string Price { get; set; }

        [System.Obsolete]
        public string Score { get; set; }
    }
}
