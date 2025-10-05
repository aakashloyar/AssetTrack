namespace AssetApp.Data
{
    public static class EmployeeQueries
    {


        public const string CreateTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
            CREATE TABLE Employees (
                EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
                FullName NVARCHAR(100) NOT NULL,
                Department NVARCHAR(100),
                Email NVARCHAR(150),
                PhoneNumber NVARCHAR(20),
                Designation NVARCHAR(100),
                Status NVARCHAR(20)
            );
        ";
        
        public const string GetAllEmployees = @"
            SELECT * FROM Employees";

        public const string GetEmployeeById = @"
            SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

        public const string AddEmployee = @"
            INSERT INTO Employees (FullName, Department, Email, PhoneNumber, Designation, Status)
            VALUES (@FullName, @Department, @Email, @PhoneNumber, @Designation, @Status);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdateEmployee = @"
            UPDATE Employees
            SET FullName=@FullName,
            Department=@Department,
            Email=@Email,
            PhoneNumber=@PhoneNumber,
            Designation=@Designation,
            Status=@Status
            WHERE EmployeeID=@EmployeeID";

        public const string DeleteEmployee = @"
            DELETE FROM Employees WHERE EmployeeId=@EmployeeID";
            
    }
}