using System.Collections.Generic;

namespace NhibernateTest
{
    public class Message : BaseEntity<int>
    {
        public virtual string Content
        { get; set; }

        public virtual MessageType Type
        { get; set; }

        public virtual IList<File> Files
        { get; set; }

        public virtual IList<Comment> Comments
        { get; set; }
    }
}
