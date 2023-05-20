using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsCpnv
{
    public class Item
    {
        #region private attributes
        /* _name - the name of this item
         * _code - the code of this item for the machine
         * _quantity - the quantity available for this item
         * _price - the price of this item
         */
        private string _name;
        private string _code;
        private int _quantity;
        private decimal _price;
        #endregion

        #region constructor
        public Item(string name, string code, int quantity, decimal price)
        {
            _name = name;
            _code = code;
            _quantity = quantity;
            _price = price;
        }
        #endregion

        #region public methods
        /* This method is used to set or get the name of this item */
        public string Name 
        { 
            get { return _name; } 
            set { _name = value; }
        }

        /* This method is used to set or get the code of this item */
        public string Code 
        { 
           get { return _code; } 
           set {  _code = value; }
        }

        /* This method is used to set or get the quantity of this item */
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /* This method is used to set or get the price of this item */
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        #endregion
    }
}
