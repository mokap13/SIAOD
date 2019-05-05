using EconomicKurs.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicKurs.ViewModel
{
    class RentOrderVM : ViewModelBase
    {
        private Order rentOrder;

        public Order RentOrder
        {
            get { return rentOrder; }
            set { Set(ref rentOrder, value); }
        }

        public RentOrderVM()
        {

        }
    }
}
