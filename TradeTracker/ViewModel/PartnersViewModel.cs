
using System.Collections.ObjectModel;
using System.Windows.Input;
using TradeTracker.DataModel;
using TradeTracker.Services;

namespace TradeTracker.ViewModel;

public class PartnersViewModel : BindableObject {

    private DataService Database;

    public PartnersViewModel(DataService database) {
        this.Database = database;
        this.Partners = new ObservableCollection<string>();
    }

    #region Properties

    private ObservableCollection<string> _partners;
    public ObservableCollection<string> Partners {
        get { return this._partners;}
        set {
            this._partners = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Command

    public ICommand NewSettingCommand => new Command(NewSetting);
    private async void NewSetting() {
        ;
        //await Shell.Current.GoToAsync("TradeEditor?id=new");
    }

    #endregion

    public async Task RefreshList() {
        var currencies = await this.Database.Operation(async db =>
            await db.Table<PartnerDataModel>()
            .OrderBy(o => o.Name)
            .ToListAsync());
        var names = currencies.Select(o => o.Name);
        this.Partners = new ObservableCollection<string>(names);
    }

}