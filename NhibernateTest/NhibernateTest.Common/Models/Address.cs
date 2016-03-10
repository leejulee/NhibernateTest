using System;

namespace NhibernateTest
{
    public class AddressInfo
    {

        public virtual string Address
        { get; set; }

        public virtual string Zip
        { get; set; }

        public virtual string Country
        { get; set; }
    }
}
