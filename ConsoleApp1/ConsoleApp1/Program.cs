using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    enum Currencies
    {
        BYN, //Белорусский рубль
        RUB, //Российский рубль
        EUR, //Евро
        USD, //Доллар США
        UAH, //Украинская гривна
        PLN, //Потльский злотый
        CNY, //Китайский юань
        SEK, //Шведская крона
        TRY, //Турецкая лира
        KZT, //Казахстанский тенге
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            CurrencyConverter converter = new CurrencyConverter();
            checkOnCurrenciesExisting(ref converter);
            Console.ReadKey();
        }

        static void checkOnCurrenciesExisting(ref CurrencyConverter converter)
        {
            if (converter.ExchangeRates.Count == 0)
            {
                Console.WriteLine("Нет сохранённых курсов. Желаете добавить? (Y/N)");
                Console.WriteLine("");
                handleFirstRateCreation(ref converter);
            }
            else
            {
                Console.WriteLine("Желаете добавить ещё один курс? (Y/N)");
                Console.WriteLine("");
                handleOneMoreRateCreation(ref converter);
            }
        }

        static void handleFirstRateCreation(ref CurrencyConverter converter)
        {
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.Y:
                    ExchangeRate newRate = getNewRate(ref converter);
                    converter.AddExchangeRate(newRate);
                    Console.WriteLine($"Список всех сохранённых курсов: {converter}");
                    checkOnCurrenciesExisting(ref converter);
                    break;
                case ConsoleKey.N:
                    Console.WriteLine("Нажмите любую клавишу для выхода");
                    break;
                default:
                    Console.WriteLine("Нажмите Y/N");
                    handleFirstRateCreation(ref converter);
                    break;
            }
        }

        static void handleOneMoreRateCreation(ref CurrencyConverter converter)
        {
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.Y:
                    ExchangeRate newRate = getNewRate(ref converter);
                    converter.AddExchangeRate(newRate);
                    Console.WriteLine($"Список всех сохранённых курсов: {converter}");
                    checkOnCurrenciesExisting(ref converter);
                    break;
                case ConsoleKey.N:
                    Console.WriteLine("Желаете произвести конверцию? (Y/N)");
                    handleConvertation(ref converter);
                    break;
                default:
                    Console.WriteLine("Нажмите Y/N");
                    handleOneMoreRateCreation(ref converter);
                    break;
            }
        }

        static void handleConvertation(ref CurrencyConverter converter)
        {
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.Y:
                    Console.WriteLine($"Введите валюту, которую хотите конвертировать: ");
                    Currencies fromCurrency = (Currencies)Enum.Parse(typeof(Currencies), Console.ReadLine().Trim().ToUpper());
                    Console.WriteLine($"Введите номинал валюты: ");
                    int count = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Введите валюту, в которую хотите конвертировать: ");
                    Currencies toCurrency = (Currencies)Enum.Parse(typeof(Currencies), Console.ReadLine().Trim().ToUpper());
                    if(converter.ExchangeRates.Any(rate => rate.FirstCurrency == fromCurrency && rate.SecondCurrency == toCurrency))
                    {
                        ExchangeRate convertedRate = converter.Convert(fromCurrency, toCurrency, count);
                        Console.WriteLine($"Конвертация выполнена: {convertedRate}");
                        Console.WriteLine("");
                        Console.WriteLine("Желаете произвести ещё конверцию? (Y/N)");
                        handleConvertation(ref converter);
                    }
                    else
                    {
                        Console.WriteLine("Курса к введённой валютной паре нет в системе.");
                        checkOnCurrenciesExisting(ref converter);
                    }
                    break;
                case ConsoleKey.N:
                    Console.WriteLine("Нажмите любую клавишу для выхода");
                    break;
                default:
                    Console.WriteLine("Нажмите Y/N");
                    handleConvertation(ref converter);
                    break;
            }
        }

        static ExchangeRate getNewRate(ref CurrencyConverter converter)
        {
            Console.WriteLine("Введите валютную пару, к которой хотите задать курс, в формате USD/RUB:");
            Console.Write("Допустимые валюты: ");
            foreach (int value in Enum.GetValues(typeof(Currencies)))
            {
                Console.Write($"{((Currencies)value)} ");
            };
            Console.WriteLine("");
            string pairString = Console.ReadLine();
            string[] pairArray = pairString.Split('/');
            if (pairArray.Length != 2)
            {
                Console.WriteLine("Вы вводите данные в неправильном формате. Пожалуйста, попробуйте ещё раз.");
                Console.WriteLine("");
                return getNewRate(ref converter);
            }
            Currencies firstCurrency = (Currencies)Enum.Parse(typeof(Currencies), pairArray[0].Trim().ToUpper());
            Currencies secondCurrency = (Currencies)Enum.Parse(typeof(Currencies), pairArray[1].Trim().ToUpper());
            if(converter.ExchangeRates.Any(r => r.FirstCurrency == firstCurrency && r.SecondCurrency == secondCurrency))
            {
                converter.TryToDeleteExchangeRate(firstCurrency, secondCurrency);
            }
            Console.Write($"Введите номинал валюты первой валюты ({pairArray[0].Trim().ToUpper()}): ");
            float firstValue = (float)Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            Console.Write($"Введите номинал второй валюты ({pairArray[1].Trim().ToUpper()}): ");
            float secondValue = (float)Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            ExchangeRate rate = new ExchangeRate(firstCurrency, secondCurrency, (float)Math.Round(secondValue / firstValue, 2));
            Console.WriteLine($"Добаленный курс: {rate}");
            Console.WriteLine("");
            return rate;
        }
    }
}
