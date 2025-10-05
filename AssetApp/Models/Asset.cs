namespace AssetApp.Models
{
    public class Asset
    {
        public int AssetID { get; set; }                    
        public string AssetName { get; set; }              
        public AssetType AssetType { get; set; }          
        public string MakeModel { get; set; }               // e.g., Dell Inspiron 15
        public string SerialNumber { get; set; }            
        public DateTime PurchaseDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }  
        public AssetCondition Condition { get; set; }      
        public AssetStatus Status { get; set; }            
        public IsSpare IsSpare { get; set; }                
        public string Specifications { get; set; }        
    }
}
