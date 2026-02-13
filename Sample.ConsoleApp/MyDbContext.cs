using Arebis.Core.EntityFramework;
using Arebis.Core.EntityFramework.ValueConversion;
using Arebis.Types.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ConsoleApp
{
    public class MyDbContext : Arebis.Core.EntityFramework.BaseDbContext<MyDbContext>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Setup proxies:
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseChangeTrackingProxies();

            // Setup validation:
            optionsBuilder.UseValidation();

            // Setup further options:
            optionsBuilder.UseStringTrimming();
            optionsBuilder.UseStoreEmptyAsNullAttributes();
        }

        public DbSet<Customer> Customers { get; set; } = null!;
    }

    [Table("Customer")]
    public class Customer : IContextualEntity<MyDbContext>
    {
        [Key]
        public virtual int Id { get; set; }
     
        public virtual string Name { get; set; } = string.Empty;

        [StoreEmptyAsNull]
        [Converter(typeof(JsonValueConverter<DefaultDictionary<string, string>>)/*, typeof(JsonValueComparer<DefaultDictionary<string, string>>)*/)]
        public virtual DefaultDictionary<string, string> Properties { get; set; } = [];
        
        public MyDbContext? Context { get; set; }
    }

/*
     
CREATE TABLE [dbo].[Customer]
(
    [Id] int IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	[Properties] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO
     
*/

}
