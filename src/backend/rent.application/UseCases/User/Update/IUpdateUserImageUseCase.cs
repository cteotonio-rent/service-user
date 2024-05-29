namespace rent.application.UseCases.User.Update
{
    public interface IUpdateUserImageUseCase
    {
        Task Execute(MemoryStream memoryStream, string extension);
    }
}
