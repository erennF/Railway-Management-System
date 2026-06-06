SET FOREIGN_KEY_CHECKS = 0;

INSERT INTO Country (Name) VALUES ('Turkiye'), ('Syria'), ('Jordan'), ('Saudi Arabia'); 

INSERT INTO Station (Name, StationType, CountryID) VALUES ('Istanbul Haydarpasa', 'Main', 1), ('Gaziantep Border Crossing', 'Border', 1), ('Aleppo Central', 'Main', 2), ('Damascus Station', 'Regular', 2), ('Amman Logistics Hub', 'Freight', 3), ('Amman Passenger', 'Main', 3), ('Riyadh Main Station', 'Main', 4), ('Jeddah Port Station', 'Freight', 4); 

INSERT INTO TrackSegment (StartStationID, EndStationID, Distance, MaxPassengerSpeed, MaxFreightSpeed, Status) VALUES (1, 2, 1140.00, 200, 100, 'Active'), (2, 3, 120.50, 120, 80, 'Active'), (3, 4, 350.00, 150, 90, 'Active'), (4, 6, 200.00, 150, 90, 'Active'), (6, 7, 1450.00, 250, 120, 'Active'), (5, 8, 1200.00, 0, 100, 'Active'); 

INSERT INTO Train (Name, TrainType) VALUES ('Hejaz Express', 'Passenger'), ('Bosphorus-Riyadh Line', 'Passenger'), ('Levant Regional', 'Passenger'), ('Red Sea Logistics', 'Freight'), ('Anatolia-Gulf Cargo', 'Freight'); 

INSERT INTO PassengerTrain (TrainID, PassengerCapacity) VALUES (1, 600), (2, 450), (3, 300); 

INSERT INTO FreightTrain (TrainID, MaxCargoWeight) VALUES (4, 25000.00), (5, 18000.00); 

INSERT INTO Route (RouteName) VALUES ('Istanbul - Riyadh International Express'), ('Amman - Jeddah Freight Corridor'); 

INSERT INTO RouteStop (RouteID, StationID, SequenceNumber) VALUES (1, 1, 1), (1, 2, 2), (1, 3, 3), (1, 4, 4), (1, 6, 5), (1, 7, 6), (2, 5, 1), (2, 8, 2); 

INSERT INTO Passenger (Name, Email) VALUES ('Firdevs Eren', 'firdevs.eren@mail.com'), ('Ahmed Al-Saud', 'ahmed.saud@mail.com'), ('Omar Hassan', 'omar.hassan@mail.com'), ('Leyla Yilmaz', 'leyla.yilmaz@mail.com'), ('Ebrar Akyol', 'ebrar.akyol@mail.com'); 

INSERT INTO Dependent (PassengerID, DependentID, Name) VALUES (1, 1, 'Ali Eren'), (2, 1, 'Fatima Al-Saud'), (2, 2, 'Zayn Al-Saud'); 

INSERT INTO LoyaltyAccount (PassengerID, TotalMiles, Class) VALUES (1, 12500, 'Green'), (2, 110000, 'Gold'), (3, 5000, 'Bronze'), (4, 55000, 'Silver'), (5, 0, 'Bronze'); 

INSERT INTO TrainSchedule (TrainID, RouteID, Date, DepartureTime) VALUES (1, 1, '2026-07-15', '09:00:00'), (2, 1, '2026-07-16', '14:30:00'), (4, 2, '2026-07-15', '22:00:00'); 

INSERT INTO Reservation (PassengerID, ScheduleID, Date, ReservationStatus) VALUES (1, 1, '2026-06-01', 'Confirmed'), (2, 1, '2026-06-02', 'Confirmed'), (3, 2, '2026-06-05', 'Pending'), (4, 2, '2026-06-05', 'Confirmed'); 

INSERT INTO Seat (ReservationID, SeatNumber, CoachType) VALUES (1, 'A1-12A', 'Business'), (1, 'A1-12B', 'Business'), (2, 'A2-04C', 'Economy'), (4, 'B1-22D', 'Economy'); 

INSERT INTO Luggage (ReservationID, Weight) VALUES (1, 20.50), (2, 15.00), (2, 10.50), (4, 25.00); 

INSERT INTO WaitingList (PassengerID, ScheduleID, DateAdded) VALUES (5, 1, '2026-06-10 14:30:00'); 

INSERT INTO Staff (Name, Role, Email) VALUES ('Hakan Demir', 'Driver', 'hakan@staff.com'), ('Tariq Abdullah', 'Station Manager', 'tariq@staff.com'), ('Aisha Rahman', 'Customs Coordinator', 'aisha@staff.com'), ('Mehmet Celik', 'Maintenance Technician', 'mehmet@staff.com'); 

INSERT INTO StaffAssignment (StaffID, ScheduleID, AssignmentDate) VALUES (1, 1, '2026-07-15'), (2, 2, '2026-07-16'); 

INSERT INTO MaintenanceRecord (TrainID, TrackID, Date, Details) VALUES (1, NULL, '2026-06-20', 'Hejaz Express Engine overhaul completed.'), (NULL, 1, '2026-06-25', 'Track alignment checked between Istanbul and Gaziantep.'); 

INSERT INTO FreightShipment (TrainID, Shipper, Weight, Status) VALUES (4, 'Gulf Logistics Corp', 12500.00, 'In Transit'), (5, 'Anatolia Export', 8500.00, 'Customs Check'); 

INSERT INTO CargoContainer (ShipmentID, ContainerType) VALUES (1, 'Refrigerated'), (1, 'Standard Dry'), (2, 'Hazardous Material'); 

INSERT INTO CustomsClearance (ShipmentID, StationID, Date, Status) VALUES (1, 2, '2026-07-16', 'Approved'), (2, 5, '2026-07-17', 'Pending Inspection');

SET FOREIGN_KEY_CHECKS = 1;