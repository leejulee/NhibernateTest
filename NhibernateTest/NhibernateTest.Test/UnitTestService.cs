using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NhibernateTest.Service;

namespace NhibernateTest.Test
{
    [TestClass]
    public class UnitTestService
    {
        [TestInitialize]
        public void TestInit()
        {
            ServiceFactory.UserService = new UserService();
            ServiceFactory.MessageService = new MessageService();
            ServiceFactory.CommentService = new CommentService();
            var utility = new NHibernateUtility();
            utility.Initialize();
        }

        [TestMethod]
        public void TestUserService()
        {
            string email = "Leoli@xxxx";

            ServiceFactory.UserService.Insert(new User()
            {
                CreateTime = DateTime.Now,
                Email = email
            });

            var users = ServiceFactory.UserService.GetAll();
            var user = users.Last();
            Assert.AreEqual(user.Email, email);
        }
    }
}
