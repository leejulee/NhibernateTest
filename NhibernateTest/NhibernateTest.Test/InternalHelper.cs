using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhibernateTest.Test
{
    internal class InternalHelper
    {
        public static HbmMapping GetAllMapper()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(UserEntityMap));
            mapper.AddMapping(typeof(AdminEntityMap));
            mapper.AddMapping(typeof(FileEntityMap));
            mapper.AddMapping(typeof(ImageEntityMap));
            mapper.AddMapping(typeof(VideoEntityMap));
            mapper.AddMapping(typeof(MessageEntityMap));
            mapper.AddMapping(typeof(CommentEntityMap));
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}
