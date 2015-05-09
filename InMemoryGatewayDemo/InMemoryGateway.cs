using System.Collections.Generic;
using System.Linq;

namespace InMemoryGatewayDemo
{
    public class InMemoryGateway<T> where T : Entity
    {
        private long lastId;
        private readonly IDictionary<long, T> entities;

        public InMemoryGateway()
        {
            entities = new Dictionary<long, T>();
        }

        public virtual void Save(T entity)
        {
            if (entity.Id == 0)
            {
                lastId++;
                entity.Id = lastId;
            }

            T entityClone = entity.Clone<T>();
            if (entities.ContainsKey(entity.Id))
            {
                entities[entity.Id] = entityClone;
            }
            else
            {
                entities.Add(entity.Id, entityClone);
            }
        }

        public virtual T Get(long id)
        {
            return (entities.ContainsKey(id)) ? entities[id].Clone<T>() : default(T);
        }

        public virtual IList<T> GetAll()
        {
            return entities.Select(x => x.Value.Clone<T>()).ToList();
        }

        public virtual void Delete(T entity)
        {
            entities.Remove(entity.Id);
        }
    }
}
