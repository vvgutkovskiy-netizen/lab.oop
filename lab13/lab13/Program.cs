using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Виберіть завдання (1-12) або 0 для виходу:");
            Console.Write("Вибір: ");
            var choice = Console.ReadLine();
            if (choice == "0" || choice == null) break;

            Console.WriteLine();
            switch (choice.Trim())
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "4": Task4(); break;
                case "5": Task5(); break;
                case "6": Task6(); break;
                case "7": Task7(); break;
                case "8": Task8(); break;
                case "9": Task9(); break;
                case "10": Task10(); break;
                case "11": Task11(); break;
                case "12": Task12(); break;
                default:
                    Console.WriteLine("Невірний вибір. Введіть число від 1 до 12 або 0 для виходу.");
                    break;
            }
        }
    }

    // ---------- HELPERS ----------
    static string ReadLineNonNull()
    {
        var s = Console.ReadLine();
        return s ?? string.Empty;
    }

    // ---------- TASK 1 ----------
    // Вивести всіх студентів з групи номер 2. Використовуйте LINQ.
    // Впорядкуйте студентів за іменем (FirstName) — щоб відповідати прикладу.
    static void Task1()
    {
        Console.WriteLine("Вводьте дані (First Last Group) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last, int Group)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            if (!int.TryParse(p[2], out int grp)) continue;
            students.Add((p[0], p[1], grp));
        }

        // LINQ + lambda: filter group==2 and order by First (to match example)
        var res = students.Where(s => s.Group == 2).OrderBy(s => s.First);
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 2 ----------
    // Студенти, у яких First < Last лексикографічно. Порядок появи.
    static void Task2()
    {
        Console.WriteLine("Вводьте дані (First Last) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 2) continue;
            students.Add((p[0], p[1]));
        }

        // LINQ Where with lambda retains input order
        var res = students.Where(s => string.Compare(s.First, s.Last, StringComparison.Ordinal) < 0);
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 3 ----------
    // Students by age 18..24. Return First Last Age in input order.
    static void Task3()
    {
        Console.WriteLine("Вводьте дані (First Last Age) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last, int Age)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            if (!int.TryParse(p[2], out int age)) continue;
            students.Add((p[0], p[1], age));
        }

        var res = students.Where(s => s.Age >= 18 && s.Age <= 24);
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last} {s.Age}");
    }

    // ---------- TASK 4 ----------
    // Sort students: last asc, then first desc (use lambda LINQ)
    static void Task4()
    {
        Console.WriteLine("Вводьте дані (First Last) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 2) continue;
            students.Add((p[0], p[1]));
        }

        // LINQ with lambdas
        var sorted = students.OrderBy(s => s.Last).ThenByDescending(s => s.First);
        foreach (var s in sorted) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 5 ----------
    // Filter by email domain @gmail.com in order of appearance.
    static void Task5()
    {
        Console.WriteLine("Вводьте дані (First Last Email) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last, string Email)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            students.Add((p[0], p[1], p[2]));
        }

        var res = students.Where(s => s.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase));
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 6 ----------
    // Filter by phone starting with 02 or +3592 (Sofia prefixes)
    static void Task6()
    {
        Console.WriteLine("Вводьте дані (First Last Phone) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last, string Phone)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            students.Add((p[0], p[1], p[2]));
        }

        var res = students.Where(s => s.Phone.StartsWith("02") || s.Phone.StartsWith("+3592"));
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 7 ----------
    // Excellent students — хоча б одна оцінка == 6
    static void Task7()
    {
        Console.WriteLine("Вводьте дані (First Last grade1 grade2 ...) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last, List<int> Grades)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            var grades = p.Skip(2).Select(x => int.Parse(x)).ToList();
            students.Add((p[0], p[1], grades));
        }

        var res = students.Where(s => s.Grades.Any(g => g == 6));
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 8 ----------
    // Weak students — have at least 2 grades <= 3
    static void Task8()
    {
        Console.WriteLine("Вводьте дані (First Last grade1 grade2 ...) по одному на рядок. Закінчіть 'END'.");
        var students = new List<(string First, string Last, List<int> Grades)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            var grades = p.Skip(2).Select(x => int.Parse(x)).ToList();
            students.Add((p[0], p[1], grades));
        }

        var res = students.Where(s => s.Grades.Count(g => g <= 3) >= 2);
        foreach (var s in res) Console.WriteLine($"{s.First} {s.Last}");
    }

    // ---------- TASK 9 ----------
    // Students enrolled in 2014 or 2015 — faculty number has "14" or "15" at positions 5-6 (indices 4-5)
    static void Task9()
    {
        Console.WriteLine("Вводьте дані (FacNum grade1 grade2 ...) по одному на рядок. Закінчіть 'END'.");
        var list = new List<(string FacNum, List<int> Grades)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 2) continue;
            var grades = p.Skip(1).Select(x => int.Parse(x)).ToList();
            list.Add((p[0], grades));
        }

        // LINQ with lambda: filter by substring and print grades
        var res = list.Where(l => l.FacNum.Length >= 6 && (l.FacNum.Substring(4, 2) == "14" || l.FacNum.Substring(4, 2) == "15"));
        foreach (var s in res) Console.WriteLine(string.Join(" ", s.Grades));
    }

    // ---------- TASK 10 ----------
    // Group by group: output "{group} - {name1}, {name2}, ..."
    static void Task10()
    {
        Console.WriteLine("Вводьте дані (First Last Group) по одному на рядок. Закінчіть 'END'.");
        var people = new List<(string FullName, int Group)>();
        string line;
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            if (!int.TryParse(p[2], out int grp)) continue;
            people.Add(($"{p[0]} {p[1]}", grp));
        }

        // GroupBy + OrderBy groups ascending
        var grouped = people.GroupBy(p => p.Group).OrderBy(g => g.Key);
        foreach (var g in grouped)
        {
            var names = string.Join(", ", g.Select(x => x.FullName));
            Console.WriteLine($"{g.Key} - {names}");
        }
    }

    // ---------- TASK 11 ----------
    // Join specialties with students. Read specialties until "Students:", then students until "END".
    static void Task11()
    {
        Console.WriteLine("Вводьте спеціальності (формат: <SpecialtyName> <FacNum>). Для переходу до студентів введіть 'Students:'.");
        var specs = new List<(string Specialty, string FacNum)>();
        string line;
        while ((line = ReadLineNonNull()) != "Students:")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) continue;
            var fac = parts.Last();
            var name = string.Join(" ", parts.Take(parts.Length - 1));
            specs.Add((name, fac));
        }

        Console.WriteLine("Тепер вводьте студентів (format: FacNum First Last). Закінчіть 'END'.");
        var students = new List<(string FacNum, string First, string Last)>();
        while ((line = ReadLineNonNull()) != "END")
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 3) continue;
            students.Add((p[0], p[1], p[2]));
        }

        // LINQ join
        var joined = students.Join(
            specs,
            st => st.FacNum,
            sp => sp.FacNum,
            (st, sp) => new { Name = st.First + " " + st.Last, FacNum = st.FacNum, Specialty = sp.Specialty });

        // Sort by student name (alphabetical) as requested
        foreach (var j in joined.OrderBy(x => x.Name))
        {
            Console.WriteLine($"{j.Name} {j.FacNum} {j.Specialty}");
        }
    }

    // ---------- TASK 12 ----------
    // Office Stuff: read n, then n lines of |Company - amount - product|
    static void Task12()
    {
        Console.WriteLine("Введіть число n (кількість записів), потім n рядків у форматі |Company - amount - product|.");
        var first = ReadLineNonNull();
        if (!int.TryParse(first, out int n))
        {
            Console.WriteLine("Перший рядок має бути цілим числом n.");
            return;
        }

        // Use SortedDictionary to order companies alphabetically
        var companies = new SortedDictionary<string, CompanyData>(StringComparer.Ordinal);
        for (int i = 0; i < n; i++)
        {
            var line = ReadLineNonNull();
            if (string.IsNullOrWhiteSpace(line)) { i--; continue; }

            // remove leading/trailing '|'
            var trimmed = line.Trim();
            if (trimmed.StartsWith("|") && trimmed.EndsWith("|"))
                trimmed = trimmed.Trim('|').Trim();

            // split by '-' into three parts
            var parts = trimmed.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(p => p.Trim()).ToArray();
            if (parts.Length < 3) continue;
            var company = parts[0];
            if (!int.TryParse(parts[1], out int amount)) continue;
            var product = parts[2];

            if (!companies.ContainsKey(company)) companies[company] = new CompanyData();
            companies[company].Add(product, amount);
        }

        // Print result: Company: product-sum, product-sum, ...
        foreach (var kv in companies)
        {
            var comp = kv.Key;
            var data = kv.Value;
            var items = data.Order.Select(prod => $"{prod}-{data.Sums[prod]}");
            Console.WriteLine($"{comp}: {string.Join(", ", items)}");
        }
    }

    // ---------- SUPPORT CLASS FOR TASK 12 ----------
    class CompanyData
    {
        
        public List<string> Order { get; } = new List<string>();
        public Dictionary<string, int> Sums { get; } = new Dictionary<string, int>();

        
        public void Add(string product, int amount)
        {
            if (!Sums.ContainsKey(product))
            {
                Sums[product] = 0;
                Order.Add(product);
            }
            Sums[product] += amount;
        }

        // Return products in appearance order
        public IEnumerable<string> OrderSeq => Order;
        public IEnumerable<string> OrderProducts => Order;
        public IEnumerable<string> OrderEnumerable => Order;

        // For convenience
        public IEnumerable<string> OrderProductsLambda() => Order;
        public IEnumerable<string> Order2 => Order;

        // Legacy property used above
        public IEnumerable<string> OrderForPrint => Order;
        // But the code uses 'Order' directly
    }
}
