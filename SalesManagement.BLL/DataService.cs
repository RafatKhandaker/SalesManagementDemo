using SalesManagement.BLL.Contracts;
using System.Collections.Generic;
using System.Linq;
using SalesManagement.Models;
using SalesManagement.Data;
using System;
using System.Data.Entity;
using System.Globalization;

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
                            O.ProductId ,
                            O.Order_Details.CustomerFirstName,
                            O.Order_Details.CustomerLastName,
                            O.Product.Name,
                            O.OrderDate,
                            O.Order_Details.DeliveryAddress,
                            O.Order_Details.AccountType.ToString(),
                            O.Quantity,
                            O.TotalCost,
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

                foreach (Sale S in dbContext.Sales.Where( w => w.User.Id == userID))
                {
                    transactions.Add(new Transaction
                        (
                            S.Order.ProductId,
                            S.Order.Order_Details.CustomerFirstName,
                            S.Order.Order_Details.CustomerLastName,
                            S.Order.Product.Name,
                            S.Order.OrderDate,
                            S.Order.Order_Details.DeliveryAddress,
                            S.Order.Order_Details.AccountType.ToString(),
                            S.Order.Quantity,
                            S.Order.TotalCost,
                            S.Order.Order_Details.AccountNumber
                        )
                        );
                }

                return transactions;
            }
        }

        public void SaveTransactionWorkFlow(Transaction form)
        {
            var key = Guid.NewGuid();

            try
            {
                using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
                {
                    var prodId = dbContext.Products.Where(t => t.Name == form.Product).Select(t => t.Id).SingleOrDefault();
                    var accTypeId = int.Parse(
                                            dbContext.AccountTypes
                                            .Where(w => w.Name == form.AccountType)
                                            .Select(S => S.Id)?
                                            .FirstOrDefault()
                                            .ToString());

                    dbContext.Order_Details.Add(new Order_Details
                    {
                        Id = key,
                        CustomerFirstName = form.CustFName,
                        CustomerLastName = form.CustLName,
                        AccountType = accTypeId,
                        DeliveryAddress = form.DeliveryAddress,
                        AccountNumber = form.AccountNumber
                    }
                    );

                    dbContext.SaveChanges();

                    dbContext.Orders.Add(new Order
                    {
                        ProductId = prodId,
                        Quantity = form.Quantity,
                        TransactionId = key,
                        TotalCost = (form.TotalCost * form.Quantity),
                        OrderDate = DateTime.Now
                    }
                    );

                    dbContext.SaveChanges();


                    var budget = new Budget
                    {
                        DepartmentId = 1,
                        Amounted = (decimal)50000.00,
                        TimeStamped = DateTime.Now,
                        Gains = form.TotalCost * (decimal)dbContext.Products.Where(t => t.Name == form.Product).Select(s => s.ProfitRate)?.FirstOrDefault(),
                        Loss = (decimal)0.00,

                    };

                    budget.budget1 = (decimal)dbContext.Budgets.OrderByDescending(o => o.TimeStamped).Select(s => s.budget1)?.FirstOrDefault() + budget.Gains;

                    dbContext.Budgets.Add(budget);

                    dbContext.SaveChanges();

                    var updateCount = dbContext.Inventories.Where(w => w.ProductId == prodId).Select(s => s.InStock)?.FirstOrDefault() - form.Quantity;
                    var inventory = new Inventory
                    {
                        ProductId = prodId,
                        InStock = updateCount
                    };

                    dbContext.Inventories.Attach(inventory);

                    dbContext.Entry(inventory).State = EntityState.Added;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception e) { return; }
        }

        public Transaction BuildTransactinForm(int pId, Transaction form)
        {
            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {
                form.Id = pId;
                form.Product = dbContext.Products.Where(w => w.Id == pId).Select(s => s.Name)?.FirstOrDefault();
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

        public bool CheckAdminAccount(int account)
        {
            using (SalesManagementDemoEntities dbContext = new SalesManagementDemoEntities())
            {
                return (dbContext.Users
                            .Where(w => w.Id == account)
                            .Select(s => s.RoleId == 1 || s.RoleId == 2) != null
                            ) ? true : false;
            }
        }
    }
}
