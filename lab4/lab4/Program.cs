using System;
using System.Collections.Generic;
using System.Linq;

namespace AllTasks
{
    // ===================== Завдання 1: Точка в прямокутнику =====================

    // Абстрактний клас для геометричних фігур
    public abstract class Shape
    {
        public abstract bool Contains(Point point);
    }

    // Клас для точок
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Клас для прямокутника, успадкований від Shape
    public class Rectangle : Shape
    {
        private int x1, y1, x2, y2;

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        // Реалізація абстрактного методу Contains
        public override bool Contains(Point point)
        {
            return point.X >= x1 && point.X <= x2 && point.Y >= y1 && point.Y <= y2;
        }
    }

    // ===================== Завдання 2: Розрахунок вартості =====================

    // Абстрактний клас для калькуляторів вартості
    public abstract class PriceCalculatorBase
    {
        public abstract void CalculatePrice(string priceStr, string season, string type);
    }

    // Клас для розрахунку вартості, успадкований від PriceCalculatorBase
    public class PriceCalculator : PriceCalculatorBase
    {
        public override void CalculatePrice(string priceStr, string season, string type)
        {
            double price = double.Parse(priceStr);
            double finalPrice = price;

            // Перевірка сезону і типу
            switch (season.ToLower())
            {
                case "summer":
                    finalPrice *= 1.4;
                    break;
                case "autumn":
                    finalPrice *= 1.2;
                    break;
                case "winter":
                    finalPrice *= 1.2;
                    break;
                case "spring":
                    finalPrice *= 1.1; // Додаткове збільшення для весни
                    break;
                default:
                    Console.WriteLine("Невідомий сезон!");
                    return;
            }

            // Перевірка типу
            if (type.ToLower() == "vip")
            {
                finalPrice *= 1.6; // Для VIP ціна збільшується на 60%
            }
            else if (type.ToLower() == "secondvisit")
            {
                finalPrice *= 0.8; // Для second visit ціна зменшується на 20%
            }
            else
            {
                finalPrice *= 1.0; // Для звичайних клієнтів ціна залишається без змін
            }

            // Виведення кінцевої ціни
            Console.WriteLine($"Кінцева ціна: {finalPrice:F2}");
        }
    }

    // ===================== Завдання 3: Госпіталь =====================

    abstract class HospitalBase
    {
        public abstract void AddPatient(string department, string doctorFullName, string patient);
        public abstract void ProcessQuery(string query);
    }

    class Hospital : HospitalBase
    {
        private Dictionary<string, List<List<string>>> departments = new();
        private Dictionary<string, List<string>> doctors = new();

        public override void AddPatient(string department, string doctorFullName, string patient)
        {
            if (!departments.ContainsKey(department))
            {
                departments[department] = new List<List<string>>();
                for (int i = 0; i < 20; i++)
                    departments[department].Add(new List<string>());
            }

            if (!doctors.ContainsKey(doctorFullName))
                doctors[doctorFullName] = new List<string>();

            foreach (var room in departments[department])
            {
                if (room.Count < 3)
                {
                    room.Add(patient);
                    doctors[doctorFullName].Add(patient);
                    return;
                }
            }

            Console.WriteLine($"Немає вільних ліжок у відділенні {department}. Пацієнт {patient} не доданий.");
        }

        public override void ProcessQuery(string query)
        {
            string[] parts = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                string department = parts[0];
                if (departments.ContainsKey(department))
                {
                    foreach (var room in departments[department])
                        foreach (var patient in room)
                            Console.WriteLine(patient);
                }
            }
            else if (parts.Length == 2 && int.TryParse(parts[1], out int roomNum))
            {
                string department = parts[0];
                if (departments.ContainsKey(department) && roomNum >= 1 && roomNum <= 20)
                {
                    var patients = departments[department][roomNum - 1].OrderBy(p => p);
                    foreach (var p in patients)
                        Console.WriteLine(p);
                }
            }
            else if (parts.Length == 2)
            {
                string doctor = $"{parts[0]} {parts[1]}";
                if (doctors.ContainsKey(doctor))
                {
                    foreach (var patient in doctors[doctor].OrderBy(p => p))
                        Console.WriteLine(patient);
                }
            }
        }
    }

    // ===================== Завдання 4: Greedy Times =====================

    abstract class TreasureCollectorBase
    {
        public abstract void Collect(long capacity, string[] items);
        public abstract void PrintBag();
    }

    class TreasureCollector : TreasureCollectorBase
    {
        private Dictionary<string, Dictionary<string, long>> bag = new();
        private long capacity;
        private long total;

        public override void Collect(long capacity, string[] items)
        {
            this.capacity = capacity;
            this.total = 0;

            for (int i = 0; i < items.Length; i += 2)
            {
                string name = items[i];
                long amount = long.Parse(items[i + 1]);
                string type = GetItemType(name);

                if (type == null) continue;
                if (total + amount > capacity) continue;

                long goldSum = GetSum("Gold");
                long gemSum = GetSum("Gem");
                long cashSum = GetSum("Cash");

                if (type == "Gem" && (gemSum + amount) > goldSum) continue;
                if (type == "Cash" && (cashSum + amount) > gemSum) continue;

                if (!bag.ContainsKey(type))
                    bag[type] = new Dictionary<string, long>();

                if (!bag[type].ContainsKey(name))
                    bag[type][name] = 0;

                bag[type][name] += amount;
                total += amount;
            }
        }

        public override void PrintBag()
        {
            foreach (var type in bag.OrderByDescending(t => t.Value.Values.Sum()))
            {
                Console.WriteLine($"<{type.Key}> ${type.Value.Values.Sum()}");
                foreach (var item in type.Value
                    .OrderByDescending(x => x.Key)
                    .ThenBy(x => x.Value))
                {
                    Console.WriteLine($"##{item.Key} - {item.Value}");
                }
            }
        }

        private long GetSum(string type) => bag.ContainsKey(type) ? bag[type].Values.Sum() : 0;

        private string GetItemType(string item)
        {
            if (item.Equals("Gold", StringComparison.OrdinalIgnoreCase))
                return "Gold";
            if (item.EndsWith("Gem", StringComparison.OrdinalIgnoreCase) && item.Length >= 4)
                return "Gem";
            if (item.Length == 3)
                return "Cash";
            return null;
        }
    }

    class Program
    {
        static void Main()
        {
            // Головне меню
            Console.WriteLine("Оберіть завдання:");
            Console.WriteLine("1. Завдання 1 – Точка в прямокутнику");
            Console.WriteLine("2. Завдання 2 – Розрахунок вартості");
            Console.WriteLine("3. Завдання 3 – Госпіталь");
            Console.WriteLine("4. Завдання 4 – Greedy Times");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunPointInRectangle();
                    break;
                case "2":
                    RunPriceCalculator();
                    break;
                case "3":
                    RunHospital();
                    break;
                case "4":
                    RunGreedyTimes();
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        // Завдання 1: Точка в прямокутнику
        static void RunPointInRectangle()
        {
            Console.WriteLine("\nЗавдання 1 – Точка в прямокутнику");
            // Введення координат для прямокутника
            Console.WriteLine("Введіть координати першого кута прямокутника (x1 y1):");
            var rectCoords1 = Console.ReadLine().Split(' ');
            int x1 = int.Parse(rectCoords1[0]);
            int y1 = int.Parse(rectCoords1[1]);

            Console.WriteLine("Введіть координати протилежного кута прямокутника (x2 y2):");
            var rectCoords2 = Console.ReadLine().Split(' ');
            int x2 = int.Parse(rectCoords2[0]);
            int y2 = int.Parse(rectCoords2[1]);

            // Введення координат точки
            Console.WriteLine("Введіть координати точки (x y):");
            var pointCoords = Console.ReadLine().Split(' ');
            int px = int.Parse(pointCoords[0]);
            int py = int.Parse(pointCoords[1]);

            // Створення прямокутника і перевірка, чи точка всередині
            Shape rectangle = new Rectangle(x1, y1, x2, y2);
            Point point = new Point(px, py);
            bool result = rectangle.Contains(point);

            // Виведення результату
            Console.WriteLine($"Точка ({px}, {py}) знаходиться в прямокутнику: {result}");
        }

        // Завдання 2: Розрахунок вартості
        static void RunPriceCalculator()
        {
            Console.WriteLine("\nЗавдання 2 – Розрахунок вартості");

            // Введення початкової ціни, сезону та типу
            Console.WriteLine("Введіть початкову ціну:");
            string priceStr = Console.ReadLine();

            Console.WriteLine("Введіть сезон (Summer, Autumn, Winter, Spring):");
            string season = Console.ReadLine();

            Console.WriteLine("Введіть тип (VIP, SecondVisit, звичайний):");
            string type = Console.ReadLine();

            // Розрахунок ціни
            PriceCalculatorBase calculator = new PriceCalculator();
            calculator.CalculatePrice(priceStr, season, type);
        }

        // Завдання 3: Госпіталь
        static void RunHospital()
        {
            Console.WriteLine("\nЗавдання 3 – Госпіталь");
            Hospital hospital = new Hospital();
            Console.WriteLine("Введіть дані у форматі: {Відділення} {Ім’яЛікаря} {ПрізвищеЛікаря} {Пацієнт}");
            Console.WriteLine("Коли завершите введення — напишіть 'Output':");

            while (true)
            {
                string line = Console.ReadLine();
                if (line == "Output") break;

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 4) continue;

                string department = parts[0];
                string doctor = parts[1] + " " + parts[2];
                string patient = parts[3];

                hospital.AddPatient(department, doctor, patient);
            }

            Console.WriteLine("Введіть запити (наприклад: 'Cardiology', 'Cardiology 1', 'Petar Petrov'). Напишіть 'End' для завершення.");
            while (true)
            {
                string query = Console.ReadLine();
                if (query == "End") break;
                hospital.ProcessQuery(query);
            }
        }

        // Завдання 4: Greedy Times
        static void RunGreedyTimes()
        {
            Console.WriteLine("\nЗавдання 4 – Greedy Times"); 
            Console.Write("Введіть місткість мішка: ");
            long capacity = long.Parse(Console.ReadLine());

            Console.WriteLine("Введіть скарби у форматі: Назва Кількість");
            string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            TreasureCollector collector = new();
            collector.Collect(capacity, input);
            collector.PrintBag();
        }
    }
}
