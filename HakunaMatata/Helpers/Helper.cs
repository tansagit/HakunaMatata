using HakunaMatata.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HakunaMatata.Helpers
{
    public class Helper
    {
        public static ClaimsPrincipal GenerateIdentity(Agent user)
        {
            string role;
            switch (user.LevelId)
            {
                case 1: role = "Admin"; break;
                case 2: role = "Manager"; break;
                case 3: role = "Customer"; break;
                default: role = "Customer"; break;
            }

            var claims = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName",user.AgentName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim("Phone",user.PhoneNumber),
                new Claim(ClaimTypes.Role , role)
            };

            var identity = new ClaimsIdentity(claims, "User Identity");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

        public static string ChangeImageURl(string url)
        {
            var imgName = url.Split('\\').Last();
            var tempUrl = Constants.LOCAL_IMG_SERVER + imgName;
            return tempUrl;
        }
    }
}
