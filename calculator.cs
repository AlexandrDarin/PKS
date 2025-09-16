using System;

namespace CalculatorApp
{
    class Program
    {
        // Память калькулятора
        private static double memory = 0;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в калькулятор!");
            Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR");
            Console.WriteLine("Для выхода введите 'exit'");
            
            bool running = true;
            double currentValue = 0;
            
            while (running)
            {
                try
                {
                    Console.WriteLine($"\nТекущее значение: {currentValue}");
                    Console.Write("Введите операцию или число: ");
                    string? input = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(input))
                        continue;
                        
                    if (input.ToLower() == "exit")
                    {
                        running = false;
                        continue;
                    }
                    
                    // Обработка операций с памятью
                    if (input.ToUpper() == "M+")
                    {
                        memory += currentValue;
                        Console.WriteLine($"Значение {currentValue} добавлено в память. Текущая память: {memory}");
                        continue;
                    }
                    else if (input.ToUpper() == "M-")
                    {
                        memory -= currentValue;
                        Console.WriteLine($"Значение {currentValue} вычтено из памяти. Текущая память: {memory}");
                        continue;
                    }
                    else if (input.ToUpper() == "MR")
                    {
                        currentValue = memory;
                        Console.WriteLine($"Восстановлено значение из памяти: {memory}");
                        continue;
                    }
                    
                    // Обработка унарных операций
                    if (input == "1/x")
                    {
                        if (currentValue == 0)
                            throw new DivideByZeroException("Ошибка: деление на ноль!");
                        currentValue = 1 / currentValue;
                        Console.WriteLine($"Результат: 1/{currentValue} = {currentValue}");
                        continue;
                    }
                    else if (input == "x^2")
                    {
                        currentValue = Math.Pow(currentValue, 2);
                        Console.WriteLine($"Результат: квадрат = {currentValue}");
                        continue;
                    }
                    else if (input == "sqrt")
                    {
                        if (currentValue < 0)
                            throw new ArgumentException("Ошибка: нельзя извлечь корень из отрицательного числа!");
                        currentValue = Math.Sqrt(currentValue);
                        Console.WriteLine($"Результат: квадратный корень = {currentValue}");
                        continue;
                    }
                    
                    // Обработка бинарных операций
                    if (IsOperator(input))
                    {
                        Console.Write("Введите второе число: ");
                        string? secondInput = Console.ReadLine();
                        
                        if (string.IsNullOrEmpty(secondInput))
                            throw new ArgumentException("Ошибка: не введено второе число!");
                            
                        if (!double.TryParse(secondInput, out double secondNumber))
                            throw new FormatException("Ошибка: введено не число!");
                            
                        currentValue = PerformOperation(currentValue, secondNumber, input);
                        Console.WriteLine($"Результат: {currentValue}");
                    }
                    else
                    {
                        // Ввод нового числа
                        if (!double.TryParse(input, out double newNumber))
                            throw new FormatException("Ошибка: введено не число!");
                            
                        currentValue = newNumber;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            
            Console.WriteLine("Работа калькулятора завершена. До свидания!");
        }
        
        static bool IsOperator(string input)
        {
            return input == "+" || input == "-" || input == "*" || input == "/" || input == "%";
        }
        
        static double PerformOperation(double first, double second, string operation)
        {
            return operation switch
            {
                "+" => first + second,
                "-" => first - second,
                "*" => first * second,
                "/" => second == 0 ? throw new DivideByZeroException("Деление на ноль!") : first / second,
                "%" => second == 0 ? throw new DivideByZeroException("Деление на ноль!") : first % second,
                _ => throw new ArgumentException("Неизвестная операция!")
            };
        }
    }
}
