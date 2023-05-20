using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EsCpnv
{
    public class Machine
    {
        #region private attributes
        /* _items - List of items sold by this machine
         * _change - The remaining change in this machine
         * _balance - The balance of this machine (total of sold items prices)
         * _hoursRevenue - List of the revenue made with the hour as index (0-23)
         * _bestRevenue - The top 3 of the best revenue by hour
         * _setTime -  The manually time for the revenue by hour check
         */
        private List<Item> _items;
        private decimal _change;
        private decimal _balance;
        private List<Decimal> _hoursRevenue;
        private string _bestRevenue;
        private string _setTime;
        #endregion

        #region constructor
        public Machine(List<Item> items)
        {
            _items = items;

            _hoursRevenue = new List<Decimal>();

            for(int i = 0; i < 24; i++) {
                _hoursRevenue.Add(0.00M);
            }
        }
        #endregion

        #region public methods
        /* This method is used to add an amount to the current change of this machine */
        public void Insert(decimal amount)
        {
            _change += amount;
        }

        /* This method is used to choose the item and return the result of the operation */
        public string Choose(string code)
        {
            int index = _items.FindIndex(item => item.Code == code);
            if (index >= 0)
            {
                Item _item = _items[index];
                if (_change >= _item.Price)
                {
                    if (_item.Quantity > 0)
                    {
                        _item.Quantity -= 1;
                        _balance += _item.Price;
                        _change -= _item.Price;
                        if (String.IsNullOrEmpty(_setTime))
                        {
                            _hoursRevenue[DateTime.Now.Hour] += _item.Price;
                        }
                        else
                        {
                            var dateTime = DateTime.ParseExact(_setTime, "s", null);
                            _hoursRevenue[dateTime.Hour] += _item.Price;
                            _setTime = null;
                        }
                        return $"Vending {_item.Name}";
                    }
                    else
                    {
                        return $"Item {_item.Name}: Out of stock!";
                    }
                }
                else
                {
                    return "Not enough money!";
                }
            }
            else
            {
                return "Invalid selection!";
            }
        }

        /* This method is used to set the time manually */
        public string SetTime
        {
            set { _setTime = value; }
        }

        /* This method is used to return the top 3 best revenue by hour */
        public string GetBestRevenueHours()
        {
            var result = _hoursRevenue
                .Select((v, i) => new { v, i })
                .OrderByDescending(x => x.v)
                .ThenByDescending(x => x.i)
                .Take(3)
                .ToArray();

            foreach (var entry in result)
            {
                _bestRevenue += $"Hour {entry.i} generated a revenue of {entry.v} \n";
            }

            return $"{_bestRevenue}";
        }

        /* This method is used to get the remaining change of this machine */
        public decimal GetChange
        {
            get { return _change; }
        }

        /* This method is used to get the balance of this machine */
        public decimal GetBalance
        {
            get { return _balance; }
        }
        #endregion
    }
}
