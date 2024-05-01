using SQLite;

namespace TradeTracker.DataModel;

[Table("Partner")]
public class PartnerDataModel {

    [PrimaryKey]
    public string Name { get; set; }
    
}