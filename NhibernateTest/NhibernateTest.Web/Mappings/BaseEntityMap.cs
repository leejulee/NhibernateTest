using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhibernateTest
{
    public abstract class BaseEntityMap<TId, TEntity> : ClassMapping<TEntity>
        where TEntity : BaseEntity<TId>
    {
        public BaseEntityMap()
        {
            this.Id(x => x.Id, map => map.Generator(Generators.Identity));
            this.Property(x => x.Creator, map =>
            {
                map.Length(200);
                map.Access(Accessor.ReadOnly);
            });
            this.Property(x => x.CreateTime, map => map.NotNullable(true));
            this.Property(x => x.LasEditor, map => map.Formula("Creator + 'Test'"));
            this.Property(x => x.LastTime);
            this.Property(x => x.EntityStatus, map => map.Column("Valid"));

        }
    }

    public class ProductEntityMap : BaseEntityMap<int, Product>
    {
        public ProductEntityMap()
        {
            Schema("dbo");
            this.Property(x => x.Category, map =>
            {
                map.NotNullable(true);
                map.Type<NHibernate.Type.EnumType<ProductCategoryEnum>>();
            });
            this.Property(x => x.Name, map => map.NotNullable(true));
            this.Property(x => x.Description, map => map.NotNullable(true));
            this.Property(x => x.Sort, map => map.NotNullable(true));
            //this.Property(x => x.ProductDetail, map =>
            //{
            //    map.Type<NHibernate.Type.XmlDocType>();
            //    map.NotNullable(true);
            //});
            //this.Discriminator(x =>
            //{
            //    x.Column("");
            //    x.Type<NHibernate.Type.XmlDocType>();
            //});
            //this.DiscriminatorValue();
            this.Lazy(true);
        }
    }

    public class FileEntityMap : BaseEntityMap<int, File>
    {
        public FileEntityMap()
        {
            this.Property(x => x.Name, map => map.NotNullable(true));
            this.Property(x => x.DisplayName, map => map.NotNullable(true));
            this.Property(x => x.Category, map => map.NotNullable(true));
            this.Property(x => x.Sort, map => map.NotNullable(true));
        }
    }
}