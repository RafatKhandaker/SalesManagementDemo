using System;

namespace SalesManagement.Models
{
    public class Transaction
    {
        private int id;
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
            int id,
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
            this.id = id;
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

        public int Id { get; set; }
        public string CustFName { get; set; }
        public string CustLName { get; set; }
        public string Product { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public string AccountType { get; set; }
        public string DeliveryAddress { get; set; }
        public int AccountNumber { get; set; }

        public int gId() { return this.id; }
        public string gCustFName() { return this.custFName; }
        public string gCustLName() { return this.custLName; }
        public string gProduct() { return this.product; }
        public DateTime gOrderDate() { return this.orderDate; }
        public int gQuantity() { return this.quantity; }
        public decimal gTotalCost() { return this.totalCost; }
        public string gAccountType() { return this.accountType; }
        public string gDeliveryAddress() { return this.deliveryAddress; }
        public int gAccountNumber() { return this.accountNumber; }
    }

}
