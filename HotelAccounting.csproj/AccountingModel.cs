using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        public double Price
        {
            get => Price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                Price = value;
            }
        }

        public int NightsCount
        {
            get => NightsCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                NightsCount = value;
            }
        }

        public double Discount
        {
            get => Discount;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException();
                }

                Discount = value;
            }
        }

        public double Total
        {
            get => Price * NightsCount * (1 - Discount / 100);
            set
            {
                if (value < 0 || value > Price * NightsCount)
                {
                    throw new ArgumentException();
                }

                Total = value;
                Discount  = 100 * (Total - Price * NightsCount) / Total;
            }
            
        }
    }
}
