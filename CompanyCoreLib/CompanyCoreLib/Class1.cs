using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyCoreLib
{
    public class Analytics
    {

        public Boolean CheckPrice(int price)
        {
            if (price <= 2000)
            {
                return false;
            }
            else
                return true;
        }

        public int DiscountPrice(int price)
        {
            int sum = ((price / 100) * 15) * price;
            return sum;
        }

        public string PriceNotNull(int price)
        {
            if (price <= 0)
            {
                return "Цена не может быть меньше или равна нулю!!!";
            }
            else
                return "Всё правильно";
        }
    }
}
