using SQLite;

namespace TradeTracker.DataModel;

[Table("Currency")]
public class CurrencyDataModel {

    [PrimaryKey]
    public string Name { get; set; }
    
}