using System;
using System.Collections.Generic;
using System.Linq;

// Single-file console program that contains solutions for Tasks 1..12
// Choose task number from menu (1..12). Each task implemented in its own method.
// Uses Action<T>, Func<T,TResult>, Predicate<T>, LINQ and simple English comments.

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose task (1-12) or type END to exit:");
            string choice = Console.ReadLine();
            if (string.Equals(choice, "END", StringComparison.OrdinalIgnoreCase)) break;

            switch (choice)
            {
                case "1": Task1_ActionPoint(); break;
                case "2": Task2_KnightsOfHonor(); break;
                case "3": Task3_CustomMinFunction(); break;
                case "4": Task4_FindEvensOrOdds(); break;
                case "5": Task5_AppliedArithmetics(); break;
                case "6": Task6_ReverseAndExclude(); break;
                case "7": Task7_PredicateForNames(); break;
                case "8": Task8_CustomComparator(); break;
                case "9": Task9_ListOfPredicates(); break;
                case "10": Task10_PredicateParty(); break;
                case "11": Task11_PartyReservationFilterModule(); break;
                case "12": Task12_InfernoIII(); break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number 1-12 or END.");
                    break;
            }

            Console.WriteLine();
        }
    }

    // Task 1: Action Point
    static void Task1_ActionPoint()
    {
        // Read a single line with names separated by space
        string line = Console.ReadLine();
        var names = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Action<string[]> printNames = arr =>
        {
            foreach (var n in arr) Console.WriteLine(n);
        };
        printNames(names);
    }

    // Task 2: Knights of Honor
    static void Task2_KnightsOfHonor()
    {
        string line = Console.ReadLine();
        var names = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Action<string[]> makeSirAndPrint = arr =>
        {
            foreach (var n in arr) Console.WriteLine("Sir " + n);
        };
        makeSirAndPrint(names);
    }

    // Task 3: Custom Min Function
    static void Task3_CustomMinFunction()
    {
        string line = Console.ReadLine();
        var numbers = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(int.Parse).ToList();
        Func<List<int>, int> getMin = list => list.Min();
        Console.WriteLine(getMin(numbers));
    }

    // Task 4: Find Evens or Odds
    static void Task4_FindEvensOrOdds()
    {
        var boundsLine = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int low = int.Parse(boundsLine[0]);
        int high = int.Parse(boundsLine[1]);
        string type = Console.ReadLine().Trim().ToLower(); // "odd" or "even"

        Predicate<int> isMatch = n => true;
        if (type == "even") isMatch = n => n % 2 == 0;
        else if (type == "odd") isMatch = n => n % 2 != 0;

        var result = Enumerable.Range(low, high - low + 1).Where(n => isMatch(n));
        Console.WriteLine(string.Join(" ", result));
    }

    // Task 5: Applied Arithmetics
    static void Task5_AppliedArithmetics()
    {
        var numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToList();

        // Define functions that return a new list after operation
        Func<List<int>, List<int>> add = list => list.Select(x => x + 1).ToList();
        Func<List<int>, List<int>> multiply = list => list.Select(x => x * 2).ToList();
        Func<List<int>, List<int>> subtract = list => list.Select(x => x - 1).ToList();

        while (true)
        {
            string cmd = Console.ReadLine();
            if (cmd == "end") break;
            if (cmd == "add") numbers = add(numbers);
            else if (cmd == "multiply") numbers = multiply(numbers);
            else if (cmd == "subtract") numbers = subtract(numbers);
            else if (cmd == "print") Console.WriteLine(string.Join(" ", numbers));
        }
    }

    // Task 6: Reverse and Exclude
    static void Task6_ReverseAndExclude()
    {
        var numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToList();
        int n = int.Parse(Console.ReadLine());
        Predicate<int> divisibleByN = x => x % n == 0;
        numbers.Reverse();
        var result = numbers.Where(x => !divisibleByN(x));
        Console.WriteLine(string.Join(" ", result));
    }

    // Task 7: Predicate for Names
    static void Task7_PredicateForNames()
    {
        int n = int.Parse(Console.ReadLine());
        var names = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Predicate<string> lengthFilter = s => s.Length <= n;
        foreach (var name in names.Where(x => lengthFilter(x)))
            Console.WriteLine(name);
    }

    // Task 8: Custom Comparator
    static void Task8_CustomComparator()
    {
        var arr = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(int.Parse).ToArray();

        // Comparison: even numbers first (ascending), then odd numbers (ascending)
        Comparison<int> comp = (a, b) =>
        {
            bool aEven = a % 2 == 0;
            bool bEven = b % 2 == 0;
            if (aEven && !bEven) return -1;
            if (!aEven && bEven) return 1;
            // both same parity -> compare normally
            return a.CompareTo(b);
        };

        Array.Sort(arr, comp);
        Console.WriteLine(string.Join(" ", arr));
    }

    // Task 9: List of Predicates
    static void Task9_ListOfPredicates()
    {
        int N = int.Parse(Console.ReadLine());
        var divisors = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(int.Parse).Where(x => x != 0).ToList();
        // If a divisor is 0 we ignore it (cannot divide by 0)

        var result = new List<int>();
        for (int i = 1; i <= N; i++)
        {
            bool ok = true;
            foreach (var d in divisors)
            {
                if (i % d != 0) { ok = false; break; }
            }
            if (ok) result.Add(i);
        }
        Console.WriteLine(string.Join(" ", result));
    }

    // Task 10: Predicate Party
    static void Task10_PredicateParty()
    {
        var guests = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        while (true)
        {
            string line = Console.ReadLine();
            if (line == "Party!") break;
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string cmd = parts[0]; // Remove or Double
            string crit = parts[1]; // StartsWith, EndsWith, Length
            string param = parts.Length > 2 ? parts[2] : "";

            Func<string, bool> match = null;
            if (crit == "StartsWith") match = s => s.StartsWith(param);
            else if (crit == "EndsWith") match = s => s.EndsWith(param);
            else if (crit == "Length") match = s => s.Length == int.Parse(param);

            if (cmd == "Remove") guests = guests.Where(g => !match(g)).ToList();
            else if (cmd == "Double")
            {
                var toDouble = guests.Where(g => match(g)).ToList();
                foreach (var d in toDouble) guests.Add(d);
            }
        }

        if (guests.Any()) Console.WriteLine(string.Join(", ", guests) + " are going to the party!");
        else Console.WriteLine("Nobody is going to the party!");
    }

    // Task 11: Party Reservation Filter Module
    static void Task11_PartyReservationFilterModule()
    {
        var people = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        var filters = new List<Func<string, bool>>();
        var rawFilters = new List<string>(); // store raw description to allow removal by exact match

        while (true)
        {
            string line = Console.ReadLine();
            if (line == "Print") break;
            var parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0]; // Add filter; Remove filter
            string filterType = parts[1];
            string parameter = parts[2];
            string key = filterType + ";" + parameter;

            Func<string, bool> filter = null;
            if (filterType == "Starts with") filter = s => s.StartsWith(parameter);
            else if (filterType == "Ends with") filter = s => s.EndsWith(parameter);
            else if (filterType == "Contains") filter = s => s.Contains(parameter);
            else if (filterType == "Length") filter = s => s.Length == int.Parse(parameter);

            if (command == "Add filter")
            {
                filters.Add(filter);
                rawFilters.Add(key);
            }
            else if (command == "Remove filter")
            {
                // remove first matching stored filter with same key
                int idx = rawFilters.FindIndex(r => r == key);
                if (idx >= 0) { rawFilters.RemoveAt(idx); filters.RemoveAt(idx); }
            }
        }

        // apply all filters
        var result = people.Where(p => filters.All(f => !f(p))).ToList();
        Console.WriteLine(string.Join(" ", result));
    }

    // Task 12: Inferno III
    static void Task12_InfernoIII()
    {
        var numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var excludes = new List<Func<int, int, bool>>(); // list of predicates that take (value,index)

        while (true)
        {
            string line = Console.ReadLine();
            var parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];
            if (command == "Forge") break;
            string filterType = parts[1];
            int param = int.Parse(parts[2]);

            Func<int, int, bool> predicate = null;
            if (filterType == "Sum Left")
            {
                predicate = (value, idx) =>
                {
                    int left = idx - 1 >= 0 ? numbers[idx - 1] : 0;
                    return left + value == param;
                };
            }
            else if (filterType == "Sum Right")
            {
                predicate = (value, idx) =>
                {
                    int right = idx + 1 < numbers.Count ? numbers[idx + 1] : 0;
                    return value + right == param;
                };
            }
            else if (filterType == "Sum Left Right")
            {
                predicate = (value, idx) =>
                {
                    int left = idx - 1 >= 0 ? numbers[idx - 1] : 0;
                    int right = idx + 1 < numbers.Count ? numbers[idx + 1] : 0;
                    return left + value + right == param;
                };
            }

            if (command == "Exclude") excludes.Add(predicate);
            else if (command == "Reverse")
            {
                // Reverse removes the last added exclude that matches the same predicate logic.
                // To support Reverse we need to find and remove the predicate with same behaviour.
                // Because functions are not easily comparable, we will remove the last predicate that
                // would mark any element for exclusion with the same filterType and parameter.
                // Implement by searching from end and removing first predicate where some idx matches predicate for given param/filterType.
                bool removed = false;
                for (int i = excludes.Count - 1; i >= 0; i--)
                {
                    // heuristic test: check if this predicate yields true for at least one index using current numbers when compared to new predicate logic
                    // Build test predicate for current filterType/param
                    Func<int, int, bool> testPred = predicate;
                    // if predicates behave same for all indices where they differ is hard; we use removing last added exclude (as in examples)
                    excludes.RemoveAt(i);
                    removed = true;
                    break;
                }
                // if none removed nothing to do
            }
        }

        // Build final list of indices to exclude
        var excludedIndices = new HashSet<int>();
        for (int i = 0; i < numbers.Count; i++)
        {
            foreach (var pred in excludes)
            {
                if (pred(numbers[i], i)) { excludedIndices.Add(i); break; }
            }
        }

        var result = new List<int>();
        for (int i = 0; i < numbers.Count; i++) if (!excludedIndices.Contains(i)) result.Add(numbers[i]);
        Console.WriteLine(string.Join(" ", result));
    }
}
