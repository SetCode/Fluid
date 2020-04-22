using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.DomainBase
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        protected EntityBase()
            : this(default(TKey))
        {

        }

        protected EntityBase(TKey key)
        {
            this.Key = key;
        }
        [ReadOnly(true)]
        [Browsable(false)]
        [JsonProperty]
        public TKey Key { get; private set; }
        [ReadOnly(true)]
        [Browsable(false)]
        public string EntityName => this.GetType().Name + this.Key.ToString();

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is EntityBase<TKey>
                && this == (EntityBase<TKey>)entity;
        }

        public static bool operator ==(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (!entity1.Key.Equals(entity2.Key))
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override string ToString()
        {
            return this.EntityName;
        }
    }
}
