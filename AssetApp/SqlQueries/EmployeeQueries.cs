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
        

        public static string GetFilteredEmployees(string? fullName, string? department, string? email, string? phoneNumber)
        {
            var sql = @"
                SELECT *
                FROM Employees
                WHERE 1=1";

            if (!string.IsNullOrEmpty(fullName))
                sql += " AND FullName LIKE '%' + @FullName + '%'";
            if (!string.IsNullOrEmpty(department))
                sql += " AND Department = @Department";
            if (!string.IsNullOrEmpty(email))
                sql += " AND Email LIKE '%' + @Email + '%'";
            if (!string.IsNullOrEmpty(phoneNumber))
                sql += " AND PhoneNumber LIKE '%' + @PhoneNumber + '%'";

            sql += " ORDER BY FullName"; // default sort
            return sql;
        }
    }
}