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
        Task<bool> RegisterUser(VM_Register registerUser);
        bool CheckExist(string loginName);
        bool IsValidUser(int userId, int realEstateId);
    }

    public class AccountServices : IAccountServices
    {
        private readonly HakunaMatataContext _dbContext;

        public AccountServices(HakunaMatataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckExist(string loginName)
        {
            return _dbContext.Agent.Any(x => x.LoginName.Equals(loginName));
        }

        public Agent GetUser(VM_Login login)
        {
            var user = _dbContext.Agent.SingleOrDefault(x => x.LoginName == login.LoginName && x.Password == login.Password);
            return user;
        }

        public bool IsValidUser(int userId, int realEstateId)
        {
            var realEstate = _dbContext.RealEstate.Find(realEstateId);
            var user = _dbContext.Agent.Find(userId);
            if (realEstate != null && user != null)
            {
                return user.LevelId == 1 || userId == realEstate.AgentId;
            }
            return false;
        }

        public async Task<bool> RegisterUser(VM_Register registerUser)
        {
            try
            {
                if (registerUser != null)
                {
                    var user = new Agent()
                    {
                        LoginName = registerUser.LoginName,
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
                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
