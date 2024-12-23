using System;
using System.Linq;
using System.Text;

class Program
{
    delegate int CalculationOperation(int[] array);
    delegate void ModificationOperation(int[] array);

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        int[] array = { -3, 5, -1, 6, 2, 0, 9, -7, 8, 4 };

        Console.WriteLine("Виберіть тип операції з масивом:");
        Console.WriteLine("1. Обчислення значення");
        Console.WriteLine("2. Зміна масиву");

        string typeChoice = Console.ReadLine();

        Action<int> operationType = type =>
        {
            var calculationOperations = new CalculationOperation[]
            {
                CountNegativeElements,
                SumAllElements,
                CountPrimeNumbers
            };

            var modificationOperations = new ModificationOperation[]
            {
                ReplaceNegativesWithZero,
                SortArray,
                MoveEvenToFront
            };

            if (type == 1)
            {
                Console.WriteLine("Виберіть операцію:");
                Console.WriteLine("1. Обчислити кількість негативних елементів");
                Console.WriteLine("2. Визначити суму всіх елементів");
                Console.WriteLine("3. Обрахувати кількість простих чисел");

                int calcChoice = ReadValidNumber(1, calculationOperations.Length) - 1;
                Console.WriteLine($"Результат: {calculationOperations[calcChoice](array)}");
            }
            else if (type == 2)
            {
                Console.WriteLine("Виберіть операцію:");
                Console.WriteLine("1. Змінити всі негативні елементи на 0");
                Console.WriteLine("2. Відсортувати масив");
                Console.WriteLine("3. Перемістити всі парні елементи на початок");

                int modChoice = ReadValidNumber(1, modificationOperations.Length) - 1;
                modificationOperations[modChoice](array);

                Console.WriteLine("Результат:");
                PrintArray(array);
            }
        };

        operationType(ValidateTypeChoice(typeChoice, 1, 2));
    }

    static int CountNegativeElements(int[] array) => array.Count(x => x < 0);

    static int SumAllElements(int[] array) => array.Sum();

    static int CountPrimeNumbers(int[] array) =>
        array.Count(x => x > 1 && Enumerable.Range(2, (int)Math.Sqrt(x)).All(d => x % d != 0));

    static void ReplaceNegativesWithZero(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] < 0) array[i] = 0;
    }

    static void SortArray(int[] array) => Array.Sort(array);

    static void MoveEvenToFront(int[] array)
    {
        var sorted = array.OrderBy(x => x % 2 != 0).ToArray();
        Array.Copy(sorted, array, array.Length);
    }

    static void PrintArray(int[] array) => Console.WriteLine(string.Join(", ", array));

    static int ReadValidNumber(int min, int max)
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number) || number < min || number > max)
            Console.WriteLine($"Будь ласка, введіть число від {min} до {max}:");
        return number;
    }

    static int ValidateTypeChoice(string input, int min, int max)
    {
        if (!int.TryParse(input, out int choice) || choice < min || choice > max)
        {
            Console.WriteLine($"Вибір некоректний. Введіть число від {min} до {max}:");
            return ValidateTypeChoice(Console.ReadLine(), min, max);
        }
        return choice;
    }
}
