using AssetApp.Models;
using AssetApp.SqlQueries;

namespace AssetApp.Services
{
    public class AssignmentService
    {
        private readonly DatabaseService _dbService;

        public AssignmentService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task CreateTableIfNotExistsAsync()
        {
            await _dbService.ExecuteAsync(AssignmentQueries.CreateTable);
        }

        public async Task<IEnumerable<Assignment>> GetAllAsync()
        {
            return await _dbService.QueryAsync<Assignment>(AssignmentQueries.GetAllAssignments);
        }

        public async Task<Assignment?> GetByIdAsync(int assignmentId)
        {
            return await _dbService.QuerySingleAsync<Assignment>(
                AssignmentQueries.GetAssignmentById,
                new { AssignmentID = assignmentId }
            );
        }

        // Assign an asset to an employee (only if Available)
        public async Task<int> AddAsync(Assignment assignment)
        {
            // 1. Check asset status
            var status = await _dbService.QuerySingleAsync<string>(
                AssignmentQueries.CheckAssetAvailable,
                new { AssetID = assignment.AssetID }
            );

            if (status != "Available")
                throw new InvalidOperationException("Asset is not available for assignment.");

            // 2. Check if employee exists
            var employeeExists = await _dbService.QuerySingleAsync<int>(
                AssignmentQueries.CheckEmployeeExists,
                new { EmployeeID = assignment.EmployeeID }
            );

            if (employeeExists == 0)
                throw new InvalidOperationException("Employee with this ID does not exist.");

            // 3. Insert assignment
            var id = await _dbService.ExecuteScalarAsync<int>(
                AssignmentQueries.AddAssignment,
                new
                {
                    assignment.AssetID,
                    assignment.EmployeeID,
                    assignment.AssignedDate,
                    assignment.Notes
                }
            );

            // 4. Update asset status to Assigned
            await _dbService.ExecuteAsync(
                AssignmentQueries.UpdateAssetStatus,
                new { AssetID = assignment.AssetID, Status = "Assigned" }
            );

            return id;
        }


        // Update assignment (only notes or returned date)
        public async Task<bool> UpdateAsync(Assignment assignment)
        {
            // Fetch existing assignment
            var existing = await GetByIdAsync(assignment.AssignmentID);
            if (existing == null)
                throw new InvalidOperationException("Assignment not found.");

            // Prevent changing both AssetID and EmployeeID
            if (assignment.AssetID != existing.AssetID || assignment.EmployeeID != existing.EmployeeID)
                throw new InvalidOperationException("Cannot change AssetID or EmployeeID in update.");

            // Update assignment
            var rows = await _dbService.ExecuteAsync(
                AssignmentQueries.UpdateAssignment,
                new
                {
                    assignment.AssignmentID,
                    assignment.ReturnedDate,
                    assignment.Notes
                }
            );

            // If returning asset, update asset status
            if (assignment.ReturnedDate.HasValue && !existing.ReturnedDate.HasValue)
            {
                await _dbService.ExecuteAsync(
                    AssignmentQueries.UpdateAssetStatus,
                    new { AssetID = existing.AssetID, Status = "Available" }
                );
            }

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int assignmentId)
        {
            // 1. Get the assignment
            var assignment = await GetByIdAsync(assignmentId);
            if (assignment == null)
                throw new InvalidOperationException("Assignment not found.");

            // 2. Delete the assignment
            var rows = await _dbService.ExecuteAsync(
                AssignmentQueries.DeleteAssignment,
                new { AssignmentID = assignmentId }
            );

            // 3. If the asset was still assigned (ReturnedDate is null), set it back to Available
            if (!assignment.ReturnedDate.HasValue)
            {
                await _dbService.ExecuteAsync(
                    AssignmentQueries.UpdateAssetStatus,
                    new { AssetID = assignment.AssetID, Status = "Available" }
                );
            }

            return rows > 0;
        }

        public async Task<IEnumerable<Assignment>> GetFilteredAssignmentsAsync(
            int? assetId,
            int? employeeId,
            DateTime? fromDate,
            DateTime? toDate)
        {
            var sql = AssignmentQueries.GetFilteredAssignmentsSimple(assetId, employeeId, fromDate, toDate);

            return await _dbService.QueryAsync<Assignment>(sql, new
            {
                AssetID = assetId,
                EmployeeID = employeeId,
                FromDate = fromDate,
                ToDate = toDate
            });
        }
    }
}
