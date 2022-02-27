using FluentMigrator;

namespace SimpleBookingSystemService.Migrations
{
    [Migration(1)]
    public class DbInitialization : Migration
    {
        public override void Up()
        {
            Create.Table("Resource")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable();

            Create.Table("Booking")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("ResourceId").AsInt32().NotNullable()
                .WithColumn("FromDate").AsDateTime().NotNullable()
                .WithColumn("ToDate").AsDateTime().NotNullable()
                .WithColumn("BookedQuantity").AsInt32().NotNullable();

            Create.ForeignKey("FK_Booking_Resource") // You can give the FK a name or just let Fluent Migrator default to one
                .FromTable("Booking").ForeignColumn("ResourceId")
                .ToTable("Resource").PrimaryColumn("Id");

        }

        public override void Down()
        {
            Delete.Table("Resource");
            Delete.Table("Booking");
        }

    }
}
