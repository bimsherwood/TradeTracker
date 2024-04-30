namespace TradeTracker.DataModel;

public class TransactionDataModel {
    public int Id { get; set; }
    public string Partner { get; set; }
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public string Currency { get; set; }
    public string Description { get; set; }
}