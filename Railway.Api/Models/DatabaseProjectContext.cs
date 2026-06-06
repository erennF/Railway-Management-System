using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Railway.Api.Models;

public partial class DatabaseProjectContext : DbContext
{
    public DatabaseProjectContext()
    {
    }

    public DatabaseProjectContext(DbContextOptions<DatabaseProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargocontainer> Cargocontainers { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customsclearance> Customsclearances { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Freightshipment> Freightshipments { get; set; }

    public virtual DbSet<Freighttrain> Freighttrains { get; set; }

    public virtual DbSet<Loyaltyaccount> Loyaltyaccounts { get; set; }

    public virtual DbSet<Luggage> Luggage { get; set; }

    public virtual DbSet<Maintenancerecord> Maintenancerecords { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Passengertrain> Passengertrains { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Routestop> Routestops { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Staffassignment> Staffassignments { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Tracksegment> Tracksegments { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<Trainschedule> Trainschedules { get; set; }

    public virtual DbSet<Waitinglist> Waitinglists { get; set; }
    public virtual DbSet<Sensor> Sensors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cargocontainer>(entity =>
        {
            entity.HasKey(e => e.ContainerId).HasName("PRIMARY");

            entity.ToTable("cargocontainer");

            entity.HasIndex(e => e.ShipmentId, "ShipmentID");

            entity.Property(e => e.ContainerId).HasColumnName("ContainerID");
            entity.Property(e => e.ContainerType).HasMaxLength(50);
            entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

            entity.HasOne(d => d.Shipment).WithMany(p => p.Cargocontainers)
                .HasForeignKey(d => d.ShipmentId)
                .HasConstraintName("cargocontainer_ibfk_1");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("country");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customsclearance>(entity =>
        {
            entity.HasKey(e => e.ClearanceId).HasName("PRIMARY");

            entity.ToTable("customsclearance");

            entity.HasIndex(e => e.ShipmentId, "ShipmentID");

            entity.HasIndex(e => e.StationId, "StationID");

            entity.Property(e => e.ClearanceId).HasColumnName("ClearanceID");
            entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Shipment).WithMany(p => p.Customsclearances)
                .HasForeignKey(d => d.ShipmentId)
                .HasConstraintName("customsclearance_ibfk_1");

            entity.HasOne(d => d.Station).WithMany(p => p.Customsclearances)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customsclearance_ibfk_2");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => new { e.PassengerId, e.DependentId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("dependent");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.DependentId).HasColumnName("DependentID");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Passenger).WithMany(p => p.Dependents)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("dependent_ibfk_1");
        });

        modelBuilder.Entity<Freightshipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentId).HasName("PRIMARY");

            entity.ToTable("freightshipment");

            entity.HasIndex(e => e.TrainId, "TrainID");

            entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");
            entity.Property(e => e.Shipper).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TrainId).HasColumnName("TrainID");
            entity.Property(e => e.Weight).HasPrecision(10, 2);

            entity.HasOne(d => d.Train).WithMany(p => p.Freightshipments)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("freightshipment_ibfk_1");
        });

        modelBuilder.Entity<Freighttrain>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PRIMARY");

            entity.ToTable("freighttrain");

            entity.Property(e => e.TrainId)
                .ValueGeneratedNever()
                .HasColumnName("TrainID");
            entity.Property(e => e.MaxCargoWeight).HasPrecision(10, 2);

            entity.HasOne(d => d.Train).WithOne(p => p.Freighttrain)
                .HasForeignKey<Freighttrain>(d => d.TrainId)
                .HasConstraintName("freighttrain_ibfk_1");
        });

        modelBuilder.Entity<Loyaltyaccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("loyaltyaccount");

            entity.HasIndex(e => e.PassengerId, "PassengerID").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Bronze'");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.TotalMiles).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.Passenger).WithOne(p => p.Loyaltyaccount)
                .HasForeignKey<Loyaltyaccount>(d => d.PassengerId)
                .HasConstraintName("loyaltyaccount_ibfk_1");
        });

        modelBuilder.Entity<Luggage>(entity =>
        {
            entity.HasKey(e => e.LuggageId).HasName("PRIMARY");

            entity.ToTable("luggage");

            entity.HasIndex(e => e.ReservationId, "ReservationID");

            entity.Property(e => e.LuggageId).HasColumnName("LuggageID");
            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.Weight).HasPrecision(5, 2);

            entity.HasOne(d => d.Reservation).WithMany(p => p.Luggage)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("luggage_ibfk_1");
        });

        modelBuilder.Entity<Maintenancerecord>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PRIMARY");

            entity.ToTable("maintenancerecord");

            entity.HasIndex(e => e.TrackId, "TrackID");

            entity.HasIndex(e => e.TrainId, "TrainID");

            entity.Property(e => e.MaintenanceId).HasColumnName("MaintenanceID");
            entity.Property(e => e.Details).HasColumnType("text");
            entity.Property(e => e.TrackId).HasColumnName("TrackID");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.HasOne(d => d.Track).WithMany(p => p.Maintenancerecords)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("maintenancerecord_ibfk_2");

            entity.HasOne(d => d.Train).WithMany(p => p.Maintenancerecords)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("maintenancerecord_ibfk_1");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PRIMARY");

            entity.ToTable("passenger");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Passengertrain>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PRIMARY");

            entity.ToTable("passengertrain");

            entity.Property(e => e.TrainId)
                .ValueGeneratedNever()
                .HasColumnName("TrainID");

            entity.HasOne(d => d.Train).WithOne(p => p.Passengertrain)
                .HasForeignKey<Passengertrain>(d => d.TrainId)
                .HasConstraintName("passengertrain_ibfk_1");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PRIMARY");

            entity.ToTable("reservation");

            entity.HasIndex(e => e.PassengerId, "PassengerID");

            entity.HasIndex(e => e.ScheduleId, "ScheduleID");

            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.ReservationStatus).HasMaxLength(50);
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservation_ibfk_1");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservation_ibfk_2");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PRIMARY");

            entity.ToTable("route");

            entity.HasIndex(e => e.RouteName, "RouteName").IsUnique();

            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.RouteName).HasMaxLength(150);
        });

        modelBuilder.Entity<Routestop>(entity =>
        {
            entity.HasKey(e => new { e.RouteId, e.StationId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("routestop");

            entity.HasIndex(e => e.StationId, "StationID");

            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.StationId).HasColumnName("StationID");

            entity.HasOne(d => d.Route).WithMany(p => p.Routestops)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("routestop_ibfk_1");

            entity.HasOne(d => d.Station).WithMany(p => p.Routestops)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("routestop_ibfk_2");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PRIMARY");

            entity.ToTable("seat");

            entity.HasIndex(e => e.ReservationId, "ReservationID");

            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.CoachType).HasMaxLength(50);
            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.SeatNumber).HasMaxLength(10);

            entity.HasOne(d => d.Reservation).WithMany(p => p.Seats)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("seat_ibfk_1");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PRIMARY");

            entity.ToTable("staff");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Role).HasMaxLength(100);
        });

        modelBuilder.Entity<Staffassignment>(entity =>
        {
            entity.HasKey(e => new { e.StaffId, e.ScheduleId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("staffassignment");

            entity.HasIndex(e => e.ScheduleId, "ScheduleID");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Staffassignments)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("staffassignment_ibfk_2");

            entity.HasOne(d => d.Staff).WithMany(p => p.Staffassignments)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("staffassignment_ibfk_1");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PRIMARY");

            entity.ToTable("station");

            entity.HasIndex(e => e.CountryId, "CountryID");

            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.StationType).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Stations)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("station_ibfk_1");
        });

        modelBuilder.Entity<Tracksegment>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("PRIMARY");

            entity.ToTable("tracksegment");

            entity.HasIndex(e => e.EndStationId, "EndStationID");

            entity.HasIndex(e => e.StartStationId, "StartStationID");

            entity.Property(e => e.TrackId).HasColumnName("TrackID");
            entity.Property(e => e.Distance).HasPrecision(10, 2);
            entity.Property(e => e.EndStationId).HasColumnName("EndStationID");
            entity.Property(e => e.StartStationId).HasColumnName("StartStationID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.EndStation).WithMany(p => p.TracksegmentEndStations)
                .HasForeignKey(d => d.EndStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tracksegment_ibfk_2");

            entity.HasOne(d => d.StartStation).WithMany(p => p.TracksegmentStartStations)
                .HasForeignKey(d => d.StartStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tracksegment_ibfk_1");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PRIMARY");

            entity.ToTable("train");

            entity.Property(e => e.TrainId).HasColumnName("TrainID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TrainType).HasMaxLength(50);
        });

        modelBuilder.Entity<Trainschedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PRIMARY");

            entity.ToTable("trainschedule");

            entity.HasIndex(e => e.RouteId, "RouteID");

            entity.HasIndex(e => e.TrainId, "TrainID");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.DepartureTime).HasColumnType("time");
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.HasOne(d => d.Route).WithMany(p => p.Trainschedules)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trainschedule_ibfk_2");

            entity.HasOne(d => d.Train).WithMany(p => p.Trainschedules)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trainschedule_ibfk_1");
        });

        modelBuilder.Entity<Waitinglist>(entity =>
        {
            entity.HasKey(e => e.WaitlistId).HasName("PRIMARY");

            entity.ToTable("waitinglist");

            entity.HasIndex(e => e.PassengerId, "PassengerID");

            entity.HasIndex(e => e.ScheduleId, "ScheduleID");

            entity.Property(e => e.WaitlistId).HasColumnName("WaitlistID");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Waitinglists)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("waitinglist_ibfk_1");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Waitinglists)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("waitinglist_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
