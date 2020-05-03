using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using System.Linq;

namespace HakunaMatata.Services
{
    public interface IAccountServices
    {
        Agent GetUser(VM_Login login);
    }

    public class AccountServices : IAccountServices
    {
        private readonly HakunaMatataContext _dbContext;

        public AccountServices(HakunaMatataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Agent GetUser(VM_Login login)
        {
            var user = _dbContext.Agent.SingleOrDefault(x => x.LoginName == login.LoginName && x.Password == login.Password);
            return user;
        }
    }
}
