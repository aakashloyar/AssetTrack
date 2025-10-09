using AssetApp.SqlQueries;
using AssetApp.Models;

namespace AssetApp.Services
{
    public partial class DashboardService
    {
        private readonly DatabaseService _dbService;

        public DashboardService(DatabaseService dbService)
        {
            _dbService = dbService;
        }
        public async Task<int> GetTotalAssetsAsync()
        {
            return await _dbService.QuerySingleAsync<int>(DashboardQueries.GetTotalAssets);
        }

        public async Task<Dictionary<string, int>> GetAssetsByTypeAsync()
        {
            var results = await _dbService.QueryAsync<(string AssetType, int Count)>(DashboardQueries.GetAssetsByType);
            return results.ToDictionary(x => x.AssetType, x => x.Count);
        }

        public async Task<int> GetAssignedAssetsAsync()
        {
            return await _dbService.QuerySingleAsync<int>(DashboardQueries.GetAssignedAssets);
        }

        public async Task<int> GetAvailableAssetsAsync()
        {
            return await _dbService.QuerySingleAsync<int>(DashboardQueries.GetAvailableAssets);
        }

        public async Task<int> GetUnderRepairAssetsAsync()
        {
            return await _dbService.QuerySingleAsync<int>(DashboardQueries.GetUnderRepairAssets);
        }

        public async Task<int> GetRetiredAssetsAsync()
        {
            return await _dbService.QuerySingleAsync<int>(DashboardQueries.GetRetiredAssets);
        }

        public async Task<int> GetSpareAssetsAsync()
        {
            return await _dbService.QuerySingleAsync<int>(DashboardQueries.GetSpareAssets);
        }
    }
}
