using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using RedStar.Invoicing.Models;

namespace RedStar.Invoicing.Migrations
{
    [ContextType(typeof(InvoicesDbContext))]
    partial class v001
    {
        public override string Id
        {
            get { return "20150609184347_v0.0.1"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12943"; }
        }
        
        public override IModel Target
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("RedStar.Invoicing.Models.Invoice", b =>
                    {
                        b.Property<string>("Html")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Key("Id");
                    });
                
                builder.Entity("RedStar.Invoicing.Models.UserSettings", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("InvoiceTemplate")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("LogoUrl")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("UserId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}
