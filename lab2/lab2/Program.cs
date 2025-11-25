using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Task1();
        Task2();
        Task3();
        Task4();
        Task5();
        Task6();
        Task7();
        Task8();
        Task9();
        Task10();
    }

    // ===== 1 =====

    static void Task1()
    {
        string[] arr1 = { "hi", "php", "java", "csharp", "sql", "html", "css", "js" };
        string[] arr2 = { "hi", "php", "java", "js", "softuni", "nakov", "java", "learn" };

        int left = 0; 
        for (int i = 0; i < Math.Min(arr1.Length, arr2.Length); i++)
        {
            if (arr1[i] == arr2[i]) left++;
            else break;
        }

        int right = 0;
        for (int i = 0; i < Math.Min(arr1.Length, arr2.Length); i++)
        {
            if (arr1[arr1.Length - 1 - i] == arr2[arr2.Length - 1 - i]) right++;
            else break;
        }

        if (left > right)
            Console.WriteLine("1) Найбільший спільний кінець ліворуч довжини " + left);
        else if (right > left)
            Console.WriteLine("1) Найбільший спільний кінець праворуч довжини " + right);
        else
            Console.WriteLine("1) Немає спільних кінців");
    }

    // ===== 2 =====

    static void Task2()
    {
        int[] arr = { 3, 2, 4, -1 };
        int k = 2;
        int[] sum = new int[arr.Length];
        int[] temp = (int[])arr.Clone();

        for (int r = 0; r < k; r++)
        {
            temp = RotateRight(temp, 1);
            for (int i = 0; i < arr.Length; i++)
                sum[i] += temp[i];
        }

        Console.WriteLine("2) Сума після обертань: " + string.Join(" ", sum));
    }
    static int[] RotateRight(int[] arr, int k)
    {
        int n = arr.Length;
        int[] res = new int[n];
        for (int i = 0; i < n; i++)
            res[(i + k) % n] = arr[i];
        return res;
    }

    // ===== 3 =====

    static void Task3()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8 };
        int k = arr.Length / 4;

        int[] left = arr.Take(k).Reverse().ToArray();
        int[] right = arr.Skip(3 * k).Reverse().ToArray();
        int[] top = left.Concat(right).ToArray();
        int[] bottom = arr.Skip(k).Take(2 * k).ToArray();

        int[] sum = new int[2 * k];
        for (int i = 0; i < 2 * k; i++)
            sum[i] = top[i] + bottom[i];

        Console.WriteLine("3) Fold and Sum: " + string.Join(" ", sum));
    }

    // ===== 4 =====

    static void Task4()
    {
        int n = 25;
        bool[] prime = Enumerable.Repeat(true, n + 1).ToArray();
        prime[0] = prime[1] = false;

        for (int p = 2; p * p <= n; p++)
        {
            if (prime[p])
            {
                for (int multiple = p * p; multiple <= n; multiple += p)
                    prime[multiple] = false;
            }
        }

        Console.WriteLine("4) Прості числа до " + n + ":");
        for (int i = 2; i <= n; i++)
            if (prime[i]) Console.Write(i + " ");
        Console.WriteLine();
    }

    // ===== 5 =====

    static void Task5()
    {
        char[] a = { 'a', 'n', 'n', 'i', 'e' };
        char[] b = { 'a', 'n' };

        string s1 = new string(a);
        string s2 = new string(b);

        if (string.Compare(s1, s2) <= 0)
            Console.WriteLine("5) " + s1 + " " + s2);
        else
            Console.WriteLine("5) " + s2 + " " + s1);
    }

    // ===== 6 =====

    static void Task6()
    {
        int[] arr = { 2, 1, 1, 2, 3, 3, 2, 2, 2, 1 };
        int bestLen = 1, bestStart = 0;
        int curLen = 1;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == arr[i - 1]) curLen++;
            else curLen = 1;

            if (curLen > bestLen)
            {
                bestLen = curLen;
                bestStart = i - curLen + 1;
            }
        }

        Console.Write("6) Найдовша послідовність: ");
        for (int i = 0; i < bestLen; i++)
            Console.Write(arr[bestStart + i] + " ");
        Console.WriteLine();
    }

    // ===== 7 =====

    static void Task7()
    {
        int[] arr = { 3, 2, 3, 4, 2, 2, 4 };
        int bestLen = 1, bestStart = 0;
        int curLen = 1, curStart = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > arr[i - 1]) curLen++;
            else
            {
                curLen = 1;
                curStart = i;
            }

            if (curLen > bestLen)
            {
                bestLen = curLen;
                bestStart = curStart;
            }
        }

        Console.Write("7) Найдовша зростаюча послідовність: ");
        for (int i = 0; i < bestLen; i++)
            Console.Write(arr[bestStart + i] + " ");
        Console.WriteLine();
    }

    // ===== 8 =====

    static void Task8()
    {
        int[] arr = { 4, 1, 1, 4, 2, 3, 4, 4, 1, 2, 4, 9, 3 };
        Dictionary<int, int> freq = new Dictionary<int, int>();

        foreach (int x in arr)
        {
            if (!freq.ContainsKey(x)) freq[x] = 0;
            freq[x]++;
        }

        int bestVal = arr[0], bestCount = 0;
        foreach (var kv in freq)
        {
            if (kv.Value > bestCount)
            {
                bestVal = kv.Key;
                bestCount = kv.Value;
            }
        }

        Console.WriteLine($"8) Число {bestVal} зустрічається {bestCount} разів");
    }

    // ===== 9 =====
    
    static void Task9()
    {
        string word = "softuni";
        char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        Console.WriteLine("9) Індекси букв у слові '" + word + "':");
        foreach (char c in word)
        {
            int index = Array.IndexOf(alphabet, c);
            Console.WriteLine(c + " -> " + index);
        }
    }

    // ===== 10 =====
    
    static void Task10()
    {
        int[] arr = { 1, 5, 3, 4, 2 };
        int diff = 2;
        int count = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (Math.Abs(arr[i] - arr[j]) == diff)
                    count++;
            }
        }

        Console.WriteLine($"10) Кількість пар з різницею {diff}: {count}");
    }
}