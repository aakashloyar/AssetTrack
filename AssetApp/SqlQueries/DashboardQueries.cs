namespace AssetApp.Data
{
    public static class DashboardQueries
    {
        // Existing queries...

        public const string GetTotalAssets = @"SELECT COUNT(*) FROM Assets;";

        public const string GetAssetsByType = @"
            SELECT AssetType, COUNT(*) AS Count
            FROM Assets
            GROUP BY AssetType;
        ";

        public const string GetAssignedAssets = @"SELECT COUNT(*) FROM Assets WHERE Status = 'Assigned';";
        public const string GetAvailableAssets = @"SELECT COUNT(*) FROM Assets WHERE Status = 'Available';";
        public const string GetUnderRepairAssets = @"SELECT COUNT(*) FROM Assets WHERE Status = 'UnderRepair';";
        public const string GetRetiredAssets = @"SELECT COUNT(*) FROM Assets WHERE Status = 'Retired';";
        public const string GetSpareAssets = @"SELECT COUNT(*) FROM Assets WHERE IsSpare = 'Yes';";
    }
}
