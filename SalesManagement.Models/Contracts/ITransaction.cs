using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Models.Contracts
{
    interface ITransaction
    {
        string CustFName();
        string CustLName();
        string Product();
        DateTime OrderDate();
        int Quantity();
        decimal TotalCost();
        string AccountType();
        string DeliveryAddress();
        

    }
}
