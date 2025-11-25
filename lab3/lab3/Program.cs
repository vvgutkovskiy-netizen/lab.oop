using System;

#region Завдання 1–3 (Person, Family)

class Person
{
    private string name;
    private int age;

    public string Name { get { return name; } }
    public int Age { get { return age; } }

    public Person()
    {
        this.name = "No name";
        this.age = 1;
    }

    public Person(int age)
    {
        this.name = "No name";
        this.age = age;
    }

    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

class Family
{
    private Person[] members;
    private int count;

    public Family()
    {
        members = new Person[100]; 
        count = 0;
    }

    public void AddMember(Person person)
    {
        members[count] = person;
        count++;
    }

    public Person GetOldestMember()
    {
        Person oldest = members[0];
        for (int i = 1; i < count; i++)
        {
            if (members[i].Age > oldest.Age)
            {
                oldest = members[i];
            }
        }
        return oldest;
    }
}

#endregion

#region Завдання 4. Реєстр компаній

class Employee
{
    public string Name;
    public double Salary;
    public string Position;
    public string Department;
    public string Email;
    public int Age;

    public Employee(string name, double salary, string position, string department)
    {
        Name = name;
        Salary = salary;
        Position = position;
        Department = department;
        Email = "n/a";
        Age = 0;
    }
}

#endregion

#region Завдання 5. Перегони

class Car
{
    public string Model;
    public double FuelAmount;
    public double FuelConsumptionPerKm;
    public int DistanceTraveled;

    public Car(string model, double fuelAmount, double fuelConsumption)
    {
        Model = model;
        FuelAmount = fuelAmount;
        FuelConsumptionPerKm = fuelConsumption;
        DistanceTraveled = 0;
    }

    public void Drive(int km)
    {
        double needed = km * FuelConsumptionPerKm;
        if (needed <= FuelAmount)
        {
            FuelAmount -= needed;
            DistanceTraveled += km;
        }
        else
        {
            Console.WriteLine("Insufficient fuel for the drive");
        }
    }
}

#endregion

#region Завдання 6. Продаж автомобілів

class Engine
{
    public string Model;
    public int Power;
    public string Displacement;
    public string Efficiency;

    public Engine(string model, int power)
    {
        Model = model;
        Power = power;
        Displacement = "n/a";
        Efficiency = "n/a";
    }
}

class CarSale
{
    public string Model;
    public Engine Engine;
    public string Weight;
    public string Color;

    public CarSale(string model, Engine engine)
    {
        Model = model;
        Engine = engine;
        Weight = "n/a";
        Color = "n/a";
    }

    public void Print()
    {
        Console.WriteLine(Model + ":");
        Console.WriteLine("  " + Engine.Model + ":");
        Console.WriteLine("    Power: " + Engine.Power);
        Console.WriteLine("    Displacement: " + Engine.Displacement);
        Console.WriteLine("    Efficiency: " + Engine.Efficiency);
        Console.WriteLine("  Weight: " + Weight);
        Console.WriteLine("  Color: " + Color);
    }
}

#endregion

class Program
{
    static void Main()
    {
        //Task3();
        //Task4();
        //Task5();
        Task6();
    }

    static void Task3()
    {
        Console.WriteLine("Введіть кількість людей:");
        int n = int.Parse(Console.ReadLine());
        Family family = new Family();

        Console.WriteLine("Введіть людей у форматі <Name> <Age>:");

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            string name = input[0];
            int age = int.Parse(input[1]);
            Person person = new Person(name, age);
            family.AddMember(person);
        }

        Person oldest = family.GetOldestMember();
        Console.WriteLine("Найстарший член сім'ї:");
        Console.WriteLine(oldest.Name + " " +" " + oldest.Age);
    }

    static void Task4()
    {
        Console.WriteLine("Введіть кількість працівників:");
        int n = int.Parse(Console.ReadLine());
        Employee[] employees = new Employee[n];

        Console.WriteLine("Вводіть дані у форматі:");
        Console.WriteLine("<Name> <Salary> <Position> <Department> [Email] [Age]");

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split();
            string name = parts[0];
            double salary = double.Parse(parts[1]);
            string position = parts[2];
            string department = parts[3];
            Employee emp = new Employee(name, salary, position, department);

            if (parts.Length == 5)
            {
                if (parts[4].Contains("@")) emp.Email = parts[4];
                else emp.Age = int.Parse(parts[4]);
            }
            else if (parts.Length == 6)
            {
                emp.Email = parts[4];
                emp.Age = int.Parse(parts[5]);
            }

            employees[i] = emp;
        }

        string bestDept = "";
        double bestAvg = 0;

        for (int i = 0; i < n; i++)
        {
            string dept = employees[i].Department;
            double sum = 0;
            int count = 0;
            for (int j = 0; j < n; j++)
            {
                if (employees[j].Department == dept)
                {
                    sum += employees[j].Salary;
                    count++;
                }
            }
            double avg = sum / count;
            if (avg > bestAvg)
            {
                bestAvg = avg;
                bestDept = dept;
            }
        }

        Console.WriteLine("Highest Average Salary: " + bestDept);

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (employees[j].Salary > employees[i].Salary)
                {
                    Employee temp = employees[i];
                    employees[i] = employees[j];
                    employees[j] = temp;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (employees[i].Department == bestDept)
            {
                Console.WriteLine(employees[i].Name + " " +
                    employees[i].Salary.ToString("F2") + " " +
                    employees[i].Email + " " +
                    employees[i].Age);
            }
        }
    }

    static void Task5()
    {
        Console.WriteLine("Введіть кількість автомобілів:");
        int n = int.Parse(Console.ReadLine());
        Car[] cars = new Car[n];

        Console.WriteLine("Вводіть дані у форматі:");
        Console.WriteLine("<Model> <FuelAmount> <FuelConsumptionPerKm>");

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split();
            cars[i] = new Car(parts[0], double.Parse(parts[1]), double.Parse(parts[2]));
        }

        Console.WriteLine("Вводіть команди у форматі:");
        Console.WriteLine("Drive <CarModel> <Km> або End для завершення");

        string line;
        while ((line = Console.ReadLine()) != "End")
        {
            string[] parts = line.Split();
            string model = parts[1];
            int km = int.Parse(parts[2]);

            for (int i = 0; i < n; i++)
            {
                if (cars[i].Model == model)
                {
                    cars[i].Drive(km);
                }
            }
        }

        Console.WriteLine("Результат:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(cars[i].Model + " " +
                cars[i].FuelAmount.ToString("F2") + " " +
                cars[i].DistanceTraveled);
        }
    }

    static void Task6()
    {
        Console.WriteLine("Введіть кількість двигунів:");
        int n = int.Parse(Console.ReadLine());
        Engine[] engines = new Engine[n];

        Console.WriteLine("Вводіть дані про двигуни у форматі:");
        Console.WriteLine("<Model> <Power> [Displacement] [Efficiency]");

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split();
            Engine engine = new Engine(parts[0], int.Parse(parts[1]));
            if (parts.Length == 3)
            {
                int value;
                if (int.TryParse(parts[2], out value))
                    engine.Displacement = parts[2];
                else
                    engine.Efficiency = parts[2];
            }
            else if (parts.Length == 4)
            {
                engine.Displacement = parts[2];
                engine.Efficiency = parts[3];
            }
            engines[i] = engine;
        }

        Console.WriteLine("Введіть кількість автомобілів:");
        int m = int.Parse(Console.ReadLine());
        CarSale[] cars = new CarSale[m];

        Console.WriteLine("Вводіть дані про авто у форматі:");
        Console.WriteLine("<Model> <EngineModel> [Weight] [Color]");

        for (int i = 0; i < m; i++)
        {
            string[] parts = Console.ReadLine().Split();
            Engine found = null;
            for (int j = 0; j < n; j++)
            {
                if (engines[j].Model == parts[1]) found = engines[j];
            }

            CarSale car = new CarSale(parts[0], found);
            if (parts.Length == 3)
            {
                int value;
                if (int.TryParse(parts[2], out value))
                    car.Weight = parts[2];
                else
                    car.Color = parts[2];
            }
            else if (parts.Length == 4)
            {
                car.Weight = parts[2];
                car.Color = parts[3];
            }
            cars[i] = car;
        }

        Console.WriteLine("Результат:");
        for (int i = 0; i < m; i++)
        {
            cars[i].Print();
        }
    }
}