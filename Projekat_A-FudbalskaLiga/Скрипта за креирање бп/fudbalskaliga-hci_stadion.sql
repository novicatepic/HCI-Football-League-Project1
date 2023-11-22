CREATE DATABASE  IF NOT EXISTS `fudbalskaliga-hci` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `fudbalskaliga-hci`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: fudbalskaliga-hci
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `stadion`
--

LOCK TABLES `stadion` WRITE;
/*!40000 ALTER TABLE `stadion` DISABLE KEYS */;
INSERT INTO `stadion` VALUES (1,'Etihad!',22000,'Manchester'),(2,'Old Trafford',45000,'Manchester'),(3,'Emirates Stadium',60704,'London'),(4,'Stamford Bridge',40341,'London'),(5,'White Hart Lane',62850,'London'),(6,'Anfield',54000,'Liverpool'),(7,'London Stadium',90000,'London'),(8,'Goodison Park',39572,'Liverpool'),(9,'St. James\' Park',52000,'Newcastle'),(10,'Selhurst Park',25487,'London!'),(11,'St Mary\'s Stadium',32384,'Southampton'),(12,'King Power Stadium',32261,'Leicester'),(13,'Villa Park',42640,'Birmingham'),(14,'Vitality Stadium',11379,'Kings Park'),(15,'Molineux Stadium',32050,'Wolverhampton'),(16,'Falmer Stadium',31800,'Falmer'),(17,'Gtech Stadium',17250,'Brentford'),(18,'Craven Cottage',25700,'Fulham'),(19,'City Ground',62850,'West Bridgford'),(20,'Elland Road',37895,'Leeds'),(27,'123',123,'123');
/*!40000 ALTER TABLE `stadion` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-10 19:54:34
