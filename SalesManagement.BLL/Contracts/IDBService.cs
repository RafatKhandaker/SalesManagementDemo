using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Data;
using SalesManagement.Models;

namespace SalesManagement.BLL.Contracts
{
    public interface IDBService
    {
        IEnumerable<Transaction> RetrieveAllTransactions();
        IEnumerable<Transaction> RetrieveUserTransactions( string userID );
        Transaction BuildTransactinForm( int pID, Transaction form );
        void SaveTransactionWorkFlow( Transaction form );
        void UpdateSalesRecord( string user );
        bool CheckAdminAccount( string account );
    }
}
