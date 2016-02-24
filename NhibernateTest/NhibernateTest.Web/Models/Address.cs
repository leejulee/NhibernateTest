using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

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
