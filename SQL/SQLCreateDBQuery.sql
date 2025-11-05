-- Создание базы данных
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'RealtyAgencyDB')
BEGIN
    CREATE DATABASE RealtyAgencyDB;
END
GO

USE RealtyAgencyDB;
GO

-- =============================================
-- Таблица: Users (Пользователь)
-- =============================================
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Not','Admin', 'Manager', 'Client')),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Индекс на логин для быстрого поиска
CREATE NONCLUSTERED INDEX IX_Users_Login ON Users(Login);
GO

-- =============================================
-- Таблица: ClientProfiles (Профиль клиента)
-- =============================================
CREATE TABLE ClientProfiles (
    ClientId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    CONSTRAINT FK_ClientProfiles_Users FOREIGN KEY (UserId) 
        REFERENCES Users(UserId) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Индекс на UserId для связи
CREATE NONCLUSTERED INDEX IX_ClientProfiles_UserId ON ClientProfiles(UserId);
GO

-- =============================================
-- Таблица: ManagerProfiles (Профиль риелтора)
-- =============================================
CREATE TABLE ManagerProfiles (
    ManagerId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    LicenseNumber NVARCHAR(50),
    Email NVARCHAR(100),
    HireDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ManagerProfiles_Users FOREIGN KEY (UserId) 
        REFERENCES Users(UserId) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Индекс на UserId для связи
CREATE NONCLUSTERED INDEX IX_ManagerProfiles_UserId ON ManagerProfiles(UserId);
GO

-- =============================================
-- Таблица: Properties (Недвижимость)
-- =============================================
CREATE TABLE Properties (
    PropertyId INT IDENTITY(1,1) PRIMARY KEY,
    ManagerId INT NULL,
    Address NVARCHAR(255) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    Area DECIMAL(10,2) NOT NULL,
    Rooms INT NOT NULL,
    Price MONEY NOT NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Created',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Properties_ManagerProfiles FOREIGN KEY (ManagerId) 
        REFERENCES ManagerProfiles(ManagerId) ON DELETE SET NULL ON UPDATE CASCADE
);
GO

-- Индексы для часто используемых полей
CREATE NONCLUSTERED INDEX IX_Properties_Status ON Properties(Status);
CREATE NONCLUSTERED INDEX IX_Properties_ManagerId ON Properties(ManagerId);
CREATE NONCLUSTERED INDEX IX_Properties_Price ON Properties(Price);
GO

-- =============================================
-- Таблица: Photos (Фото)
-- =============================================
CREATE TABLE Photos (
    PhotoId INT IDENTITY(1,1) PRIMARY KEY,
    PropertyId INT NOT NULL,
    PhotoData NVARCHAR(MAX) NOT NULL,
    IsMain BIT DEFAULT 0,
    CONSTRAINT FK_Photos_Properties FOREIGN KEY (PropertyId) 
        REFERENCES Properties(PropertyId) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Индекс на PropertyId
CREATE NONCLUSTERED INDEX IX_Photos_PropertyId ON Photos(PropertyId);
GO

-- =============================================
-- Таблица: Requests (Заявка)
-- =============================================
CREATE TABLE Requests (
    RequestId INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT NOT NULL,
    DesiredType NVARCHAR(50) NOT NULL,
    BudgetMin MONEY,
    BudgetMax MONEY,
    Status NVARCHAR(30) NOT NULL DEFAULT 'New',
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Requests_ClientProfiles FOREIGN KEY (ClientId) 
        REFERENCES ClientProfiles(ClientId) ON DELETE NO ACTION ON UPDATE CASCADE,
);
GO

-- Индексы
CREATE NONCLUSTERED INDEX IX_Requests_ClientId ON Requests(ClientId);
CREATE NONCLUSTERED INDEX IX_Requests_Status ON Requests(Status);
GO

-- =============================================
-- Таблица: Viewings (Показ)
-- =============================================
CREATE TABLE Viewings (
    ViewingId INT IDENTITY(1,1) PRIMARY KEY,
    RequestId INT NOT NULL,
    PropertyId INT NOT NULL,
    ScheduledTime DATETIME NOT NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Scheduled',
    Notes NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Viewings_Requests FOREIGN KEY (RequestId) 
        REFERENCES Requests(RequestId),
    CONSTRAINT FK_Viewings_Properties FOREIGN KEY (PropertyId) 
        REFERENCES Properties(PropertyId)
);
GO

-- Индексы
CREATE NONCLUSTERED INDEX IX_Viewings_RequestId ON Viewings(RequestId);
CREATE NONCLUSTERED INDEX IX_Viewings_PropertyId ON Viewings(PropertyId);
CREATE NONCLUSTERED INDEX IX_Viewings_ScheduledTime ON Viewings(ScheduledTime);
GO

-- =============================================
-- Таблица: Deals (Сделка)
-- =============================================
CREATE TABLE Deals (
    DealId INT IDENTITY(1,1) PRIMARY KEY,
    PropertyId INT NULL,
    ClientId INT NOT NULL,
    SalePrice MONEY NOT NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Pending',
    SignedDate DATE,
    CommissionRate DECIMAL(5,2) DEFAULT 0.00,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Deals_Properties FOREIGN KEY (PropertyId) 
        REFERENCES Properties(PropertyId) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT FK_Deals_ClientProfiles FOREIGN KEY (ClientId) 
        REFERENCES ClientProfiles(ClientId)
);
GO

-- Индексы
CREATE NONCLUSTERED INDEX IX_Deals_PropertyId ON Deals(PropertyId);
CREATE NONCLUSTERED INDEX IX_Deals_ClientId ON Deals(ClientId);
CREATE NONCLUSTERED INDEX IX_Deals_Status ON Deals(Status);
GO
