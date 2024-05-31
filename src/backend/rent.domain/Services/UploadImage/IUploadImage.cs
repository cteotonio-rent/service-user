namespace rent.domain.Services.UploadImage
{
    public interface IUploadImage
    {   
        Task Execute(MemoryStream memoryStream, string fileName, string contentType);
    }
}
