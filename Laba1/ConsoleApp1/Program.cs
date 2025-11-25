using System;

class Program
{
    static void Main()
    {



        Console.WriteLine("#5");
        sbyte num1 = -100;
        byte num2 = 128;
        short num3 = -3546;
        ushort num4 = 64876;
        uint num5 = 2147483648;
        long num6 = 1141583228;
        Console.WriteLine(num1);
        Console.WriteLine(num2);
        Console.WriteLine(num3);
        Console.WriteLine(num4);
        Console.WriteLine(num5);
        Console.WriteLine(num6);




        Console.WriteLine();
        Console.WriteLine("#6");
        double d1 = 3.1231231424141;
        float d2 = 1.1231231f;
        decimal d3 = 7.12312314241413123131m;
        Console.WriteLine(d1);
        Console.WriteLine(d2);
        Console.WriteLine(d3);




        Console.WriteLine();
        Console.WriteLine("#7");
        string univ = "CHNU";
        char b = 'B';
        char y = 'y';
        string phrase = "I love prog";
        Console.WriteLine(univ);
        Console.WriteLine(b);
        Console.WriteLine(y);
        Console.WriteLine(phrase);




        Console.WriteLine();
        Console.WriteLine("#8");
        double a1 = double.Parse(Console.ReadLine());
        double a2 = double.Parse(Console.ReadLine());
        double a3 = double.Parse(Console.ReadLine());

        double avg = (a1 + a2 + a3) / 3;

        Console.WriteLine($"Середнє знач: {avg}");




        Console.WriteLine();
        Console.WriteLine("#9");
        double b1 = double.Parse(Console.ReadLine());
        double b2 = double.Parse(Console.ReadLine());
        double b3 = double.Parse(Console.ReadLine());

        double S = ((b1 + b2) / 2) * b3;

        Console.WriteLine($"Площа: {S}");




        Console.WriteLine();
        Console.WriteLine("#10");
        double n = double.Parse(Console.ReadLine());


        double last = n % 10;

        Console.WriteLine($"last digit{n}: {last}");




        Console.WriteLine();
        Console.WriteLine("#11");
        int number = int.Parse(Console.ReadLine());
        Console.WriteLine("Введіть число з кінця: ");
        int num = int.Parse(Console.ReadLine());
        string res = "-";

        if (num > 0 && num <= number.ToString().Length)
        {
            int digit = (number / (int)Math.Pow(10, num - 1)) % 10;
            res = digit.ToString();
        }

        Console.WriteLine($"Ваша цифра: {res}");





        Console.WriteLine();
        Console.WriteLine("#12");

        int numb = int.Parse(Console.ReadLine());


        bool resultat = (numb > 20) && (numb % 2 == 1);

        Console.WriteLine(resultat);



        Console.WriteLine();
        Console.WriteLine("#13");



        int numberr = int.Parse(Console.ReadLine());


        bool resultt = (numberr % 9 == 0||numberr % 11 == 0||numberr % 13 == 0);

        Console.WriteLine(resultt);




        Console.WriteLine();
        Console.WriteLine("#14");
        int z = int.Parse(Console.ReadLine());
        int x = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());

        int biggest = z;
        if (biggest < x)
        {
            biggest = x;
        }
        if (biggest < c)
        {
            biggest = c;
        }
        Console.WriteLine($"Ваша цифра: {biggest}");




        Console.WriteLine();
        Console.WriteLine("#15");
        double z1 = double.Parse(Console.ReadLine());
        double x1 = double.Parse(Console.ReadLine());
        double c1 = double.Parse(Console.ReadLine());

        int prod = 0;
        if (z1 < 0)
        {
            prod++;
        }
        if (x1 < 0)
        {
            prod++;
        }
        if (c1 < 0)
        {
            prod++;
        }
        if (prod == 0 || prod == 2)
        {
            Console.WriteLine($"Ваш добуток: додатній");
        }
        else
        {
            Console.WriteLine($"Ваш добуток: від'ємний");
        }




        Console.WriteLine();
        Console.WriteLine("#16");
        double day = double.Parse(Console.ReadLine());


        switch (day)
        {
            case 1:
                Console.WriteLine("monday");
                break;
            case 2:
                Console.WriteLine("tuesday");
                break;
            case 3:
                Console.WriteLine("wednesday");
                break;
            case 4:
                Console.WriteLine("thursday");
                break;
            case 5:
                Console.WriteLine("friday");
                break;
            case 6:
                Console.WriteLine("saturday");
                break;
            case 7:
                Console.WriteLine("sunday");
                break;
            default:
                Console.WriteLine("wrong");
                break;
        }




        Console.WriteLine();
        Console.WriteLine("#17");
        int nomer = int.Parse(Console.ReadLine());

        long factorial = 1;

        if (nomer >= 0)
        {
            for (int i = 1; i <= nomer; i++)
            {
                factorial *= i;
            }
        }
        Console.WriteLine($"Факторіал числа {nomer} = {factorial}");

    }
}