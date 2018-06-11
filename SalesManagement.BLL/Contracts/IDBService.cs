using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Data;
using SalesManagement.Model;

namespace SalesManagement.BLL.Contracts
{
    public interface IDBService
    {
        IEnumerable<Transaction> RetrieveAllTransactions();
        IEnumerable<Transaction> RetrieveUserTransactions( int userID );
    }
}
