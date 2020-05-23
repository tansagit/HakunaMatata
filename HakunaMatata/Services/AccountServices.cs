using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Services
{
    public interface IAccountServices
    {
        Agent GetUser(VM_Login login);
        Task<bool> RegisterUser(VM_Register user);
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

        public async Task<bool> RegisterUser(VM_Register registerUser)
        {
            try
            {
                var user = new Agent()
                {
                    LoginName = registerUser.PhoneNumber,
                    Password = registerUser.Password,
                    AgentName = registerUser.AgentName,
                    LevelId = 3,
                    IsActive = true,
                    PhoneNumber = registerUser.PhoneNumber,
                    ConfirmPhoneNumber = false
                };
                _dbContext.Agent.Add(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
