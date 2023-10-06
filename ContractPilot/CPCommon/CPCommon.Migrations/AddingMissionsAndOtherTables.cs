using System;
using System.Collections.Generic;
using CPCommon.Data;
using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CPCommon.Migrations;

[DbContext(typeof(CPContext))]
[Migration("20210628035937_AddingMissionsAndOtherTables")]
public class AddingMissionsAndOtherTables : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey("FK_UserAirplane_Airplane_AirplaneId", "UserAirplane");
		migrationBuilder.DropForeignKey("FK_UserAirplane_Users_UserId", "UserAirplane");
		migrationBuilder.DropPrimaryKey("PK_UserAirplane", "UserAirplane");
		migrationBuilder.DropPrimaryKey("PK_Airplane", "Airplane");
		migrationBuilder.DropColumn("IsHome", "UserAirports");
		migrationBuilder.RenameTable("UserAirplane", null, "UserAirplanes");
		migrationBuilder.RenameTable("Airplane", null, "Airplanes");
		migrationBuilder.RenameIndex("IX_UserAirplane_AirplaneId", "IX_UserAirplanes_AirplaneId", "UserAirplanes");
		object defaultValue = new Guid("00000000-0000-0000-0000-000000000000");
		migrationBuilder.AddColumn<Guid>("CurrentAirportId", "Users", "uuid", null, null, rowVersion: false, null, nullable: false, defaultValue);
		defaultValue = new Guid("00000000-0000-0000-0000-000000000000");
		migrationBuilder.AddColumn<Guid>("HomeAirportId", "Users", "uuid", null, null, rowVersion: false, null, nullable: false, defaultValue);
		defaultValue = new Guid("00000000-0000-0000-0000-000000000000");
		migrationBuilder.AddColumn<Guid>("Id", "UserAirplanes", "uuid", null, null, rowVersion: false, null, nullable: false, defaultValue);
		migrationBuilder.AddColumn<double>("Hours", "Airplanes", "double precision", null, null, rowVersion: false, null, nullable: false, 0.0);
		defaultValue = new Guid("00000000-0000-0000-0000-000000000000");
		migrationBuilder.AddColumn<Guid>("MakeModelInfoId", "Airplanes", "uuid", null, null, rowVersion: false, null, nullable: false, defaultValue);
		migrationBuilder.AddPrimaryKey("PK_UserAirplanes", "UserAirplanes", new string[2] { "UserId", "AirplaneId" });
		migrationBuilder.AddPrimaryKey("PK_Airplanes", "Airplanes", "Id");
		migrationBuilder.CreateTable("AirplaneMakeModelInfos", (ColumnsBuilder table) => new
		{
			Id = table.Column<Guid>("uuid"),
			Name = table.Column<string>("text", null, null, rowVersion: false, null, nullable: true),
			Range = table.Column<int>("integer"),
			NumerOfEngines = table.Column<int>("integer"),
			WeightCapacity = table.Column<double>("double precision"),
			EngineType = table.Column<int>("integer"),
			PayloadStations = table.Column<ICollection<PayloadStation>>("jsonb", null, null, rowVersion: false, null, nullable: true),
			FuelTanks = table.Column<ICollection<FuelTank>>("jsonb", null, null, rowVersion: false, null, nullable: true)
		}, null, table =>
		{
			table.PrimaryKey("PK_AirplaneMakeModelInfos", x => x.Id);
		});
		migrationBuilder.CreateTable("Missions", (ColumnsBuilder table) => new
		{
			Id = table.Column<Guid>("uuid"),
			IsActive = table.Column<bool>("boolean"),
			IsComplete = table.Column<bool>("boolean"),
			StartingDateTime = table.Column<DateTime>("timestamp without time zone", null, null, rowVersion: false, null, nullable: true),
			TargetDateTime = table.Column<DateTime>("timestamp without time zone", null, null, rowVersion: false, null, nullable: true),
			StartingAirportId = table.Column<Guid>("uuid"),
			DestinationAirportId = table.Column<Guid>("uuid"),
			RequiredAirplaneMakeModelInfoId = table.Column<Guid>("uuid", null, null, rowVersion: false, null, nullable: true),
			Cargo = table.Column<Cargo>("jsonb", null, null, rowVersion: false, null, nullable: true),
			Passengers = table.Column<ICollection<Passenger>>("jsonb", null, null, rowVersion: false, null, nullable: true)
		}, null, table =>
		{
			table.PrimaryKey("PK_Missions", x => x.Id);
			table.ForeignKey("FK_Missions_AirplaneMakeModelInfos_RequiredAirplaneMakeModelIn~", x => x.RequiredAirplaneMakeModelInfoId, "AirplaneMakeModelInfos", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Missions_Airports_DestinationAirportId", x => x.DestinationAirportId, "Airports", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_Missions_Airports_StartingAirportId", x => x.StartingAirportId, "Airports", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateIndex("IX_Users_CurrentAirportId", "Users", "CurrentAirportId");
		migrationBuilder.CreateIndex("IX_Users_HomeAirportId", "Users", "HomeAirportId");
		migrationBuilder.CreateIndex("IX_Airplanes_MakeModelInfoId", "Airplanes", "MakeModelInfoId");
		migrationBuilder.CreateIndex("IX_Missions_DestinationAirportId", "Missions", "DestinationAirportId");
		migrationBuilder.CreateIndex("IX_Missions_RequiredAirplaneMakeModelInfoId", "Missions", "RequiredAirplaneMakeModelInfoId");
		migrationBuilder.CreateIndex("IX_Missions_StartingAirportId", "Missions", "StartingAirportId");
		migrationBuilder.AddForeignKey("FK_Airplanes_AirplaneMakeModelInfos_MakeModelInfoId", "Airplanes", "MakeModelInfoId", "AirplaneMakeModelInfos", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
		migrationBuilder.AddForeignKey("FK_UserAirplanes_Airplanes_AirplaneId", "UserAirplanes", "AirplaneId", "Airplanes", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
		migrationBuilder.AddForeignKey("FK_UserAirplanes_Users_UserId", "UserAirplanes", "UserId", "Users", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
		migrationBuilder.AddForeignKey("FK_Users_Airports_CurrentAirportId", "Users", "CurrentAirportId", "Airports", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
		migrationBuilder.AddForeignKey("FK_Users_Airports_HomeAirportId", "Users", "HomeAirportId", "Airports", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey("FK_Airplanes_AirplaneMakeModelInfos_MakeModelInfoId", "Airplanes");
		migrationBuilder.DropForeignKey("FK_UserAirplanes_Airplanes_AirplaneId", "UserAirplanes");
		migrationBuilder.DropForeignKey("FK_UserAirplanes_Users_UserId", "UserAirplanes");
		migrationBuilder.DropForeignKey("FK_Users_Airports_CurrentAirportId", "Users");
		migrationBuilder.DropForeignKey("FK_Users_Airports_HomeAirportId", "Users");
		migrationBuilder.DropTable("Missions");
		migrationBuilder.DropTable("AirplaneMakeModelInfos");
		migrationBuilder.DropIndex("IX_Users_CurrentAirportId", "Users");
		migrationBuilder.DropIndex("IX_Users_HomeAirportId", "Users");
		migrationBuilder.DropPrimaryKey("PK_UserAirplanes", "UserAirplanes");
		migrationBuilder.DropPrimaryKey("PK_Airplanes", "Airplanes");
		migrationBuilder.DropIndex("IX_Airplanes_MakeModelInfoId", "Airplanes");
		migrationBuilder.DropColumn("CurrentAirportId", "Users");
		migrationBuilder.DropColumn("HomeAirportId", "Users");
		migrationBuilder.DropColumn("Id", "UserAirplanes");
		migrationBuilder.DropColumn("Hours", "Airplanes");
		migrationBuilder.DropColumn("MakeModelInfoId", "Airplanes");
		migrationBuilder.RenameTable("UserAirplanes", null, "UserAirplane");
		migrationBuilder.RenameTable("Airplanes", null, "Airplane");
		migrationBuilder.RenameIndex("IX_UserAirplanes_AirplaneId", "IX_UserAirplane_AirplaneId", "UserAirplane");
		migrationBuilder.AddColumn<bool>("IsHome", "UserAirports", "boolean", null, null, rowVersion: false, null, nullable: false, false);
		migrationBuilder.AddPrimaryKey("PK_UserAirplane", "UserAirplane", new string[2] { "UserId", "AirplaneId" });
		migrationBuilder.AddPrimaryKey("PK_Airplane", "Airplane", "Id");
		migrationBuilder.AddForeignKey("FK_UserAirplane_Airplane_AirplaneId", "UserAirplane", "AirplaneId", "Airplane", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
		migrationBuilder.AddForeignKey("FK_UserAirplane_Users_UserId", "UserAirplane", "UserId", "Users", null, null, "Id", ReferentialAction.NoAction, ReferentialAction.Cascade);
	}

	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresExtension("postgis").HasAnnotation("Relational:MaxIdentifierLength", 63).HasAnnotation("ProductVersion", "5.0.7")
			.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
		modelBuilder.Entity("CPCommon.Model.Airplane", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<double>("Hours").HasColumnType("double precision");
			b.Property<Guid>("MakeModelInfoId").HasColumnType("uuid");
			b.Property<string>("TailNumber").HasColumnType("text");
			b.HasKey("Id");
			b.HasIndex("MakeModelInfoId");
			b.ToTable("Airplanes");
		});
		modelBuilder.Entity("CPCommon.Model.AirplaneMakeModelInfo", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<int>("EngineType").HasColumnType("integer");
			b.Property<ICollection<FuelTank>>("FuelTanks").HasColumnType("jsonb");
			b.Property<string>("Name").HasColumnType("text");
			b.Property<int>("NumerOfEngines").HasColumnType("integer");
			b.Property<ICollection<PayloadStation>>("PayloadStations").HasColumnType("jsonb");
			b.Property<int>("Range").HasColumnType("integer");
			b.Property<double>("WeightCapacity").HasColumnType("double precision");
			b.HasKey("Id");
			b.ToTable("AirplaneMakeModelInfos");
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
		modelBuilder.Entity("CPCommon.Model.Mission", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uuid");
			b.Property<Cargo>("Cargo").HasColumnType("jsonb");
			b.Property<Guid>("DestinationAirportId").HasColumnType("uuid");
			b.Property<bool>("IsActive").HasColumnType("boolean");
			b.Property<bool>("IsComplete").HasColumnType("boolean");
			b.Property<ICollection<Passenger>>("Passengers").HasColumnType("jsonb");
			b.Property<Guid?>("RequiredAirplaneMakeModelInfoId").HasColumnType("uuid");
			b.Property<Guid>("StartingAirportId").HasColumnType("uuid");
			b.Property<DateTime?>("StartingDateTime").HasColumnType("timestamp without time zone");
			b.Property<DateTime?>("TargetDateTime").HasColumnType("timestamp without time zone");
			b.HasKey("Id");
			b.HasIndex("DestinationAirportId");
			b.HasIndex("RequiredAirplaneMakeModelInfoId");
			b.HasIndex("StartingAirportId");
			b.ToTable("Missions");
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
			b.Property<Guid>("CurrentAirportId").HasColumnType("uuid");
			b.Property<Guid>("HomeAirportId").HasColumnType("uuid");
			b.Property<int>("InstrumentHours").HasColumnType("integer");
			b.Property<int>("JetHours").HasColumnType("integer");
			b.Property<string>("Name").HasColumnType("text");
			b.Property<int>("PistonHours").HasColumnType("integer");
			b.Property<int>("TotalHours").HasColumnType("integer");
			b.Property<int>("TurboPropHours").HasColumnType("integer");
			b.Property<int>("TwinHours").HasColumnType("integer");
			b.Property<int>("UnloggedHours").HasColumnType("integer");
			b.HasKey("Id");
			b.HasIndex("CurrentAirportId");
			b.HasIndex("HomeAirportId");
			b.ToTable("Users");
		});
		modelBuilder.Entity("CPCommon.Model.UserAirplane", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("UserId").HasColumnType("uuid");
			b.Property<Guid>("AirplaneId").HasColumnType("uuid");
			b.Property<int>("Hours").HasColumnType("integer");
			b.Property<Guid>("Id").HasColumnType("uuid");
			b.HasKey("UserId", "AirplaneId");
			b.HasIndex("AirplaneId");
			b.ToTable("UserAirplanes");
		});
		modelBuilder.Entity("CPCommon.Model.UserAirport", delegate(EntityTypeBuilder b)
		{
			b.Property<Guid>("UserId").HasColumnType("uuid");
			b.Property<Guid>("AirportId").HasColumnType("uuid");
			b.Property<Guid>("Id").HasColumnType("uuid");
			b.Property<int>("Notoriety").HasColumnType("integer");
			b.HasKey("UserId", "AirportId");
			b.HasIndex("AirportId");
			b.ToTable("UserAirports");
		});
		modelBuilder.Entity("CPCommon.Model.Airplane", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.AirplaneMakeModelInfo", "MakeModelInfo").WithMany("Airplanes").HasForeignKey("MakeModelInfoId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("MakeModelInfo");
		});
		modelBuilder.Entity("CPCommon.Model.Mission", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.Airport", "DestinationAirport").WithMany().HasForeignKey("DestinationAirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("CPCommon.Model.AirplaneMakeModelInfo", "RequiredAirplaneMakeModelInfo").WithMany().HasForeignKey("RequiredAirplaneMakeModelInfoId");
			b.HasOne("CPCommon.Model.Airport", "StartingAirport").WithMany().HasForeignKey("StartingAirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("DestinationAirport");
			b.Navigation("RequiredAirplaneMakeModelInfo");
			b.Navigation("StartingAirport");
		});
		modelBuilder.Entity("CPCommon.Model.Parking", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.Airport", "Airport").WithMany("Parking").HasForeignKey("AirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("Airport");
		});
		modelBuilder.Entity("CPCommon.Model.User", delegate(EntityTypeBuilder b)
		{
			b.HasOne("CPCommon.Model.Airport", "CurrentAirport").WithMany().HasForeignKey("CurrentAirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("CPCommon.Model.Airport", "HomeAirport").WithMany().HasForeignKey("HomeAirportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("CurrentAirport");
			b.Navigation("HomeAirport");
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
		modelBuilder.Entity("CPCommon.Model.AirplaneMakeModelInfo", delegate(EntityTypeBuilder b)
		{
			b.Navigation("Airplanes");
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
