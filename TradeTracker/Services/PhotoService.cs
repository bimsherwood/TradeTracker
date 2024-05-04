namespace TradeTracker.Services;

public class PhotoService {

    public async Task<FileResult> PromptForPhoto() {
        string action = await App.Current.MainPage.DisplayActionSheet(
            "New or Existing Photo?",
            "Cancel",
            null,
            "Take Photo",
            "Pick Photo");
        if(action == "Take Photo"){
            return await MediaPicker.Default.CapturePhotoAsync();
        } else if(action == "Pick Photo"){
            return await MediaPicker.Default.PickPhotoAsync();
        } else {
            return null;
        }
    }

} 