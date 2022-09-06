using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class CurrencyConverter
    {
        public List<ExchangeRate> ExchangeRates;

        public CurrencyConverter()
        {
            this.ExchangeRates = new List<ExchangeRate>();
        }

        public void AddExchangeRate(ExchangeRate rate)
        {
            TryToDeleteExchangeRate(rate.FirstCurrency, rate.SecondCurrency);
            this.ExchangeRates.Add(rate);
        }

        // Не понимаю, зачем нужен был этот метод

        //public void AddExchangeRates(ExchangeRate[] rates)
        //{
        //    this.ExchangeRates.AddRange(rates);
        //}

        public void TryToDeleteExchangeRate(Currencies firstCurrency, Currencies secondCurrency)
        {
            if (this.ExchangeRates.Any(r => r.FirstCurrency == firstCurrency && r.SecondCurrency == secondCurrency))
            {
                ExchangeRate rate = FindExchangeRate(firstCurrency, secondCurrency);
                this.ExchangeRates.Remove(rate);
            }
        }

        private ExchangeRate FindExchangeRate(Currencies firstCurrency, Currencies secondCurrency)
        {
            return this.ExchangeRates.Find(r => r.FirstCurrency == firstCurrency && r.SecondCurrency == secondCurrency);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ExchangeRate rate in this.ExchangeRates)
            {
                sb.Append($" {rate.ToString()}");
            }
            return sb.ToString();
        }

        public ExchangeRate Convert(Currencies CurrencyFirst, Currencies CurrencySecond, int count)
        {
            ExchangeRate rate = FindExchangeRate(CurrencyFirst, CurrencySecond);
            ExchangeRate convertedRate = new ExchangeRate(CurrencyFirst, CurrencySecond, count * rate.Value);
            convertedRate.SetCount(count);
            return convertedRate;
        }
    }
}
