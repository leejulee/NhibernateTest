using System;

namespace NhibernateTest
{
    public class BaseEntity<T>
    {
        public virtual T Id { get; set; }

        public virtual string Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual string LasEditor { get; set; }

        public virtual DateTime LastTime { get; set; }

        public virtual EntityStatus EntityStatus { get; set; }
    }
}