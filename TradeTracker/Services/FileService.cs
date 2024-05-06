namespace TradeTracker.Services;

public class FileService {

    private readonly string PhotoFolder = Path.Combine(FileSystem.AppDataDirectory, "TradePhotos");

    public async Task<ImageSource> LoadImageForTrade(int tradeId){
        var photoFilePath = Path.Combine(PhotoFolder, $"photo{tradeId}.photo");
        if(File.Exists(photoFilePath)) {
            // Cannnot use .FromFile() due to caching!
            var imageStream = await File.ReadAllBytesAsync(photoFilePath);
            var imageBuffer = new MemoryStream(imageStream);
            return ImageSource.FromStream(() => imageBuffer);
        } else {
            return null;
        }
    }

    public async Task WriteTradePhoto(int tradeId, Stream content){
        if(!File.Exists(PhotoFolder)){
            Directory.CreateDirectory(PhotoFolder);
        }
        var photoFilePath = Path.Combine(PhotoFolder, $"photo{tradeId}.photo");
        using var file = File.OpenWrite(photoFilePath);
        await content.CopyToAsync(file);
    }

    public async Task DeleteAllTradePhotos(){
        if(Directory.Exists(PhotoFolder)){
            foreach(var photo in Directory.GetFiles(PhotoFolder)){
                File.Delete(photo);
            }
        }
    }

}