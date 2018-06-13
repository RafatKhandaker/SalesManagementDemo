using SalesManagement.BLL.Contracts;
using System.Collections.Generic;
using System.Linq;
using SalesManagement.Models;
using SalesManagement.Data;
using System;
using System.Data.Entity;

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
                            int.Parse(O.TotalCost.ToString()),
                            O.Order_Details.AccountNumber
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
                            int.Parse(S.Order.TotalCost.ToString()),
                            S.Order.Order_Details.AccountNumber
                        )
                        );
                }

                return transactions;
            }
        }

        public void SaveTransactionWorkFlow(Transaction form)
        {
            var key = new Guid();

            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {
                var prodId = dbContext.Products.Where(t => t.Name == form.Product).Select(t => t.Id).SingleOrDefault();

                dbContext.Order_Details.Add( new Order_Details
                    {
                        Id = key,
                        CustomerFirstName = form.CustFName,
                        CustomerLastName = form.CustLName,
                        AccountType = int.Parse(form.AccountType),
                        AccountNumber = form.AccountNumber
                    }
                );

                dbContext.Orders.Add( new Order
                    {
                        OrderDate = new DateTime(),
                        TransactionId = key,
                        Quantity = form.Quantity,
                        ProductId = prodId,
                        TotalCost = form.TotalCost 
                    }
                );

                var budget = new Budget
                {
                    DepartmentId = 1,
                    Amounted = (decimal)50000.00,
                    TimeStamped = new DateTime(),
                    Gains = form.TotalCost * (decimal)dbContext.Products.Where(t => t.Name == form.Product).Select(s => s.ProfitRate)?.FirstOrDefault(),
                    Loss = (decimal)0.00,

                };

                budget.budget1 = (decimal)dbContext.Budgets.OrderByDescending(o => o.TimeStamped).Select(s => s.budget1)?.FirstOrDefault() + budget.Gains;

                dbContext.Budgets.Add(budget);

                var inventory = new Inventory
                {
                    ProductId = prodId,
                    InStock = dbContext.Inventories.Where(w => w.ProductId == prodId).Select(s => s.InStock)?.FirstOrDefault() - form.Quantity
                };

                dbContext.Inventories.Attach( inventory );

                dbContext.Entry( inventory ).State = EntityState.Modified;

                dbContext.SaveChanges(); 
            }
        }

        public Transaction BuildTransactinForm(int pId, Transaction form)
        {
            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {
                form.TotalCost = (decimal) dbContext.Products.Where(w => w.Id == pId ).Select(s => s.Cost)?.FirstOrDefault();
            }

                return form;
                
        }

        public void UpdateSalesRecord( string user )
        {
            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {
                dbContext.Sales.Add(new Sale
                    {
                        OrdersId = dbContext.Orders.OrderByDescending(o => o.OrderDate).Select(s => s.Id)?.FirstOrDefault(),
                        BudgetId = dbContext.Budgets.OrderByDescending(o => o.TimeStamped).Select(s => s.Id).FirstOrDefault(),
                        UserId =   dbContext.Users.Where(w => w.UserName == user).Select(s => s.Id).FirstOrDefault()
                    }
                );

                dbContext.SaveChanges();
            }
        }
    }
}
