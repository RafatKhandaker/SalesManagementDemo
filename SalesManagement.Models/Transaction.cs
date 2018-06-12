using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Models.Contracts;

namespace SalesManagement.Models
{
    public class Transaction : ITransaction
    {
        private string custFName;
        private string custLName;
        private string product;
        private DateTime orderDate;
        private string deliveryAddress;
        private string accountType;
        private int quantity;
        private decimal totalCost;

        public Transaction
        ( 
            string custFName,
            string custLName,
            string product,
            DateTime orderDate,
            string deliveryAddress,
            string accountType,
            int quantity,          
            decimal totalCost
        )
        {
            this.custFName = custFName;
            this.custLName = custLName;
            this.product = product;





            this.orderDate = orderDate;
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

        public decimal TotalCost()
        {
            return this.totalCost;
        }

        public DateTime OrderDate()
        {
            return this.orderDate;
        }
    }
}
