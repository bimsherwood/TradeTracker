namespace TradeTracker.Behaviour;

public class CurrencyFormatBehaviour : Behavior<Entry>
{

    private const string DefaultText = "0.00";

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        entry.Focused += OnEntryFocused;
        CorrectEntry(entry, entry.Text ?? DefaultText);
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnEntryTextChanged;
        entry.Focused -= OnEntryFocused;
        base.OnDetachingFrom(entry);
    }

    private void OnEntryFocused(object sender, FocusEventArgs e){
        if(sender is Entry entry){
            CorrectEntry(entry, entry.Text ?? "");
        }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        if(sender is Entry entry){
            CorrectEntry(entry, args.NewTextValue);
        }
    }

    private void CorrectEntry(Entry entry, string candidateTextValue){

        var parts = candidateTextValue.Split(".");
        
        // Add a decimal part if the decimal point is missing
        if(parts.Length == 1){
            parts = new[]{ parts[0], "" };
        }

        // Ignore leading parts if there are multiple decimal points
        if(parts.Length > 2){
            parts = new[]{
                parts[parts.Length - 2],
                parts[parts.Length - 1]
            };
        }

        // Split the string and strip invalid characters
        var wholePart = new string(parts[0].Where(char.IsDigit).ToArray());
        var decimalPart = new string(parts[1].Where(char.IsDigit).ToArray());

        // If the whole part is empty, set it to 0
        if(wholePart.Length == 0){
            wholePart = "0";
        }

        // Shift the value to 2 decimal places
        var multiplier = Math.Pow(10, decimalPart.Length - 2);
        double.TryParse($"{wholePart}.{decimalPart}", out var currentValue);
        var newValue = currentValue * multiplier;
        var newValueFormatted = newValue.ToString("0.00");

        // Correct the entry format
        entry.Text = newValueFormatted;
        entry.CursorPosition = entry.Text.Length;
        return;

    }
    
}