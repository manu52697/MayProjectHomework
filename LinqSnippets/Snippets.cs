using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LinqSnippets
{



    public class Snippets
    {



        public static void BasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "WV California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;
            
            foreach(var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is audi (SELECT AUDIS)
            var audiList = from car in cars 
                           where car.Contains("Audi") 
                           select car;
            foreach( var audi in audiList)
            {
                Console.WriteLine(audi);
            }

            
        }

        // Number examples
        public static void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each number multiplied by 3
            // take al nums but 9
            // Sort ascending

            var processedNumberList = numbers
                                            .Select(x => x * 3)
                                            .Where(x => x != 9)
                                            .OrderBy(x => x);
        }


        public static void searchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1. First element
            string first = textList.First();

            // 2. First instance of "c"
            var cText = textList.First(text => text == "c");

            // 3. First element that contains the letter "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains "z" or a default value
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // Default value is ""

            // 5. Last element that contains "z" or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));


            // 6. Single values
            var singleValues = textList.Single();
            var singleOrDefault = textList.SingleOrDefault();


            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Except
            var except = evenNumbers.Except(otherEvenNumbers);

        }

        public static void MultipleSelects()
        {

            // SELECT MANY

            string[] myOpinions =
            {
                "Optinion 1, text 1",
                "Optinion 2, text 2",
                "Optinion 3, text 3",
                "Optinion 4, text 4",
            };

            var myOpinionSelection = myOpinions.SelectMany(op => op.Split(","));

            var enterprises = new[] 
            {
                new Enterprise()
                {
                    Id=1,
                    Name="Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id=1,
                            Name = "Martin",
                            Email= "martin@example.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id=1,
                            Name = "Pepe",
                            Email= "pepe@example.com",
                            Salary = 1000
                        },
                        new Employee()
                        {
                            Id=1,
                            Name = "Juanjo",
                            Email= "juanjo@example.com",
                            Salary = 2000
                        }
                    }
                },
                new Enterprise()
                {
                    Id=1,
                    Name="Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id=4,
                            Name = "Ana",
                            Email= "ana@example.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id=5,
                            Name = "Maria",
                            Email= "maria@example.com",
                            Salary = 1500
                        },
                        new Employee()
                        {
                            Id=6,
                            Name = "Marta",
                            Email= "marta@example.com",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtain all employees of all enterprises
            var employeeList = enterprises.SelectMany(ent => ent.Employees);

            // Know if any list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(ent => ent.Employees.Any());

            // Do all have employees with salaries over 1k/ month
            bool hasEmployeeWithSalaryOver1000 = enterprises.Any(ent => ent.Employees.Any(emp => emp.Salary >= 1000));

        }

        public static void LinqCollections()
        {
            var firstList = new List<string>() {  "a", "b", "c" };
            var secondList = new List<string>() { "a", "b", "d" };

            // INNER JOIN 
            var commonResult = from element in firstList
                               join secondElement in secondList on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement });


            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList on element equals secondElement
                                into tempList
                                from tempElement in tempList.DefaultIfEmpty()
                                where element != tempElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };

            // OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                join element in firstList on secondElement equals element
                                into tempList
                                from tempElement in tempList.DefaultIfEmpty()
                                where secondElement != tempElement
                                select new { Element = secondElement };

            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        public static void SkipTakeLinq()
        {
            var myList = new[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            // SKIP 

            var skipTwoFirstValues = myList.Skip(2); // {3, 4, 5, 6, 7, 8, 9, 10}

            var skipLastTwo = myList.SkipLast(2); // {1, 2, 3, 4, 5, 6, 7, 8}

            var skipWhileSmallerThanFour = myList.SkipWhile(s => s < 4); // { 4, 5, 6, 7, 8, 9, 10}

            // TAKE 
            var takeFirstTwo = myList.Take(2);

            var takeLastTwo = myList.TakeLast(2);

            var takeSmallerThanFour = myList.TakeWhile(x => x < 4);

        }

        // TODO:

        // Variables

        // ZIP

        // Repeat

        // ALL

        // AGREGATE

        // DISTINCT

        // GROUPBY

    }
}