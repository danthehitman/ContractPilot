using System;
using System.Collections.Generic;
using CPCommon.Data;
using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CPCommon.Migrations;

[DbContext(typeof(CPContext))]
internal class CPContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
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
