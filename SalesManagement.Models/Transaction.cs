using System;

namespace SalesManagement.Models
{
    public class Transaction
    {
        private string custFName;
        private string custLName;
        private string product;
        private DateTime orderDate;
        private string deliveryAddress;
        private string accountType;
        private int quantity;
        private decimal totalCost;
        private int accountNumber;

        public Transaction() { }

        public Transaction
        ( 
            string custFName,
            string custLName,
            string product,
            DateTime orderDate,
            string deliveryAddress,
            string accountType,
            int quantity,          
            decimal totalCost,
            int accountNumber
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
            this.accountNumber = accountNumber;
        }

        public string CustFName { get; set; }
        public string CustLName { get; set; }
        public string Product { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public string AccountType { get; set; }
        public string DeliveryAddress { get; set; }
        public int AccountNumber { get; set; }

    }
}
