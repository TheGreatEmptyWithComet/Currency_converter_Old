using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;

namespace WpfAppSimpleProject
{
    internal class CurrensyConverter
    {
        private string _hrer = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date=";//20200302&json
        private string _fullHref = string.Empty;
        private DateTime date;
        public ObservableCollection<Currency> Currencies { get; private set; } = new ObservableCollection<Currency>();

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                _fullHref = _hrer + date.ToString("yyyyMMdd") + "&json";
                LoadCurrency();
            }
        }

        public string BankHrefApi
        {
            get
            {
                return _fullHref;
            }
        }
        public CurrensyConverter()
        {
            Date = DateTime.Now;
        }
        private void LoadCurrency()
        {
            WebClient webClient = new WebClient();
            string currencyAsJson = webClient.DownloadString(_fullHref);
            
            var result = JsonConvert.DeserializeObject<List<Currency>>(currencyAsJson);
        }
    }
}
