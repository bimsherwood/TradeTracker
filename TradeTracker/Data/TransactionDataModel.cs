using SQLite;

namespace TradeTracker.DataModel;

[Table("TradeTransaction")]
public class TransactionDataModel {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed(Name = "CurrencyPartnerDate", Order = 1)]
    public string Currency { get; set; }

    [Indexed(Name = "CurrencyPartnerDate", Order = 2)]
    public string Partner { get; set; }

    [Indexed(Name = "CurrencyPartnerDate", Order = 3)]
    public DateTime Date { get; set; }
    
    public double Price { get; set; }
    
    public string Description { get; set; }
    
}