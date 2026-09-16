USE [EmployeeManagementSystem];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Employees)
BEGIN
    INSERT INTO dbo.Departments (Name, CreatedAt)
    VALUES
        (N'Engineering', SYSUTCDATETIME()),
        (N'Human Resources', SYSUTCDATETIME()),
        (N'Finance', SYSUTCDATETIME()),
        (N'Sales', SYSUTCDATETIME()),
        (N'Marketing', SYSUTCDATETIME()),
        (N'Operations', SYSUTCDATETIME()),
        (N'IT Support', SYSUTCDATETIME());

    INSERT INTO dbo.Employees
    (
        FirstName,
        LastName,
        Email,
        Phone,
        DepartmentId,
        Position,
        HireDate,
        Status,
        Salary,
        CreatedAt,
        UpdatedAt
    )
    SELECT
        seed.FirstName,
        seed.LastName,
        seed.Email,
        seed.Phone,
        department.DepartmentId,
        seed.Position,
        seed.HireDate,
        seed.Status,
        seed.Salary,
        SYSUTCDATETIME(),
        SYSUTCDATETIME()
    FROM
    (
        VALUES
            (N'Juan',    N'Dela Cruz', N'juan.delacruz@example.com',    N'+63 917 555 0101', N'Engineering',     N'Senior Software Engineer', '2018-03-12', N'Active',   95000.00),
            (N'Juana',   N'Dela Cruz', N'juana.delacruz@example.com',   N'+63 917 555 0102', N'Human Resources', N'HR Manager',               '2017-07-01', N'Active',   88000.00),
            (N'Jose',    N'Dela Cruz', N'jose.delacruz@example.com',    N'+63 917 555 0103', N'Engineering',     N'DevOps Engineer',          '2020-08-17', N'Active',   83000.00),
            (N'Josefa',  N'Dela Cruz', N'josefa.delacruz@example.com',  N'+63 917 555 0104', N'Finance',         N'Finance Manager',          '2016-05-30', N'Active',   97000.00),
            (N'John',    N'Dela Cruz', N'john.delacruz@example.com',    N'+63 917 555 0105', N'Engineering',     N'Software Engineer',        '2021-06-15', N'Active',   72000.00),
            (N'Joanna',  N'Dela Cruz', N'joanna.delacruz@example.com',  N'+63 917 555 0106', N'Finance',         N'Accountant',               '2020-02-03', N'Active',   61000.00),
            (N'James',   N'Dela Cruz', N'james.delacruz@example.com',   N'+63 917 555 0107', N'Sales',           N'Sales Representative',     '2022-09-19', N'Active',   54000.00),
            (N'Jeremy',  N'Dela Cruz', N'jeremy.delacruz@example.com',  N'+63 917 555 0108', N'Marketing',       N'Marketing Associate',      '2023-01-23', N'Active',   48000.00),
            (N'Jasmine', N'Dela Cruz', N'jasmine.delacruz@example.com', N'+63 917 555 0109', N'Marketing',       N'Marketing Manager',        '2018-04-09', N'Inactive', 85000.00),
            (N'Jerome',  N'Dela Cruz', N'jerome.delacruz@example.com',  N'+63 917 555 0110', N'Operations',      N'Operations Analyst',       '2019-11-04', N'Active',   66000.00),
            (N'Jessa',   N'Dela Cruz', N'jessa.delacruz@example.com',   N'+63 917 555 0111', N'Human Resources', N'HR Specialist',            '2022-03-07', N'Active',   45000.00),
            (N'Jomar',   N'Dela Cruz', N'jomar.delacruz@example.com',   N'+63 917 555 0112', N'IT Support',      N'IT Support Specialist',    '2021-10-11', N'Active',   42000.00),
            (N'Joy',     N'Dela Cruz', N'joy.delacruz@example.com',     N'+63 917 555 0113', N'Sales',           N'Sales Manager',            '2015-12-14', N'Active',   91000.00),
            (N'Julius',  N'Dela Cruz', N'julius.delacruz@example.com',  N'+63 917 555 0114', N'Engineering',     N'QA Engineer',              '2022-07-25', N'Active',   58000.00),
            (N'Jocelyn', N'Dela Cruz', N'jocelyn.delacruz@example.com', N'+63 917 555 0115', N'Operations',      N'Operations Manager',       '2014-01-20', N'Active',   99000.00)
    ) AS seed (FirstName, LastName, Email, Phone, DepartmentName, Position, HireDate, Status, Salary)
    INNER JOIN dbo.Departments AS department ON department.Name = seed.DepartmentName;
END
GO
