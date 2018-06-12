using SalesManagement.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Models;
using SalesManagement.Data;

namespace SalesManagement.BLL
{
    public class DataService : IDBService
    {
        public IEnumerable<Transaction> RetrieveAllTransactions()
        {
           IList<Transaction> transactions = new List<Transaction>();

            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {
                
                foreach (Order O in dbContext.Orders)
                {
                    transactions.Add(new Transaction
                        (
                            O.Order_Details.CustomerFirstName,
                            O.Order_Details.CustomerLastName,
                            O.Product.Name,
                            O.OrderDate,
                            O.Order_Details.DeliveryAddress,
                            O.Order_Details.AccountType.ToString(),
                            O.Quantity,
                            int.Parse(O.TotalCost.ToString())
                        )
                        );
                }

                return transactions;
            }
           
        }

        public IEnumerable<Transaction> RetrieveUserTransactions(int userID)
        {
            IList<Transaction> transactions = new List<Transaction>();

            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {

                foreach (Sale S in dbContext.Sales.Where( w => w.UserId == userID))
                {
                    transactions.Add(new Transaction
                        (
                            S.Order.Order_Details.CustomerFirstName,
                            S.Order.Order_Details.CustomerLastName,
                            S.Order.Product.Name,
                            S.Order.OrderDate,
                            S.Order.Order_Details.DeliveryAddress,
                            S.Order.Order_Details.AccountType.ToString(),
                            S.Order.Quantity,
                            int.Parse(S.Order.TotalCost.ToString())
                        )
                        );
                }

                return transactions;
            }
        }
    }
}
