using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
//using NHibernate.Bytecode;
//using NHibernate.Dialect;
//using NHibernate.Driver;

namespace NhibernateTest.Test
{
    [TestClass]
    public class UnitTestCfgXml
    {
        [TestMethod]
        public void TestConfigMappingByCode()
        {
            var config = new NHibernate.Cfg.Configuration();
            config.Configure();
            config.DataBaseIntegration(db =>
            {
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            });

            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(UserEntityMap));
            mapper.AddMapping(typeof(AdminEntityMap));
            mapper.AddMapping(typeof(FileEntityMap));
            mapper.AddMapping(typeof(ImageEntityMap));
            mapper.AddMapping(typeof(VideoEntityMap));
            mapper.AddMapping(typeof(MessageEntityMap));
            mapper.AddMapping(typeof(CommentEntityMap));
            var maps = mapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddDeserializedMapping(maps, "Models");

            var factory = config.BuildSessionFactory();

        }
    }
}
