using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExercises
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Class { get; set; } = "";
        public int Points { get; set; }

        public override string ToString() =>
            $"{Id}: {Name}, {Age} years, class {Class}, {Points} points";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Anna", Age = 19, Class = "A", Points = 85 },
                new Student { Id = 2, Name = "Bertil", Age = 22, Class = "B", Points = 72 },
                new Student { Id = 3, Name = "Cecilia", Age = 20, Class = "A", Points = 91 },
                new Student { Id = 4, Name = "David", Age = 24, Class = "C", Points = 64 },
                new Student { Id = 5, Name = "Eva", Age = 19, Class = "B", Points = 85 },
                new Student { Id = 6, Name = "Felix", Age = 21, Class = "A", Points = 77 },
                new Student { Id = 7, Name = "Gustav", Age = 23, Class = "C", Points = 58 },
                new Student { Id = 8, Name = "Hanna", Age = 20, Class = "B", Points = 95 }
            };

            Console.WriteLine("linq exercises with students\n");

            //where
            Heading("where");

            var olderThan20 = students.Where(s => s.Age > 20);
            Console.WriteLine("students older than 20:");
            Print(olderThan20);

            var moreThan80 = students.Where(s => s.Points > 80);
            Console.WriteLine("students with more than 80 points:");
            Print(moreThan80);

            //select
            Heading("select");

            var names = students.Select(s => s.Name);
            Console.WriteLine("names: " + string.Join(", ", names));

            var namesAndPoints = students.Select(s => new { s.Name, s.Points });
            Console.WriteLine("names and points (anonymous type):");
            foreach (var x in namesAndPoints)
                Console.WriteLine($"  {x.Name} - {x.Points}");

            //orderby
            Heading("orderby");

            var byAge = students.OrderBy(s => s.Age);
            Console.WriteLine("sorted by age:");
            Print(byAge);

            var byPoints = students.OrderByDescending(s => s.Points);
            Console.WriteLine("sorted by points, highest first:");
            Print(byPoints);

            //coutn
            Heading("count");

            Console.WriteLine($"number of students: {students.Count()}");
            Console.WriteLine($"number in class a: {students.Count(s => s.Class == "A")}");

            //take
            Heading("take");

            Console.WriteLine("the first three students:");
            Print(students.Take(3));

            var topTwo = students.OrderByDescending(s => s.Points).Take(2).Select(s => s.Points);
            Console.WriteLine("the two best scores: " + string.Join(", ", topTwo));

            //disticnt
            Heading("distinct");

            var classes = students.Select(s => s.Class).Distinct();
            Console.WriteLine("unique classes: " + string.Join(", ", classes));

            var ages = students.Select(s => s.Age).Distinct().OrderBy(a => a);
            Console.WriteLine("unique ages: " + string.Join(", ", ages));

            //any all
            Heading("any / all");

            bool anyWith100 = students.Any(s => s.Points == 100);
            Console.WriteLine($"is there any student with 100 points? {(anyWith100 ? "yes" : "no")}");

            bool allAtLeast50 = students.All(s => s.Points >= 50);
            Console.WriteLine($"do all students have at least 50 points? {(allAtLeast50 ? "yes" : "no")}");

            //firstOrDefault / Last / Single
            Heading("firstordefault / last / single");

            var firstInB = students.FirstOrDefault(s => s.Class == "B");
            Console.WriteLine($"first student in class b: {firstInB?.ToString() ?? "none"}");

            var last = students.Last();
            Console.WriteLine($"last student in the list: {last}");

            int searchId = 4;
            var withId = students.Single(s => s.Id == searchId);
            Console.WriteLine($"student with id {searchId}: {withId}");

            //groupby
            Heading("groupby");

            var perClass = students.GroupBy(s => s.Class).OrderBy(g => g.Key);
            foreach (var group in perClass)
                Console.WriteLine($"class {group.Key}: {string.Join(", ", group.Select(s => s.Name))}");

            //sum/average/min/max
            Heading("sum / average / min / max");

            Console.WriteLine($"sum of all points: {students.Sum(s => s.Points)}");
            Console.WriteLine($"average points: {students.Average(s => s.Points):F2}");
            Console.WriteLine($"lowest points: {students.Min(s => s.Points)}");
            Console.WriteLine($"highest points: {students.Max(s => s.Points)}");
        }

        static void Heading(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"<===|| {title} ||===>");
        }

        static void Print(IEnumerable<Student> list)
        {
            foreach (var s in list)
                Console.WriteLine("  " + s);
        }
    }
}
