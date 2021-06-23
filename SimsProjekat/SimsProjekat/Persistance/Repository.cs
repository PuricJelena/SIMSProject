using SimsProjekat.Core;
using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Persistance
{
    public class Repository<TEntity> : Irepository<TEntity> where TEntity : class
    {
        public void Add(Entity entity)
        {
            SIMSContext.Instance.Get(typeof(TEntity)).Add(entity);
        }

        public Entity Get(string id)
        {
            return SIMSContext.Instance.Get(typeof(TEntity)).Where(x => x.Id == id).FirstOrDefault(); 
        }

        public IEnumerable<Entity> GetAll()
        {
            return SIMSContext.Instance.Get(typeof(TEntity));
        }

        public void Remove(Entity entity)
        {
            Entity entityToRemove = Get(entity.Id);

            SIMSContext.Instance.Get(typeof(TEntity)).Remove(entityToRemove);
        }

        public virtual IEnumerable<Entity> Search(string term="")
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            SIMSContext.Instance.Save();
        }
    }
}
