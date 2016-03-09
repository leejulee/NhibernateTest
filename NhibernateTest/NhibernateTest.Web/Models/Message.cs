using System;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;

namespace NhibernateTest
{
    public class Message : BaseEntity<int>
    {
        public virtual string Content
        { get; set; }

        public virtual MessageType Type
        { get; set; }

        public virtual IEnumerable<File> Files
        { get; set; }

        public virtual IEnumerable<Comment> Comments
        { get; set; }
    }
}
