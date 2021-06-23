using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Persistance
{
    public class LoginUserRepository : Repository<LogInUser>
    {
        public Entity GetAllForLoginUser(string username, string password)
        {

            foreach (Entity loginUser in SIMSContext.Instance.LoginUsers)
            {
                if (((LogInUser)loginUser).LoginUsername == username && ((LogInUser)loginUser).LoginPassword == password) ;
                {
                    return loginUser;
                }
            }

            return null;
        }
    }
}
