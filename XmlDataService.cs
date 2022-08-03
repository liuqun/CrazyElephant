using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace MyApp
{
    public class XmlDataService : IDataService
    {
        List<Dish> IDataService.FindAllDishes()
        {
            List<Dish> list = new List<Dish>();
            string dataDir = @"Data";
            string dataFile = @"Data.xml";
            string xmlFilePath = dataDir + @"\" + dataFile;
            string xmlFileFullPath = Path.Combine(Environment.CurrentDirectory, xmlFilePath);
            XDocument xDoc = XDocument.Load(xmlFileFullPath);
            IEnumerable<XElement> dishes = xDoc.Descendants("Dish");
            foreach (XElement d in dishes)
            {
                Dish dish = new Dish
                {
                    Name = d.Element("Name").Value,
                    Category = d.Element("Category").Value,
                    Price = d.Element("Price").Value,
                    Description = d.Element("Description").Value,
                    Score = d.Element("Score").Value
                };
                list.Add(dish);
            }
            return list;
        }
    }
}
