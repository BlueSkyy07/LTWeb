using SV20T1020095.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020095.DataLayer.SQLServer
{
    public class CustomerAccountDAL : _BaseDAL, IUserAccountDAL

    {
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {
        }

        public UserAccount? Authorize(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
