using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Infrastructure.DomainBase
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EntityMgr<TEntity, TKey> : IEntityManager<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        private string path;

        [JsonProperty]
        protected List<TEntity> list = new List<TEntity>();

        public int Count => this.list.Count();

        public EntityMgr()
        {
            path = typeof(TEntity).Name + ".mgr";
        }

        public EntityMgr(string dir)
        {
            path = dir + "\\" + typeof(TEntity).Name + ".mgr";
        }
        public void SetPath(string path)
        {
            this.path = path;
        }
        public string GetPath()
        {
            return this.path;
        }

        public TEntity this[TKey key]
        {
            get
            {
                return this.FindBy(key);
            }
            set
            {
                this.Remove(key);
                this.Add(value);
            }
        }

        public void Add(TEntity entity)
        {
            this.Remove(entity.Key);
            this.list.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                this.Remove(item.Key);
            }
            this.list.AddRange(entities);
        }

        public void Insert(TEntity entity)
        {
            this.Remove(entity.Key);
            this.list.Insert(0, entity);
        }

        public void InsertIndex(int index, TEntity entity)
        {
            this.Remove(entity.Key);
            this.list.Insert(index, entity);
        }

        public void InsertRange(int index, IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                this.Remove(item.Key);
            }
            this.list.InsertRange(index, entities);
        }

        public void Remove(TEntity entity)
        {
            this.list.Remove(entity);
        }

        public void Remove(TKey key)
        {
            foreach (var item in this.list)
            {
                if (item.Key.Equals(key))
                {
                    this.list.Remove(item);
                    break;
                }
            }
        }

        public TEntity FindBy(TKey key)
        {
            foreach (var item in this.list)
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
            return default(TEntity);
        }

        public List<TEntity> FindAll()
        {
            return this.list;
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this.list, Formatting.Indented);
                System.IO.File.WriteAllText(this.path, json);
                //JsonUtil.Serialize<List<TEntity>>(this.path,this.list);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Load()
        {
            try
            {
                //string json = System.IO.File.ReadAllText(this.path);
                //this.list = JsonConvert.DeserializeObject<List<TEntity>>(json);
                List<TEntity> listLoad;
                listLoad = JsonUtil.Deserialize<List<TEntity>>(this.path);
                if (listLoad == null)
                {
                    return false;
                }
                else
                {
                    this.list = listLoad;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
