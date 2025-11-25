using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Select a problem (1-5) or type 'END' to exit:");
            string choice = Console.ReadLine();

            if (choice == "END") break;

            switch (choice)
            {
                case "1":
                    Problem1();
                    break;
                case "2":
                    Problem2();
                    break;
                case "3":
                    Problem3();
                    break;
                case "4":
                    Problem4();
                    break;
                case "5":
                    Problem5();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    // Problem 1: Harvesting Fields
    static void Problem1()
    {
        // Define fields and the corresponding commands
        List<string> privateFields = new List<string>
        {
            "Int32 testInt", "Int64 testLong", "Calendar aCalendar", "Char testChar", "BigInteger testBigInt",
            "Thread aThread", "Object aPredicate", "Object hiddenObject", "String anotherString", "Exception internalException",
            "Stream secretStream"
        };

        List<string> protectedFields = new List<string>
        {
            "String testString", "Double aDouble", "Byte testByte", "StringBuilder aBuffer", "BigInteger testBigNumber",
            "Single testFloat", "Object testPredicate", "Object fatherMotherObject", "String moarString", "Exception inheritableException",
            "Stream moarStreamz"
        };

        List<string> publicFields = new List<string>
        {
            "Double testDouble", "String aString", "StringBuilder aBuilder", "Int16 testShort", "Byte aByte",
            "Single aFloat", "Thread testThread", "Object anObject", "Int32 anotherIntBitesTheDust", "Exception justException",
            "Stream aStream"
        };

        string command;
        while ((command = Console.ReadLine()) != "HARVEST")
        {
            switch (command)
            {
                case "private":
                    foreach (var field in privateFields)
                        Console.WriteLine($"private {field}");
                    break;
                case "protected":
                    foreach (var field in protectedFields)
                        Console.WriteLine($"protected {field}");
                    break;
                case "public":
                    foreach (var field in publicFields)
                        Console.WriteLine($"public {field}");
                    break;
                case "all":
                    foreach (var field in privateFields)
                        Console.WriteLine($"private {field}");
                    foreach (var field in protectedFields)
                        Console.WriteLine($"protected {field}");
                    foreach (var field in publicFields)
                        Console.WriteLine($"public {field}");
                    break;
            }
        }
    }

    // Problem 2: Black Box Integer
    static void Problem2()
    {
        // Simulate BlackBoxInt class
        BlackBoxInt box = new BlackBoxInt();
        string command;
        while ((command = Console.ReadLine()) != "END")
        {
            string[] parts = command.Split('_');
            string method = parts[0];
            int value = int.Parse(parts[1]);

            switch (method)
            {
                case "Add":
                    box.Add(value);
                    break;
                case "Subtract":
                    box.Subtract(value);
                    break;
                case "Multiply":
                    box.Multiply(value);
                    break;
                case "Divide":
                    box.Divide(value);
                    break;
                case "LeftShift":
                    box.LeftShift(value);
                    break;
                case "RightShift":
                    box.RightShift(value);
                    break;
            }
            Console.WriteLine(box.InternalValue);
        }
    }

    // Problem 3: Traffic Lights
    static void Problem3()
    {
        string[] lights = Console.ReadLine().Split();
        int n = int.Parse(Console.ReadLine());

        // Simulate Traffic Lights cycle
        List<string> lightCycle = new List<string> { "Red", "Green", "Yellow" };

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < lights.Length; j++)
            {
                string currentLight = lights[j];
                int index = lightCycle.IndexOf(currentLight);
                string nextLight = lightCycle[(index + 1) % 3];
                lights[j] = nextLight;
            }
            Console.WriteLine(string.Join(" ", lights));
        }
    }

    // Problem 4: Inferno Infinity
    static void Problem4()
    {
        Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
        string command;

        while ((command = Console.ReadLine()) != "END")
        {
            string[] parts = command.Split(';');
            string action = parts[0];
            string weaponName = parts[1];

            switch (action)
            {
                case "Create":
                    string weaponType = parts[2];
                    if (!weapons.ContainsKey(weaponName)) // Check if the weapon already exists
                    {
                        weapons[weaponName] = new Weapon(weaponType, weaponName);
                    }
                    break;
                case "Add":
                    if (weapons.ContainsKey(weaponName)) // Check if the weapon exists
                    {
                        int socketIndex = int.Parse(parts[2]);
                        string gemType = parts[3];
                        weapons[weaponName].AddGem(socketIndex, gemType); // Add the gem to the weapon
                    }
                    else
                    {
                        Console.WriteLine($"Weapon {weaponName} not found.");
                    }
                    break;
                case "Remove":
                    if (weapons.ContainsKey(weaponName)) // Check if the weapon exists
                    {
                        int socketIndex = int.Parse(parts[2]);
                        weapons[weaponName].RemoveGem(socketIndex); // Remove the gem from the weapon
                    }
                    else
                    {
                        Console.WriteLine($"Weapon {weaponName} not found.");
                    }
                    break;
                case "Print":
                    if (weapons.ContainsKey(weaponName)) // Check if the weapon exists
                    {
                        weapons[weaponName].PrintWeapon(); // Print weapon info
                    }
                    else
                    {
                        Console.WriteLine($"Weapon {weaponName} not found.");
                    }
                    break;
            }
        }
    }

    // Problem 5: Create Custom Class Attribute
    static void Problem5()
    {
        // Отримуємо атрибути класу Weapon
        var type = typeof(Weapon);
        var attributes = type.GetCustomAttributes(false);

        foreach (var attribute in attributes)
        {
            if (attribute is WeaponInfoAttribute weaponInfo)
            {
                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    switch (command)
                    {
                        case "Author":
                            Console.WriteLine($"Author: {weaponInfo.Author}");
                            break;
                        case "Revision":
                            Console.WriteLine($"Revision: {weaponInfo.Revision}");
                            break;
                        case "Description":
                            Console.WriteLine($"Class description: {weaponInfo.Description}");
                            break;
                        case "Reviewers":
                            Console.WriteLine($"Reviewers: {string.Join(", ", weaponInfo.Reviewers)}");
                            break;
                    }
                }
            }
        }
    }

    class BlackBoxInt
    {
        private int internalValue = 0;

        public void Add(int value) => internalValue += value;
        public void Subtract(int value) => internalValue -= value;
        public void Multiply(int value) => internalValue *= value;
        public void Divide(int value) => internalValue /= value;
        public void LeftShift(int value) => internalValue <<= value;
        public void RightShift(int value) => internalValue >>= value;

        public int InternalValue => internalValue;
    }

    [WeaponInfo("Pesho", 3, "Used for C# OOP Advanced Course - Enumerations and Attributes.", "Petro", "Stepan")]
    class Weapon
    {
        public string Name { get; set; }
        public string Type { get; set; }
        private List<string> Gems { get; set; } = new List<string>();

        public Weapon(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public void AddGem(int index, string gemType)
        {
            if (Gems.Count <= index)
                Gems.Add(gemType); // Add gem at the specified index
            else
                Gems[index] = gemType; // Replace gem at the specified index
        }

        public void RemoveGem(int index)
        {
            if (index >= 0 && index < Gems.Count)
                Gems.RemoveAt(index); // Remove gem from the specified index
        }

        public void PrintWeapon()
        {
            Console.WriteLine($"{Name}: {Type} Weapon Info");
            // Add logic to print the actual weapon info with gems and stats
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class WeaponInfoAttribute : Attribute
    {
        public string Author { get; }
        public int Revision { get; }
        public string Description { get; }
        public string[] Reviewers { get; }

        public WeaponInfoAttribute(string author, int revision, string description, params string[] reviewers)
        {
            Author = author;
            Revision = revision;
            Description = description;
            Reviewers = reviewers;
        }
    }
}
