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
                //Test EventListener mark
                //map.Access(Accessor.ReadOnly);
            });
            this.Property(x => x.CreateTime, map => map.NotNullable(true));
            //Test EventListener mark
            //Sqlite string Concatenate
            //this.Property(x => x.LastEditor, map => map.Formula("Creator || ' Test'"));
            //Mssql string Concatenate
            //this.Property(x => x.LastEditor, map => map.Formula("Creator + 'Test'"));
            this.Property(x => x.LastEditor);
            this.Property(x => x.LastTime);
            this.Property(x => x.EntityStatus, map => map.Column("Valid"));

        }
    }

    public class UserEntityMap : BaseEntityMap<int, User>
    {
        public UserEntityMap()
        {
            //改用 Hbm2DDLKeyWords.AutoQuote 可不用自已寫關鍵字處理
            //this.Table("`User`");
            this.Property(x => x.Email);
            this.Property(x => x.Password);

            this.Component(x => x.Address, x =>
            {
                x.Property(p => p.Country, p => { p.Column("Country"); });
                x.Property(p => p.Zip, p => { p.Column("ZipCode"); });
                x.Property(p => p.Address, p => { p.Column("Address"); });
            });
        }
    }
    public class AdminEntityMap : JoinedSubclassMapping<Admin>
    {
        public AdminEntityMap()
        {
            this.Property(x => x.Phone);

            this.Map(x => x.Settings,
                x =>
                {
                    x.Table("AdminSetting");
                    x.Key(k => k.Column("AdminId"));
                    x.Cascade(Cascade.DeleteOrphans);
                },
                x =>
                {
                    x.Element(e => e.Column("`Key`"));
                }
                ,
                x =>
                {
                    x.Element(e => e.Column("Value"));
                }
                );
        }
    }

    public class FileEntityMap : BaseEntityMap<int, File>
    {
        public FileEntityMap()
        {
            //改用 Hbm2DDLKeyWords.AutoQuote 可不用自已寫關鍵字處理
            //this.Table("`File`");
            this.Discriminator(x =>
            {
                x.Column("FileType");
                    x.Type<NHibernate.Type.Int32Type>();
            });

            this.DiscriminatorValue(0);

            //this.Bag(x => x.Messages,
            //    x =>
            //    {
            //        x.Key(k => k.Column("FileId"));
            //        x.Table("MessageOfFile");
            //    },
            //    x => x.ManyToMany(m => m.Column("MessageId"))
            //    );
        }
    }

    public class ImageEntityMap : SubclassMapping<Image>
    {
        public ImageEntityMap()
        {
            this.DiscriminatorValue(1);
            this.Property(x => x.Height);
            this.Property(x => x.Width);
        }
    }

    public class VideoEntityMap : SubclassMapping<Video>
    {
        public VideoEntityMap()
        {
            this.DiscriminatorValue(2);
            this.Property(x => x.Length);
        }
    }

    public class MessageEntityMap : BaseEntityMap<int, Message>
    {
        public MessageEntityMap()
        {
            this.Property(x => x.Content, x => x.Length(2000));
            this.Property(x => x.Type, x => x.Type<NHibernate.Type.EnumType<MessageType>>());
            this.Bag(x => x.Comments,
                x =>
                {
                    x.Key(k =>
                        {
                            k.Column("MessageId");
                            k.OnDelete(OnDeleteAction.Cascade);
                        });
                    //啟用時，無法從Message.Comments來新增Comment的關聯
                    x.Inverse(true);
                },
                x => x.OneToMany());

            this.Bag(x => x.Files,
                x =>
                {
                    x.Key(k => k.Column("MessageId"));
                    x.Table("MessageOfFile");
                },
                x => x.ManyToMany(m => m.Column("FileId"))
                );
        }
    }

    public class CommentEntityMap : BaseEntityMap<int, Comment>
    {
        public CommentEntityMap()
        {
            this.Property(x => x.Content, x => x.Length(2000));

            this.ManyToOne(x => x.Message, x => x.Column("MessageId"));
        }
    }
}