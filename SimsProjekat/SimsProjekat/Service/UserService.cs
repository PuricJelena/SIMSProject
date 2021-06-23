using SimsProjekat.Model;
using SimsProjekat.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Service
{
    public class UserService
    {
    
        private UserRepository repository = new UserRepository();

        public  IEnumerable<Entity> SearchService(string term = "")
        {
          return  repository.Search(term);
       
        }

        public Entity GetAllForUserService(string username, string password)
        {
            return repository.GetAllForUser(username, password);
        }

        public List<Entity> GetAllForUserTypeService(string userTYPE = "pacijent")
        {
            return repository.GetAllForUserType(userTYPE);
        }


        public IEnumerable<Entity> GetAllService()
        {
            return repository.GetAll();
        }

        public void SaveService()
        {
             repository.Save();
        }

        public void RemoveService(Entity entity)
        {
            repository.Remove(entity);
        }
    }
}
