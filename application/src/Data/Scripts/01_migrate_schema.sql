USE [EmployeeManagementSystem];
GO

IF OBJECT_ID(N'dbo.Departments', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Departments
    (
        DepartmentId INT IDENTITY(1, 1) NOT NULL,
        Name NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Departments_CreatedAt DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_Departments PRIMARY KEY (DepartmentId)
    );

    CREATE UNIQUE INDEX UX_Departments_Name ON dbo.Departments (Name);
END
GO

IF OBJECT_ID(N'dbo.Employees', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Employees
    (
        EmployeeId INT IDENTITY(1, 1) NOT NULL,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Phone NVARCHAR(30) NULL,
        DepartmentId INT NOT NULL,
        Position NVARCHAR(100) NULL,
        HireDate DATE NOT NULL,
        Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Employees_Status DEFAULT N'Active',
        Salary DECIMAL(18, 2) NOT NULL,
        CreatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Employees_CreatedAt DEFAULT SYSUTCDATETIME(),
        UpdatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Employees_UpdatedAt DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_Employees PRIMARY KEY (EmployeeId),
        CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
            REFERENCES dbo.Departments (DepartmentId),
        CONSTRAINT CK_Employees_Status CHECK (Status IN (N'Active', N'Inactive'))
    );

    CREATE UNIQUE INDEX UX_Employees_Email ON dbo.Employees (Email);
    CREATE INDEX IX_Employees_Name ON dbo.Employees (LastName, FirstName);
END
GO
