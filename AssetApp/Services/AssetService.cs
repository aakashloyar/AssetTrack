using AssetApp.Models;
using AssetApp.Data;

namespace AssetApp.Services
{
    public class AssetService
    {
        private readonly DatabaseService _dbService;

        public AssetService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        // Create table if not exists
        public async Task CreateTableIfNotExistsAsync()
        {
            await _dbService.ExecuteAsync(AssetQueries.CreateTable);
        }

        //  Get all assets
        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            return await _dbService.QueryAsync<Asset>(AssetQueries.GetAllAssets);
        }

        //  Get asset by ID
        public async Task<Asset?> GetByIdAsync(int assetId)
        {
            return await _dbService.QuerySingleAsync<Asset>(
                AssetQueries.GetAssetById,
                new { AssetID = assetId }
            );
        }

        // Add new asset
        public async Task<int> AddAsync(Asset asset)
        {
            return await _dbService.ExecuteScalarAsync<int>(
                AssetQueries.AddAsset,
                new
                {
                    asset.AssetName,
                    AssetType = asset.AssetType.ToString(),
                    asset.MakeModel,
                    asset.SerialNumber,
                    asset.PurchaseDate,
                    asset.WarrantyExpiryDate,
                    Condition = asset.Condition.ToString(),
                    Status = asset.Status.ToString(),
                    IsSpare = asset.IsSpare.ToString(),
                    asset.Specifications
                }
            );
        }

        //  Update existing asset
        public async Task<bool> UpdateAsync(Asset asset)
        {
            var rowsAffected = await _dbService.ExecuteAsync(
                AssetQueries.UpdateAsset,
                new
                {
                    asset.AssetID,
                    asset.AssetName,
                    AssetType = asset.AssetType.ToString(),
                    asset.MakeModel,
                    asset.SerialNumber,
                    asset.PurchaseDate,
                    asset.WarrantyExpiryDate,
                    Condition = asset.Condition.ToString(),
                    Status = asset.Status.ToString(),
                    IsSpare = asset.IsSpare.ToString(),
                    asset.Specifications
                }
            );
            return rowsAffected > 0;
        }

        //  Delete asset
        public async Task<bool> DeleteAsync(int assetId)
        {
            var rowsAffected = await _dbService.ExecuteAsync(
                AssetQueries.DeleteAsset,
                new { AssetID = assetId }
            );
            return rowsAffected > 0;
        }
    }
}
