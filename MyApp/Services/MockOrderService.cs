using System;
using System.Collections.Generic;
using System.IO;

namespace MyApp.Services
{
    public class MockOrderService : IOrderService
    {
        public string MockOutputFileName { get; set; } = @"MyOrder.txt";
        void IOrderService.PlaceOrder(List<string> ordered)
        {
            string[] array = ordered.ToArray();
            string path = Path.Combine(Environment.CurrentDirectory, MockOutputFileName);
            File.WriteAllLines(path, array);
        }
    }
}
