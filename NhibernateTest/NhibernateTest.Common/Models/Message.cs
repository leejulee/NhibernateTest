using System.Collections.Generic;

namespace NhibernateTest
{
    public class Message : BaseEntity<int>
    {
        public Message()
        {
            this.Comments = new List<Comment>();
            this.Files = new List<File>();
        }

        public virtual string Content
        { get; set; }

        public virtual MessageType Type
        { get; set; }

        public virtual IList<File> Files
        { get; set; }

        public virtual IList<Comment> Comments
        { get; set; }

        //可配合Nhibernate List 對應(非必要在map設定屬性)
        //public virtual int Sort
        //{ get; set; }
    }
}
