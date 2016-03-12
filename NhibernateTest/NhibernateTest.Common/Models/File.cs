
namespace NhibernateTest
{
    public class File : BaseEntity<int>
    {
        public virtual string Path
        { get; set; }

        //public virtual IList<Message> Messages
        //{ get; set; }
    }
}
