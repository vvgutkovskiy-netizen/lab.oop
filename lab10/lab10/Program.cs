using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ------------------- ЗАВДАННЯ 1 і 2 -------------------
class ListyIterator<T> : IEnumerable<T>
{
    private List<T> items;
    private int index;

    public ListyIterator(IEnumerable<T> collection)
    {
        items = new List<T>(collection);
        index = 0;
    }

    public bool Move()
    {
        if (HasNext())
        {
            index++;
            return true;
        }
        return false;
    }

    public bool HasNext()
    {
        return index + 1 < items.Count;
    }

    public void Print()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Invalid Operation!");
        Console.WriteLine(items[index]);
    }

    public void PrintAll()
    {
        Console.WriteLine(string.Join(" ", items));
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in items)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// ------------------- ЗАВДАННЯ 3 -------------------
class Person : IComparable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }

    public Person(string name, int age, string city)
    {
        Name = name;
        Age = age;
        City = city;
    }

    public int CompareTo(Person other)
    {
        int result = Name.CompareTo(other.Name);
        if (result == 0)
        {
            result = Age.CompareTo(other.Age);
            if (result == 0)
                result = City.CompareTo(other.City);
        }
        return result;
    }
}

// ------------------- ЗАВДАННЯ 4 -------------------
class PersonByNameLengthComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        int result = x.Name.Length.CompareTo(y.Name.Length);
        if (result == 0)
            result = string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        return result;
    }
}

class PersonByAgeComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        return x.Age.CompareTo(y.Age);
    }
}

// ------------------- ЗАВДАННЯ 5 -------------------
class Pet
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Kind { get; set; }

    public Pet(string name, int age, string kind)
    {
        Name = name;
        Age = age;
        Kind = kind;
    }

    public override string ToString() => $"{Name} {Age} {Kind}";
}

class Clinic
{
    private Pet[] rooms;

    public Clinic(int roomsCount)
    {
        if (roomsCount % 2 == 0)
            throw new ArgumentException("Invalid Operation!");
        rooms = new Pet[roomsCount];
    }

    public bool Add(Pet pet)
    {
        int middle = rooms.Length / 2;

        for (int i = 0; i < rooms.Length; i++)
        {
            int index = (i % 2 == 0) ? middle - i / 2 : middle + (i + 1) / 2;
            if (index >= 0 && index < rooms.Length && rooms[index] == null)
            {
                rooms[index] = pet;
                return true;
            }
        }
        return false;
    }

    public bool Release()
    {
        int middle = rooms.Length / 2;
        for (int i = middle; i < rooms.Length; i++)
        {
            if (rooms[i] != null)
            {
                rooms[i] = null;
                return true;
            }
        }
        for (int i = 0; i < middle; i++)
        {
            if (rooms[i] != null)
            {
                rooms[i] = null;
                return true;
            }
        }
        return false;
    }

    public bool HasEmptyRooms() => rooms.Any(r => r == null);

    public void Print()
    {
        foreach (var room in rooms)
            Console.WriteLine(room?.ToString() ?? "Room empty");
    }

    public void Print(int room)
    {
        if (rooms[room - 1] != null)
            Console.WriteLine(rooms[room - 1]);
        else
            Console.WriteLine("Room empty");
    }
}



// ------------------- ПРОГРАМА -------------------
class Program
{
    static void RunTask1And2()
    {
        Console.WriteLine("Приклад:");
        Console.WriteLine("Create 1 2 3");
        Console.WriteLine("Move");
        Console.WriteLine("HasNext");
        Console.WriteLine("Print");
        Console.WriteLine("PrintAll");
        Console.WriteLine("END");
        Console.WriteLine("--------------------------------");

        ListyIterator<string> iterator = null;
        string command;

        while ((command = Console.ReadLine()) != "END")
        {
            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                switch (parts[0])
                {
                    case "Create":
                        iterator = new ListyIterator<string>(parts.Skip(1));
                        break;
                    case "Move":
                        Console.WriteLine(iterator.Move());
                        break;
                    case "HasNext":
                        Console.WriteLine(iterator.HasNext());
                        break;
                    case "Print":
                        iterator.Print();
                        break;
                    case "PrintAll":
                        iterator.PrintAll();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static void RunTask3()
    {
        Console.WriteLine("Приклад:");
        Console.WriteLine("Pesho 22 Vraca");
        Console.WriteLine("Gogo 22 Vraca");
        Console.WriteLine("Gogo 22 Vraca");
        Console.WriteLine("END");
        Console.WriteLine("2");
        Console.WriteLine("--------------------------------");

        var people = new List<Person>();
        string line;
        while ((line = Console.ReadLine()) != "END")
        {
            var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 3)
                people.Add(new Person(tokens[0], int.Parse(tokens[1]), tokens[2]));
        }

        int n = int.Parse(Console.ReadLine());
        var personToCompare = people[n - 1];

        int equal = people.Count(p => p.CompareTo(personToCompare) == 0);
        if (equal == 1) Console.WriteLine("No matches");
        else Console.WriteLine($"{equal} {people.Count - equal} {people.Count}");
    }

    static void RunTask4()
    {
        Console.WriteLine("Приклад:");
        Console.WriteLine("3");
        Console.WriteLine("Pesho 20");
        Console.WriteLine("Joro 100");
        Console.WriteLine("Pencho 1");
        Console.WriteLine("--------------------------------");

        int n = int.Parse(Console.ReadLine());
        var people = new List<Person>();

        for (int i = 0; i < n; i++)
        {
            var tokens = Console.ReadLine().Split();
            people.Add(new Person(tokens[0], int.Parse(tokens[1]), "City"));
        }

        var byName = new SortedSet<Person>(new PersonByNameLengthComparer());
        var byAge = new SortedSet<Person>(new PersonByAgeComparer());

        foreach (var p in people)
        {
            byName.Add(p);
            byAge.Add(p);
        }

        foreach (var p in byName) Console.WriteLine($"{p.Name} {p.Age}");
        foreach (var p in byAge) Console.WriteLine($"{p.Name} {p.Age}");
    }

    static void RunTask5()
    {
        Console.WriteLine("Приклад:");
        Console.WriteLine("Create Pet Gosho 5 Cat");
        Console.WriteLine("Create Clinic Sunny 3");
        Console.WriteLine("Add Gosho Sunny");
        Console.WriteLine("Print Sunny");
        Console.WriteLine("END");
        Console.WriteLine("--------------------------------");

        var pets = new Dictionary<string, Pet>();
        var clinics = new Dictionary<string, Clinic>();

        string command;
        while ((command = Console.ReadLine()) != "END")
        {
            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                switch (parts[0])
                {
                    case "Create":
                        if (parts[1] == "Pet")
                        {
                            pets[parts[2]] = new Pet(parts[2], int.Parse(parts[3]), parts[4]);
                        }
                        else if (parts[1] == "Clinic")
                        {
                            clinics[parts[2]] = new Clinic(int.Parse(parts[3]));
                        }
                        break;

                    case "Add":
                        if (pets.ContainsKey(parts[1]) && clinics.ContainsKey(parts[2]))
                            Console.WriteLine(clinics[parts[2]].Add(pets[parts[1]]));
                        else
                            Console.WriteLine("Invalid Operation!");
                        break;

                    case "Release":
                        Console.WriteLine(clinics[parts[1]].Release());
                        break;

                    case "HasEmptyRooms":
                        Console.WriteLine(clinics[parts[1]].HasEmptyRooms());
                        break;

                    case "Print":
                        if (parts.Length == 2)
                            clinics[parts[1]].Print();
                        else
                            clinics[parts[1]].Print(int.Parse(parts[2]));
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid Operation!");
            }
        }
    }

    // ------------------- MAIN -------------------
    static void Main()
    {
        Console.WriteLine("Виберіть завдання (1–5):");
        int task = int.Parse(Console.ReadLine());

        switch (task)
        {
            case 1:
            case 2: RunTask1And2(); break;
            case 3: RunTask3(); break;
            case 4: RunTask4(); break;
            case 5: RunTask5(); break;
            default: Console.WriteLine("Невірний номер завдання."); break;
        }
    }
}