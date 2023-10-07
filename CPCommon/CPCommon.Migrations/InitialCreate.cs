using System;
using CPCommon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CPCommon.Migrations;

[DbContext(typeof(CPContext))]
[Migration("20210615235442_InitialCreate")]
public class InitialCreate : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterDatabase().Annotation("Npgsql:PostgresExtension:postgis", ",,");
		migrationBuilder.CreateTable("Airplane", (ColumnsBuilder table) => new
		{
			Id = table.Column<Guid>("uuid"),
			TailNumber = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true)
		}, null, table =>
		{
			table.PrimaryKey("PK_Airplane", x => x.Id);
		});
		migrationBuilder.CreateTable("Airports", (ColumnsBuilder table) => new
		{
			Id = table.Column<Guid>("uuid"),
			Name = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			City = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			Rating = table.Column<int>("integer"),
			Ident = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			LonX = table.Column<double>("double precision"),
			LatY = table.Column<double>("double precision"),
			Location = table.Column<Point>("geometry", null, null, rowVersion: false, null, nullable: true),
			Altitude = table.Column<int>("integer"),
			LongestRunwayLength = table.Column<int>("integer")
		}, null, table =>
		{
			table.PrimaryKey("PK_Airports", x => x.Id);
		});
		migrationBuilder.CreateTable("Users", (ColumnsBuilder table) => new
		{
			Id = table.Column<Guid>("uuid"),
			Name = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			TotalHours = table.Column<int>("integer"),
			CrossCountryHours = table.Column<int>("integer"),
			InstrumentHours = table.Column<int>("integer"),
			UnloggedHours = table.Column<int>("integer"),
			PistonHours = table.Column<int>("integer"),
			TurboPropHours = table.Column<int>("integer"),
			JetHours = table.Column<int>("integer"),
			TwinHours = table.Column<int>("integer")
		}, null, table =>
		{
			table.PrimaryKey("PK_Users", x => x.Id);
		});
		migrationBuilder.CreateTable("Parking", (ColumnsBuilder table) => new
		{
			Id = table.Column<Guid>("uuid"),
			AirportId = table.Column<Guid>("uuid"),
			Type = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			Name = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			Number = table.Column<int>("integer"),
			Radius = table.Column<double>("double precision"),
			Heading = table.Column<double>("double precision"),
			LonX = table.Column<double>("double precision"),
			LatY = table.Column<double>("double precision"),
			Location = table.Column<Point>("geometry", null, null, rowVersion: false, null, nullable: true)
		}, null, table =>
		{
			table.PrimaryKey("PK_Parking", x => x.Id);
			table.ForeignKey("FK_Parking_Airports_AirportId", x => x.AirportId, "Airports", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateTable("UserAirplane", (ColumnsBuilder table) => new
		{
			UserId = table.Column<Guid>("uuid"),
			AirplaneId = table.Column<Guid>("uuid"),
			Hours = table.Column<int>("integer")
		}, null, table =>
		{
			table.PrimaryKey("PK_UserAirplane", x => new { x.UserId, x.AirplaneId });
			table.ForeignKey("FK_UserAirplane_Airplane_AirplaneId", x => x.AirplaneId, "Airplane", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_UserAirplane_Users_UserId", x => x.UserId, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateTable("UserAirports", (ColumnsBuilder table) => new
		{
			AirportId = table.Column<Guid>("uuid"),
			UserId = table.Column<Guid>("uuid"),
			IsHome = table.Column<bool>("boolean"),
			Notoriety = table.Column<int>("integer"),
			Id = table.Column<Guid>("uuid")
		}, null, table =>
		{
			table.PrimaryKey("PK_UserAirports", x => new { x.UserId, x.AirportId });
			table.ForeignKey("FK_UserAirports_Airports_AirportId", x => x.AirportId, "Airports", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_UserAirports_Users_UserId", x => x.UserId, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateIndex("IX_Parking_AirportId", "Parking", "AirportId");
		migrationBuilder.CreateIndex("IX_UserAirplane_AirplaneId", "UserAirplane", "AirplaneId");
		migrationBuilder.CreateIndex("IX_UserAirports_AirportId", "UserAirports", "AirportId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("Parking");
		migrationBuilder.DropTable("UserAirplane");
		migrationBuilder.DropTable("UserAirports");
		migrationBuilder.DropTable("Airplane");
		migrationBuilder.DropTable("Airports");
		migrationBuilder.DropTable("Users");
	}

	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresExtension("postgis").HasAnnotation("Relational:MaxIdentifierLength", 63).HasAnnotation("ProductVersion", "5.0.7")
			.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
		modelBuilder.Entity("CPCommon.Model.Airplane", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<string>("TailNumber").HasColumnType("text");
			b.HasKey("Id");
			b.ToTable("Airplane");
		});
		modelBuilder.Entity("CPCommon.Model.Airport", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<int>("Altitude").HasColumnType("integer");
			b.Property<string>("City").HasColumnType("text");
			b.Property<string>("Ident").HasColumnType("text");
			b.Property<double>("LatY").HasColumnType("double precision");
			b.Property<Point>("Location").HasColumnType("geometry");
			b.Property<double>("LonX").HasColumnType("double precision");
			b.Property<int>("LongestRunwayLength").HasColumnType("integer");
			b.Property<string>("Name").HasColumnType("text");
			b.Property<int>("Rating").HasColumnType("integer");
			b.HasKey("Id");
			b.ToTable("Airports");
		});
		modelBuilder.Entity("CPCommon.Model.Parking", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<Guid>("AirportId").HasColumnType("uuid");
			b.Property<double>("Heading").HasColumnType("double precision");
			b.Property<double>("LatY").HasColumnType("double precision");
			b.Property<Point>("Location").HasColumnType("geometry");
			b.Property<double>("LonX").HasColumnType("double precision");
			b.Property<string>("Name").HasColumnType("text");
			b.Property<int>("Number").HasColumnType("integer");
			b.Property<double>("Radius").HasColumnType("double precision");
			b.Property<string>("Type").HasColumnType("text");
			b.HasKey("Id");
			b.HasIndex("AirportId");
			b.ToTable("Parking");
		});
		modelBuilder.Entity("CPCommon.Model.User", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<int>("CrossCountryHours").HasColumnType("integer");
			b.Property<int>("InstrumentHours").HasColumnType("integer");
			b.Property<int>("JetHours").HasColumnType("integer");
			b.Property<string>("Name").HasColumnType("text");
			b.Property<int>("PistonHours").HasColumnType("integer");
			b.Property<int>("TotalHours").HasColumnType("integer");
			b.Property<int>("TurboPropHours").HasColumnType("integer");
			b.Property<int>("TwinHours").HasColumnType("integer");
			b.Property<int>("UnloggedHours").HasColumnType("integer");
			b.HasKey("Id");
			b.ToTable("Users");
		});
		modelBuilder.Entity("CPCommon.Model.UserAirplane", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("UserId").HasColumnType("uuid");
			b.Property<Guid>("AirplaneId").HasColumnType("uuid");
			b.Property<int>("Hours").HasColumnType("integer");
			b.HasKey("UserId", "AirplaneId");
			b.HasIndex("AirplaneId");
			b.ToTable("UserAirplane");
		});
		modelBuilder.Entity("CPCommon.Model.UserAirport", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("UserId").HasColumnType("uuid");
			b.Property<Guid>("AirportId").HasColumnType("uuid");
			b.Property<Guid>("Id").HasColumnType("uuid");
			b.Property<bool>("IsHome").HasColumnType("boolean");
			b.Property<int>("Notoriety").HasColumnType("integer");
			b.HasKey("UserId", "AirportId");
			b.HasIndex("AirportId");
			b.ToTable("UserAirports");
		});
		modelBuilder.Entity("CPCommon.Model.Parking", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.Airport", "Airport").WithMany("Parking").HasForeignKey("AirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("Airport");
		});
		modelBuilder.Entity("CPCommon.Model.UserAirplane", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.Airplane", "Airplane").WithMany().HasForeignKey("AirplaneId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("CPCommon.Model.User", "User").WithMany("UserAirplanes").HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("Airplane");
			b.Navigation("User");
		});
		modelBuilder.Entity("CPCommon.Model.UserAirport", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.Airport", "Airport").WithMany().HasForeignKey("AirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("CPCommon.Model.User", "User").WithMany("UserAirports").HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("Airport");
			b.Navigation("User");
		});
		modelBuilder.Entity("CPCommon.Model.Airport", delegate(EntityTypeBuilder b)
		{
			b.Navigation("Parking");
		});
		modelBuilder.Entity("CPCommon.Model.User", delegate(EntityTypeBuilder b)
		{
			b.Navigation("UserAirplanes");
			b.Navigation("UserAirports");
		});
	}
}
