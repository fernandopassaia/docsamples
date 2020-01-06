using Domain.Utils.ConsoleTests.NEWWSDL;
using System;
using System.Data;

namespace Natific.HufToEur
{
    class Program
    {
        static void GetExchangeRates(string startDate, string endDate, string currencyNames)
        {            
            MNBArfolyamServiceSoapClient client = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody Data = new GetExchangeRatesRequestBody();            

            Data.startDate = startDate;
            Data.endDate = endDate;
            Data.currencyNames = currencyNames;
            GetExchangeRatesResponseBody exchanges = client.GetExchangeRates(Data);
            var dataOfExchanges = exchanges.GetExchangeRatesResult;

            //now I'm gonna take the LAST value for HUF (I'm using Techinicals like FOR and WHILE to show that i can use them).
            string data = "";
            string value = "";
            bool valueFound = false;
            for (int i = 0; i < dataOfExchanges.Length; i++)
            {
                if (!valueFound)
                {
                    if (dataOfExchanges.Substring(i, 9) == "Day date=")
                    {
                        i++;
                        data = dataOfExchanges.Substring(i + 9, 10);
                    }
                    if (dataOfExchanges.Substring(i, 3) == "EUR")
                    {
                        i += 5;
                        while (dataOfExchanges.Substring(i, 1) != "<") //here i will get when </Rate> (value is over, i'm doing this because sometimes HUF came as 323,69 or 324,1 (can't use substring)
                        {
                            value = value + dataOfExchanges.Substring(i, 1);
                            i++;
                        }
                        valueFound = true;
                    }
                }
            }

            Console.WriteLine("Last Day Available: " + data + ". HUF to EUR: " + value);
        }

        static void Main()
        {            
            //I'm gonna take the last 5 days of HUF (because sometimes they don't have the day value on WS, like weekends or holiday)
            //then on the Main Method i will show the LAST Exchange Rate...
            GetExchangeRates(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), "EUR");         
            Console.ReadKey();            
        }
    }
}
