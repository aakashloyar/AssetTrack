namespace AssetApp.Data
{
    public static class AssignmentQueries
    {
        public const string CreateTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Assignments' AND xtype='U')
            CREATE TABLE Assignments (
                AssignmentID INT IDENTITY(1,1) PRIMARY KEY,
                AssetID INT NOT NULL,
                EmployeeID INT NOT NULL,
                AssignedDate DATETIME NOT NULL DEFAULT GETDATE(),
                ReturnedDate DATETIME NULL,
                Notes NVARCHAR(MAX) NULL
            );
        ";

        public const string GetAllAssignments = @"
            SELECT AssignmentID, AssetID, EmployeeID, AssignedDate, ReturnedDate, Notes
            FROM Assignments
            ORDER BY AssignedDate DESC;
        ";

        public const string GetAssignmentById = @"
            SELECT AssignmentID, AssetID, EmployeeID, AssignedDate, ReturnedDate, Notes
            FROM Assignments
            WHERE AssignmentID = @AssignmentID;
        ";

        // Insert new assignment (status must be checked in service)
        public const string AddAssignment = @"
            INSERT INTO Assignments (AssetID, EmployeeID, AssignedDate, Notes)
            VALUES (@AssetID, @EmployeeID, @AssignedDate, @Notes);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";

        // Update only notes or returned date — cannot change both assetId and employeeId
        public const string UpdateAssignment = @"
            UPDATE Assignments
            SET ReturnedDate = @ReturnedDate,
                Notes = @Notes
            WHERE AssignmentID = @AssignmentID;
        ";

        public const string DeleteAssignment = @"
            DELETE FROM Assignments WHERE AssignmentID = @AssignmentID;
        ";

        // Filter/search
        public static string GetFilteredAssignmentsSimple(
            int? assetId,
            int? employeeId,
            DateTime? fromDate,
            DateTime? toDate)
        {
            var sql = @"SELECT * FROM Assignments WHERE 1=1";

            if (assetId.HasValue)
                sql += " AND AssetID = @AssetID";
            if (employeeId.HasValue)
                sql += " AND EmployeeID = @EmployeeID";
            if (fromDate.HasValue)
                sql += " AND AssignedDate >= @FromDate";
            if (toDate.HasValue)
                sql += " AND AssignedDate <= @ToDate";

            sql += " ORDER BY AssignedDate DESC";
            return sql;
        }

        // Check if asset is available
        public const string CheckAssetAvailable = @"
            SELECT Status FROM Assets WHERE AssetID = @AssetID;
        ";

        // Update asset status
        public const string UpdateAssetStatus = @"
            UPDATE Assets SET Status = @Status WHERE AssetID = @AssetID;
        ";

        public const string CheckEmployeeExists = @"
            SELECT COUNT(1) FROM Employees WHERE EmployeeID = @EmployeeID
        ";
    }
}
