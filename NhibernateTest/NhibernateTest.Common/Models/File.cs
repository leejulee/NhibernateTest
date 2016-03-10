
namespace NhibernateTest
{
    public class File : BaseEntity<int>
    {
        public virtual string Path
        { get; set; }

        public virtual string Name
        { get; set; }

        public virtual string DisplayName
        { get; set; }

        public virtual int Category
        { get; set; }

        public virtual int Sort
        { get; set; }

        //public virtual IList<Message> Messages
        //{ get; set; }
    }
}
