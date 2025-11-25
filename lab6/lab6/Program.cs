using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab6
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Оберіть завдання (1–4):");
            Console.WriteLine("1 - Книгарня");
            Console.WriteLine("2 - Людство");
            Console.WriteLine("3 - Онлайн-радіо");
            Console.WriteLine("4 - Mordor");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunTask1();
                    break;
                case "2":
                    RunTask2();
                    break;
                case "3":
                    RunTask3();
                    break;
                case "4":
                    RunTask4();
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }

        // =========================
        // Завдання 1: Книгарня
        // =========================
        static void RunTask1()
        {
            try
            {
                Console.Write("Введіть автора: ");
                string author = Console.ReadLine();
                Console.Write("Введіть назву книги: ");
                string title = Console.ReadLine();
                Console.Write("Введіть ціну книги: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Book book = new Book(author, title, price);
                GoldenEditionBook goldenEditionBook = new GoldenEditionBook(author, title, price);

                Console.WriteLine();
                Console.WriteLine(book + Environment.NewLine);
                Console.WriteLine(goldenEditionBook);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        // =========================
        // Завдання 2: Людство
        // =========================
        static void RunTask2()
        {
            try
            {
                Console.WriteLine("Введіть дані студента (Ім'я Прізвище НомерФакультету):");
                string[] studentData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Student student = new Student(studentData[0], studentData[1], studentData[2]);

                Console.WriteLine("Введіть дані працівника (Ім'я Прізвище Зарплата Годин/день):");
                string[] workerData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Worker worker = new Worker(workerData[0], workerData[1],
                    double.Parse(workerData[2]), double.Parse(workerData[3]));

                Console.WriteLine();
                Console.WriteLine(student);
                Console.WriteLine();
                Console.WriteLine(worker);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        // =========================
        // Завдання 3: Онлайн-радіо
        // =========================
        static void RunTask3()
        {
            Console.Write("Введіть кількість пісень: ");
            int n = int.Parse(Console.ReadLine());
            List<Song> songs = new List<Song>();

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введіть пісню {i + 1} у форматі <виконавець>;<назва>;<хв:сек>: ");
                string input = Console.ReadLine();

                try
                {
                    string[] parts = input.Split(';');
                    Song song = new Song(parts[0], parts[1], parts[2]);
                    songs.Add(song);
                    Console.WriteLine("Song added.");
                }
                catch (InvalidSongException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            int totalSeconds = songs.Sum(s => s.TotalSeconds);
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            Console.WriteLine($"Songs added: {songs.Count}");
            Console.WriteLine($"Playlist length: {hours}h {minutes}m {seconds}s");
        }

        // =========================
        // Завдання 4: Mordor
        // =========================
        static void RunTask4()
        {
            Console.WriteLine("Введіть усю їжу, яку з'їв Гендальф (через пробіл):");
            Console.WriteLine("\n===== Відомі продукти =====");
            Console.WriteLine("Cram → +2 балів");
            Console.WriteLine("Lembas → +3 бали");
            Console.WriteLine("Apple → +1 бал");
            Console.WriteLine("Melon → +1 бал");
            Console.WriteLine("HoneyCake → +5 балів");
            Console.WriteLine("Mushrooms → -10 балів");
            Console.WriteLine("Інша їжа → -1 бал");

            string[] foodsInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            FoodFactory foodFactory = new FoodFactory();
            MoodFactory moodFactory = new MoodFactory();
            int happiness = 0;

            foreach (var foodName in foodsInput)
            {
                Food food = foodFactory.CreateFood(foodName);
                happiness += food.HappinessPoints;
            }

            Mood mood = moodFactory.CreateMood(happiness);

            Console.WriteLine(happiness);
            Console.WriteLine(mood.GetType().Name);
        }
    }

    #region Task1 Bookstore
    public class Book
    {
        private string title;
        private string author;
        private decimal price;

        public Book(string author, string title, decimal price)
        {
            Author = author;
            Title = title;
            Price = price;
        }

        public string Author
        {
            get => author;
            private set
            {
                if (char.IsDigit(value.Split(' ')[^1][0]))
                    throw new ArgumentException("Author not valid!");
                author = value;
            }
        }

        public string Title
        {
            get => title;
            private set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Title not valid!");
                title = value;
            }
        }

        public virtual decimal Price
        {
            get => price;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Price not valid!");
                price = value;
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"Type: {GetType().Name}")
                  .AppendLine($"Title: {Title}")
                  .AppendLine($"Author: {Author}")
                  .AppendLine($"Price: {Price:f2}");
            return result.ToString().TrimEnd();
        }
    }

    public class GoldenEditionBook : Book
    {
        public GoldenEditionBook(string author, string title, decimal price)
            : base(author, title, price)
        { }

        public override decimal Price => base.Price * 1.3m;
    }
    #endregion

    #region Task2 Humanity
    public class Human
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        public Human(string firstName, string lastName)
        {
            if (!char.IsUpper(firstName[0]))
                throw new ArgumentException("Expected upper case letter! Argument: firstName");
            if (firstName.Length < 4)
                throw new ArgumentException("Expected length at least 4 symbols! Argument: firstName");
            if (!char.IsUpper(lastName[0]))
                throw new ArgumentException("Expected upper case letter! Argument: lastName");
            if (lastName.Length < 3)
                throw new ArgumentException("Expected length at least 3 symbols! Argument: lastName");

            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class Student : Human
    {
        public string FacultyNumber { get; }

        public Student(string firstName, string lastName, string facultyNumber)
            : base(firstName, lastName)
        {
            if (facultyNumber.Length < 5 || facultyNumber.Length > 10 || !facultyNumber.All(char.IsLetterOrDigit))
                throw new ArgumentException("Invalid faculty number!");
            FacultyNumber = facultyNumber;
        }

        public override string ToString()
        {
            return $"First Name: {FirstName}\nLast Name: {LastName}\nFaculty number: {FacultyNumber}";
        }
    }

    public class Worker : Human
    {
        public double WeekSalary { get; }
        public double WorkHoursPerDay { get; }

        public Worker(string firstName, string lastName, double weekSalary, double workHoursPerDay)
            : base(firstName, lastName)
        {
            if (weekSalary <= 10)
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            if (workHoursPerDay < 1 || workHoursPerDay > 12)
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");

            WeekSalary = weekSalary;
            WorkHoursPerDay = workHoursPerDay;
        }

        public double SalaryPerHour => WeekSalary / (5 * WorkHoursPerDay);

        public override string ToString()
        {
            return $"First Name: {FirstName}\nLast Name: {LastName}\nWeek Salary: {WeekSalary:F2}\nHours per day: {WorkHoursPerDay:F2}\nSalary per hour: {SalaryPerHour:F2}";
        }
    }
    #endregion

    #region Task3 Online Radio
    public class InvalidSongException : Exception
    {
        public InvalidSongException(string message = "Invalid song.") : base(message) { }
    }

    public class InvalidArtistNameException : InvalidSongException
    {
        public InvalidArtistNameException() : base("Artist name should be between 3 and 20 symbols.") { }
    }

    public class InvalidSongNameException : InvalidSongException
    {
        public InvalidSongNameException() : base("Song name should be between 3 and 30 symbols.") { }
    }

    public class InvalidSongLengthException : InvalidSongException
    {
        public InvalidSongLengthException(string message = "Invalid song length.") : base(message) { }
    }

    public class InvalidSongMinutesException : InvalidSongLengthException
    {
        public InvalidSongMinutesException() : base("Song minutes should be between 0 and 14.") { }
    }

    public class InvalidSongSecondsException : InvalidSongLengthException
    {
        public InvalidSongSecondsException() : base("Song seconds should be between 0 and 59.") { }
    }

    public class Song
    {
        public string ArtistName { get; }
        public string SongName { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public Song(string artistName, string songName, string length)
        {
            if (artistName.Length < 3 || artistName.Length > 20)
                throw new InvalidArtistNameException();
            if (songName.Length < 3 || songName.Length > 30)
                throw new InvalidSongNameException();

            var parts = length.Split(':');
            if (parts.Length != 2)
                throw new InvalidSongLengthException();

            if (!int.TryParse(parts[0], out int minutes))
                throw new InvalidSongMinutesException();
            if (!int.TryParse(parts[1], out int seconds))
                throw new InvalidSongSecondsException();

            if (minutes < 0 || minutes > 14)
                throw new InvalidSongMinutesException();
            if (seconds < 0 || seconds > 59)
                throw new InvalidSongSecondsException();

            ArtistName = artistName;
            SongName = songName;
            Minutes = minutes;
            Seconds = seconds;
        }

        public int TotalSeconds => Minutes * 60 + Seconds;
    }
    #endregion

    #region Task4 Mordor
    public abstract class Food
    {
        public int HappinessPoints { get; protected set; }
    }

    public class Cram : Food { public Cram() { HappinessPoints = 2; } }
    public class Lembas : Food { public Lembas() { HappinessPoints = 3; } }
    public class Apple : Food { public Apple() { HappinessPoints = 1; } }
    public class Melon : Food { public Melon() { HappinessPoints = 1; } }
    public class HoneyCake : Food { public HoneyCake() { HappinessPoints = 5; } }
    public class Mushrooms : Food { public Mushrooms() { HappinessPoints = -10; } }
    public class OtherFood : Food { public OtherFood() { HappinessPoints = -1; } }

    public class FoodFactory
    {
        public Food CreateFood(string name)
        {
            name = name.ToLower();
            return name switch
            {
                "cram" => new Cram(),
                "lembas" => new Lembas(),
                "apple" => new Apple(),
                "melon" => new Melon(),
                "honeycake" => new HoneyCake(),
                "mushrooms" => new Mushrooms(),
                _ => new OtherFood()
            };
        }
    }

    public abstract class Mood { }

    public class Angry : Mood { }
    public class Sad : Mood { }
    public class Happy : Mood { }
    public class JavaScript : Mood { }

    public class MoodFactory
    {
        public Mood CreateMood(int happiness)
        {
            if (happiness < -5) return new Angry();
            if (happiness <= 0) return new Sad();
            if (happiness <= 15) return new Happy();
            return new JavaScript();
        }
    }
    #endregion
}
