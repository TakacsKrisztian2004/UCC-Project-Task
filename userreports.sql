-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Már 14. 16:01
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET FOREIGN_KEY_CHECKS=0;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `userreports`
--
CREATE DATABASE IF NOT EXISTS `userreports` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `userreports`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `reports`
--
-- Létrehozva: 2025. Már 14. 13:45
-- Utolsó frissítés: 2025. Már 14. 14:16
--

DROP TABLE IF EXISTS `reports`;
CREATE TABLE `reports` (
  `ID` char(36) NOT NULL,
  `Title` tinytext NOT NULL,
  `Occurrence` datetime NOT NULL,
  `Description` text DEFAULT NULL,
  `Customer` tinytext NOT NULL,
  `Resolved` tinyint(1) NOT NULL,
  `UserID` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- TÁBLA KAPCSOLATAI `reports`:
--   `UserID`
--       `users` -> `ID`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--
-- Létrehozva: 2025. Már 12. 12:33
-- Utolsó frissítés: 2025. Már 14. 14:14
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `ID` char(36) NOT NULL,
  `Name` tinytext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- TÁBLA KAPCSOLATAI `users`:
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `verification`
--
-- Létrehozva: 2025. Már 12. 18:46
--

DROP TABLE IF EXISTS `verification`;
CREATE TABLE `verification` (
  `ID` int(11) NOT NULL,
  `code` varchar(6) NOT NULL,
  `email` varchar(320) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- TÁBLA KAPCSOLATAI `verification`:
--

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `reports`
--
ALTER TABLE `reports`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Reports UserID - Users ID` (`UserID`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `verification`
--
ALTER TABLE `verification`
  ADD PRIMARY KEY (`ID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `verification`
--
ALTER TABLE `verification`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `reports`
--
ALTER TABLE `reports`
  ADD CONSTRAINT `Reports UserID - Users ID` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;


--
-- Metaadat
--
USE `phpmyadmin`;

--
-- A(z) reports tábla metaadatai
--

--
-- A(z) users tábla metaadatai
--

--
-- A(z) verification tábla metaadatai
--

--
-- A(z) userreports adatbázis metaadatai
--
SET FOREIGN_KEY_CHECKS=1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
