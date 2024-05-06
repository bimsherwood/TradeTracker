using TradeTracker.DataModel;

namespace TradeTracker.Services;

public class CsvService {

    private static readonly string[] Headers = new[] {
            nameof(TransactionDataModel.Id),
            nameof(TransactionDataModel.Currency),
            nameof(TransactionDataModel.Partner),
            nameof(TransactionDataModel.Date),
            nameof(TransactionDataModel.Price),
            nameof(TransactionDataModel.Description)
        };

    public async Task WriteHeader(TextWriter output){
        await WriteRow(Headers, output);
    }

    public async Task Export(IEnumerable<TransactionDataModel> transactions, TextWriter output){
        foreach (var transaction in transactions) {
            var data = new []{
                transaction.Id.ToString(),
                transaction.Currency,
                transaction.Partner,
                transaction.Date.ToString("yyyy-MM-dd"),
                transaction.Price.ToString("F2"),
                transaction.Description
            };
            await WriteRow(data, output);
        }
    }

    private async Task WriteRow(string[] cells, TextWriter output){
        var escapedValues = cells.Select(Escape);
        var row = string.Join(",", escapedValues);
        await output.WriteLineAsync(row);
    }

    private string Escape(string value){
        var reservedCharacters = new[]{ ',', '"' };
        if(value.Any(reservedCharacters.Contains)) {
            var doubleQuotes = value.Replace("\"", "\"\"");
            return $"\"{doubleQuotes}\"";
        } else {
            return value;
        }
    }

}