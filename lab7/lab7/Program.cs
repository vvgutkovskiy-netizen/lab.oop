using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Lab6_WithInterfaces_Fixed.Task2;

namespace Lab6_WithInterfaces_Fixed
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("Оберіть завдання (1–3), 0 - вихід:");
                Console.WriteLine("1 - Телефонія");
                Console.WriteLine("2 - Утопія (частини 1-3)");
                Console.WriteLine("3 - Військова еліта");
                Console.Write("Ваш вибір: ");
                var c = Console.ReadLine();
                Console.WriteLine();

                if (c == "0") break;
                if (c == "1") Task1.Task1Runner.Run();
                else if (c == "2") Task2.Task2Runner.Run();
                else if (c == "3") Task3.Task3Runner.Run();
                else Console.WriteLine("Невірний вибір.\n");
            }
        }
    }

    // =========================
    // TASK 1 - TELEPHONY
    // =========================
    namespace Task1
    {
        // Інтерфейси (контракти поведінки)
        public interface ICaller
        {
            string Call(string number);
        }

        public interface IBrowser
        {
            string Browse(string url);
        }

        // Клас реалізує обидва інтерфейси
        public class Smartphone : ICaller, IBrowser
        {
            public string Call(string number)
            {
                if (string.IsNullOrWhiteSpace(number) || number.Any(ch => !char.IsDigit(ch)))
                    return "Invalid number!";
                return $"Дзвінок... {number}";
            }

            public string Browse(string url)
            {
                if (string.IsNullOrWhiteSpace(url) || url.Any(char.IsDigit))
                    return "Invalid URL!";
                return $"Перегляд: {url}!";
            }
        }

        public static class Task1Runner
        {
            public static void Run()
            {
                var phone = new Smartphone();
                Console.WriteLine("=== Телефонія ===");
                Console.WriteLine("Введіть номери телефонів через пробіл (перший рядок):");
                Console.Write("Приклад: 0882134215 0882134333 08992134215 0558123 3333 1\nНомери: ");
                var numbers = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("\nВведіть URL-адреси через пробіл (другий рядок):");
                Console.Write("Приклад: http://softuni.bg http://youtube.com http://www.g00gle.com\nСайти: ");
                var urls = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("\n--- Результат ---");
                foreach (var n in numbers) Console.WriteLine(phone.Call(n));
                foreach (var u in urls) Console.WriteLine(phone.Browse(u));
                Console.WriteLine();
            }
        }
    }

    // =========================
    // TASK 2 - UTOPIA (ALL PARTS)
    // =========================
    namespace Task2
    {
        // Інтерфейси
        public interface IIdentifiable { string Id { get; } }
        public interface IBirthable { string BirthDate { get; } } // формат dd/MM/yyyy
        public interface IBuyer { int Food { get; } void BuyFood(); }

        // Класи
        public class Citizen : IIdentifiable, IBirthable, IBuyer
        {
            public string Name { get; }
            public int Age { get; }
            public string Id { get; }
            public string BirthDate { get; }
            public int Food { get; private set; }

            public Citizen(string name, int age, string id, string birthDate)
            {
                Name = name; Age = age; Id = id; BirthDate = birthDate; Food = 0;
            }

            public void BuyFood() => Food += 10;
        }

        public class Robot : IIdentifiable
        {
            public string Model { get; }
            public string Id { get; }
            public Robot(string model, string id) { Model = model; Id = id; }
        }

        public class Pet : IBirthable
        {
            public string Name { get; }
            public string BirthDate { get; }
            public Pet(string name, string birthDate) { Name = name; BirthDate = birthDate; }
        }

        public class Rebel : IBuyer
        {
            public string Name { get; }
            public int Age { get; }
            public string Group { get; }
            public int Food { get; private set; }

            public Rebel(string name, int age, string group) { Name = name; Age = age; Group = group; Food = 0; }
            public void BuyFood() => Food += 5;
        }

        public static class Task2Runner
        {
            public static void Run()
            {
                Console.WriteLine("=== Утопія ===");
                Console.WriteLine("Оберіть частину: 1 - Border Control, 2 - Birthdays, 3 - Food Shortage");
                Console.Write("Частина: ");
                var p = Console.ReadLine();
                Console.WriteLine();

                if (p == "1") Part1_BorderControl();
                else if (p == "2") Part2_Birthdays();
                else if (p == "3") Part3_FoodShortage();
                else Console.WriteLine("Невірна частина.\n");
            }

            // Part 1
            static void Part1_BorderControl()
            {
                Console.WriteLine("Вводьте рядки до 'End'. Формати:");
                Console.WriteLine("<name> <age> <id> (Citizen) або <model> <id> (Robot)");
                var list = new List<IIdentifiable>();

                while (true)
                {
                    var line = Console.ReadLine();
                    if (line == null) return;
                    if (line == "End") break;
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 3 && int.TryParse(parts[1], out _))
                    {
                        list.Add(new Citizen(parts[0], int.Parse(parts[1]), parts[2], "01/01/1900"));
                    }
                    else if (parts.Length == 2)
                    {
                        list.Add(new Robot(parts[0], parts[1]));
                    }
                    else if (parts.Length > 0)
                    {
                        list.Add(new Robot(parts[0], parts.Last()));
                    }
                }

                Console.Write("Введіть суфікс id для фільтрації: ");
                var suffix = Console.ReadLine() ?? "";
                foreach (var item in list.Where(x => x.Id.EndsWith(suffix)))
                    Console.WriteLine(item.Id);

                Console.WriteLine();
            }

            // Part 2
            static void Part2_Birthdays()
            {
                Console.WriteLine("Вводьте рядки до 'End'. Формати:");
                Console.WriteLine("Citizen <name> <age> <id> <birthdate>");
                Console.WriteLine("Robot <model> <id>");
                Console.WriteLine("Pet <name> <birthdate>");
                var births = new List<string>();

                while (true)
                {
                    var line = Console.ReadLine();
                    if (line == null) return;
                    if (line == "End") break;
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;
                    if (parts[0] == "Citizen" && parts.Length >= 5) births.Add(parts[4]);
                    else if (parts[0] == "Pet" && parts.Length >= 3) births.Add(parts[2]);
                }

                Console.Write("Введіть рік (yyyy): ");
                var year = Console.ReadLine() ?? "";

                foreach (var bd in births)
                    if (bd.EndsWith(year))
                        Console.WriteLine(bd);

                Console.WriteLine();
            }

            // Part 3
            static void Part3_FoodShortage()
            {
                Console.WriteLine("Перший рядок: N — кількість людей.");
                if (!int.TryParse(Console.ReadLine(), out int n))
                {
                    Console.WriteLine("Невірне число.");
                    return;
                }

                var buyers = new Dictionary<string, IBuyer>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < n; i++)
                {
                    var line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) { i--; continue; }
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 4) // Citizen
                    {
                        if (int.TryParse(parts[1], out int age))
                            buyers[parts[0]] = new Citizen(parts[0], age, parts[2], parts[3]);
                    }
                    else if (parts.Length == 3) // Rebel
                    {
                        if (int.TryParse(parts[1], out int age))
                            buyers[parts[0]] = new Rebel(parts[0], age, parts[2]);
                    }
                }

                while (true)
                {
                    var name = Console.ReadLine();
                    if (name == null || name == "End") break;
                    if (buyers.TryGetValue(name, out var buyer)) buyer.BuyFood();
                }

                Console.WriteLine(buyers.Values.Sum(x => x.Food));
                Console.WriteLine();
            }
        }
    }

    // =========================
    // TASK 3 - MILITARY ELITE
    // =========================
    namespace Task3
        {
            // Інтерфейси для кожного концепту
            public interface ISoldier
            {
                string Id { get; }
                string FirstName { get; }
                string LastName { get; }
            }

            public interface IPrivate : ISoldier
            {
                double Salary { get; }
            }

            public interface ILeutenantGeneral : ISoldier
            {
                IReadOnlyCollection<IPrivate> Privates { get; }
            }

            public interface ISpecialisedSoldier : ISoldier
            {
                string Corps { get; }
            }

            public interface IEngineer : ISpecialisedSoldier
            {
                IReadOnlyCollection<IRepair> Repairs { get; }
            }

            public interface ICommando : ISpecialisedSoldier
            {
                IReadOnlyCollection<IMission> Missions { get; }
            }

            public interface ISpy : ISoldier
            {
                int CodeNumber { get; }
            }

            public interface IRepair
            {
                string PartName { get; }
                int HoursWorked { get; }
            }

            public interface IMission
            {
                string CodeName { get; }
                string State { get; } // inProgress | Finished
                void CompleteMission();
            }

            // Абстрактний базовий клас
            public abstract class Soldier : ISoldier
            {
                public string Id { get; }
                public string FirstName { get; }
                public string LastName { get; }

                protected Soldier(string id, string first, string last)
                {
                    Id = id; FirstName = first; LastName = last;
                }
            }

            // Private
            public class Private : Soldier, IPrivate
            {
                public double Salary { get; }
                public Private(string id, string first, string last, double salary) : base(id, first, last) { Salary = salary; }
                public override string ToString() => $"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:F2}";
            }

            // LeutenantGeneral
            public class LeutenantGeneral : Soldier, ILeutenantGeneral
            {
                private readonly List<IPrivate> privates = new();
                public IReadOnlyCollection<IPrivate> Privates => privates.AsReadOnly();
                public double Salary { get; }
                public LeutenantGeneral(string id, string first, string last, double salary) : base(id, first, last) { Salary = salary; }
                public void AddPrivate(IPrivate p) => privates.Add(p);
                public override string ToString()
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:F2}");
                    sb.AppendLine("Privates:");
                    foreach (var p in privates) sb.AppendLine(" " + p.ToString());
                    return sb.ToString().TrimEnd();
                }
            }

            // Repair
            public class Repair : IRepair
            {
                public string PartName { get; }
                public int HoursWorked { get; }
                public Repair(string part, int hours) { PartName = part; HoursWorked = hours; }
                public override string ToString() => $" Part Name: {PartName} Hours Worked: {HoursWorked}";
            }

            // Mission
            public class Mission : IMission
            {
                public string CodeName { get; }
                public string State { get; private set; }
                public Mission(string codeName, string state) { CodeName = codeName; State = state; }
                public void CompleteMission() { if (State == "inProgress") State = "Finished"; }
                public override string ToString() => $" Code Name: {CodeName} State: {State}";
            }

            // Engineer
            public class Engineer : Soldier, IEngineer
            {
                public string Corps { get; }
                public double Salary { get; }
                private readonly List<IRepair> repairs = new();
                public IReadOnlyCollection<IRepair> Repairs => repairs.AsReadOnly();
                public Engineer(string id, string first, string last, double salary, string corps) : base(id, first, last) { Salary = salary; Corps = corps; }
                public void AddRepair(IRepair r) => repairs.Add(r);
                public override string ToString()
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:F2}");
                    sb.AppendLine($"Corps: {Corps}");
                    sb.AppendLine("Repairs:");
                    foreach (var r in repairs) sb.AppendLine(r.ToString());
                    return sb.ToString().TrimEnd();
                }
            }

            // Commando
            public class Commando : Soldier, ICommando
            {
                public string Corps { get; }
                public double Salary { get; }
                private readonly List<IMission> missions = new();
                public IReadOnlyCollection<IMission> Missions => missions.AsReadOnly();
                public Commando(string id, string first, string last, double salary, string corps) : base(id, first, last) { Salary = salary; Corps = corps; }
                public void AddMission(IMission m) => missions.Add(m);
                public override string ToString()
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:F2}");
                    sb.AppendLine($"Corps: {Corps}");
                    sb.AppendLine("Missions:");
                    foreach (var m in missions) sb.AppendLine(m.ToString());
                    return sb.ToString().TrimEnd();
                }
            }

            // Spy
            public class Spy : Soldier, ISpy
            {
                public int CodeNumber { get; }
                public Spy(string id, string first, string last, int code) : base(id, first, last) { CodeNumber = code; }
                public override string ToString()
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id}");
                    sb.AppendLine($"Code Number: {CodeNumber}");
                    return sb.ToString().TrimEnd();
                }
            }

            // Виконання Task3 (читання, створення об'єктів, вивід)
            public static class Task3Runner
            {
                public static void Run()
                {
                    Console.WriteLine("=== Військова еліта ===");
                    Console.WriteLine("Вводьте рядки (кінець — End). Формат наведено у підказках.");
                    var registry = new
        Dictionary<string, Soldier>();
                    var outputs = new List<string>();

                    while (true)
                    {
                        var line = Console.ReadLine();
                        if (line == null) return;
                        if (line == "End") break;
                        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 0) continue;
                        var type = parts[0];

                        try
                        {
                            if (type == "Private" && parts.Length >= 5)
                            {
                                var id = parts[1]; var first = parts[2]; var last = parts[3];
                                var sal = double.Parse(parts[4], CultureInfo.InvariantCulture);
                                var p = new Private(id, first, last, sal);
                                registry[id] = p;
                                outputs.Add(p.ToString());
                            }
                            else if (type == "LeutenantGeneral" && parts.Length >= 5)
                            {
                                var id = parts[1]; var first = parts[2]; var last = parts[3];
                                var sal = double.Parse(parts[4], CultureInfo.InvariantCulture);
                                var lg = new LeutenantGeneral(id, first, last, sal);
                                for (int i = 5; i < parts.Length; i++)
                                {
                                    var pid = parts[i];
                                    if (registry.TryGetValue(pid, out var s) && s is IPrivate ip) lg.AddPrivate(ip);
                                }
                                registry[id] = lg;
                                outputs.Add(lg.ToString());
                            }
                            else if (type == "Engineer" && parts.Length >= 6)
                            {
                                var id = parts[1]; var first = parts[2]; var last = parts[3];
                                var sal = double.Parse(parts[4], CultureInfo.InvariantCulture);
                                var corps = parts[5];
                                if (corps != "Airforces" && corps != "Marines") continue;
                                var eng = new Engineer(id, first, last, sal, corps);
                                for (int i = 6; i + 1 < parts.Length; i += 2)
                                {
                                    var part = parts[i];
                                    if (int.TryParse(parts[i + 1], out int hours)) eng.AddRepair(new Repair(part, hours));
                                }
                                registry[id] = eng;
                                outputs.Add(eng.ToString());
                            }
                            else if (type == "Commando" && parts.Length >= 6)
                            {
                                var id = parts[1]; var first = parts[2]; var last = parts[3];
                                var sal = double.Parse(parts[4], CultureInfo.InvariantCulture);
                                var corps = parts[5];
                                if (corps != "Airforces" && corps != "Marines") continue;
                                var comm = new Commando(id, first, last, sal, corps);
                                for (int i = 6; i + 1 < parts.Length; i += 2)
                                {
                                    var code = parts[i]; var state = parts[i + 1];
                                    if (state != "inProgress" && state != "Finished") continue;
                                    comm.AddMission(new Mission(code, state));
                                }
                                registry[id] = comm;
                                outputs.Add(comm.ToString());
                            }
                            else if (type == "Spy" && parts.Length >= 5)
                            {
                                var id = parts[1]; var first = parts[2]; var last = parts[3];
                                var code = int.Parse(parts[4]);
                                var spy = new Spy(id, first, last, code);
                                registry[id] = spy;
                                outputs.Add(spy.ToString());
                            }
                        }
                        catch
                        {
                            // ігноруємо некоректні рядки
                        }
                    }

                    // вивід
                    foreach (var o in outputs) Console.WriteLine(o);
                    Console.WriteLine();
                }
            }
        }
    }

