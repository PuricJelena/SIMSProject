using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimsProjekat.Model;

namespace SimsProjekat.Persistance
{
   public class UserRepository : Repository<User>
    {
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in SIMSContext.Instance.Users)
            {
                if (((User)entity).Name.Contains(term) || ((User)entity).LastName.Contains(term))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public Entity GetAllForUser(string username, string password)
        {

            foreach (Entity user in SIMSContext.Instance.Users)
            {
                if (((User)user).userId == username && ((User)user).password == password) 
                {
                    return user;
                }
            }

            return null;
        }

        public List<Entity> GetAllForUserType(string userTYPE = "pacijent")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity user in SIMSContext.Instance.Users)
            {
                if (((User)user).userType == userTYPE)
                {
                    result.Add(user);
                }
            }

            return result;
        }

     
    }
}
