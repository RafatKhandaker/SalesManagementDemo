using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Model.Contracts;

namespace SalesManagement.Model
{
    public class Transaction : ITransaction
    {
        private string custFName;
        private string custLName;
        private string product;
        private string deliveryAddress;
        private string accountType;
        private int quantity;
        private int totalCost;

        public Transaction
        (
            string custFName,
            string custLName,
            string product,
            string deliveryAddress,
            string accountType,
            int quantity,
            int totalCost
        )
        {
            this.custFName = custFName;
            this.custLName = custLName;
            this.product = product;
            this.deliveryAddress = deliveryAddress;
            this.accountType = accountType;
            this.quantity = quantity;
            this.totalCost = totalCost;
        }

        public string AccountType()
        {
            return this.accountType;
        }

        public string CustFName()
        {
            return this.custFName;
        }

        public string CustLName()
        {
            return this.custLName;
        }

        public string DeliveryAddress()
        {
            return this.deliveryAddress;
        }

        public string Product()
        {
            return this.product;
        }

        public int Quantity()
        {
            return this.quantity;
        }

        public int TotalCost()
        {
            return this.totalCost;
        }
    }
}
