namespace AssetApp.Models
{
    public enum EmployeeStatus
    {
        Active,
        Inactive
    }

    public enum AssetCondition
    {
        New,
        Good,
        NeedsRepair,
        Damaged
    }

    public enum AssetStatus
    {
        Available,
        Assigned,
        UnderRepair,
        Retired
    }

    public enum IsSpare
    {
        No,
        Yes
    }

    public enum AssetType
    {
        Laptop,
        Desktop,
        Monitor,
        Printer,
        Projector,
        Furniture,
        Other
    }
}
