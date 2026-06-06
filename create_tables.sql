CREATE DATABASE IF NOT EXISTS railway_db;
USE railway_db;
CREATE TABLE `cargocontainer` (
  `ContainerID` int NOT NULL AUTO_INCREMENT,
  `ShipmentID` int NOT NULL,
  `ContainerType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ContainerID`),
  KEY `ShipmentID` (`ShipmentID`),
  CONSTRAINT `cargocontainer_ibfk_1` FOREIGN KEY (`ShipmentID`) REFERENCES `freightshipment` (`ShipmentID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `country` (
  `CountryID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`CountryID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `customsclearance` (
  `ClearanceID` int NOT NULL AUTO_INCREMENT,
  `ShipmentID` int NOT NULL,
  `StationID` int NOT NULL,
  `Date` date NOT NULL,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ClearanceID`),
  KEY `ShipmentID1` (`ShipmentID`),
  KEY `StationID` (`StationID`),
  CONSTRAINT `customsclearance_ibfk_1` FOREIGN KEY (`ShipmentID`) REFERENCES `freightshipment` (`ShipmentID`) ON DELETE CASCADE,
  CONSTRAINT `customsclearance_ibfk_2` FOREIGN KEY (`StationID`) REFERENCES `station` (`StationID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `dependent` (
  `PassengerID` int NOT NULL,
  `DependentID` int NOT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`PassengerID`,`DependentID`),
  CONSTRAINT `dependent_ibfk_1` FOREIGN KEY (`PassengerID`) REFERENCES `passenger` (`PassengerID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `freightshipment` (
  `ShipmentID` int NOT NULL AUTO_INCREMENT,
  `TrainID` int NOT NULL,
  `Shipper` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Weight` decimal(10,2) NOT NULL,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ShipmentID`),
  KEY `TrainID` (`TrainID`),
  CONSTRAINT `freightshipment_ibfk_1` FOREIGN KEY (`TrainID`) REFERENCES `freighttrain` (`TrainID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `freighttrain` (
  `TrainID` int NOT NULL,
  `MaxCargoWeight` decimal(10,2) NOT NULL,
  PRIMARY KEY (`TrainID`),
  CONSTRAINT `freighttrain_ibfk_1` FOREIGN KEY (`TrainID`) REFERENCES `train` (`TrainID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `loyaltyaccount` (
  `AccountID` int NOT NULL AUTO_INCREMENT,
  `PassengerID` int NOT NULL,
  `TotalMiles` int DEFAULT '0',
  `Class` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'Bronze',
  PRIMARY KEY (`AccountID`),
  UNIQUE KEY `PassengerID` (`PassengerID`),
  CONSTRAINT `loyaltyaccount_ibfk_1` FOREIGN KEY (`PassengerID`) REFERENCES `passenger` (`PassengerID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `luggage` (
  `LuggageID` int NOT NULL AUTO_INCREMENT,
  `ReservationID` int NOT NULL,
  `Weight` decimal(5,2) NOT NULL,
  PRIMARY KEY (`LuggageID`),
  KEY `ReservationID` (`ReservationID`),
  CONSTRAINT `luggage_ibfk_1` FOREIGN KEY (`ReservationID`) REFERENCES `reservation` (`ReservationID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `maintenancerecord` (
  `MaintenanceID` int NOT NULL AUTO_INCREMENT,
  `TrainID` int DEFAULT NULL,
  `TrackID` int DEFAULT NULL,
  `Date` date NOT NULL,
  `Details` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MaintenanceID`),
  KEY `TrackID` (`TrackID`),
  KEY `TrainID1` (`TrainID`),
  CONSTRAINT `maintenancerecord_ibfk_1` FOREIGN KEY (`TrainID`) REFERENCES `train` (`TrainID`) ON DELETE CASCADE,
  CONSTRAINT `maintenancerecord_ibfk_2` FOREIGN KEY (`TrackID`) REFERENCES `tracksegment` (`TrackID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `passenger` (
  `PassengerID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`PassengerID`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `passengertrain` (
  `TrainID` int NOT NULL,
  `PassengerCapacity` int NOT NULL,
  PRIMARY KEY (`TrainID`),
  CONSTRAINT `passengertrain_ibfk_1` FOREIGN KEY (`TrainID`) REFERENCES `train` (`TrainID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `reservation` (
  `ReservationID` int NOT NULL AUTO_INCREMENT,
  `PassengerID` int NOT NULL,
  `ScheduleID` int NOT NULL,
  `Date` date NOT NULL,
  `ReservationStatus` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`ReservationID`),
  KEY `PassengerID1` (`PassengerID`),
  KEY `ScheduleID` (`ScheduleID`),
  CONSTRAINT `reservation_ibfk_1` FOREIGN KEY (`PassengerID`) REFERENCES `passenger` (`PassengerID`),
  CONSTRAINT `reservation_ibfk_2` FOREIGN KEY (`ScheduleID`) REFERENCES `trainschedule` (`ScheduleID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `route` (
  `RouteID` int NOT NULL AUTO_INCREMENT,
  `RouteName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`RouteID`),
  UNIQUE KEY `RouteName` (`RouteName`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `routestop` (
  `RouteID` int NOT NULL,
  `StationID` int NOT NULL,
  `SequenceNumber` int NOT NULL,
  PRIMARY KEY (`RouteID`,`StationID`),
  KEY `StationID1` (`StationID`),
  CONSTRAINT `routestop_ibfk_1` FOREIGN KEY (`RouteID`) REFERENCES `route` (`RouteID`) ON DELETE CASCADE,
  CONSTRAINT `routestop_ibfk_2` FOREIGN KEY (`StationID`) REFERENCES `station` (`StationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `seat` (
  `SeatID` int NOT NULL AUTO_INCREMENT,
  `ReservationID` int NOT NULL,
  `SeatNumber` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CoachType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`SeatID`),
  KEY `ReservationID1` (`ReservationID`),
  CONSTRAINT `seat_ibfk_1` FOREIGN KEY (`ReservationID`) REFERENCES `reservation` (`ReservationID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `sensors` (
  `SensorId` int NOT NULL AUTO_INCREMENT,
  `SensorName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SensorType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TrainId` int NOT NULL,
  PRIMARY KEY (`SensorId`),
  KEY `IX_Sensors_TrainId` (`TrainId`),
  CONSTRAINT `FK_Sensors_train_TrainId` FOREIGN KEY (`TrainId`) REFERENCES `train` (`TrainID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `staff` (
  `StaffID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Role` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`StaffID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `staffassignment` (
  `StaffID` int NOT NULL,
  `ScheduleID` int NOT NULL,
  `AssignmentDate` date NOT NULL,
  PRIMARY KEY (`StaffID`,`ScheduleID`),
  KEY `ScheduleID1` (`ScheduleID`),
  CONSTRAINT `staffassignment_ibfk_1` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`) ON DELETE CASCADE,
  CONSTRAINT `staffassignment_ibfk_2` FOREIGN KEY (`ScheduleID`) REFERENCES `trainschedule` (`ScheduleID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `station` (
  `StationID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `StationType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CountryID` int NOT NULL,
  PRIMARY KEY (`StationID`),
  KEY `CountryID` (`CountryID`),
  CONSTRAINT `station_ibfk_1` FOREIGN KEY (`CountryID`) REFERENCES `country` (`CountryID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `tracksegment` (
  `TrackID` int NOT NULL AUTO_INCREMENT,
  `StartStationID` int NOT NULL,
  `EndStationID` int NOT NULL,
  `Distance` decimal(10,2) NOT NULL,
  `MaxPassengerSpeed` int NOT NULL,
  `MaxFreightSpeed` int NOT NULL,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`TrackID`),
  KEY `EndStationID` (`EndStationID`),
  KEY `StartStationID` (`StartStationID`),
  CONSTRAINT `tracksegment_ibfk_1` FOREIGN KEY (`StartStationID`) REFERENCES `station` (`StationID`),
  CONSTRAINT `tracksegment_ibfk_2` FOREIGN KEY (`EndStationID`) REFERENCES `station` (`StationID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `train` (
  `TrainID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TrainType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`TrainID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `trainschedule` (
  `ScheduleID` int NOT NULL AUTO_INCREMENT,
  `TrainID` int NOT NULL,
  `RouteID` int NOT NULL,
  `Date` date NOT NULL,
  `DepartureTime` time NOT NULL,
  PRIMARY KEY (`ScheduleID`),
  KEY `RouteID` (`RouteID`),
  KEY `TrainID2` (`TrainID`),
  CONSTRAINT `trainschedule_ibfk_1` FOREIGN KEY (`TrainID`) REFERENCES `train` (`TrainID`),
  CONSTRAINT `trainschedule_ibfk_2` FOREIGN KEY (`RouteID`) REFERENCES `route` (`RouteID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
CREATE TABLE `waitinglist` (
  `WaitlistID` int NOT NULL AUTO_INCREMENT,
  `PassengerID` int NOT NULL,
  `ScheduleID` int NOT NULL,
  `DateAdded` datetime NOT NULL,
  PRIMARY KEY (`WaitlistID`),
  KEY `PassengerID2` (`PassengerID`),
  KEY `ScheduleID2` (`ScheduleID`),
  CONSTRAINT `waitinglist_ibfk_1` FOREIGN KEY (`PassengerID`) REFERENCES `passenger` (`PassengerID`) ON DELETE CASCADE,
  CONSTRAINT `waitinglist_ibfk_2` FOREIGN KEY (`ScheduleID`) REFERENCES `trainschedule` (`ScheduleID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
