using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace Lib.Selection
{
    // C# program in-place Merge Sort
// sum.
    class PizzaPlanet {

        /* Driver code */
        public static void PrintOutput()
        {
            JArray obj = JArray.Parse(File.ReadAllText("./Code-Screen/Data.json"));
            IList<Person> people = obj.ToObject<IList<Person>>();
            var DeptGroup = from row in people
                                group row by row.department into _g
                                select new
                                {
                                    DepartmentName = _g.Key,
                                    EmployeeCount = _g.Count(),
                                    PineappleCount = (from top in _g.SelectMany(x => x.toppings)
                                                    where top == "Pineapple"
                                                    select top).Count(),
                                    Combos =        _g.Select(x => x.toppings)
                                };
            var max = DeptGroup.Select(x => x.PineappleCount).Max();
            var MaxDept = DeptGroup.Where(x => x.PineappleCount == max);
            Console.WriteLine("The " + MaxDept.First().DepartmentName + " department has the largest number of employees who like Pineapple on their pizzas");

            int engineeringDepartmentEmployeeCount = DeptGroup
                                                    .Where(x => x.DepartmentName == "Engineering")
                                                    .Select(x => x.EmployeeCount)
                                                    .DefaultIfEmpty(0)
                                                    .Sum();
            decimal pizzaToOrder = (decimal)engineeringDepartmentEmployeeCount / 4;
            Console.WriteLine("You need to order " + Math.Ceiling(pizzaToOrder) + " pizzas to feed the engineering department");
            
            
            foreach(var row in DeptGroup)
            {
                Dictionary<string,int> count = new Dictionary<string, int>();
                foreach(var comb in row.Combos){
                    string key = string.Join(",",comb);
                    if(count.ContainsKey(key)){
                        count[key] += 1;
                    }
                    else{
                        count.Add(key, 1);
                    }
                }
                var mostPopularCount = count.Max(x => x.Value);
                var mostPopular = count.Where(x => x.Value == mostPopularCount).First();
                Console.WriteLine("The " +  mostPopular.Key + " pizza topping combination is the most popular in the " + row.DepartmentName + " Department");
            }
        }
    }
    public class Person
    {
        public string name{get;set;}
        public string department{get;set;}
        public string[] toppings{get;set;}
    }
}
