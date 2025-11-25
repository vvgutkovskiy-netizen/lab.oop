using System;
using System.Collections.Generic;
using System.Linq;

namespace AllTasks
{
    // ========== Завдання 1. Ферма ==========
    class Chicken
    {
        private string name;
        private int age;

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім’я не може бути порожнім.");
                name = value;
            }
        }

        public int Age
        {
            get => age;
            private set
            {
                if (value < 0 || value > 15)
                    throw new ArgumentException("Вік має бути в межах від 0 до 15.");
                age = value;
            }
        }

        public Chicken(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public double ProductPerDay => CalculateProductPerDay();

        private double CalculateProductPerDay()
        {
            if (age < 6) return 2;
            if (age <= 11) return 1;
            return 0.75;
        }

        public override string ToString()
        {
            return $"Курка {Name} (вік {Age}) може нести {ProductPerDay} яєць на день.";
        }
    }

    // ========== Завдання 2. Покупка ==========
    class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }

        public Product(string name, double price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва товару не може бути порожньою.");
            if (price < 0)
                throw new ArgumentException("Ціна не може бути від’ємною.");

            Name = name;
            Price = price;
        }
    }

    class Person
    {
        public string Name { get; private set; }
        public double Money { get; private set; }
        private List<Product> bag;

        public Person(string name, double money)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ім’я не може бути порожнім.");
            if (money < 0)
                throw new ArgumentException("Гроші не можуть бути від’ємними.");

            Name = name;
            Money = money;
            bag = new List<Product>();
        }

        public void Buy(Product product)
        {
            if (Money >= product.Price)
            {
                Money -= product.Price;
                bag.Add(product);
                Console.WriteLine($"{Name} купив(ла) {product.Name}");
            }
            else
            {
                Console.WriteLine($"{Name} не має достатньо грошей для покупки {product.Name}");
            }
        }

        public override string ToString()
        {
            string bought = bag.Count > 0 ? string.Join(", ", bag.Select(p => p.Name)) : "нічого не куплено";
            return $"{Name} - {bought}";
        }
    }

    // ========== Завдання 3. Калорійність піци ==========
    class Dough
    {
        private static Dictionary<string, double> flourModifiers = new Dictionary<string, double>()
        {
            {"White", 1.5},
            {"Wholegrain", 1.0}
        };

        private static Dictionary<string, double> bakingTechModifiers = new Dictionary<string, double>()
        {
            {"Crispy", 0.9},
            {"Chewy", 1.1},
            {"Homemade", 1.0}
        };

        private string flourType;
        private string bakingTechnique;
        private double weight;

        public string FlourType
        {
            get => flourType;
            private set
            {
                if (!flourModifiers.ContainsKey(value))
                    throw new ArgumentException("Невірний тип тіста.");
                flourType = value;
            }
        }

        public string BakingTechnique
        {
            get => bakingTechnique;
            private set
            {
                if (!bakingTechModifiers.ContainsKey(value))
                    throw new ArgumentException("Невірний тип випікання тіста.");
                bakingTechnique = value;
            }
        }

        public double Weight
        {
            get => weight;
            private set
            {
                if (value < 1 || value > 200)
                    throw new ArgumentException("Вага тіста має бути в межах [1..200].");
                weight = value;
            }
        }

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public double Calories => 2 * Weight * flourModifiers[FlourType] * bakingTechModifiers[BakingTechnique];
    }

    class Topping
    {
        private static Dictionary<string, double> toppingModifiers = new Dictionary<string, double>()
        {
            {"Meat", 1.2},
            {"Veggies", 0.8},
            {"Cheese", 1.1},
            {"Sauce", 0.9}
        };

        private string type;
        private double weight;

        public string Type
        {
            get => type;
            private set
            {
                if (!toppingModifiers.ContainsKey(value))
                    throw new ArgumentException($"Не можна додати {value} на піцу.");
                type = value;
            }
        }

        public double Weight
        {
            get => weight;
            private set
            {
                if (value < 1 || value > 50)
                    throw new ArgumentException($"Вага начинки {Type} має бути в межах [1..50].");
                weight = value;
            }
        }

        public Topping(string type, double weight)
        {
            Type = type;
            Weight = weight;
        }

        public double Calories => 2 * Weight * toppingModifiers[Type];
    }

    class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings = new List<Topping>();

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 15)
                    throw new ArgumentException("Назва піци має бути від 1 до 15 символів.");
                name = value;
            }
        }

        public Pizza(string name)
        {
            Name = name;
        }

        public void SetDough(Dough dough) => this.dough = dough;

        public void AddTopping(Topping topping)
        {
            if (toppings.Count == 10)
                throw new ArgumentException("Кількість начинок має бути в межах [0..10].");
            toppings.Add(topping);
        }

        public double TotalCalories => dough.Calories + toppings.Sum(t => t.Calories);

        public override string ToString()
        {
            return $"{Name} - {TotalCalories:F2} калорій.";
        }
    }

    // ========== Головна програма ==========
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("========== Вибір завдання ==========");
            Console.WriteLine("1 - Ферма (курка)");
            Console.WriteLine("2 - Покупки (люди і товари)");
            Console.WriteLine("3 - Піца (тісто, начинка, калорійність)");
            Console.Write("Введіть номер завдання: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        RunChicken();
                        break;
                    case "2":
                        RunShopping();
                        break;
                    case "3":
                        RunPizza();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        static void RunChicken()
        {
            Console.WriteLine("=== Завдання 1: Курка ===");
            Console.Write("Введіть ім’я курки: ");
            string name = Console.ReadLine();

            Console.Write("Введіть вік курки (0–15): ");
            int age = int.Parse(Console.ReadLine());

            var chicken = new Chicken(name, age);
            Console.WriteLine();
            Console.WriteLine(chicken);
        }

        static void RunShopping()
        {
            Console.WriteLine("=== Завдання 2: Покупки ===");
            Console.WriteLine("Введіть людей (формат: Ім’я=сума;Ім’я=сума):");
            var people = new Dictionary<string, Person>();
            var products = new Dictionary<string, Product>();

            string line = Console.ReadLine();
            foreach (var part in line.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                var data = part.Split('=');
                people[data[0]] = new Person(data[0], double.Parse(data[1]));
            }

            Console.WriteLine("\nВведіть товари (формат: Назва=ціна;Назва=ціна):");
            line = Console.ReadLine();
            foreach (var part in line.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                var data = part.Split('=');
                products[data[0]] = new Product(data[0], double.Parse(data[1]));
            }

            Console.WriteLine("\nВводьте покупки (формат: Ім’я Товар), або 'END' щоб завершити:");
            while ((line = Console.ReadLine()) != "END")
            {
                var data = line.Split(' ');
                people[data[0]].Buy(products[data[1]]);
            }

            Console.WriteLine("\n=== Результати покупок ===");
            foreach (var p in people.Values)
                Console.WriteLine(p);
        }

        static void RunPizza()
        {
            Console.WriteLine("=== Завдання 3: Піца ===");
            try
            {
                Console.WriteLine("Введіть назву піци (формат: Pizza Назва):");
                string[] pizzaInfo = Console.ReadLine().Split(' ');
                var pizza = new Pizza(pizzaInfo[1]);

                Console.WriteLine("Введіть тісто (формат: Dough тип_борошна(White, Wholegrain) тип_випікання(Crispy, Chewy, Homemade) вага):");
                string[] doughInfo = Console.ReadLine().Split(' ');
                var dough = new Dough(doughInfo[1], doughInfo[2], double.Parse(doughInfo[3]));
                pizza.SetDough(dough);

                Console.WriteLine("Додавайте начинки (формат: Topping тип(Meat, Veggies, Cheese, Sauce) вага), або 'END' щоб завершити:");
                string line;
                while ((line = Console.ReadLine()) != "END")
                {
                    var data = line.Split(' ');
                    var topping = new Topping(data[1], double.Parse(data[2]));
                    pizza.AddTopping(topping);
                }

                Console.WriteLine();
                Console.WriteLine(pizza);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}