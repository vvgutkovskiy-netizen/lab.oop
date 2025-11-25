using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lab6_AllTasks_Fixed
{
    #region VEHICLES
    abstract class Vehicle
    {
        public double FuelQuantity { get; protected set; }
        public double FuelConsumption { get; protected set; }
        public double TankCapacity { get; protected set; }

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity = double.MaxValue)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity > tankCapacity ? 0 : fuelQuantity;
            FuelConsumption = fuelConsumption;
        }

        public abstract void Drive(double distance);
        public abstract void Refuel(double liters);
    }

    class Car : Vehicle
    {
        private const double AC = 0.9;
        public Car(double fuel, double cons, double cap = double.MaxValue) : base(fuel, cons, cap) { }
        public override void Drive(double distance)
        {
            double needed = distance * (FuelConsumption + AC);
            if (needed <= FuelQuantity) { FuelQuantity -= needed; Console.WriteLine($"Car travelled {distance} km"); }
            else Console.WriteLine("Car needs refueling");
        }
        public override void Refuel(double liters)
        {
            if (liters <= 0) { Console.WriteLine("Fuel must be a positive number"); return; }
            if (FuelQuantity + liters > TankCapacity) { Console.WriteLine($"Cannot fit {liters} fuel in the tank"); return; }
            FuelQuantity += liters;
        }
    }

    class Truck : Vehicle
    {
        private const double AC = 1.6;
        public Truck(double fuel, double cons, double cap = double.MaxValue) : base(fuel, cons, cap) { }
        public override void Drive(double distance)
        {
            double needed = distance * (FuelConsumption + AC);
            if (needed <= FuelQuantity) { FuelQuantity -= needed; Console.WriteLine($"Truck travelled {distance} km"); }
            else Console.WriteLine("Truck needs refueling");
        }
        public override void Refuel(double liters)
        {
            if (liters <= 0) { Console.WriteLine("Fuel must be a positive number"); return; }
            double actual = liters * 0.95;
            if (FuelQuantity + actual > TankCapacity) { Console.WriteLine($"Cannot fit {liters} fuel in the tank"); return; }
            FuelQuantity += actual;
        }
    }

    class Bus : Vehicle
    {
        private const double AC = 1.4;
        public Bus(double fuel, double cons, double cap) : base(fuel, cons, cap) { }
        public override void Drive(double distance)
        {
            // default: with people -> AC on
            double needed = distance * (FuelConsumption + AC);
            if (needed <= FuelQuantity) { FuelQuantity -= needed; Console.WriteLine($"Bus travelled {distance} km"); }
            else Console.WriteLine("Bus needs refueling");
        }
        public void DriveEmpty(double distance)
        {
            double needed = distance * FuelConsumption;
            if (needed <= FuelQuantity) { FuelQuantity -= needed; Console.WriteLine($"Bus travelled {distance} km"); }
            else Console.WriteLine("Bus needs refueling");
        }
        public override void Refuel(double liters)
        {
            if (liters <= 0) { Console.WriteLine("Fuel must be a positive number"); return; }
            if (FuelQuantity + liters > TankCapacity) { Console.WriteLine($"Cannot fit {liters} fuel in the tank"); return; }
            FuelQuantity += liters;
        }
    }
    #endregion

    #region FARM CLASSES
    abstract class Food { public int Quantity { get; protected set; } public Food(int q) { Quantity = q; } }
    class Vegetable : Food { public Vegetable(int q) : base(q) { } }
    class Fruit : Food { public Fruit(int q) : base(q) { } }
    class Meat : Food { public Meat(int q) : base(q) { } }
    class Seeds : Food { public Seeds(int q) : base(q) { } }

    abstract class Animal
    {
        public string Name { get; protected set; }
        public double Weight { get; protected set; }
        public int FoodEaten { get; protected set; }
        protected Animal(string name, double weight) { Name = name; Weight = weight; FoodEaten = 0; }
        public abstract void MakeSound();
        public abstract void Eat(Food food);
    }

    abstract class Bird : Animal
    {
        public double WingSize { get; protected set; }
        protected Bird(string name, double weight, double wing) : base(name, weight) => WingSize = wing;
    }

    class Owl : Bird
    {
        public Owl(string n, double w, double wing) : base(n, w, wing) { }
        public override void MakeSound() => Console.WriteLine("Hoot Hoot");
        public override void Eat(Food food)
        {
            if (food is Meat) { Weight += 0.25 * food.Quantity; FoodEaten += food.Quantity; }
            else Console.WriteLine($"Owl does not eat {food.GetType().Name}!");
        }
        public override string ToString() => $"Owl [{Name}, {WingSize}, {Weight:F2}, {FoodEaten}]";
    }

    class Hen : Bird
    {
        public Hen(string n, double w, double wing) : base(n, w, wing) { }
        public override void MakeSound() => Console.WriteLine("Cluck");
        public override void Eat(Food food) { Weight += 0.35 * food.Quantity; FoodEaten += food.Quantity; }
        public override string ToString() => $"Hen [{Name}, {WingSize}, {Weight:F2}, {FoodEaten}]";
    }

    abstract class Mammal : Animal
    {
        public string LivingRegion { get; protected set; }
        protected Mammal(string n, double w, string region) : base(n, w) => LivingRegion = region;
    }

    class Mouse : Mammal
    {
        public Mouse(string n, double w, string r) : base(n, w, r) { }
        public override void MakeSound() => Console.WriteLine("Squeak");
        public override void Eat(Food food)
        {
            if (food is Vegetable || food is Fruit) { Weight += 0.10 * food.Quantity; FoodEaten += food.Quantity; }
            else Console.WriteLine($"Mouse does not eat {food.GetType().Name}!");
        }
        public override string ToString() => $"Mouse [{Name}, {Weight:F2}, {LivingRegion}, {FoodEaten}]";
    }

    class Dog : Mammal
    {
        public Dog(string n, double w, string r) : base(n, w, r) { }
        public override void MakeSound() => Console.WriteLine("Woof!");
        public override void Eat(Food food)
        {
            if (food is Meat) { Weight += 0.40 * food.Quantity; FoodEaten += food.Quantity; }
            else Console.WriteLine($"Dog does not eat {food.GetType().Name}!");
        }
        public override string ToString() => $"Dog [{Name}, {Weight:F2}, {LivingRegion}, {FoodEaten}]";
    }

    abstract class Feline : Mammal
    {
        public string Breed { get; protected set; }
        protected Feline(string n, double w, string r, string b) : base(n, w, r) => Breed = b;
    }

    class Cat : Feline
    {
        public Cat(string n, double w, string r, string b) : base(n, w, r, b) { }
        public override void MakeSound() => Console.WriteLine("Meow");
        public override void Eat(Food food)
        {
            if (food is Meat || food is Vegetable) { Weight += 0.30 * food.Quantity; FoodEaten += food.Quantity; }
            else Console.WriteLine($"Cat does not eat {food.GetType().Name}!");
        }
        public override string ToString() => $"Cat [{Name}, {Breed}, {Weight:F2}, {LivingRegion}, {FoodEaten}]";
    }

    class Tiger : Feline
    {
        public Tiger(string n, double w, string r, string b) : base(n, w, r, b) { }
        public override void MakeSound() => Console.WriteLine("ROAR!!!");
        public override void Eat(Food food)
        {
            if (food is Meat) { Weight += 1.00 * food.Quantity; FoodEaten += food.Quantity; }
            else Console.WriteLine($"Tiger does not eat {food.GetType().Name}!");
        }
        public override string ToString() => $"Tiger [{Name}, {Breed}, {Weight:F2}, {LivingRegion}, {FoodEaten}]";
    }
    #endregion

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("Оберіть завдання 1..3 (0-вихід):");
                Console.WriteLine("1 - Завдання 1 (Car & Truck)");
                Console.WriteLine("2 - Завдання 2 (Car, Truck, Bus)");
                Console.WriteLine("3 - Завдання 3 (Ферма)");
                Console.Write("Ваш вибір: ");
                var sel = Console.ReadLine();
                Console.WriteLine();
                if (sel == "0") break;
                if (sel == "1") RunTask1();
                else if (sel == "2") RunTask2();
                else if (sel == "3") RunTask3();
                else Console.WriteLine("Невірний вибір.\n");
            }
        }

        #region Task1
        static void RunTask1()
        {
            Console.WriteLine("Введіть (приклад):");
            Console.WriteLine("Car 15 0.3");
            Console.WriteLine("Truck 100 0.9");
            Console.WriteLine("4");
            Console.WriteLine("Drive Car 9");
            Console.WriteLine("...");
            Console.WriteLine();

            try
            {
                string[] carInfo = ReadNonEmptyLineSplit();
                string[] truckInfo = ReadNonEmptyLineSplit();
                int n = int.Parse(Console.ReadLine() ?? "0");
                Car car = new Car(ParseDoubleInvariant(carInfo[1]), ParseDoubleInvariant(carInfo[2]));
                Truck truck = new Truck(ParseDoubleInvariant(truckInfo[1]), ParseDoubleInvariant(truckInfo[2]));

                for (int i = 0; i < n; i++)
                {
                    string[] parts = ReadNonEmptyLineSplit();
                    string cmd = parts[0];
                    string type = parts[1];
                    double val = ParseDoubleInvariant(parts[2]);

                    if (cmd == "Drive")
                    {
                        if (type == "Car") car.Drive(val);
                        else if (type == "Truck") truck.Drive(val);
                    }
                    else if (cmd == "Refuel")
                    {
                        if (type == "Car") car.Refuel(val);
                        else if (type == "Truck") truck.Refuel(val);
                    }
                }

                Console.WriteLine($"Car: {car.FuelQuantity:F2}");
                Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            Console.WriteLine();
        }
        #endregion

        #region Task2
        static void RunTask2()
        {
            Console.WriteLine("Формат (приклад):");
            Console.WriteLine("Car 30 0.04 70");
            Console.WriteLine("Truck 100 0.5 300");
            Console.WriteLine("Bus 40 0.3 150");
            Console.WriteLine("8");
            Console.WriteLine("Refuel Car -10");
            Console.WriteLine("...");
            Console.WriteLine();

            try
            {
                string[] carInfo = ReadNonEmptyLineSplit();
                string[] truckInfo = ReadNonEmptyLineSplit();
                string[] busInfo = ReadNonEmptyLineSplit();
                int n = int.Parse(Console.ReadLine() ?? "0");

                Car car = new Car(ParseDoubleInvariant(carInfo[1]), ParseDoubleInvariant(carInfo[2]), ParseDoubleInvariant(carInfo[3]));
                Truck truck = new Truck(ParseDoubleInvariant(truckInfo[1]), ParseDoubleInvariant(truckInfo[2]), ParseDoubleInvariant(truckInfo[3]));
                Bus bus = new Bus(ParseDoubleInvariant(busInfo[1]), ParseDoubleInvariant(busInfo[2]), ParseDoubleInvariant(busInfo[3]));

                for (int i = 0; i < n; i++)
                {
                    string[] parts = ReadNonEmptyLineSplit();
                    string cmd = parts[0];
                    string type = parts[1];
                    double val = ParseDoubleInvariant(parts[2]);

                    if (cmd == "Drive")
                    {
                        if (type == "Car") car.Drive(val);
                        else if (type == "Truck") truck.Drive(val);
                        else if (type == "Bus") bus.Drive(val); // with people
                    }
                    else if (cmd == "DriveEmpty")
                    {
                        if (type == "Bus") bus.DriveEmpty(val);
                    }
                    else if (cmd == "Refuel")
                    {
                        if (type == "Car") car.Refuel(val);
                        else if (type == "Truck") truck.Refuel(val);
                        else if (type == "Bus") bus.Refuel(val);
                    }
                }

                Console.WriteLine($"Car: {car.FuelQuantity:F2}");
                Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
                Console.WriteLine($"Bus: {bus.FuelQuantity:F2}");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            Console.WriteLine();
        }
        #endregion

        #region Task3 (Farm)
        static void RunTask3()
        {
            Console.WriteLine("Вводьте пари рядків: рядок про тварину, потім рядок з їжею.");
            Console.WriteLine("Коли закінчите — введіть End на рядку тварини.");
            Console.WriteLine("Приклад тварини:");
            Console.WriteLine("Cat Pesho 1.1 Home Persian");
            Console.WriteLine("Приклад їжі:");
            Console.WriteLine("Vegetable 4");
            Console.WriteLine();

            var animals = new List<Animal>();

            while (true)
            {
                string animalLine = Console.ReadLine();
                if (animalLine == null) break;
                if (animalLine.Trim() == "End") break;
                if (string.IsNullOrWhiteSpace(animalLine)) continue;

                string[] aParts = animalLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (aParts.Length == 0) continue;

                string foodLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(foodLine))
                {
                    Console.WriteLine("Мусить бути рядок з їжею після тварини. Спробуйте ще раз.");
                    continue;
                }
                string[] fParts = foodLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (fParts.Length < 2)
                {
                    Console.WriteLine("Формат їжі: <Type> <Quantity>. Спробуйте ще раз.");
                    continue;
                }

                try
                {
                    Animal animal = CreateAnimalSafe(aParts);
                    if (animal == null) { Console.WriteLine("Невідомий тип тварини або невірний формат."); continue; }

                    Food food = CreateFoodSafe(fParts);
                    if (food == null) { Console.WriteLine("Невідомий тип їжі або невірна кількість."); continue; }

                    animal.MakeSound();
                    animal.Eat(food);
                    animals.Add(animal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка при обробці: " + ex.Message);
                }
            }

            foreach (var a in animals) Console.WriteLine(a);
            Console.WriteLine();
        }

        static Animal CreateAnimalSafe(string[] p)
        {
            // p[0] - type
            string type = p[0];
            if (type == "Owl" && p.Length >= 4)
            {
                if (double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double w)
                    && double.TryParse(p[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double wing))
                    return new Owl(p[1], w, wing);
            }
            else if (type == "Hen" && p.Length >= 4)
            {
                if (double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double w)
                    && double.TryParse(p[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double wing))
                    return new Hen(p[1], w, wing);
            }
            else if (type == "Mouse" && p.Length >= 4)
            {
                if (double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double w))
                    return new Mouse(p[1], w, p[3]);
            }
            else if (type == "Dog" && p.Length >= 4)
            {
                if (double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double w))
                    return new Dog(p[1], w, p[3]);
            }
            else if (type == "Cat" && p.Length >= 5)
            {
                if (double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double w))
                    return new Cat(p[1], w, p[3], p[4]);
            }
            else if (type == "Tiger" && p.Length >= 5)
            {
                if (double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double w))
                    return new Tiger(p[1], w, p[3], p[4]);
            }

            return null; // unknown or bad format
        }

        static Food CreateFoodSafe(string[] p)
        {
            string type = p[0];
            if (!int.TryParse(p[1], out int qty)) return null;
            return type switch
            {
                "Vegetable" => new Vegetable(qty),
                "Fruit" => new Fruit(qty),
                "Meat" => new Meat(qty),
                "Seeds" => new Seeds(qty),
                _ => null
            };
        }
        #endregion

        #region Helpers
        static string[] ReadNonEmptyLineSplit()
        {
            while (true)
            {
                string? l = Console.ReadLine();
                if (l == null) throw new Exception("Unexpected end of input");
                if (string.IsNullOrWhiteSpace(l)) continue;
                return l.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }

        static double ParseDoubleInvariant(string s)
        {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }
        #endregion
    }
}

