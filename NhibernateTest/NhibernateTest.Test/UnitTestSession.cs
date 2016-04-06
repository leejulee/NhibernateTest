using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Bytecode;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;

namespace NhibernateTest.Test
{
    [TestClass]
    public class UnitTestSession
    {
        [TestInitialize]
        public void Init()
        {
            var utility = new NHibernateUtility();
            utility.Initialize();
        }

        [TestMethod]
        public void TestMethod_Transaction()
        {
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    try
                    {
                        Message m = new Message() { Content = "Hi,Leo" };
                        session.Save(m);

                        Comment c = new Comment() { Message = m, Content = "Say Hi" };
                        session.Save(c);

                        session.Flush();
                        trans.Commit();

                        Assert.IsTrue(true);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        Assert.Fail();
                    }
                }
            }
        }

        [TestMethod]
        public void TestMethod_Merge()
        {
            Message message;
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message = new Message() { Content = "Hi,Leoli 2" };
                session.Save(message);
                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message.Content = "Merge....";
                var mergeMessage = session.Merge(message);
                session.Flush();

                //Content => Merge....
                Assert.AreEqual(mergeMessage.Content, message.Content); //新實例與舊實例會Merge
                Assert.AreNotSame(mergeMessage, message);

                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message.Id = 3;
                message.Content = "xxxxxxxxxxxxxxx=========";
                message = session.Merge(message);//沒有id是3
                Assert.AreEqual(2, message.Id);//新增一筆資料，因此自動編號為2
                session.Flush();
            }
        }

        [TestMethod]
        public void TestMehod_Replicate()
        {
            Message message;
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message = new Message() { Content = "Hi,leoli", Type = MessageType.Self };
                session.Save(message);
                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message.Content = "Leoli 2";
                session.Replicate(message, ReplicationMode.Ignore);
                Assert.AreEqual("Hi,leoli", session.Get<Message>(message.Id).Content);
                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message.Content = "Leoli 3";
                session.Replicate(message, ReplicationMode.Overwrite);
                Assert.AreEqual("Leoli 3", session.Get<Message>(message.Id).Content);
                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message.Id = 3;
                message.Content = "Leoli 4";
                session.Replicate(message, ReplicationMode.Ignore);//沒有id是3
                Assert.AreEqual(2, message.Id);//新增一筆資料，因此自動編號為2
                session.Flush();
            }
        }

        [TestMethod]
        public void TestMethod_Refresh()
        {
            Message message1;
            Message message2;
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message1 = new Message() { Content = "one", Type = MessageType.Self };
                session.Save(message1);
                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message2 = new Message() { Id = message1.Id, Content = "two" };
                session.Refresh(message2);
                Assert.AreEqual(message1.Content, message2.Content);
                session.Flush();
            }
        }

        [TestMethod]
        public void TestMethod_Contains()
        {
            Message message1;
            Message message2;
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message1 = new Message() { Content = "one", Type = MessageType.Self };
                session.Save(message1);
                session.Flush();
            }

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message2 = new Message() { Id = message1.Id };
                Assert.IsFalse(session.Contains(message2));
                session.Flush();
            }
        }

        [TestMethod]
        public void TestMethod_GetAndLoad()
        {
            Message message;
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message = new Message() { Content = "one", Type = MessageType.Self };
                session.Save(message);
                session.Flush();
            }

            Console.WriteLine("Get");

            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message = session.Get<Message>(message.Id);
                try
                {
                    session.Get<Message>(message.Id + 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Get Fail");
                }
            }

            Console.WriteLine("Load");
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                message = session.Load<Message>(message.Id);
                Assert.IsNotNull(message);

                message = session.Load<Message>(message.Id + 1);
            }
            //TODO why null?
            //Assert.IsNull(message);
        }

        [TestMethod]
        public void TestMethod_Query()
        {
            using (var session = NHibernateUtility.SessionFactory.OpenSession())
            {
                IEnumerable<Message> messages;
                var day = DateTime.Today.AddDays(-7);

                messages = session.Query<Message>()
                    .Where(x => x.CreateTime >= day).ToList();

                messages = session.QueryOver<Message>()
                    .Where(x => x.CreateTime >= day).List();

                IQuery hQuery = session.CreateQuery("Select m From Message m Where m.CreateTime >= ?");
                hQuery.SetParameter(0, day);
                messages = hQuery.List<Message>();

                ISQLQuery sqlQuery = session.CreateSQLQuery("Select * From Message m Where m.CreateTime >= ?");
                sqlQuery.SetParameter(0, day);
                messages = sqlQuery.List<Message>();

                ICriteria criterQuery = session.CreateCriteria<Message>();
                criterQuery.Add(Expression.Ge("CreateTime", day));
                messages = criterQuery.List<Message>();
            }
        }
    }
}
