using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ExchangeRate
    {
        public Currencies FirstCurrency;
        public Currencies SecondCurrency;
        public float Value;
        public int CurrencyCount = 1;

        public ExchangeRate(Currencies FirstCurrency, Currencies SecondCurrency, float Value = 1)
        {
            this.FirstCurrency = FirstCurrency;
            this.SecondCurrency = SecondCurrency;
            this.Value = Value;
        }

        public override string ToString()
        {
            return $"{CurrencyCount}{FirstCurrency} = {Value}{SecondCurrency}";
        }

        public void SetValue(float value)
        {
            this.Value = value;
        }

        public void SetCount(int count)
        {
            this.CurrencyCount = count;
        }
    }
}
