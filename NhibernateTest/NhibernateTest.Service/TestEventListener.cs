using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhibernateTest.Service
{
    public class TestUpdateEventListener : IPreUpdateEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var baseEntity = @event.Entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.LastEditor = "Leoli_Update_Event";
                baseEntity.LastTime = DateTime.Now;

                this.Set(@event.Persister, @event.State, "LastEditor", baseEntity.LastEditor);
                this.Set(@event.Persister, @event.State, "LastTime", baseEntity.LastTime);
                //or
                //@event.State.SetValue("Leoli_Update_Event", Array.IndexOf(@event.Persister.PropertyNames, "LastEditor"));
                //@event.State.SetValue(baseEntity.LastTime, Array.IndexOf(@event.Persister.PropertyNames, "LastTime"));
            }
            return false;
        }
        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
            {
                return;
            }
            state[index] = value;
        }
    }
    public class TestSaveOrUpdateEventListener : DefaultSaveEventListener
    {
        protected override object PerformSaveOrUpdate(SaveOrUpdateEvent @event)
        {
            var baseEntity = @event.Entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.Creator = "Leoli";
                baseEntity.LastEditor = "Leoli_SaveOrUpdate_Event";

                baseEntity.CreateTime = DateTime.Now;
                baseEntity.LastTime = DateTime.Now;
            }

            return base.PerformSaveOrUpdate(@event);
        }
    }
}
