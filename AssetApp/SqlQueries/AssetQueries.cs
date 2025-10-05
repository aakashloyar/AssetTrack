namespace AssetApp.Data
{
    public static class AssetQueries
    {
        public const string CreateTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Assets' AND xtype='U')
            CREATE TABLE Assets (
                AssetID INT IDENTITY(1,1) PRIMARY KEY,
                AssetName NVARCHAR(150) NOT NULL,
                AssetType NVARCHAR(50),
                MakeModel NVARCHAR(150),
                SerialNumber NVARCHAR(100),
                PurchaseDate DATETIME NOT NULL,
                WarrantyExpiryDate DATETIME NULL,
                Condition NVARCHAR(50),
                Status NVARCHAR(50),
                IsSpare NVARCHAR(10),
                Specifications NVARCHAR(MAX)
            );
        ";

        public const string GetAllAssets = @"SELECT * FROM Assets";

        public const string GetAssetById = @"SELECT * FROM Assets WHERE AssetID = @AssetID";

        public const string AddAsset = @"
            INSERT INTO Assets 
            (AssetName, AssetType, MakeModel, SerialNumber, PurchaseDate, WarrantyExpiryDate, Condition, Status, IsSpare, Specifications)
            VALUES 
            (@AssetName, @AssetType, @MakeModel, @SerialNumber, @PurchaseDate, @WarrantyExpiryDate, @Condition, @Status, @IsSpare, @Specifications);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";

        public const string UpdateAsset = @"
            UPDATE Assets SET
                AssetName = @AssetName,
                AssetType = @AssetType,
                MakeModel = @MakeModel,
                SerialNumber = @SerialNumber,
                PurchaseDate = @PurchaseDate,
                WarrantyExpiryDate = @WarrantyExpiryDate,
                Condition = @Condition,
                Status = @Status,
                IsSpare = @IsSpare,
                Specifications = @Specifications
            WHERE AssetID = @AssetID;
        ";

        public const string DeleteAsset = @"DELETE FROM Assets WHERE AssetID = @AssetID";
    }
}
