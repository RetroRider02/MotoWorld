## Bemutatás

Ezen projekt keretein belül megvalósításra került egy olyan webalkalmazás, amelynek célja az, hogy a felhasználók egyszerűen és hatékonyan legyenek képesek egy intuitív felhasználói felületen keresztül **fiókjuk  menedzselésére**, **hirdetések feladására, kezelésére,** továbbá **hirdetések böngészésére és szűrésére**. A webalkalmazás a jövőben várhatóan új funkciókkal fog bővülni, amely funkcióknak a részeletezése lentebb található.

<img width="1539" height="1000" alt="mw_1" src="https://github.com/user-attachments/assets/4a7825ee-1aa0-4a12-9c1b-726eeaeebc2c" />
<img width="1539" height="997" alt="mw_2" src="https://github.com/user-attachments/assets/071398b5-60b0-4abb-9e34-47a122bfc4e9" />

#### Főbb funkciók

- Jogosultságkezelés
- Felhasználói fiókok kezelése, törlése
- Hirdetésfeladás
- Hirdetések törlése, módosítása
- Képek tárolása, kezelése hirdetésekhez
- Hirdetések jegelése
- Hirdetések böngészése, szűrése
- Helyek térképeken való megjelenítése

#### Várható funkciók

- Valós idejű üzenetküldések
- Nemkívánatos hirdetések jelentése
- Kedvencek funkció hirdetések mentéséhez

## E-K Diagram

<img width="1367" height="609" alt="db" src="https://github.com/user-attachments/assets/ba9704be-c2b1-45e8-b200-dbf789d8d106" />

## Adatbázis táblák

### MotorcycleType

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| ----------------------------- | ------------- | ------------- |
| ID                            | int           | PRIMARY KEY   |
| Manufacturer                  | nvarchar(30)  | NOT NULL      |
| Model                         | nvarchar(50)  | NOT NULL      |

### Motorcycles

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| --------------------- | ------------- | ------------- |
| ID                    | int           | PRIMARY KEY   |
| MotrocycleTypeID      | int           | FOREIGN KEY   |
| YearOfManufacture     | int           | NOT NULL      |
| Category              | nvarchar      | NOT NULL      |
| Color                 | nvarchar(15)  | NOT NULL      |
| Condition             | nvarchar      | NOT NULL      |
| Mileage               | int           | NOT NULL      |
| Weight                | int           | NOT NULL      |
| Fuel                  | nvarchar      | NOT NULL      |
| Power                 | int           | NOT NULL      |
| WorkSchedule          | int           | NOT NULL      |
| NumberOfCylinders     | int           | NOT NULL      |
| CylinderCapacity      | int           | NOT NULL      |
| ValvesPerCylinders    | int           | NOT NULL      |
| CylinderArrangement   | nvarchar      | NOT NULL      |
| Mixture               | nvarchar      | NOT NULL      |
| Cooling               | nvarchar      | NOT NULL      |
| DriveType             | nvarchar      | NOT NULL      |
| Transmission          | nvarchar      | NOT NULL      |

### MotorcycleAdvertising

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| --------------------- | ------------- | ----------------------- |
| MotorcycleID          | int           | PRIMARY KEY, FOREIGN KEY|
| AdvertisingID         | int           | PRIMARY KEY, FOREIGN KEY|

### Advertising

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| --------------------- | ------------- | ----------------------- |
| ID                    | int           | PRIMARY KEY             |
| IdentityUserID        | nvarchar(450) | FOREIGN KEY             |
| PlaceID               | int           | FOREIGN KEY             |
| Price                 | int           | NOT NULL                |
| Description           | nvarchar(1024)| NOT NULL                |
| LastModification      | datetime2     | NOT NULL                |
| Created               | datetime2     | NOT NULL                |
| Frozen                | bit           | NOT NULL                |

### Picture

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| --------------------- | ------------- | ------------- |
| ID                    | int           | PRIMARY KEY   |
| AdvertisingID         | int           | FOREIGN KEY   |
| FileName              | nvarchar(75)  | NOT NULL      |
| ContentType           | nvarchar(15)  | NOT NULL      |
| Data                  | varbinary     | NOT NULL      |
| UploadDate            | datetime2     | NOT NULL      |

### Place

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| --------------------- | ------------- | ----------------------- |
| ID                    | int           | PRIMARY KEY             |
| ZipCode               | int           | NOT NULL                |
| CityName              | nvarchar(30)  | NOT NULL                |
| Street                | nvarchar(30)  | NOT NULL                |
| HouseNumber           | nvarchar(10)  | NOT NULL                |

### Messages

|<div style="width: 220px">Name</div>| <div style="width: 120px">Data Type</div> | <div style="width: 200px">Constraints</div> |
| --------------------- | ------------- | ----------------------- |
| ID                    | int           | PRIMARY KEY             |
| AdvertisingID         | int           | FOREIGN KEY             |
| IdentityUserID        | nvarchar(450) | FOREIGN KEY             |
| Content               | nvarchar(1024)| NOT NULL                |
| Created               | datetime2     | NOT NULL                |
