using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Model.Contracts
{
    interface ITransaction
    {
        string CustFName();
        string CustLName();
        string Product();
        int Quantity();
        int TotalCost();
        string AccountType();
        string DeliveryAddress();

    }
}
